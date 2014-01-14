using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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

        private static string playerPath = "..\\..\\..\\ARdevKitPlayer\\Debug\\Player.exe";

        /// <summary>
        /// Starts the player with the specified settings.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        /// <param name="mode">
        /// Tells if an image<see cref="IMAGE"/> or video<see cref="VIDEO"/>
        /// should be loaded or if a virtual camera<see cref="CAMERA"/> should be started
        /// </param>
        private static void StartPlayer(string projectPath, int mode)
        {
            player = new Process();
            player.StartInfo.FileName = playerPath;
            player.StartInfo.Arguments = projectPath + " -" + mode;
            
            switch (mode)
            {
                case (IMAGE):
                    OpenFileDialog openTestImageDialog = new OpenFileDialog();
                    openTestImageDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|PPM Files (*.ppm)|*.ppm|PGM Files (*.pgm)|*.pgm";
                    if (openTestImageDialog.ShowDialog() == DialogResult.OK)
                    {
                        string testFilePath = openTestImageDialog.FileName;
                        player.StartInfo.Arguments += " -" + testFilePath;
                        player.Start();
                    }
                    break;
                case (VIDEO):
                    OpenFileDialog openTestVideoDialog = new OpenFileDialog();
                    if (openTestVideoDialog.ShowDialog() == DialogResult.OK)
                    {
                        string testFilePath = openTestVideoDialog.FileName;
                        player.StartInfo.Arguments += " -" + testFilePath;
                        player.Start();
                    }
                    break;
                case (CAMERA):
                    OpenFileDialog openVirualCameraPathDialog = new OpenFileDialog();
                    if (openVirualCameraPathDialog.ShowDialog() == DialogResult.OK)
                    {
                        string virtualCameraPath = openVirualCameraPathDialog.FileName;

                        Process vCam = new Process();
                        vCam.StartInfo.FileName = virtualCameraPath;
                        vCam.Start();

                        player.Start();
                    }
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the projectPath (startupPath\currentProject) and a testFilePath
        /// which is the result of an opened FileDialog.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithImage()
        {
            string projectPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "currentProject");
            StartPlayer(projectPath, IMAGE);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the passed projectPath and a testFilePath which is the result of an
        /// opened FileDialog.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithImage(string projectPath)
        {
            StartPlayer(projectPath, IMAGE);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the projectPath (startupPath\currentProject) and a testFilePath
        /// which is the result of an opened FileDialog.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithVideo()
        {
            string projectPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "currentProject");
            StartPlayer(projectPath, VIDEO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the passed projectPath and a testFilePath which is the result of an
        /// opened FileDialog.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithVideo(string projectPath)
        {
            StartPlayer(projectPath, VIDEO);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the projectPath (startupPath\currentProject) and an extern virtualCamera which
        /// is used to load test content.
        /// FileDialog.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithVirtualCamera()
        {
            string projectPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "currentProject");
            StartPlayer(projectPath, CAMERA);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts the player passing the passed projectPath and an extern virtualCamera which
        /// is used to load test content.
        /// FileDialog.
        /// </summary>
        /// <param name="projectPath">The location of the project</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void StartWithVirtualCamera(string projectPath)
        {
            StartPlayer(projectPath, CAMERA);
        }
    }
}
