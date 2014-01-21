using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class BarChartData : ChartData
    {
        /// <summary>
        /// Gets or sets the color of the maximum value,
        /// which can be displayed.
        /// </summary>
        /// <value>
        /// The color of the maximum value.
        /// </value>
        [CategoryAttribute("Color")]
        public Color MaxValueColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the minimal value,
        /// which can be displayed.
        /// </summary>
        /// <value>
        /// The color of the minimum value.
        /// </value>
        [CategoryAttribute("Color")]
        public Color MinValueColor { get; set; }

        public BarChartData(string name)
        {
            this.name = name;
        }

        public BarChartData(string name, double[] dataSet) : this (name)
        {
            this.dataSet = dataSet;
            this.MinValueColor = Color.LawnGreen;
            this.MaxValueColor = Color.PaleVioletRed;
        }

        public BarChartData(string name, double[] dataSet, Color minValueColor, Color maxValueColor)
            : this(name, dataSet)
        {
            this.MinValueColor = minValueColor;
            this.MaxValueColor = maxValueColor;
        }
    }
}
