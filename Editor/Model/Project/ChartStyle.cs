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
        private string position;

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        private int top = -1;

        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        private int left = -1;

        public int Left
        {
            get { return left; }
            set { left = value; }
        }
        private int bottom = -1;

        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }
        private int right = -1;

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        public ChartStyle()
        {
            position = "static";
            top = 100;
            left = 100;
        }
    }
}
