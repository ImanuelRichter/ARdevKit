using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A tag has a begin and an end part as well as a level what means the tabs. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Tag
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Level { get; set; }

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
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text"> The text. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Tag(string text)
        {
            Start = text.Insert(0, "<");
            Start = Start.Insert(Start.Length, ">");
            End = Start.Insert(1, "/");
            Level = 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text">         The text. </param>
        /// <param name="extension">    The extension. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Tag(string text, string extension) : this(text)
        {
            Start = Start.Insert(Start.Length - 1, " " + extension);
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
