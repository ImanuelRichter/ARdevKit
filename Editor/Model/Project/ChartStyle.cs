using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Used to set the position of a chart. </summary>
    ///
    /// <remarks>   Imanuel, 20.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Serializable]
    public class ChartStyle
    {
        /// <summary>   The positioning mode. </summary>
        private string position;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the positioning mode. </summary>
        ///
        /// <value> The position. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>   The distance to the top in pixel. </summary>
        private int top = -1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the distance to the top in pixel. </summary>
        ///
        /// <value> The top. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        private int left = -1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the distance to the left in pixel. </summary>
        ///
        /// <value> The left. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        /// <summary>   The distance to the bottom in pixel. </summary>
        private int bottom = -1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the distance to the bottom in pixel. </summary>
        ///
        /// <value> The bottom. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        /// <summary>   The distance to the right in pixel. </summary>
        private int right = -1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the right in pixel. </summary>
        ///
        /// <value> The right. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Imanuel, 20.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ChartStyle()
        {
            position = "static";
        }
    }
}
