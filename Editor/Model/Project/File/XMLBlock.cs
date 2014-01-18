using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="XMLBlock"/> is an <see cref="AbstractBlock"/> which can have
    ///             <see cref="XMLTag"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class XMLBlock : AbstractBlock
    {

        /// <summary>   The lines. </summary>
        protected List<XMLLine> lines;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public XMLBlock(XMLTag tag)
        {
            this.blockMarker = tag;
            level = 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the <see cref="BlockMarker"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Update(XMLTag tag)
        {
            this.blockMarker = tag;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a line. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="line">  The cln. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddLine(XMLLine line)
        {
            lines = lines == null ? new List<XMLLine>() : lines;
            line.parentBlock = this;
            line.level = level + 1;
            lines.Add(line);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Write(System.IO.StreamWriter writer)
        {
            string tabs = getTabs();
            writer.WriteLine(tabs + blockMarker);
            if (lines != null)
            {
                foreach (XMLLine l in lines)
                {
                    l.Write(writer);
                }
            }
            if (blocks != null)
            {
                foreach (XMLBlock block in blocks)
                {
                    block.Write(writer);
                }
            }
            writer.WriteLine(tabs + blockMarker);
        }
    }
}
