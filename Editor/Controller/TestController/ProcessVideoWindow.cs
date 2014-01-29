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
    public partial class ProcessVideoWindow : Form
    {
        private string testFilePath;
        private string tmpPath;
        public int FPS { get; set; }

        public ProcessVideoWindow()
        {
            InitializeComponent();
        }

        public void extractFrames(string testFilePath, string tmpPath)
        {
            this.testFilePath = testFilePath;
            this.tmpPath = tmpPath;

            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 0;

            extractor.RunWorkerAsync();
        }

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

        private void extractor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void extractor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
