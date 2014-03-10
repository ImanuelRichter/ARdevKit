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
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Values that represent positioning modes. </summary>
        ///
        /// <remarks>   Imanuel, 27.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public enum PositioningModes { STATIC, ABSOLUTE, RELATIVE };

        /// <summary>   The positioning mode. </summary>
        private PositioningModes positioningMode;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the positioning mode. </summary>
        ///
        /// <value> The positioning mode. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public PositioningModes PositioningMode
        {
            get { return positioningMode; }
            set { positioningMode = value; }
        }

        /// <summary>
        /// The top padding
        /// </summary>
        private int top;
        /// <summary>
        /// Gets or sets the top padding.
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
        /// Gets or sets the left padding.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 27.01.2014. </remarks>
        ///
        /// <param name="positioningMode"> The position. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ChartPositioning(PositioningModes positioningMode)
        {
            this.positioningMode = positioningMode;
        }
    }
}
