using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnTouchEndedEvent"/> is an <see cref="AbstractEvent"/>
    /// that is triggered when the touch on the arel.Oject has ended.
    /// </summary>
    [Serializable]
    public class OnTouchEndedEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnTouchEndedEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public OnTouchEndedEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onTouchEnded = function()";
            content = new string[] { "/*This function is called when the touch on the arel.Oject has ended.", "TODO Add your code here*/" };
        }
    }
}
