using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Just a class for AbstractAugmentations. With this we are able to List all the customUserEvents. 
    /// See issue #13 for reason of this class.
    /// </summary>
    public class CustomUserEvent
    {
        private string name;
        private string[] content;

        public CustomUserEvent(string name, string[] content)
        {
            this.name = name;
            this.content = content;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string[] Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
