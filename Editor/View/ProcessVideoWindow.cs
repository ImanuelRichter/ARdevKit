using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit.View
{
    /// <summary>
    /// The ProcessVideoWindow processes a video and shows the progress.
    /// </summary>
    public partial class ProcessVideoWindow : Form
    {
        private delegate void CloseCallback();
        private delegate void UpdateExpectedSizeCallback(decimal size);

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessVideoWindow"/> class.
        /// </summary>
        public ProcessVideoWindow()
        {
            InitializeComponent();

            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 1;
        }

        /// <summary>
        /// Updates the expected size.
        /// </summary>
        /// <param name="size">The size.</param>
        public void UpdateExpectedSize(decimal size)
        {
            if (this.InvokeRequired)
            {
                UpdateExpectedSizeCallback d = new UpdateExpectedSizeCallback(UpdateExpectedSize);
                this.Invoke(d, new object[] {size});
            }
            else
                lbl_text.Text = "Video wird vorbereitet (" + size + " MB)...";
        }

        /// <summary>
        /// Reports the progress.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="frames">The frames.</param>
        /// <param name="calculatedFrames">The calculated frames.</param>
        /// <param name="remainingTime">The remaining time.</param>
        public void ReportProgress(int value, long frames, long calculatedFrames, TimeSpan remainingTime)
        {
            progressBar.Value = value;
            lbl_info.Text = calculatedFrames + "/" + frames + " (" + remainingTime.ToString("hh\\:mm\\:ss") + ")";
        }
    }
}
