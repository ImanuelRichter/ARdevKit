using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A configuration file. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public abstract class ConfigFile
    {
        /// <summary>   The header. </summary>
        protected string header;

        /// <summary>   The starting tag. </summary>
        protected Tag tag;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sections. </summary>
        ///
        /// <value> The sections. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<Section> sections;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a section. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="cs">   The section to be added. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual void AddSection(Section cs)
        {
            sections = sections == null ? new List<Section>() : sections;
            cs.ParentFile = this;
            sections.Add(cs);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the file at the projectPath. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="projectPath">  The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void Write(string projectPath);
    }
}
