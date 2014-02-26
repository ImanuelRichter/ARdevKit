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
        private bool isDouble;
        private ARdevKit.Model.Project.Abstract2DAugmentation property;

        private double sliderValueDouble;
        public double SliderValueDouble
        {
            get { return sliderValueDouble; }
            set { sliderValueDouble = value; }
        }


        private int sliderValueInt;
        public int SliderValueInt
        {
            get { return sliderValueInt; }
            set { sliderValueInt = value; }
        }

        public Slider()
        {
            InitializeComponent();
        }

        public Slider(int initValue, int maxValue, ITypeDescriptorContext context) : this()
        {
            trackBar1.Maximum = maxValue;
            trackBar1.Value = initValue;
            isDouble = false;

            if (context.Instance is ARdevKit.Model.Project.Abstract2DAugmentation)
                property = (ARdevKit.Model.Project.Abstract2DAugmentation)context.Instance;

            label1.Text = "1";
            label2.Text = "500";
            label3.Text = "1000";
        }

        public Slider(double initValue) : this()
        {
            trackBar1.Value = (int)(initValue * 10);
            isDouble = true;
        }

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
