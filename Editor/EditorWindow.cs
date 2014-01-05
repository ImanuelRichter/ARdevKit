using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ARdevKit
{
    public partial class EditorWindow : Form
    {
        private Process player = new Process();
        private string playerPath = "D:\\Dropbox\\dev\\ARdevKit - Player\\bin\\Debug\\Player.exe";
        private string projectPath = "D:\\Dropbox\\dev\\ARdevKit - Player\\res";

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
            //TestWindow testWindow = new TestWindow();
            //testWindow.Show();

            player.StartInfo.FileName = playerPath;
            player.StartInfo.Arguments = projectPath;
            player.Start();
        }

        private void tsm_editor_menu_test_loadVideo_Click(object sender, EventArgs e)
        {
            TestWindow testWindow = new TestWindow();
            testWindow.Show();
        }
    }
}
