using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit.View
{
    /// <summary>
    /// UserControl for an Trackbar/Slider. Currently not used (2.3.14)
    /// </summary>
    public partial class Slider : UserControl
    {
        /// <summary>
        /// Variable to check if the return value should be an int or double
        /// </summary>
        private bool isDouble;
        
        /// <summary>
        /// Variable of the slider value in double
        /// </summary>
        private double sliderValueDouble;
        /// <summary>
        /// Get and set the sliderValueDouble
        /// </summary>
        public double SliderValueDouble
        {
            get { return sliderValueDouble; }
            set { sliderValueDouble = value; }
        }

        /// <summary>
        /// Variable of the slider value in int
        /// </summary>
        private int sliderValueInt;
        /// <summary>
        /// Get and set the sliderValueInt
        /// </summary>
        public int SliderValueInt
        {
            get { return sliderValueInt; }
            set { sliderValueInt = value; }
        }

        /// <summary>
        /// Init the Slider Form
        /// </summary>
        private Slider()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init the SliderForm for an integer return value.
        /// </summary>
        /// <param name="initValue">Initial value</param>
        /// <param name="maxValue">Maximum value of the trackbar</param>
        public Slider(int initValue, int maxValue) : this()
        {
            trackBar1.Maximum = maxValue;
            trackBar1.Value = initValue;
            isDouble = false;

            label1.Text = "1";
            label2.Text = "500";
            label3.Text = "1000";
        }

        /// <summary>
        /// Init the SliderForm for a double return value.
        /// </summary>
        /// <param name="initValue">Initial value</param>
        public Slider(double initValue) : this()
        {
            trackBar1.Value = (int)(initValue * 10);
            isDouble = true;
        }

        /// <summary>
        /// Event of the trackBar. Checks the value of the trackbar and converts it appropriate return value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (isDouble)
                sliderValueDouble = (double)trackBar1.Value / 10.0;
            else
            {
                sliderValueInt = trackBar1.Value;
            }
        }
    }
}
