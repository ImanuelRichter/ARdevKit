using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    public class OnUnloadedEvent : AbstractEvent
    {
        public OnUnloadedEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onUnloaded = function()";
            content = new string[] { "/*This function is called when the arel.Oject is unloaded.", "TODO Add your code here*/" };
        }
    }
}
