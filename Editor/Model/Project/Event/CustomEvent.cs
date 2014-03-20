using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    public class CustomEvent : AbstractEvent
    {
        public CustomEvent(string augmentationID)
            : base(augmentationID)
        {
            content = new string[] {"/*This template allows you to write your own functions which will be loaded together with the corresponding augmentation", "TODO add your code here*/"};
            head = null;
            blockMarker = null;
        }
    }
}
