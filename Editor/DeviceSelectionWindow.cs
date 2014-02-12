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
            deviceConnectionController = new DeviceConnectionController(this);
            InitializeComponent();
        }

        private void DeviceSelectionWindow_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void connectTo_Click(object sender, EventArgs e)
        {
            if (deviceList.Items.Count != 0)
            {
                if (deviceList.FocusedItem.Index > 0)
                {
                    deviceConnectionController.connectTo(0/*deviceList.FocusedItem.Index*/);
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
    }
}
