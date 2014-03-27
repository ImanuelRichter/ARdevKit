using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///             An <see cref="AbstractBlock"/> has a <see cref="level"/>
    ///             and can contain other <see cref="AbstractBlock"/>s.
    ///             It can have a <see cref="BlockMarker"/> and a <see cref="parentFile"/>.
    /// </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Serializable]
    public abstract class AbstractBlock
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The level of the <see cref="AbstractBlock"/>. </summary>
        ///
        /// <value> The level. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected int level;

        /// <summary>   The <see cref="BlockMarker"/> of this <see cref="AbstractBlock"/>. </summary>
        protected BlockMarker blockMarker;

        /// <summary>   The <see cref="AbstractFile"/> this block belongs to. </summary>
        protected AbstractFile parentFile;
        /// <summary>
        /// Gets or sets the parent file.
        /// </summary>
        /// <value>
        /// The parent file.
        /// </value>
        [Browsable(false)]
        public AbstractFile ParentFile
        {
            get { return parentFile; }
            set { parentFile = value; }
        }

        /// <summary>   A <see cref="AbstractBlock"/> this <see cref="AbstractBlock"/> belongs to. </summary>
        protected AbstractBlock parentBlock;

        /// <summary>   The <see cref="AbstractBlock"/>s that belong to this <see cref="AbstractBlock"/>. </summary>
        protected List<AbstractBlock> blocks;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds an <see cref="AbstractBlock"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="block">    The <see cref="AbstractBlock"/>. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddBlock(AbstractBlock block)
        {
            blocks = blocks == null ? new List<AbstractBlock>() : blocks;
            block.parentBlock = this;
            block.level = level + 1;
            blocks.Add(block);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a string containing [<see cref="level"/>] tabs. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <returns>   The tabs. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected virtual string getTabs()
        {
            string tabs = "";
            for (int i = 0; i < level; i++)
            {
                tabs += "\t";
            }
            return tabs;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the <see cref="AbstractBlock"/> with the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual void Write(System.IO.StreamWriter writer) { }
    }
}
