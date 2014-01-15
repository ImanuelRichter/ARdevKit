using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    class OpenTag : Tag
    {
        public OpenTag(string text) : base(text)
        {
            End = "";
        }

        public OpenTag(string text, string extension)
            : base(text, extension)
        {
            End = "";
        }
    }
}
