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
    public partial class Slider : UserControl
    {
        private double sliderValue;

        public double SliderValue
        {
            get { return sliderValue; }
            set { sliderValue = value; }
        }

        public Slider()
        {
            InitializeComponent();
        }

        public Slider(double initValue) : this()
        {
            trackBar1.Value = (int)(initValue * 10);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            sliderValue = (double)trackBar1.Value / 10.0;
        }
    }
}
