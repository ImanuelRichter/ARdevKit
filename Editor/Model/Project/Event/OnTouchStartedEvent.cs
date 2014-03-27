using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnTouchStartedEvent"/> is an <see cref="AbstractEvent"/>
    /// that is triggered when the touch on the arel.Oject has started.
    /// </summary>
    [Serializable]
    public class OnTouchStartedEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnTouchStartedEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public OnTouchStartedEvent(string augmentationID) : base(augmentationID)
        {
            head = augmentationID + ".onTouchStarted = function()";
            content = new string[] { "/*This function is called when the touch on the arel.Oject has started.", "TODO Add your code here*/" };
        }
    }
}
