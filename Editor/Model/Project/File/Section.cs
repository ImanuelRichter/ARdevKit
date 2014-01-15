using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    class Section
    {
        public int Level { get; set; }
        protected Tag tag;
        public ConfigFile ParentFile { get; set; }
        protected List<SubSection> SubSections { get; set; }
        protected List<Line> Lines { get; set; }

        public Section(Tag tag)
        {
            this.tag = tag;
            Level = 1;
        }

        public void AddSubSection(SubSection css)
        {
            SubSections = SubSections == null ? new List<SubSection>() : SubSections;
            css.ParentSection = this;
            css.Level = Level + 1;
            SubSections.Add(css);
        }

        public void AddLine(Line cln)
        {
            Lines = Lines == null ? new List<Line>() : Lines;
            cln.ParentSection = this;
            cln.Level = Level + 1;
            Lines.Add(cln);
        }

        public virtual void Write(System.IO.StreamWriter writer)
        {
            string tabs = "";
            for (int i = 0; i < Level; i++)
            {
                tabs += "\t";
            }
            writer.WriteLine(tabs + tag);
            if (Lines != null)
            {
                foreach (Line cln in Lines)
                {
                    cln.Write(writer);
                }
            }
            if (SubSections != null)
            {
                foreach (SubSection css in SubSections)
                {
                    css.Write(writer);
                }
            }
            writer.WriteLine(tabs + tag);
        }
    }
}
