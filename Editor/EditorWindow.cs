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
    public partial class EditorWindow : Form
    {
        //[DllImport("MetaioWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        //private static extern void initializeSDK();

        public EditorWindow()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        private void tsm_editor_menu_file_new_Click(object sender, EventArgs e)
        {
            MyMetaioWrapper wrapper = new MyMetaioWrapper();
            unsafe
            {
                void* panel = pnl_editor_preview.Handle.ToPointer();
                wrapper.initializeSDK(pnl_editor_preview.Width, pnl_editor_preview.Height, panel);
            }
            while (true) {
                wrapper.update();
                //pnl_editor_preview.Refresh();
            }
            //MessageBox.Show(wrapper.getVersion());
        }

        private void tsm_editor_menu_file_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
