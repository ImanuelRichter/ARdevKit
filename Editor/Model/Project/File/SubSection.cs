using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    class SubSection : Section
    {
        public SubSection(Tag tag) : base(tag) { }

        public void AddSubSection(SubSection css)
        {
            SubSections = SubSections == null ? new List<SubSection>() : SubSections;
            css.Parent = this;
            css.Level = Level + 1;
            SubSections.Add(css);
        }

        public void AddLine(ValuedLine cln)
        {
            Lines = Lines == null ? new List<Line>() : Lines;
            cln.Parent = this;
            cln.Level = Level + 1;
            Lines.Add(cln);
        }
    }
}
