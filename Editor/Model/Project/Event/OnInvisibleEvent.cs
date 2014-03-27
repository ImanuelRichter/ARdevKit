using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnInvisibleEvent"/> is an <see cref="AbstractEvent"/>
    /// that is triggered when the arel.Oject is set to invisible.
    /// </summary>
    [Serializable]
    public class OnInvisibleEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnInvisibleEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public OnInvisibleEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onInvisible = function()";
            content = new string[] { "/*This function is called when the arel.Oject is set to invisible", "TODO Add your code here*/" };
        }
    }
}
