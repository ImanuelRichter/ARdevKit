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
    ///     Controller which provides functions, to gather Information about Devices, which are running ARdevKitPlayer and are connected
    ///     to the local Network. On top of that it provides functions to send Projects and receive Debuginformation.
    /// </summary>
    public class DeviceConnectionController
    {
        private List<IPEndPoint> reportedDevices;
        private UdpClient udpClient;
        private EditorWindow editorWindow;


        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceConnectionController"/> class. Uses UDPListener to 
        /// get information about devices. Communicating via HTTP order to secure currency of connections and sending the zipped project.
        /// </summary>
        /// <param name="ew">The ew.</param>
        public DeviceConnectionController(Form window)
        {
            editorWindow = (EditorWindow) window;
            reportedDevices = new List<IPEndPoint>();
            udpClient = new UdpClient(12345);
            refresh();
        } 

        private void sendBroadcast()
        {
            IPEndPoint broadcastAddress = new IPEndPoint(IPAddress.Broadcast, 12345);
            byte[] broadcastmsg = UTF8Encoding.UTF8.GetBytes("respond if you want to be listed by ARDevKit");
            udpClient.Send(broadcastmsg, broadcastmsg.Length, broadcastAddress);
        }

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
        /// index of the <see cref="reportedDevices"/> List. Exceptions which are thrown are written to a log file, in the path in which the Program is executed.
        /// </summary>
        /// <param name="index">index of the List of <see cref="reportedDevices"/></param>
        /// <returns>False, if the project could not be send or an Exception was thown</returns>
        public bool sendProject(int index)
        {
            bool successfullySent = false;
            TcpClient sender = null;
            FileStream project = null;
            NetworkStream sendStream = null;
            try
            {
                exportRecentProject();
                sender = new TcpClient(reportedDevices[index].Address.ToString(), 12345);
                if (File.Exists("currentProject.zip"))
                {
                    File.Delete("currentProject.zip");
                }
                ZipFile.CreateFromDirectory("currentProject", "currentProject.zip");
                project = File.OpenRead("currentProject.zip");
                byte[] size = new byte[8];
                size = BitConverter.GetBytes(project.Length);
                if (!BitConverter.IsLittleEndian)
                {
                    size.Reverse();
                }

                sendStream = sender.GetStream();

                sendStream.Write(ASCIIEncoding.ASCII.GetBytes("project\n"), 0,  ASCIIEncoding.ASCII.GetByteCount("project\n"));
                
                sendStream.Write(size, 0, 8);

                byte[] buffer = new byte[64000];
                int current = 0, all = 0;
                sender.SendBufferSize = (int)project.Length;
                while (all < project.Length)
                {
                    current = project.Read(buffer, 0, buffer.Length);
                    sendStream.Write(buffer, 0, current);
                    all += current;
                }
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
                if(new FileInfo("connectionDebugLog.txt").Length > 400000)
                {
                    File.Delete("connectionDebugLog.txt");
                }
                FileStream log = File.Open("connectionDebugLog.txt", FileMode.Append | FileMode.OpenOrCreate, FileAccess.Write);
                byte [] logLine = UTF8Encoding.UTF8.GetBytes(DateTime.Now.ToString() + " - Exception from: " + ex.Source + " Message: " + ex.Message + " StackTrace: " + ex.StackTrace + "\n\r");
                log.Write(logLine, 0, logLine.Length);
                log.Close();
            }
            finally
            {
                sendStream.Close();
                sender.Close();
                project.Close();
            }
            return successfullySent;
        }
        private void exportRecentProject()
        {
            string originalProjectPath = editorWindow.project.ProjectPath;
            if (editorWindow.project.ProjectPath == null || editorWindow.project.ProjectPath.Length <= 0)
                editorWindow.project.ProjectPath = "tmp\\project";
            ARdevKit.Controller.ProjectController.ExportVisitor exporter = new ARdevKit.Controller.ProjectController.ExportVisitor();
            editorWindow.project.Accept(exporter);

            ARdevKit.Model.Project.IDFactory.Reset();
            foreach (ARdevKit.Model.Project.File.AbstractFile file in exporter.Files)
            {
                file.Save();
            }
            editorWindow.project.ProjectPath = originalProjectPath;
        }


        public bool sendDebug(int index)
        {
            bool successfullySent = false;
            TcpListener receiver = null;
            FileStream project = null;
            NetworkStream sendStream = null;
            try
            {
                receiver = new TcpListener(IPAddress.Any, 12346);
                receiver.Start(10);
                               
            }
            catch(Exception ex)
            {

            }
            return false;
        }
    }
}
