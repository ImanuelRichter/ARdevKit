using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    public class JavaScriptInLine : JavaScriptLine
    {
        private bool useComma;
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="content">  The content. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptInLine(string content, bool useComma) : base(content)
        {
            this.useComma = useComma;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="content">      The content. </param>
        /// <param name="blockMarker">  The block marker. </param>
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
    }
}
