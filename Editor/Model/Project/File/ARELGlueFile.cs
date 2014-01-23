using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An arelGlue.js. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ARELGlueFile : AbstractFile
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="projectPath">  Full pathname of the project file. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELGlueFile(string projectPath)
        {
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            filePath = Path.Combine(assetsPath, "arelGlue.js");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Saves the file to its <see cref="filePath"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            if (blocks != null)
            {
                foreach (JavaScriptBlock jsBlock in blocks)
                {
                    jsBlock.Write(writer);
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
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            filePath = Path.Combine(assetsPath, "arelGlue.js");
            Save();
        }
    }
}
