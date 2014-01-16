using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    public class JavaScriptLine : JavaScriptBlock
    {
        private string content = "";

        public JavaScriptLine(string content)
        {
            this.content = content;
        }

        public JavaScriptLine(string content, BlockMarker blockMarker)
        {
            this.content = content;
            this.blockMarker = blockMarker;
        }

        public override void Write(System.IO.StreamWriter writer)
        {
            string tabs = getTabs();
            writer.WriteLine(tabs + content + ";");
        }
    }
}
