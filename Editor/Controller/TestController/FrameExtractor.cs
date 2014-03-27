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
    /// <summary>
    /// A <see cref="FrameExtractor"/> is a <see cref="BackgroundWorker"/> that extracts
    /// the frames of a given video.
    /// </summary>
    public class FrameExtractor : BackgroundWorker
    {
        /// <summary>
        /// The process video window that shows the progress of this <see cref="FrameExtractor"/>.
        /// </summary>
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
        /// A value indicating whether processing is [finished] or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [finished]; otherwise, <c>false</c>.
        /// </value>
        private bool finished;

        /// <summary>
        /// Getter for <see cref="finished"/>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [finished]; otherwise, <c>false</c>.
        /// </value>
        public bool Finished
        {
            get { return finished; }
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

        /// <summary>
        /// The total frames.
        /// </summary>
        private long totalFrames;

        /// <summary>
        /// The calculated frames.
        /// </summary>
        private long calculatedFrames;

        /// <summary>
        /// The remaining time.
        /// </summary>
        private TimeSpan remainingTime;

        /// <summary>
        /// The current size in byte.
        /// </summary>
        long currentSizeByte;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameExtractor"/> class.
        /// </summary>
        /// <param name="processVideoWindow">The process video window.</param>
        /// <param name="testFilePath">The test file path.</param>
        /// <param name="tmpPath">The temporary path.</param>
        public FrameExtractor(ProcessVideoWindow processVideoWindow, string testFilePath, string tmpPath)
        {
            this.ready = true;
            this.finished = false;
            this.processVideoWindow = processVideoWindow;

            this.WorkerReportsProgress = true;
            this.DoWork += new DoWorkEventHandler(frameExtractor_DoWork);
            this.ProgressChanged += new ProgressChangedEventHandler(frameExtractor_ProgressChanged);
            this.RunWorkerCompleted += new RunWorkerCompletedEventHandler(frameExtractor_RunWorkerCompleted);

            finished = false;
            if (File.Exists(testFilePath))
                this.testFilePath = testFilePath;
            else
            {
                MessageBox.Show("Das Video im angegebenen Pfad (" + testFilePath + ") existiert nicht (mehr).");
                ready = false;
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
                ready = false;
            }
            int height = reader.Height;
            int width = reader.Width;
            totalFrames = reader.FrameCount;
            reader.Close();

            long maxSizeByte = height * width * totalFrames * 8;
            decimal maxSizeMegaByte = Math.Round((decimal)(maxSizeByte / (1000 * 1000)), 2);

            DriveInfo drive = new FileInfo(tmpPath).GetDriveInfo();
            long freeDiskSpaceByte = drive.AvailableFreeSpace;
            decimal freeDiskSpaceMegaByte = Math.Round((decimal)(freeDiskSpaceByte / (1000 * 1000)), 2);

            //maxSizeByte a lot bigger than the expected size
            if (maxSizeByte > freeDiskSpaceByte)
            {
                MessageBox.Show("Die Verarbeitung des Videos könnte bis zu " + maxSizeMegaByte + " MB freien Speicher verbrauchen, es stehen aber nur " + freeDiskSpaceMegaByte + " MB zur Verfügung.");
                ready = false;
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
            DateTime startTime = DateTime.Now;
            VideoFileReader reader = new VideoFileReader();
            try
            {
                reader.Open(testFilePath);
            }
            // Same handling for every exception so general Exception is cought
            catch (Exception ex)
            {
                MessageBox.Show("Das Video im angegebenen Pfad (" + testFilePath + ") konnte nicht geöffnet werden.\n" + ex.Message);
                return;
            }
            fps = reader.FrameRate;

            for (calculatedFrames = 0; calculatedFrames < totalFrames; calculatedFrames++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                try
                {
                    string fileName = Path.Combine(tmpPath, (calculatedFrames + 1) + ".png");
                    videoFrame.Save(fileName, ImageFormat.Png);
                    FileInfo fi = new FileInfo(fileName);
                    currentSizeByte += fi.Length;
                }
                // Same handling for every exception so general Exception is cought
                catch (Exception ex)
                {
                    MessageBox.Show("Frame " + (calculatedFrames + 1) + " konnte nicht in " + tmpPath + " gespeichert werden.\n" + ex.Message);
                    reader.Close();
                    return;
                }
                videoFrame.Dispose();
                int progress = (int)Math.Round((decimal)((calculatedFrames + 1) * 100.0) / totalFrames);
                TimeSpan passedTime = DateTime.Now - startTime;
                TimeSpan frameCalculationTime = TimeSpan.FromMilliseconds(passedTime.TotalMilliseconds / (calculatedFrames + 1));
                remainingTime = TimeSpan.FromMilliseconds(frameCalculationTime.TotalMilliseconds * (totalFrames - (calculatedFrames + 1)));

                if (calculatedFrames > 0)
                {
                    long expectedSizeByte = currentSizeByte / calculatedFrames * totalFrames;
                    decimal expectedSizeMegaByte = Math.Round((decimal)(expectedSizeByte / (1000 * 1000)), 2);
                    processVideoWindow.UpdateExpectedSize(expectedSizeMegaByte);
                }
                else
                {
                    long maxSizeByte = reader.Height * reader.Width * totalFrames * 8;
                    decimal maxSizeMegaByte = Math.Round((decimal)(maxSizeByte / (1000 * 1000)), 2);
                    processVideoWindow.UpdateExpectedSize(maxSizeMegaByte);
                }

                ReportProgress(progress);

                /* Try to calculate when the video processing is buffered so that the player can start before extraction is finished
                int calculatedFPS = (int)(1000 / frameCalculationTime.TotalMilliseconds);
                if (calculatedFrames >= 5 && ((1 - (calculatedFPS / fps)) * n) >= calculatedFrames)
                {
                    ready = true;
                    processVideoWindow.BufferingIsReady();
                }
                */
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
            processVideoWindow.ReportProgress(e.ProgressPercentage, totalFrames, calculatedFrames, remainingTime);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the extractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void frameExtractor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            finished = true;
            processVideoWindow.Close();
        }
    }
}
