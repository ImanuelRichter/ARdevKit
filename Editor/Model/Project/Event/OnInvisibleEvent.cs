using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    [Serializable]
    public class OnInvisibleEvent : AbstractEvent
    {
        public OnInvisibleEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onInvisible = function()";
            content = new string[] { "/*This function is called when the arel.Oject is set to invisible", "TODO Add your code here*/" };
        }
    }
}
