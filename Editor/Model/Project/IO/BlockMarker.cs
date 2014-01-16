using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    public class BlockMarker
    {
        /// <summary>   true if closed. </summary>
        protected bool closed = true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the start. </summary>
        ///
        /// <value> The start. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Start { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the end. </summary>
        ///
        /// <value> The end. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string End { get; set; }

        protected BlockMarker() { }

        public BlockMarker(string start, string end)
        {
            Start = start;
            End = end;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns the start and end part alternating.
        ///             Beginning with the start part on first call.</summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <returns>   Eine Zeichenfolge, die das aktuelle Objekt darstellt. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override string ToString()
        {
            if (closed)
            {
                closed = false;
                return Start;
            }
            else
            {
                closed = true;
                return End;
            }
        }
    }
}
