﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A arel[projectName].html. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ARELProjectFile : AbstractFile
    {
        /// <summary>   The header. </summary>
        protected string header;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="filePath">The file path.</param>
        /// <remarks>
        /// Imanuel, 15.01.2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELProjectFile(string header, string filePath)
        {
            this.header = header;
            this.filePath = filePath;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the file to its <see cref="filePath"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            if (header != null && header != "")
                writer.WriteLine(header);
            if (blocks != null)
            {
                foreach (XMLBlock htmlBlock in blocks)
                {
                    htmlBlock.Write(writer);
                }
            }
            writer.Close();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the file to the using the passed <see cref="projectPath"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="filePath"> The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Save(string filePath)
        {
            this.filePath = filePath;
            Save();
        }
    }
}
