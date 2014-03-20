using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    public class OnTouchEndedEvent : AbstractEvent
    {
        public OnTouchEndedEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onTouchEnded = function()";
            content = new string[] { "/*This function is called when the touch on the arel.Oject has ended.", "TODO Add your code here*/" };
        }
    }
}
