using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.IO
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A project configuration html. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ARELProjectFile : AbstractARELFile
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELProjectFile(string header, string filePath)
        {
            this.header = header;
            this.filePath = filePath;
        }

        public override void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            if (header != null && header != "")
                writer.WriteLine(header);
            if (blocks != null)
            {
                foreach (HTMLBlock htmlBlock in blocks)
                {
                    htmlBlock.Write(writer);
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

        public override void Save(string filePath)
        {
            this.filePath = filePath;
            Save();
        }
    }
}
