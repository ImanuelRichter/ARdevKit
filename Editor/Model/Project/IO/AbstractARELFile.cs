using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A configuration file. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public abstract class AbstractARELFile
    {
        protected string filePath;
        public string FilePath
        {
            get { return filePath; }
        }

        /// <summary>   The header. </summary>
        protected string header;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sections. </summary>
        ///
        /// <value> The sections. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<AbstractBlock> blocks;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a section. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="block">   The section to be added. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual void AddBlock(AbstractBlock block)
        {
            if (blocks == null)
                blocks = new List<AbstractBlock>();
            block.ParentFile = this;
            blocks.Add(block);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the file at the projectPath. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="projectPath">  The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void Save(string projectPath);

        public abstract void Save();
    }
}
