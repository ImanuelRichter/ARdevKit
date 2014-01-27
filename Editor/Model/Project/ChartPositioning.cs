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
    public class ChartPositioning
    {
        public enum PositioningMode { STATIC, ABSOLUTE, RELATIVE };

        /// <summary>
        /// The position, describes
        /// in which way the chart is positioned.
        /// </summary>
        private PositioningMode mode;
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public PositioningMode Mode
        {
            get { return mode; }
            set { mode = value; }
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
        /// Initializes a new instance of the <see cref="ChartPositioning"/> class.
        /// </summary>
        public ChartPositioning(PositioningMode position)
        {
            this.mode = position;
        }
    }
}
