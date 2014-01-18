using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A trackingData_[SensorType][SensorSubType].xml. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class TrackingDataFile : AbstractARELFile
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="header">       The header. </param>
        /// <param name="projectPath">  Full pathname of the project file. </param>
        /// <param name="fileName">     Filename of the file. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TrackingDataFile(string header, string projectPath, string fileName)
        {
            this.header = header;
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            filePath = Path.Combine(assetsPath, fileName);
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
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            filePath = Path.Combine(assetsPath, "TrackingData.xml");
            Save();
        }
    }
}
