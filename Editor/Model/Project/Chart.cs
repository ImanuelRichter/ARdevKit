using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;
using System.IO;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Describes a <see cref="Chart"/> with its <see cref="ChartStyle"/>
    /// titel, subtitle, xAxisTitle. It is a <see cref="AbstractDynamic2DAugmentation"/>
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class Chart : AbstractDynamic2DAugmentation 
    {
        /// <summary>
        /// The style used by HighChart.
        /// </summary>
        protected ChartStyle style;
        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [CategoryAttribute("General"), ReadOnly(true)]
        //[Browsable(false)]
        public ChartStyle Style
        {
            get { return style; }
            set { style = value; }
        }

        protected string options;

        [CategoryAttribute("Expert"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Options
        {
            get { return options; }
            set { options = value; }
        }

        /// <summary>
        /// The title used by HighChart.
        /// </summary>
        protected string title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [CategoryAttribute("General")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// The subtitle used by HighChart.
        /// </summary>
        protected string subtitle;
        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        /// <value>
        /// The subtitle.
        /// </value>
        [CategoryAttribute("General")]
        public string Subtitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }

        /// <summary>
        /// The axis title used by HighChart.
        /// </summary>
        protected string xAxisTitle;

        /// <summary>
        /// Gets or sets the axis title used by HighChart.
        /// </summary>
        /// <value>
        /// The x coordinate axis title.
        /// </value>
        public string XAxisTitle
        {
            get { return xAxisTitle; }
            set { xAxisTitle = value; }
        }

        /// <summary>
        /// The categories used by HighChart.
        /// </summary>
        protected string[] categories;
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        [CategoryAttribute("General")]
        public string[] Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        /// <summary>
        /// The Y axis title used by HighChart.
        /// </summary>
        protected string yAxisTitle;
        /// <summary>
        /// Gets or sets the Y axis title.
        /// </summary>
        /// <value>
        /// The y coordinate axis title.
        /// </value>
        [CategoryAttribute("General")]
        public string YAxisTitle
        {
            get { return yAxisTitle; }
            set { yAxisTitle = value; }
        }

        /// <summary>
        /// The point padding used by HighChart.
        /// </summary>
        private double pointPadding;
        /// <summary>
        /// Gets or sets the point padding.
        /// </summary>
        /// <value>
        /// The point padding.
        /// </value>
        [CategoryAttribute("Size")]
        public double PointPadding
        {
            get { return pointPadding; }
            set { pointPadding = value; }
        }

        /// <summary>
        /// Width of the border used by HighChart.
        /// </summary>
        protected int borderWidth;
        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>
        /// The width of the border.
        /// </value>
        [CategoryAttribute("Size")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        /// <summary>
        /// Gets or sets the maximum value, which
        /// can be displayed.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        [CategoryAttribute("General")]
        public int MaxValue {get; set;}

        /// <summary>
        /// Gets or sets the minimum value, which
        /// can be displayed.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        [CategoryAttribute("General")]
        public int MinValue { get; set; }

        public bool UseOptions { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Specialised default constructor for use only by derived classes. </summary>
        ///
        /// <remarks>   Imanuel, 20.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected Chart() : base()
        {
            UseOptions = true;
            title = "Titel";
            subtitle = "Untertitel";
            xAxisTitle = "Kategorien";
            yAxisTitle = "Skala";
            categories = new string[] {"Kategorie 1", "Kategorie 2"};
            pointPadding = 0.2;
            borderWidth = 0;
            Height = 200;
            Width = 200;
        }
    }
}
