using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    public class ValuedLine : Line
    {
        private string value;

        public ValuedLine(Tag tag, string value) : base(tag)
        {
            this.value = value;
        }

        public override void Write(System.IO.StreamWriter writer)
        {
            string tabs = "";
            for (int i = 0; i < Level; i++)
            {
                tabs += "\t";
            }
            writer.WriteLine(tabs + tag + value + tag);
        }
    }
}
