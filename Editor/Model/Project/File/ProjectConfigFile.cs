using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A project configuration html. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ProjectConfigFile : ConfigFile
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ProjectConfigFile(Tag tag)
        {
            header = "<!DOCTYPE html>";
            this.tag = tag;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the file at the projectPath. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="path">  The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Write(string path)
        {
            StreamWriter writer = new StreamWriter(path + ".html");
            writer.WriteLine(header);
            writer.WriteLine(tag);
            if (sections != null)
            {
                foreach (Section cs in sections)
                {
                    cs.Write(writer);
                }
            }
            writer.WriteLine(tag);
            writer.Close();
        }
    }
}
