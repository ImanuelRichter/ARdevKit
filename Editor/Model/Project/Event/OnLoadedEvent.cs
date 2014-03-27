using ARdevKit.Model.Project.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// An <see cref="OnLoadedEvent"/> is an <see cref="AbstractEvent"/>
    /// that is triggered when the arel.Oject is loaded.
    /// </summary>
    [Serializable]
    public class OnLoadedEvent : AbstractEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnLoadedEvent"/> class.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public OnLoadedEvent(string augmentationID)
            : base(augmentationID)
        {
            head = augmentationID + ".onLoaded = function()";
            content = new string[] { "/*This function is called when the arel.Oject is loaded.", "TODO Add your code here*/" };
        }
    }
}
