using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetaioWrapper;

namespace ARdevKit
{
    public partial class TestWindow : Form
    {
        private System.Windows.Forms.Timer updateSDKTimer;
        private MyMetaioWrapper wrapper;
        private Panel metaioPanel;
        private Label version;
        private unsafe static void* panelPointer;
        private int fps = 60;

        public TestWindow()
        {
            InitializeComponent();

            metaioPanel = pnl_TestWindowMetaioRenderer;
            version = lbl_TestWindowVersion;

            updateSDKTimer = new System.Windows.Forms.Timer();
            updateSDKTimer.Tick += new EventHandler(performUpdate);
            updateSDKTimer.Interval = 1000 / fps;

            unsafe
            {
                panelPointer = metaioPanel.Handle.ToPointer();
                wrapper = new MyMetaioWrapper(metaioPanel.Width, metaioPanel.Height, panelPointer);
            }
            version.Text = wrapper.getVersion();

            String trackingConfigurationPath = "..\\res\\trackingconfigurations\\TrackingData_MarkerlessFast.xml";
            if (!wrapper.setTrackingConfiguration(trackingConfigurationPath))
            {
                MessageBox.Show("Failed to load tracking configuration", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            updateSDKTimer.Start();
        }

        private void performUpdate(object o, EventArgs e)
        {
            wrapper.update();
        }
    }
}
