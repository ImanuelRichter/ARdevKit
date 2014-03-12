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
        /// Reports the progress.
        /// </summary>
        /// <param name="value">The value.</param>
        public void ReportProgress(int value)
        {
            progressBar.Value = value;
        }

        public void ReportException(Exception e)
        {
            Close();
        }
    }
}
