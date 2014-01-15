using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    abstract class ConfigFile
    {
        protected string header;

        protected Tag tag;
        protected List<Section> Sections { get; set; }

        public void AddSection(Section cs)
        {
            Sections = Sections == null ? new List<Section>() : Sections;
            cs.ParentFile = this;
            Sections.Add(cs);
        }

        public abstract void Write(string projectPath);
    }
}
