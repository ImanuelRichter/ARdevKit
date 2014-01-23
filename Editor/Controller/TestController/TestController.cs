using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Controller.TestController
{
    static class TestController
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used in <see cref="StartPlayer(string, int)"/> to load an image
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private const int IMAGE = 0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used in <see cref="StartPlayer(string, int)"/> load a video
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private const int VIDEO = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used in <see cref="StartPlayer(string, int)"/> start a virtual camera
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private const int CAMERA = 2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// New process for the player.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private static Process player;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The path to the player. Gonna be the startupPath at the end.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private static string playerPath = "Player.exe";

        /// <summary>
        /// Starts the player with the specified settings.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        /// <param name="mode">
        /// Tells if an image<see cref="IMAGE"/> or video<see cref="VIDEO"/>
        /// should be loaded or if a virtual camera<see cref="CAMERA"/> should be started
        /// </param>
        private static void StartPlayer(Project project, int mode)
        {
            ExportVisitor exporter = new ExportVisitor(true);
            project.Accept(exporter);

            IDFactory.Reset();
            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }

            player = new Process();
            player.StartInfo.Arguments = project.ProjectPath + " -" + mode;

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
                        open = true;
                    }
                    break;
                case (VIDEO):
                    OpenFileDialog openTestVideoDialog = new OpenFileDialog();
                    openTestVideoDialog.Title = "Bitte ein Video auswählen, an dem getestet werden soll";
                    if (openTestVideoDialog.ShowDialog() == DialogResult.OK)
                    {
                        string testFilePath = openTestVideoDialog.FileName;
                        player.StartInfo.Arguments += " -" + testFilePath;
                        open = true;
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
                        open = true;
                    }
                    break;
            }
            if (File.Exists(playerPath) && open)
            {
                player.StartInfo.FileName = playerPath;
                player.Start();
            }
            else if (open)
            {
                OpenFileDialog openPlayerDialog = new OpenFileDialog();
                openPlayerDialog.Title = "Bitte Player auswählen";
                openPlayerDialog.Filter = "Programm (*.exe)|*.exe";
                if (openPlayerDialog.ShowDialog() == DialogResult.OK)
                {
                    player.StartInfo.FileName = openPlayerDialog.FileName;
                    player.Start();
                }
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the passed projectPath and a testFilePath which is the result of an
        /// opened FileDialog.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithImage(Project project)
        {
            StartPlayer(project, IMAGE);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the passed projectPath and a testFilePath which is the result of an
        /// opened FileDialog.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithVideo(Project project)
        {
            StartPlayer(project, VIDEO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the passed projectPath and an extern virtualCamera which
        /// is used to load test content.
        /// FileDialog.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithVirtualCamera(Project project)
        {
            StartPlayer(project, CAMERA);
        }
    }
}
