using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    /// <summary>
    /// A <see cref="JavaScriptInLine"/> is a <see cref="JavaScriptLine"/> which is closed by a "," instead of a ";".
    /// </summary>
    [Serializable]
    public class JavaScriptInLine : JavaScriptLine
    {
        private bool useComma;
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="useComma">if set to <c>true</c> [use comma].</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptInLine(string content, bool useComma) : base(content)
        {
            this.useComma = useComma;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="blockMarker">The block marker.</param>
        /// <param name="useComma">if set to <c>true</c> [use comma].</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptInLine(string content, BlockMarker blockMarker, bool useComma) : base(content, blockMarker)
        {
            this.useComma = useComma;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes with the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Write(System.IO.StreamWriter writer)
        {
            string tabs = getTabs();
            if (blockMarker != null)
            {
                if (useComma)
                    writer.WriteLine(tabs + blockMarker + content + blockMarker + ",");
                else
                    writer.WriteLine(tabs + blockMarker + content + blockMarker);
            }
            else
            {
                if (useComma)
                    writer.WriteLine(tabs + content + ",");
                else
                    writer.WriteLine(tabs + content);
            }
        }

        public override string ToString()
        {
            string output = "";
            string tabs = getTabs();
            if (blockMarker != null)
            {
                if (useComma)
                    output += tabs + blockMarker + content + blockMarker + "," + Environment.NewLine;
                else
                    output += tabs + blockMarker + content + blockMarker + Environment.NewLine;
            }
            else
            {
                if (useComma)
                    output += tabs + content + "," + Environment.NewLine;
                else
                    output += tabs + content + Environment.NewLine;
            }
            return output;
        }
    }
}
