using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Used to set the position of a chart used by HighChart. </summary>
    ///
    /// <remarks>   Imanuel, 20.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class ChartStyle
    {
        /// <summary>
        /// The position, describes
        /// in which way the chart is positioned.
        /// </summary>
        private string position;
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// The top padding
        /// </summary>
        private int top;
        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        /// <summary>
        /// The left padding
        /// </summary>
        private int left;
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        /// <summary>
        /// The bottom padding
        /// </summary>
        private int bottom;
        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        /// <value>
        /// The bottom.
        /// </value>
        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        /// <summary>
        /// The right padding
        /// </summary>
        private int right;
        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartStyle"/> class.
        /// </summary>
        public ChartStyle()
        {
            position = "absolute";
        }
    }
}
