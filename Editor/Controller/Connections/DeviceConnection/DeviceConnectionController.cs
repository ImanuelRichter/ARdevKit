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
    class DeviceConnectionController
    {
        private List<IPEndPoint> reportedDevices;
        private UdpClient udpClient;
        private TcpClient tcpClient;


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
            udpClient = new UdpClient(12345);
            tcpClient = new TcpClient();
            refresh();
            List<string> devices = getReportedDevices();
            foreach (string device in devices)
            {
                ((DeviceSelectionWindow)window).DeviceList.Items.Add(new ListViewItem(device));
            }
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
        /// Runs the refresh listener and therefore locks the reportedDevicesList.
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

        public bool connectTo(int index)
        {
            tcpClient.Connect(reportedDevices[index]);
            tcpClient.GetStream().Write(UTF8Encoding.UTF8.GetBytes("test"),0 , UTF8Encoding.UTF8.GetByteCount("test"));
            byte[] response = new byte[1024];
            tcpClient.GetStream().Read(response, 0, response.Length);
            return UTF8Encoding.UTF8.GetString(response).Equals("OK");
        }

        /// <summary>
        /// Sends the project to the Device, which is chosen through connectToDevice() method, but could be easily changed to send to 
        /// any chosen Device in the List.
        /// </summary>
        /// <exception cref="System.Exception">
        /// </exception>
        public bool sendProject()
        {
            if(!tcpClient.Connected)
            {
                throw new Exception("you have to connect to a device first");
            }
            ZipFile.CreateFromDirectory("currentProject", "currentProject.zip");
            byte[] file = File.ReadAllBytes("currentProject.zip");
            byte[] response = new byte[1024];
            tcpClient.GetStream().Write(file, 0, file.Length);
            tcpClient.GetStream().Read(response, 0, response.Length);
            return UTF8Encoding.UTF8.GetString(response).Equals("OK");
        }

//public void StartListen()
//{
//    SendFind();
//    try {
//        IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 0);
//        UdpClient listenClient = new UdpClient(5001);

//        UdpState s = new UdpState();
//        s.endpoint = localEp;
//        s.client = listenClient;
//        //allow time for the find to work - aka clutching at straws
//        Thread.Sleep(500);

//        while (listenClient.Available > 0)
//        {
//            listenClient.BeginReceive(ReceiveCallback, s);
//            Thread.Sleep(500);
//        }
//    }

//    catch (SocketException e)
//    {
//        Trace.WriteLine("Could not bind to socket on " + _localPort);
//    }
//    finally
//    {
//        listenClient.Close();
//    }    
//}

//private void ReceiveCallback(IAsyncResult ar)
//{
//    UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).client;
//    IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).endpoint;
//    Byte[] receiveBytes = u.EndReceive(ar, ref e);
    
//}

//        /// <summary>
//        /// Connects to device. sets the device as connected. Is momentary depreciated, because HTTP connections do not require
//        /// active connection making. However if Problems occure during File transfer or for Debug functionality this method might be reintruduced.
//        /// </summary>
//        /// <param name="index">The index.</param>
//        /// <exception cref="System.Exception"></exception>
//        public void connectToDevice(int index)
//        {
//            lock (reportedDevices)
//            {
//                HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + reportedDevices[index].Address + ":" + reportedDevices[index].Port + "/");
//                refreshRequest.Method = "GET";
//                if (!refreshRequest.HaveResponse)
//                {
//                    throw new Exception();
//                }
//                connectedDevice = reportedDevices[index];
//            }
//        }


//        /// <summary>
//        /// Gets an actualized list containing the IPs and Ports of the possible Devices, to which the Project might be send.
//        /// </summary>
//        /// <returns>a list containing the IPs and Ports of the possible Devices</returns>
//        public List<String> getPossibleClients()
//        {
//            runRefreshListener();
//            List<string> result = new List<string>();
//            lock (reportedDevices)
//            {
//                foreach (IPEndPoint item in reportedDevices)
//                {
//                    result.Add(item.ToString());
//                }
//                return result;
//            }
//        }


//        /// <summary>
//        /// Sends the project to the Device, which is chosen through connectToDevice() method, but could be easily changed to send to 
//        /// any chosen Device in the List.
//        /// </summary>
//        /// <exception cref="System.Exception">
//        /// </exception>
//        public void sendProject()
//        {
//            HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/");
//            refreshRequest.Method = "GET";
//            if (!refreshRequest.HaveResponse)
//            {
//                throw new Exception();
//            }
//            //HttpWebRequest SendRequest = (HttpWebRequest)WebRequest.Create("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/");
//            //SendRequest.Method = "PUT";
//            //Stream sendStream = SendRequest.GetRequestStream();
//            ZipFile.CreateFromDirectory("currentProject","currentProject.zip");
//            WebClient client = new WebClient();
//            byte[] file = File.ReadAllBytes("currentProject.zip");
//            byte[] response = client.UploadData("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/", "PUT", file);
//            if(!Encoding.Default.GetString(response).Contains("OK"))
//            {
//                throw new Exception();
//            }
//        }
    }
}
