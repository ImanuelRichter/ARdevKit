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
        private delegate void AppendTextCallback(string text);

        public DebugWindow(Controller.Connections.DeviceConnection.DeviceConnectionController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.DebugConnected = false;
        }
        public void AppendText(string text)
        {
            if (this.rtb_out.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.rtb_out.AppendText(text);
            }
        }
    }
}
