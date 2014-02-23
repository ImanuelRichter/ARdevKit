using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit.View
{
    public partial class DebugWindow : Form
    {
        private Controller.Connections.DeviceConnection.DeviceConnectionController controller;

        public DebugWindow(Controller.Connections.DeviceConnection.DeviceConnectionController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.DebugConnected = false;
        }
    }
}
