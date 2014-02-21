using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using AForge;
using AForge.Video;
using AForge.Video.FFMPEG;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project;
using ARdevKit.Model.Project.File;
using System.Drawing;
using System.Drawing.Imaging;

namespace ARdevKit.Controller.TestController
{
    static class TestController
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used in <see cref="StartPlayer(string, int)"/> to load an image
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public const int IMAGE = 0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used in <see cref="StartPlayer(string, int)"/> load a video
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public const int VIDEO = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used in <see cref="StartPlayer(string, int)"/> start a virtual camera
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public const int CAMERA = 2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// New process for the player.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Process player;

        private static ProcessVideoWindow progressVideoWindow;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The path to the player. Gonna be the startupPath at the end.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private static string playerPath;

        public static void StartPlayer(Project p, int mode, bool showDebug)
        {
            StartPlayer(p, mode, 1024, 768, showDebug);
        }

        /// <summary>
        /// Starts the player with the specified settings.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        /// <param name="mode">
        /// Tells if an image<see cref="IMAGE"/> or video<see cref="VIDEO"/>
        /// should be loaded or if a virtual camera<see cref="CAMERA"/> should be started
        /// </param>
        public static void StartPlayer(Project project, int mode, int width, int height, bool showDebug)
        {
            ExportVisitor exporter = new ExportVisitor(true);
            project.Accept(exporter);

            IDFactory.Reset();
            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }

            player = new Process();
            playerPath = showDebug ? "DebugPlayer.exe" : "Player.exe";
            player.StartInfo.Arguments = "-" + width + " -" + height + " -" + (project.ProjectPath == null ? "currentProject" : project.ProjectPath) + " -" + mode;

            bool open = false;
            switch (mode)
            {
                case (IMAGE):
                    OpenFileDialog openTestImageDialog = new OpenFileDialog();
                    openTestImageDialog.Title = "Bitte ein Bild auswählen, an dem getestet werden soll";
                    openTestImageDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|PPM Files (*.ppm)|*.ppm|PGM Files (*.pgm)|*.pgm";
                    if (openTestImageDialog.ShowDialog() == DialogResult.OK)
                    {
                        string testFilePath = openTestImageDialog.FileName;
                        player.StartInfo.Arguments += " -" + testFilePath;
                        OpenPlayer();
                    }
                    break;
                case (VIDEO):
                    OpenFileDialog openTestVideoDialog = new OpenFileDialog();
                    openTestVideoDialog.Title = "Bitte ein Video auswählen, an dem getestet werden soll";
                    if (openTestVideoDialog.ShowDialog() == DialogResult.OK)
                    {
                        string testFilePath = openTestVideoDialog.FileName;
                        string tmpPath = Path.Combine("tmp", "video");

                        if (Directory.Exists(tmpPath))
                        {
                            foreach (string path in Directory.GetFiles(tmpPath))
                                File.Delete(path);
                        }
                        else
                            Directory.CreateDirectory(tmpPath);

                        player.StartInfo.Arguments += " -" + tmpPath;

                        progressVideoWindow = new ProcessVideoWindow();
                        progressVideoWindow.FormClosed += progressVideoWindow_FormClosed;
                        progressVideoWindow.Show();
                        progressVideoWindow.extractFrames(testFilePath, tmpPath);
                    }
                    break;
                case (CAMERA):
                    OpenFileDialog openVirualCameraPathDialog = new OpenFileDialog();
                    openVirualCameraPathDialog.Title = "Bitte virtuelle Kamera auswählen";
                    if (openVirualCameraPathDialog.ShowDialog() == DialogResult.OK)
                    {
                        string virtualCameraPath = openVirualCameraPathDialog.FileName;

                        Process vCam = new Process();
                        vCam.StartInfo.FileName = virtualCameraPath;
                        vCam.Start();
                        OpenPlayer();
                    }
                    break;
            }
        }

        private static void OpenPlayer()
        {
            if (File.Exists(playerPath) )
            {
                player.StartInfo.FileName = playerPath;
                player.Start();
            }
            else
            {
                OpenFileDialog openPlayerDialog = new OpenFileDialog();
                openPlayerDialog.Title = "Bitte Player auswählen";
                openPlayerDialog.Filter = "Programm (" + playerPath + ")|" + playerPath;
                if (openPlayerDialog.ShowDialog() == DialogResult.OK)
                {
                    player.StartInfo.FileName = openPlayerDialog.FileName;
                    player.Start();
                }
            }
        }

        static void progressVideoWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.StartInfo.Arguments += " -" + progressVideoWindow.FPS;
            OpenPlayer();
        }
    }
}
