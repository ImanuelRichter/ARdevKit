using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="XMLTag"/> is a <see cref="BlockMarker"/>. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class XMLTag : BlockMarker
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text"> The text within the brackets (&lt;text&gt;&lt;/text&gt;). </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public XMLTag(string text)
        {
            Start = text.Insert(0, "<");
            Start = Start.Insert(Start.Length, ">");
            End = Start.Insert(1, "/");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text">         The text. </param>
        /// <param name="extension">    The extension. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public XMLTag(string text, string extension) : this(text)
        {
            Start = Start.Insert(Start.Length - 1, " " + extension);
        }
    }
}
