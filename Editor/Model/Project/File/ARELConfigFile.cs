using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An arelConfig.xml. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ARELConfigFile : AbstractFile
    {
        /// <summary>   The header. </summary>
        protected string header;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELConfigFile(string header, string projectPath)
        {
            this.header = header;
            filePath = Path.Combine(projectPath, "arelConfig.xml");
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
        /// <param name="projectPath">  The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Save(string projectPath)
        {
            this.filePath = Path.Combine(projectPath, "arelConfig.xml");
            Save();
        }
    }
}
