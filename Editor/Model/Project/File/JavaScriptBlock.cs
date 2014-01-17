﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="JavaScriptBlock"/> block is an <see cref="AbstractBlock"/>.
    ///             It has a <see cref="head"/> and constits of other <see cref="JavaScriptBlock"/>s
    ///             and <see cref="JavaScriptLine"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class JavaScriptBlock : AbstractBlock
    {
        /// <summary>   The head. </summary>
        private string head;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptBlock() { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="head">         The head. </param>
        /// <param name="blockMarker">  The block marker. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public JavaScriptBlock(string head, BlockMarker blockMarker)
        {
            this.head = head;
            this.blockMarker = blockMarker;
        }

        /// <summary>   The lines. </summary>
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