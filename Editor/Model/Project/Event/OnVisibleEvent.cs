using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnVisibleEvent"/> is an <see cref="AbstractEvent"/> that represents
    /// a function that is called when the arel.Oject is set to visible.
    /// </summary>
    [Serializable]
    public class OnVisibleEvent : AbstractEvent
    {
        public OnVisibleEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onVisible = function()";
            content = new string[] { "/*This function is called when the arel.Oject is set to visible", "TODO Add your code here*/" };
        }
    }
}
