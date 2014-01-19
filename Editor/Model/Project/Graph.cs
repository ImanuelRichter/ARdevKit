using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class Graph : AbstractDynamic2DAugmention 
    {
        protected string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        protected string subtitle;

        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }

        protected string[] categories;

        public string[] Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        protected string yAxisTitle;

        public string YAxisTitle
        {
            get { return yAxisTitle; }
            set { yAxisTitle = value; }
        }

        private double pointPadding;

        public double PointPadding
        {
            get { return pointPadding; }
            set { pointPadding = value; }
        }

        protected int borderWidth;

        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        protected List<string> names;

        public List<string> Names
        {
            get { return names; }
            set { names = value; }
        }

        protected List<double[]> data;

        public List<double[]> Data
        {
            get { return data; }
            set { data = value; }
        }
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int MaxValue {get; set;}
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public int MinValue { get; set; }
        /// <summary>
        /// Gets or sets the scaling.
        /// </summary>
        /// <value>
        /// The scaling.
        /// </value>
        public int Scaling { get; set; }
    }
}
