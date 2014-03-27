using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="JavaScriptLine"/> is a <see cref="JavaScriptBlock"/>
    ///             which has a <see cref="content"/> that is written in a single line. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Serializable]
    public class JavaScriptLine : JavaScriptBlock
    {
        /// <summary>   The content. </summary>
        protected string content = "";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="content">  The content. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptLine(string content)
        {
            this.content = content;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="content">      The content. </param>
        /// <param name="blockMarker">  The block marker. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptLine(string content, BlockMarker blockMarker)
        {
            this.content = content;
            this.blockMarker = blockMarker;
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
                writer.WriteLine(tabs + blockMarker + content + blockMarker + ";");
            else
                writer.WriteLine(tabs + content + ";");
        }

        public override string ToString()
        {
            string output = "";
            string tabs = getTabs();
            if (blockMarker != null)
                output += tabs + blockMarker + content + blockMarker + ";" + Environment.NewLine;
            else
                output += tabs + content + ";" + Environment.NewLine;
            return output;
        }
    }
}
