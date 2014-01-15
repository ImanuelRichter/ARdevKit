using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    class ProjectConfigHTML : ConfigFile
    {
        public ProjectConfigHTML(Tag tag)
        {
            header = "<!DOCTYPE html>";
            this.tag = tag;
        }

        public override void Write(string projectPath)
        {
            StreamWriter writer = new StreamWriter(projectPath + ".html");
            writer.WriteLine(header);
            writer.WriteLine(tag);
            if (Sections != null)
            {
                foreach (Section cs in Sections)
                {
                    cs.Write(writer);
                }
            }
            writer.WriteLine(tag);
            writer.Close();
        }
    }
}
