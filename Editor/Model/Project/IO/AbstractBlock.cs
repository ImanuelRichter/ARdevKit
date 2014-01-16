using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    public abstract class AbstractBlock
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected int level;

        /// <summary>   The tag. </summary>
        protected BlockMarker blockMarker;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the parent file. </summary>
        ///
        /// <value> The parent file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected AbstractARELFile parentFile;
        public AbstractARELFile ParentFile
        {
            get { return parentFile; }
            set { parentFile = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the parent section. </summary>
        ///
        /// <value> The parent section. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected AbstractBlock parentBlock;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sub sections. </summary>
        ///
        /// <value> The sub sections. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<AbstractBlock> blocks;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a sub section. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="block">  The CSS. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddBlock(AbstractBlock block)
        {
            blocks = blocks == null ? new List<AbstractBlock>() : blocks;
            block.parentBlock = this;
            block.level = level + 1;
            blocks.Add(block);
        }

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
        /// <summary>   Writes with the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual void Write(System.IO.StreamWriter writer) { }
    }
}
