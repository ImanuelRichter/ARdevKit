using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video.FFMPEG;
using System.IO;
using System.Drawing.Imaging;

namespace ARdevKit.Controller.TestController
{
    /// <summary>
    /// The ProcessVideoWindow processes a video and shows the progress.
    /// </summary>
    public partial class ProcessVideoWindow : Form
    {
        /// <summary>
        /// The test file path.
        /// </summary>
        private string testFilePath;

        /// <summary>
        /// The temporary path.
        /// </summary>
        private string tmpPath;
        /// <summary>
        /// Gets or sets the FPS.
        /// </summary>
        /// <value>
        /// The FPS.
        /// </value>
        public int FPS { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessVideoWindow"/> class.
        /// </summary>
        public ProcessVideoWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Extracts the frames.
        /// </summary>
        /// <param name="testFilePath">The test file path.</param>
        /// <param name="tmpPath">The temporary path.</param>
        public void extractFrames(string testFilePath, string tmpPath)
        {
            this.testFilePath = testFilePath;
            this.tmpPath = tmpPath;
            if (!Directory.Exists(tmpPath))
                Directory.CreateDirectory(tmpPath);

            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 1;

            extractor.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void extractor_DoWork(object sender, DoWorkEventArgs e)
        {
            VideoFileReader reader = new VideoFileReader();
            reader.Open(testFilePath);
            FPS = reader.FrameRate;
            int n = (int)reader.FrameCount;

            reader.Open(testFilePath);
            for (int i = 1; i <= n; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                videoFrame.Save(Path.Combine(tmpPath, i + ".png"), ImageFormat.Png);
                videoFrame.Dispose();
                int progress = (int)Math.Round((decimal)(i * 100.0) / n);
                extractor.ReportProgress(progress);
            }
            reader.Close();
        }

        /// <summary>
        /// Handles the ProgressChanged event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void extractor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void extractor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
