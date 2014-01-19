using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    public class BarChartFile : AbstractARELFile
    {
        private int barChartNumber;

        public BarChartFile(string projectPath, int barChartNumber)
        {
            this.barChartNumber = barChartNumber;
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            filePath = Path.Combine(assetsPath, "barChart" + barChartNumber + ".js");
        }

        public override void Save(string projectPath)
        {
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            filePath = Path.Combine(assetsPath, "barChart" + barChartNumber + ".js");
        }

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
    }
}
