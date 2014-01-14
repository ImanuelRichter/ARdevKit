using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.Connections.DeviceConnection
{
    class UDPServer
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Ist bei der der Initialisierung leer und wird erst durch listenAndFillList() befüllt.
        ///     Hier sind die IPAdressen der im Netzwerk reagierenden Geräte gelistet.
        /// </summary>
        ///
        /// <value> The clientlist. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<String> Clientlist { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public UDPServer()
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Sendet eine Nachricht an alle im Netzwerk befindlichen Geräte auf einem festgelegten
        ///     Port. diese Nachricht beinhaltet unter anderem die IPAdresse des Gerätes, auf dem der
        ///     Editor läuft.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void broadcast()
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Wird normal einem eigenen Thread ausgeführt und nimmt Antworten an, die auf einem
        ///     bestimmten Port eingehen, falls sie einem bestimmten Format genügen werden sie in die
        ///     ClientList eingetragen. Nach einer gewissen Zeit wird die ClientList zum zurückgeben
        ///     freigegeben.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void listenAndFillList()
        {
            throw new NotImplementedException();
        }

    }
}
