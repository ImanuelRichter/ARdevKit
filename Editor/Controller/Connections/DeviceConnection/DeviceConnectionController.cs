using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.Connections.DeviceConnection
{
    class DeviceConnectionController
    {
        private HttpListener registerListener;
        private ARdevKit.Model.Project.Project project;
        private List<IPEndPoint> reportedDevices;
        private IPEndPoint connectedDevice;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Stellt eine Verbindung mit dem im View ausgewählten Listenelement aus der Liste der
        ///     möglichen Player her, das über den Listenindex spezifiziert wird. Hierzu wird die +
        ///     connect(IP : IPAdress) des TCPServers verwendet.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ///
        /// <param name="index">    Zero-based index of the. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DeviceConnectionController(EditorWindow ew)
        {
            project = ew.project;
            registerListener = new HttpListener();
            registerListener.Prefixes.Add("http://localhost:15400/register/");
            runRegisterListener();
        }

        private void runRefreshListener()
        {
            foreach (IPEndPoint item in reportedDevices)
            {
                HttpWebRequest refreshRequest = (HttpWebRequest) WebRequest.Create("http://" + item.Address + ":" + item.Port + "/refresh/");
                if(!refreshRequest.HaveResponse)
                {
                    reportedDevices.Remove(item);
                }
            }
        }

        private void runRegisterListener()
        {
            while(true)
            {
                HttpListenerContext registerContext = registerListener.GetContext();
                if (!reportedDevices.Contains(registerContext.Request.RemoteEndPoint))
                {
                    reportedDevices.Add(registerContext.Request.RemoteEndPoint);
                }
            }
        }

        public void connectToDevice(int index)
        {
            HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + reportedDevices[index].Address + ":" + reportedDevices[index].Port + "/refresh/");
            if (!refreshRequest.HaveResponse)
            {
                throw new Exception();
            }
            connectedDevice = reportedDevices[index];
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gibt eine Liste der möglichen Clients zurück, diese ist identisch mit der von der
        ///     UDPServer Klasse aufbereitet für den View und die Darstellung in einem ListPanel.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ///
        /// <returns>   The possible clients. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<String> getPossibleClients()
        {
            runRefreshListener();
            List<string> result = new List<string>();
            foreach (IPEndPoint item in reportedDevices)
            {
                result.Add(item.ToString());
            }
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Verwendet die sendFile() Methode des TCPServers um das aktuelle Projekt zu senden. Hierzu
        ///     wird mithilfe dem Editor::Controller::ProjectController::ExportVisitor ein ARELfile
        ///     erstellt und mithilfe der sendFile() Methode des TCPServer verschickt.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void sendProject()
        {
            HttpWebRequest refreshRequest = (HttpWebRequest)WebRequest.Create("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/refresh/");
            if (!refreshRequest.HaveResponse)
            {
                throw new Exception();
            }
            HttpWebRequest SendRequest = (HttpWebRequest)WebRequest.Create("http://" + connectedDevice.Address + ":" + connectedDevice.Port + "/sendProject/")
            Stream sendStream = SendRequest.GetRequestStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binFor = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binFor.Serialize(sendStream, project);
        }
    }
}
