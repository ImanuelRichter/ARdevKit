using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnUnloadedEvent"/> is an <see cref="AbstractEvent"/>
    /// that is triggered when the arel.Oject is unloaded.
    /// </summary>
    [Serializable]
    public class OnUnloadedEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnUnloadedEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public OnUnloadedEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onUnloaded = function()";
            content = new string[] { "/*This function is called when the arel.Oject is unloaded.", "TODO Add your code here*/" };
        }
    }
}
