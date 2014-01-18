using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.Connections.DeviceConnection
{
    class DeviceConnectionController
    {
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
            throw new NotImplementedException();
        }

        public void connectToDevice(int index)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
