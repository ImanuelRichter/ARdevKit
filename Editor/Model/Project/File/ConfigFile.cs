using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    public class ConfigFile
    {
        public int Level { get; set; }

        protected Tag tag;
        private List<Section> Sections { get; set; }

        public ConfigFile(Tag tag)
        {
            this.tag = tag;
            Level = 0;
        }

        public void AddSection(Section cs)
        {
            Sections = Sections == null ? new List<Section>() : Sections;
            cs.Parent = this;
            cs.Level = Level + 1;
            Sections.Add(cs);
        }

        public virtual void Write(System.IO.StreamWriter writer)
        {
            writer.WriteLine(tag);
            if (Sections != null)
            {
                foreach (Section cs in Sections)
                {
                    cs.Write(writer);
                }
            }
            writer.WriteLine(tag);
        }
    }
}
