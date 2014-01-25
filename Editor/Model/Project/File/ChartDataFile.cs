using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    public class ChartDataFile : AbstractFile
    {
        /// <summary>   Identifier for the chart. </summary>
        private string chartID;
        public ChartDataFile(string projectPath, string chartID)
        {
            this.chartID = chartID;
            string chartPath = Path.Combine(projectPath, "Assets", chartID);
            if (!Directory.Exists(chartPath))
                Directory.CreateDirectory(chartPath);
            filePath = Path.Combine(chartPath, "data.xml");
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
            string chartPath = Path.Combine(projectPath, "Assets", chartID);
            if (!Directory.Exists(chartPath))
                Directory.CreateDirectory(chartPath);
            filePath = Path.Combine(chartPath, "data.xml");
            Save();
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
                foreach (XMLBlock htmlBlock in blocks)
                {
                    htmlBlock.Write(writer);
                }
            }
            writer.Close();
        }
    }
}
