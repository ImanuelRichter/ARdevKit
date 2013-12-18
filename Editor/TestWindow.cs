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
        public TestWindow()
        {
            InitializeComponent();
        }

        private void pnl_TestWindowView_Paint(object sender, PaintEventArgs e)
        {
            MyMetaioWrapper wrapper = new MyMetaioWrapper();
            unsafe
            {
                void* panel = pnl_TestWindowView.Handle.ToPointer();
                wrapper.initializeSDK(pnl_TestWindowView.Width, pnl_TestWindowView.Height, panel);
            }
            while (true)
            {
                wrapper.update();
            }
        }
    }
}
