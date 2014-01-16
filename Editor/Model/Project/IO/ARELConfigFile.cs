using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.IO
{
    public class ARELConfigFile : AbstractFile
    {
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

        public override void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            if (header != null && header != "")
                writer.WriteLine(header);
            if (sections != null)
            {
                foreach (Section cs in sections)
                {
                    cs.Write(writer);
                }
            }
            writer.Close();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the file at the projectPath. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="path">  The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Save(string projectPath)
        {
            this.filePath = Path.Combine(projectPath, "arelConfig.xml");
            Save();
        }
    }
}
