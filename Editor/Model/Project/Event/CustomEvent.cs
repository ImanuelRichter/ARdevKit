using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// A <see cref="CustomEvent"/> is an <see cref="AbstractEvent"/> that has no head and no <see cref="BlockMarker"/>.
    /// This provides the user the possobility to include every content he wants to.
    /// The content will be loaded together with the corresponding augmentation.
    /// </summary>
    [Serializable]
    public class CustomEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public CustomEvent(string augmentationID)
            : base(augmentationID)
        {
            content = new string[] {"/*This template allows you to write your own functions which will be loaded together with the corresponding augmentation", "TODO add your code here*/"};
            head = null;
            blockMarker = null;
        }
    }
}
