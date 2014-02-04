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

namespace ARdevKit.Controller.Connections.DeviceConnection
{
    class DeviceConnectionController
    {
        private List<IPEndPoint> reportedDevices;
        private IPEndPoint connectedDevice;
        private bool isListening;
        private ListView deviceList;


        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceConnectionController"/> class. Uses UDPListener to 
        /// get information about devices. Communicating via HTTP order to secure currency of connections and sending the zipped project.
        /// </summary>
        /// <param name="ew">The ew.</param>
        public DeviceConnectionController(Form window)
        {
            //System.Net.NetworkInformation.IPGlobalProperties network = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            //System.Net.NetworkInformation.TcpConnectionInformation[] connections = network.GetActiveTcpConnections();
            reportedDevices = new List<IPEndPoint>();
            isListening = true;
            Task listenToDevices = Task.Factory.StartNew(runRegisterListener, System.Threading.Tasks.TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Runs the refresh listener and therefore locks the reportedDevicesList.
        /// </summary>
        private void runRefreshListener()
        {
            lock (reportedDevices)
            {
                foreach (IPEndPoint item in reportedDevices)
                {
                    HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + item.Address + ":" + item.Port + "/");
                    refreshRequest.Method = "GET";
                    if (!refreshRequest.HaveResponse)
                    {
                        reportedDevices.Remove(item);
                    }
                }
            }           
        }

        /// <summary>
        /// Runs the register listener, should be executed in seperate thread, or task.
        /// </summary>
        private void runRegisterListener()
        {
            UdpClient udpListener = new UdpClient();
            IPEndPoint any = new IPEndPoint(IPAddress.Any, 15000);
            while(isListening)
            {
                byte[] result = udpListener.Receive(ref any);
                string resultText = Encoding.UTF8.GetString(result);
                string[] resultTextArray = resultText.Split(':');
                IPEndPoint candidate = new IPEndPoint(IPAddress.Parse(resultTextArray[0]), Int16.Parse(resultTextArray[1]));
                lock (reportedDevices)
                {
                    if (!reportedDevices.Contains(candidate))
                    {
                        reportedDevices.Add(candidate);
                        HttpWebRequest stopRegisterRequest = (HttpWebRequest)WebRequest.Create("http://" + candidate.Address + ":" + candidate.Port + "/");
                        stopRegisterRequest.Method = "PUSH";
                    }
                }               
            }
        }

        /// <summary>
        /// Connects to device. sets the device as connected. Is momentary depreciated, because HTTP connections do not require
        /// active connection making. However if Problems occure during File transfer or for Debug functionality this method might be reintruduced.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="System.Exception"></exception>
        public void connectToDevice(int index)
        {
            lock (reportedDevices)
            {
                HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + reportedDevices[index].Address + ":" + reportedDevices[index].Port + "/");
                refreshRequest.Method = "GET";
                if (!refreshRequest.HaveResponse)
                {
                    throw new Exception();
                }
                connectedDevice = reportedDevices[index];
            }
        }


        /// <summary>
        /// Gets an actualized list containing the IPs and Ports of the possible Devices, to which the Project might be send.
        /// </summary>
        /// <returns>a list containing the IPs and Ports of the possible Devices</returns>
        public List<String> getPossibleClients()
        {
            runRefreshListener();
            List<string> result = new List<string>();
            lock (reportedDevices)
            {
                foreach (IPEndPoint item in reportedDevices)
                {
                    result.Add(item.ToString());
                }
                return result;
            }
        }


        /// <summary>
        /// Sends the project to the Device, which is chosen through connectToDevice() method, but could be easily changed to send to 
        /// any chosen Device in the List.
        /// </summary>
        /// <exception cref="System.Exception">
        /// </exception>
        public void sendProject()
        {
            HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/");
            refreshRequest.Method = "GET";
            if (!refreshRequest.HaveResponse)
            {
                throw new Exception();
            }
            //HttpWebRequest SendRequest = (HttpWebRequest)WebRequest.Create("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/");
            //SendRequest.Method = "PUT";
            //Stream sendStream = SendRequest.GetRequestStream();
            ZipFile.CreateFromDirectory("currentProject","currentProject.zip");
            WebClient client = new WebClient();
            byte[] file = File.ReadAllBytes("currentProject.zip");
            byte[] response = client.UploadData("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/", "PUT", file);
            if(!Encoding.Default.GetString(response).Contains("OK"))
            {
                throw new Exception();
            }
        }
    }
}
