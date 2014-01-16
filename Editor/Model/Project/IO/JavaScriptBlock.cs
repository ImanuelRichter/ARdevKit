using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    public class JavaScriptBlock : AbstractBlock
    {
        private string head;

        public JavaScriptBlock() { }

        public JavaScriptBlock(string head, BlockMarker blockMarker)
        {
            this.head = head;
            this.blockMarker = blockMarker;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the lines. </summary>
        ///
        /// <value> The lines. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<JavaScriptLine> lines;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a line. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="line">  The cln. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddLine(JavaScriptLine line)
        {
            lines = lines == null ? new List<JavaScriptLine>() : lines;
            line.parentBlock = this;
            line.level = level + 1;
            lines.Add(line);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes with the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Write(System.IO.StreamWriter writer)
        {
            string tabs = getTabs();
            if (head != null)
                writer.WriteLine(tabs + head);
            if (blockMarker != null)
                writer.WriteLine(tabs + blockMarker);
            if (lines != null)
            {
                foreach (JavaScriptLine l in lines)
                {
                    l.Write(writer);
                }
            }
            if (blocks != null)
            {
                foreach (JavaScriptBlock block in blocks)
                {
                    block.Write(writer);
                }
            }
            writer.WriteLine(tabs + blockMarker);
        }
    }
}
