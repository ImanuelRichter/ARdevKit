using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A sub section is a <see cref="XMLBlock"/> which has a parent <see cref="XMLBlock"/>
    ///             and can have 0..* <see cref="XMLBlock"/>s or <see cref="XMLLine"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class XMLBlock : AbstractBlock
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public XMLBlock(TerminatingTag tag)
        {
            this.blockMarker = tag;
            level = 0;
        }

        public void Update(TerminatingTag tag)
        {
            this.blockMarker = tag;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the lines. </summary>
        ///
        /// <value> The lines. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<XMLLine> lines;

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
        /// <summary>   Writes with the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual void Write(System.IO.StreamWriter writer)
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
