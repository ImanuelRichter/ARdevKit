using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit
{
    public partial class EditorWindow : Form
    {
        public EditorWindow()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        private void tsm_editor_menu_file_new_Click(object sender, EventArgs e)
        {

        }

        private void tsm_editor_menu_file_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void tsm_editor_menu_test_loadImage_Click(object sender, EventArgs e)
        {
            TestWindow testWindow = new TestWindow();
            testWindow.Show();
        }
    }
}
