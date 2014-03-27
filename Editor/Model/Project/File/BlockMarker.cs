using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="BlockMarker"/> marks an <see cref="AbstractBlock"/>.
    ///             It has a <see cref="Start"/> string and an <see cref="End"/> string
    ///             and can be open or closed. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class BlockMarker
    {
        // No state pattern is used because there are just those two states and just one different action
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public BlockMarker() { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="start">    The start. </param>
        /// <param name="end">      The end. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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
