using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ARdevKit.Controller.Connections.DeviceConnection
{
    class TCPServer
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Verbindet einen Socket mithilfe der IP zu einem RemoteEndpoint. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ///
        /// <param name="ip">   The IP to connect. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void connect(IPAddress ip)
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Wird normal in einem eigenen Thread ausgeführt und blockiert, bis ein String durch den
        ///     verbundenen Remote Socket geschickt wird. dieser wird anschließend zurückgegeben.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ///
        /// <returns>   A String. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String receiveString()
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Trennt den Socket vom mit ihm verbundenen RemoteEndpoint des Players. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Verwendet Socket.sendFile(String) um eine Datei an den verbundenen RemoteEndpoint zu senden. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void sendFile()
        {
            throw new NotImplementedException();
        }
    }
}
