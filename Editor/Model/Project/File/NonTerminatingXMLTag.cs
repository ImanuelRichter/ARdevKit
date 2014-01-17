using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="NonTerminatingXMLTag"/> is a <see cref="XMLTag"/> which has no end part. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class NonTerminatingXMLTag : XMLTag
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text"> The text. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public NonTerminatingXMLTag(string text) : base(text)
        {
            End = "";
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="text">         The text. </param>
        /// <param name="extension">    The extension. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public NonTerminatingXMLTag(string text, string extension)
            : base(text, extension)
        {
            End = "";
        }
    }
}
