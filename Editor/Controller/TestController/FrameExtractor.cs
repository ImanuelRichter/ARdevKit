using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ARdevKit.Model.Project.File;
using ARdevKit.View;

namespace ARdevKit.Controller.TestController
{
    public class FrameExtractor : BackgroundWorker
    {
        private ProcessVideoWindow processVideoWindow;

        /// <summary>
        /// A value indicating whether processing is [ready] or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ready]; otherwise, <c>false</c>.
        /// </value>
        private bool ready;

        /// <summary>
        /// Getter for <see cref="ready"/>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ready]; otherwise, <c>false</c>.
        /// </value>
        public bool Ready
        {
            get { return ready; }
        }

        /// <summary>
        /// The test file path.
        /// </summary>
        private string testFilePath;

        /// <summary>
        /// The temporary path.
        /// </summary>
        private string tmpPath;

        /// <summary>
        /// The FPS
        /// </summary>
        private int fps;

        /// <summary>
        /// Gets or sets the FPS.
        /// </summary>
        /// <value>
        /// The FPS.
        /// </value>
        public int FPS
        {
            get { return fps; }
        }

        public FrameExtractor(ProcessVideoWindow processVideoWindow, string testFilePath, string tmpPath)
        {
            this.processVideoWindow = processVideoWindow;

            this.WorkerReportsProgress = true;
            this.DoWork += new DoWorkEventHandler(frameExtractor_DoWork);
            this.ProgressChanged += new ProgressChangedEventHandler(frameExtractor_ProgressChanged);
            this.RunWorkerCompleted += new RunWorkerCompletedEventHandler(frameExtractor_RunWorkerCompleted);

            ready = false;
            if (File.Exists(testFilePath))
                this.testFilePath = testFilePath;
            else
            {
                MessageBox.Show("Das Video im angegebenen Pfad (" + testFilePath + ") existiert nicht (mehr).");
                processVideoWindow.ReportException(new FileNotFoundException());
            }

            this.tmpPath = tmpPath;

            VideoFileReader reader = new VideoFileReader();
            try
            {
                reader.Open(testFilePath);
            }
            // Same handling for every exception so general Exception is cought
            catch (Exception e)
            {
                MessageBox.Show("Das Video im angegebenen Pfad (" + testFilePath + ") konnte nicht geöffnet werden.\n" + e.Message);
                processVideoWindow.ReportException(e);
            }
            int height = reader.Height;
            int width = reader.Width;
            long frames = reader.FrameCount;
            reader.Close();

            long maxSizeByte = height * width * frames * 8;
            long propSizeByte = maxSizeByte / 8 * 3;
            decimal propSizeMegaByte = Math.Round((decimal)(propSizeByte / (1000 * 1000)), 2);

            DriveInfo drive = new FileInfo(tmpPath).GetDriveInfo();
            long freeDiskSpaceByte = drive.AvailableFreeSpace;
            decimal freeDiskSpaceMegaByte = Math.Round((decimal)(freeDiskSpaceByte / (1000 * 1000)), 2);

            if (propSizeByte > freeDiskSpaceByte)
            {
                MessageBox.Show("Die Verarbeitung des Videos benötigt " + propSizeMegaByte + " MB freien Speicher aber es stehen nur " + freeDiskSpaceMegaByte + " MB zur Verfügung.");
                processVideoWindow.ReportException(new OutOfMemoryException());
            }

            if (!Directory.Exists(tmpPath))
                Directory.CreateDirectory(tmpPath);
        }
        /// <summary>
        /// Handles the DoWork event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void frameExtractor_DoWork(object sender, DoWorkEventArgs e)
        {
            VideoFileReader reader = new VideoFileReader();
            try
            {
                reader.Open(testFilePath);
            }
            // Same handling for every exception so general Exception is cought
            catch (Exception ex)
            {
                MessageBox.Show("Das Video im angegebenen Pfad (" + testFilePath + ") konnte nicht geöffnet werden.\n" + ex.Message);
                processVideoWindow.ReportException(ex);
            }
            fps = reader.FrameRate;
            int n = (int)reader.FrameCount;

            for (int i = 1; i <= n; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                try
                {
                    videoFrame.Save(Path.Combine(tmpPath, i + ".png"), ImageFormat.Png);
                }
                // Same handling for every exception so general Exception is cought
                catch (Exception ex)
                {
                    MessageBox.Show("Frame " + i + " konnte nicht in " + tmpPath + " gespeichert werden.\n" + ex.Message);
                    reader.Close();
                    processVideoWindow.ReportException(ex);
                }
                videoFrame.Dispose();
                int progress = (int)Math.Round((decimal)(i * 100.0) / n);
                ReportProgress(progress);
            }
            reader.Close();
        }

        /// <summary>
        /// Handles the ProgressChanged event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void frameExtractor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processVideoWindow.ReportProgress(e.ProgressPercentage);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void frameExtractor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ready = true;
            processVideoWindow.Close();
        }
    }
}
