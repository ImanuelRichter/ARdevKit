using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    public class Tag
    {
        public int Level { get; set; }

        protected bool closed = true;
        public string Start { get; set; }
        public string End { get; set; }

        public Tag(string text)
        {
            Start = text.Insert(0, "<");
            Start = Start.Insert(Start.Length, ">");
            End = Start.Insert(1, "/");
            Level = 0;
        }

        public Tag(string text, string extension) : this(text)
        {
            Start.Insert(Start.Length - 1, " " + extension);
        }

        public override string ToString()
        {
            if (closed)
            {
                closed = false;
                return Start;
            }
            else
            {
                closed = true;
                return End;
            }
        }
    }
}
