using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnVisibleEvent"/> is an <see cref="AbstractEvent"/>
    /// that is triggered when the arel.Oject is set to visible.
    /// </summary>
    [Serializable]
    public class OnVisibleEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnVisibleEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public OnVisibleEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onVisible = function()";
            content = new string[] { "/*This function is called when the arel.Oject is set to visible", "TODO Add your code here*/" };
        }
    }
}
