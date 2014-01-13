using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ARdevKit
{
    static class TestController
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// New process for the player.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private static Process player;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the projectPath and a testFilePath which is the result of an opened
        /// FileDialog.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartTestMode()
        {
            string playerPath = "..\\..\\..\\ARdevKitPlayer\\bin\\Debug\\Player.exe";
            string projectPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "currentProject");
            OpenFileDialog openTestFileDialog = new OpenFileDialog();
            openTestFileDialog.ShowDialog();
            string testFilePath = openTestFileDialog.FileName;
            /*
            Process manyCam = new Process();
            manyCam.StartInfo.FileName = "VirtualCamera.lnk";
            manyCam.Start();
             * */
            player = new Process();
            player.StartInfo.FileName = playerPath;
            player.StartInfo.Arguments = projectPath + " -" + testFilePath;
            player.Start();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the projectPath and an extern virtualCamera which is used to load
        /// test content.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartTestMode(string virtualCamera)
        {
            string playerPath = "..\\..\\..\\ARdevKitPlayer\\bin\\Debug\\Player.exe";
            string projectPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "currentProject");

            Process manyCam = new Process();
            manyCam.StartInfo.FileName = "VirtualCamera.lnk";
            manyCam.Start();

            player = new Process();
            player.StartInfo.FileName = playerPath;
            player.StartInfo.Arguments = projectPath;
            player.Start();
        }
    }
}
