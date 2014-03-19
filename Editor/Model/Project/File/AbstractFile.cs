using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An <see cref="AbstractFile"/> can be an <see cref="ARELConfigFile"/>,
    ///             an <see cref="ARELProjectFile"/>, a <see cref="TrackinDataFile"/>
    ///             or an <see cref="ARELGlueFile"/>. It must have a <see cref="filePath"/>
    ///             and can have a <see cref="header"/> and consists of <see cref="AbstractBlock"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractFile
    {
        /// <summary>   Full pathname of the file. </summary>
        protected string filePath;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the full pathname of the file. </summary>
        ///
        /// <value> The full pathname of the file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A list of the <see cref="AbstractBlock"/>s this file consists of. </summary>
        ///
        /// <value> The sections. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<AbstractBlock> blocks;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds an <see cref="AbstractBlock"/>. </summary>
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
        /// <summary>   Saves the file to the using the passed <see cref="projectPath"/>. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="projectPath">  The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void Save(string projectPath);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the file to its <see cref="filePath"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void Save();
    }
}
