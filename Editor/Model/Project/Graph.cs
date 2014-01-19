using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// ToDo summary is missing
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class Graph : AbstractDynamic2DAugmention 
    {
        /// <summary>   The style. </summary>
        protected ChartStyle style;
        /// <summary>   Gets or sets the style. </summary>
        ///
        /// <value> The style. </value>
        [CategoryAttribute("General"), ReadOnly(true)]
        public ChartStyle Style
        {
            get { return style; }
            set { style = value; }
        }

        /// <summary>   The title. </summary>
        protected string title;
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        [CategoryAttribute("General")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>   The subtitle. </summary>
        protected string subtitle;
        /// <summary>   Gets or sets the subtitle. </summary>
        ///
        /// <value> The subtitle. </value>
        [CategoryAttribute("General"), ReadOnly(true)]
        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }

        /// <summary>   The axis title. </summary>
        protected string xAxisTitle;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the axis title. </summary>
        ///
        /// <value> The x coordinate axis title. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string XAxisTitle
        {
            get { return xAxisTitle; }
            set { xAxisTitle = value; }
        }

        protected string[] categories;
        /// <summary>   Gets or sets the categories. </summary>
        ///
        /// <value> The categories. </value>
        [CategoryAttribute("General")]
        public string[] Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        /// <summary>   The Y axis title. </summary>
        protected string yAxisTitle;
        /// <summary>   Gets or sets the Y axis title. </summary>
        ///
        /// <value> The y coordinate axis title. </value>
        [CategoryAttribute("General")]
        public string YAxisTitle
        {
            get { return yAxisTitle; }
            set { yAxisTitle = value; }
        }

        /// <summary>   The point padding. </summary>
        private double pointPadding;
        /// <summary>   Gets or sets the point padding. </summary>
        ///
        /// <value> The point padding. </value>
        [CategoryAttribute("Size")]
        public double PointPadding
        {
            get { return pointPadding; }
            set { pointPadding = value; }
        }

        /// <summary>   Width of the border. </summary>
        protected int borderWidth;
        /// <summary>   Gets or sets the width of the border. </summary>
        ///
        /// <value> The width of the border. </value>
        [CategoryAttribute("Size")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        /// <summary>   ToDo </summary>
        protected List<string> names;
        /// <summary>   Gets or sets the names. </summary>
        ///
        /// <value> The names. </value>
        [CategoryAttribute("General")]
        public List<string> Names
        {
            get { return names; }
            set { names = value; }
        }

        /// <summary>   The data. </summary>
        protected List<double[]> data;
        /// <summary>   Gets or sets the data. </summary>
        ///
        /// <value> The data. </value>
        [CategoryAttribute("General")]
        public List<double[]> Data
        {
            get { return data; }
            set { data = value; }
        }
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        [CategoryAttribute("General")]
        public int MaxValue {get; set;}
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        [CategoryAttribute("General")]
        public int MinValue { get; set; }
        /// <summary>
        /// Gets or sets the scaling.
        /// </summary>
        [CategoryAttribute("Size")]
        public int Scaling { get; set; }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        private string graphPath;
        /// <summary>
        /// ToDo summary is missing
        /// </summary>
        [CategoryAttribute("General")]
        public string GraphPath
        {
            get { return graphPath; }
            set { graphPath = value; }
        }

        /// <summary>
        /// ToDo summary is missing
        /// </summary>
        protected Graph() : base()
        {
            ; // missing initialization
        }
    }
}
