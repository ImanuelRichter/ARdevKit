using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    class ProjectConfigHTML
    {
        private const string header = "<!DOCTYPE html>";
        private ConfigFile file;

        public ConfigFile File
        {
            get { return file; }
            set { file = value; }
        }

        public void Write(string projectPath)
        {
            StreamWriter writer = new StreamWriter(projectPath + ".html");
            writer.WriteLine(header);
            file.Write(writer);
            writer.Close();
        }
    }
}
