using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    public class OnTouchStartedEvent : AbstractEvent
    {
        public OnTouchStartedEvent(string augmentationID) : base(augmentationID)
        {
            head = augmentationID + ".onTouchStarted = function()";
        }
    }
}
