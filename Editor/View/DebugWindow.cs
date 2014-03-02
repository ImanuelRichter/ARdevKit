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
    /// <summary>
    /// This is a Form, containing a TextBoc, which can be filled threadsafe.
    /// Its for displaying DebugInformation.
    /// </summary>
    public partial class DebugWindow : Form
    {
        private Controller.Connections.DeviceConnection.DeviceConnectionController controller;
        private delegate void AppendTextCallback(string text);

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugWindow"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public DebugWindow(Controller.Connections.DeviceConnection.DeviceConnectionController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.DebugConnected = false;
        }

        /// <summary>
        /// Appends the text.
        /// </summary>
        /// <param name="text">The text.</param>
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
