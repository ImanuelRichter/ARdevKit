using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ARdevKit.Controller.Connections.DeviceConnection
{
    /// <summary>
    /// Controller which provides functions, to gather Information about Devices, which are running ARdevKitPlayer and are connected
    /// to the local Network. On top of that it provides functions to send Projects and receive Debuginformation.
    /// </summary>
    public class DeviceConnectionController
    {
        private List<IPEndPoint> reportedDevices;
        private UdpClient udpClient;
        private EditorWindow editorWindow;
        private bool debugConnected;

        /// <summary>
        /// Gets or sets a value indicating whether [debug connected].
        /// </summary>
        /// <value>
        /// true if [debug connected] the Editor listens for
        /// Debugdata; otherwise, the connections will be closed false.
        /// </value>
        public bool DebugConnected
        {
            get { return debugConnected; }
            set { debugConnected = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceConnectionController" /> class. Uses UDPListener to
        /// get information about devices. Communicating via HTTP order to secure currency of connections and sending the zipped project.
        /// </summary>
        /// <param name="window">The window.</param>
        public DeviceConnectionController(Form window)
        {
            debugConnected = false;
            editorWindow = (EditorWindow) window;
            reportedDevices = new List<IPEndPoint>();
            udpClient = new UdpClient();
            refresh();
        } 

        /// <summary>
        /// Sends the broadcast.
        /// </summary>
        private void sendBroadcast()
        {
            IPEndPoint broadcastAddress = new IPEndPoint(IPAddress.Broadcast, 12345);
            byte[] broadcastmsg = UTF8Encoding.UTF8.GetBytes("respond if you want to be listed by ARDevKit");
            udpClient.Send(broadcastmsg, broadcastmsg.Length, broadcastAddress);
        }

        /// <summary>
        /// Receives all queued responses.
        /// </summary>
        private void receiveAllQueuedResponses()
        {
            IPEndPoint anyAddress = new IPEndPoint(IPAddress.Any, 12345);
            while(udpClient.Available > 0)
            {
                byte[] recData = udpClient.Receive(ref anyAddress);
                if(!Dns.GetHostEntry(Dns.GetHostName()).AddressList.Contains(anyAddress.Address))
                {
                    lock (reportedDevices)
                    {
                        if(!reportedDevices.Contains(anyAddress))
                        {
                            reportedDevices.Add(anyAddress);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Runs the refresh listener, using a UDP Broadcast to which the ARdevKitPlayer responds.
        /// </summary>
        public void refresh()
        {
            reportedDevices = new List<IPEndPoint>();
            sendBroadcast();
            Thread.Sleep(500);
            receiveAllQueuedResponses();
        }


        /// <summary>
        /// Gets the StringList of devices, which reported back to the UDPBroadcast.
        /// </summary>
        /// <returns>StringList of devices, to which connections could be established</returns>
        public List<string> getReportedDevices()
        {
            List<string> result = new List<string>();
            foreach(var reportedDevice in reportedDevices)
            {
                result.Add(reportedDevice.ToString());
            }
            return result;
        }

        /// <summary>
        /// Sends the opened Project to the chosen Device, using the selected index of the EditorWindowsDeviceList, which must be equal to the internal
        /// index of the <see cref="reportedDevices"/> List. Therefore it is exported. Exceptions which are thrown are written to a log file, in the path in which the Program is executed.
        /// </summary>
        /// <param name="index">index of the List of <see cref="reportedDevices"/></param>
        /// <returns>False, if the project could not be send</returns>
        public bool sendProject(int index)
        {
            //initialize needed tools
            bool successfullySent = false;
            TcpClient sender = null;
            FileStream project = null;
            NetworkStream sendStream = null;

            //trys to export the project and zip it
            try
            {
                exportRecentProject();
                sender = new TcpClient(reportedDevices[index].Address.ToString(), 12345);
                if (File.Exists("tmp\\currentProject.zip"))
                {
                    File.Delete("tmp\\currentProject.zip");
                }
                if (!System.IO.Directory.Exists("tmp"))
                {
                    System.IO.Directory.CreateDirectory("tmp");
                }
                if (editorWindow.project.ProjectPath == null || editorWindow.project.ProjectPath.Length <= 0)
                {
                ZipFile.CreateFromDirectory("tmp\\project", "tmp\\currentProject.zip");
                }
                else
                {
                    ZipFile.CreateFromDirectory(editorWindow.project.ProjectPath, "tmp\\currentProject.zip");
                }

                //gets number of bytes to send
                project = File.OpenRead("tmp\\currentProject.zip");
                byte[] size = new byte[8];
                size = BitConverter.GetBytes(project.Length);
                if (!BitConverter.IsLittleEndian)
                {
                    size.Reverse();
                }

                //sends command "project"
                sendStream = sender.GetStream();
                sendStream.Write(ASCIIEncoding.ASCII.GetBytes("project\n"), 0,  ASCIIEncoding.ASCII.GetByteCount("project\n"));
                
                //sends projectSize
                sendStream.Write(size, 0, 8);

                //opens zipFile in stream and writes it to the Network Stream in brackets
                byte[] buffer = new byte[64000];
                int current = 0, all = 0;
                sender.SendBufferSize = (int)project.Length;
                while (all < project.Length)
                {
                    current = project.Read(buffer, 0, buffer.Length);
                    sendStream.Write(buffer, 0, current);
                    all += current;
                }
                //waits for positive response
                sendStream.ReadTimeout = 20000;
                byte[] response = new byte[2];
                if (sendStream.Read(response, 0, response.Length) == 2)
                {
                    if (ASCIIEncoding.ASCII.GetString(response).Contains("OK"))
                    {
                        successfullySent = true;
                    }
                }
            }
            catch (Exception ex)
            {
                writeExceptionToLog(ex);
                throw (ex);
            }
            finally
            {
                if(!(sendStream == null))
                {
                    sendStream.Close();
                }
                if (!(sender == null))
                {
                    sender.Close();
                }
                if (!(project == null))
                {
                    project.Close();
                }
            }
            return successfullySent;
        }


        /// <summary>
        /// Exports the recent project, in order to zip it and send it.
        /// </summary>
        private void exportRecentProject()
        {
            string originalProjectPath = editorWindow.project.ProjectPath;
            if (editorWindow.project.ProjectPath == null || editorWindow.project.ProjectPath.Length <= 0)
            {
            editorWindow.project.ProjectPath = "tmp\\project";
            }
            ARdevKit.Controller.ProjectController.ExportVisitor exporter = new ARdevKit.Controller.ProjectController.ExportVisitor();
            editorWindow.project.Accept(exporter);

            ARdevKit.Model.Project.IDFactory.Reset();
            foreach (ARdevKit.Model.Project.File.AbstractFile file in exporter.Files)
            {
                file.Save();
            }
            editorWindow.project.ProjectPath = originalProjectPath;
        }


        /// <summary>
        /// Sends a Debugrequest to the selected Device and shows its DebugOutput on a PopupWindow with a RichTextbox
        /// </summary>
        /// <param name="index">index of the chosen Device</param>
        /// <returns> true if</returns>
        public bool sendDebug(int index)
        {
            //initialize needed tools
            bool success = false;
            TcpClient sender = null;
            NetworkStream sendStream = null;
            StreamReader reader = null;
            
            //trys to send command "debug" and connects tools
            try
            {
                sender = new TcpClient(reportedDevices[index].Address.ToString(), 12345);
                debugConnected = true;
                sendStream = sender.GetStream();
                sendStream.Write(ASCIIEncoding.ASCII.GetBytes("debug\n"), 0, ASCIIEncoding.ASCII.GetByteCount("debug\n"));
                reader = new StreamReader(sendStream);
                sendStream.ReadTimeout  = 1000;
                byte[] msg = new byte[1024];
                
                //listens to incomming Debuginfo, as long as Debugwindow is open
                while (debugConnected)
                {
                    if (sender.Available > 0)
                    {
                        int read = sendStream.Read(msg, 0, msg.Length);
                        editorWindow.DebugWindow.AppendText(ASCIIEncoding.ASCII.GetString(msg, 0, read) + "\n");
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    } 
                }

                //sends OK on success
                success = true;
                sendStream.Write(ASCIIEncoding.ASCII.GetBytes("OK"), 0, ASCIIEncoding.ASCII.GetByteCount("OK"));
            }
            catch (SocketException ex)
            {
                writeExceptionToLog(ex);
                if (!(sendStream == null) && !(sender == null) && sender.Connected)
                {
                    sendStream.Write(ASCIIEncoding.ASCII.GetBytes("FAIL"), 0, ASCIIEncoding.ASCII.GetByteCount("FAIL"));
                }
                MessageBox.Show("Es gab ein Problem mit den Sockets. Überprüfen sie ihre Netzwerkeinstellungen.");
            }
            catch(IOException io)
            {
                writeExceptionToLog(io);
                if (!(sendStream == null) && !(sender == null) && sender.Connected)
                {
                    sendStream.Write(ASCIIEncoding.ASCII.GetBytes("FAIL"), 0, ASCIIEncoding.ASCII.GetByteCount("FAIL"));
                }
                MessageBox.Show("Es gab ein Problem mit dem Remotehost. Überprüfen sie ihre Verbindung.");
            }
            finally
            {
                if (!(reader == null))
                {
                    reader.Close();
                }
                if (!(sendStream == null))
                {
                    sendStream.Close();
                }
                if (!(sender == null))
                {
                    sender.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// Writes the exception to log.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void writeExceptionToLog(Exception ex)
        {
            FileStream log = File.Open("connectionDebugLog.txt", FileMode.Append | FileMode.OpenOrCreate, FileAccess.Write);
            byte[] logLine = UTF8Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " - Exception from: " + ex.Source + " Message: " + ex.Message + " StackTrace: " + ex.StackTrace + "\n\r");
            log.Write(logLine, 0, logLine.Length);
            log.Close();
            if (new FileInfo("connectionDebugLog.txt").Length > 400000)
            {
                File.Delete("connectionDebugLog.txt");
            }
        }
    }
}
