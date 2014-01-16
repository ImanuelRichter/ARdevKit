using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A tag has a begin and an end part as well as a level what means the tabs. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Tag : BlockMarker
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text"> The text within the brackets (&lt;text&gt;&lt;/text&gt;). </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Tag(string text)
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

        public Tag(string text, string extension) : this(text)
        {
            Start = Start.Insert(Start.Length - 1, " " + extension);
        }
    }
}
