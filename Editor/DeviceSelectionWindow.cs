using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARdevKit.Controller.Connections.DeviceConnection;

namespace ARdevKit
{
    public partial class DeviceSelectionWindow : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The device connection controller.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private DeviceConnectionController deviceConnectionController;
        public DeviceConnectionController DeviceConnectionController
        {
            get { return deviceConnectionController; }
            set { deviceConnectionController = value; }
        }

        public DeviceSelectionWindow()
        {
            InitializeComponent();
            deviceConnectionController = new DeviceConnectionController(this);
        }

        private void DeviceSelectionWindow_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sendTo_Click(object sender, EventArgs e)
        {
            if (deviceList.Items.Count != 0)
            {
                if ( deviceList.FocusedItem != null && deviceList.FocusedItem.Index >= 0)
                {
                    try
                    {
                            //if (deviceConnectionController.checkAvailability(deviceList.FocusedItem.Index))
                            //{
                                if (deviceConnectionController.sendProject(deviceList.FocusedItem.Index))
                                {
                                    MessageBox.Show("Das Projekt wurde versand.");
                                }
                                else
                                {
                                    MessageBox.Show("Das Projekt wurde nicht versand.");
                                }    
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Es konnte keine Verbindung hergestellt werden");
                            //}
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Es wurde keine Verbindung hergestellt, stellen sie sicher, dass kein anderer Prozess diesen Port verwendet.");
                    }

                }
                else
                {
                    MessageBox.Show("Es ist kein Gerät ausgewählt, wählen sie es in der Liste aus");
                }
            }
            else
            {
                MessageBox.Show("Es ist kein Gerät verfügbar, nutzen sie die Aktualisierungsfunktion und stellen sie sicher, dass die Geräte mit dem netzwerk verbunden sind");
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            deviceList.Items.Clear();
            deviceConnectionController.refresh();
            List<string> devices = deviceConnectionController.getReportedDevices();
            foreach (string device in devices)
            {
                deviceList.Items.Add(new ListViewItem(device));                
            }
        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
