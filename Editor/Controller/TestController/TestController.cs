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
        /// <summary>
        /// The location where the temporary project is exported to.
        /// </summary>
        private const string TMP_PROJECT_PATH = "tmp\\project";

        /// <summary>
        /// The location where the temporary frames where extracted to.
        /// </summary>
        private const string TMP_VIDEO_PATH = "tmp\\video";

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

        private static EditorWindow editorWindow;

        /// <summary>
        /// A window showing the progress of processing the video
        /// </summary>
        private static ProcessVideoWindow progressVideoWindow;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The path to the player. Gonna be the startupPath at the end.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private static string playerPath;

        /// <summary>
        /// Starts the player with the specified settings.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        /// <param name="mode">
        /// Tells if an image<see cref="IMAGE"/> or video<see cref="VIDEO"/>
        /// should be loaded or if a virtual camera<see cref="CAMERA"/> should be started
        /// </param>
        public static void StartPlayer(EditorWindow ew, Project project, int mode, int width, int height, bool showDebug)
        {
            editorWindow = ew;
            string originalProjectPath = project.ProjectPath;
            if (project.ProjectPath == null || project.ProjectPath.Length <= 0)
                project.ProjectPath = TMP_PROJECT_PATH;
            ExportVisitor exporter = new ExportVisitor();
            project.Accept(exporter);

            IDFactory.Reset();
            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }

            player = new Process();
            player.EnableRaisingEvents = true;
            player.Exited += player_Exited;
            playerPath = showDebug ? "DebugPlayer.exe" : "Player.exe";
            player.StartInfo.Arguments = "-" + width + " -" + height + " -" + project.ProjectPath + " -" + mode;

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

                        if (Directory.Exists(TMP_VIDEO_PATH))
                        {
                            foreach (string path in Directory.GetFiles(TMP_VIDEO_PATH))
                                File.Delete(path);
                        }
                        else
                            Directory.CreateDirectory(TMP_VIDEO_PATH);

                        player.StartInfo.Arguments += " -" + TMP_VIDEO_PATH;

                        progressVideoWindow = new ProcessVideoWindow();
                        progressVideoWindow.FormClosed += progressVideoWindow_FormClosed;
                        progressVideoWindow.Show();
                        progressVideoWindow.extractFrames(testFilePath, TMP_VIDEO_PATH);
                    }
                    break;
                case (CAMERA):
                    OpenFileDialog openVirualCameraPathDialog = new OpenFileDialog();
                    openVirualCameraPathDialog.Title = "Bitte virtuelle Kamera auswählen";
                    openVirualCameraPathDialog.Filter = "Executable (*.exe)|*.exe";
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
            project.ProjectPath = originalProjectPath;
        }

        private static void player_Exited(object sender, EventArgs e)
        {
            if (Directory.Exists(TMP_VIDEO_PATH))
                Directory.Delete(TMP_VIDEO_PATH, true);
            editorWindow.PlayerClosed();
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
                openPlayerDialog.Filter = "Player (" + playerPath + ")|" + playerPath;
                if (openPlayerDialog.ShowDialog() == DialogResult.OK)
                {
                    player.StartInfo.FileName = openPlayerDialog.FileName;
                    player.Start();
                }
            }
            editorWindow.PlayerStarted();
        }

        static void progressVideoWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.StartInfo.Arguments += " -" + progressVideoWindow.FPS;
            OpenPlayer();
        }
    }
}
