using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Just a class for AbstractAugmentations. With this we are able to List all the customUserEvents. 
    /// See issue #13 for reason of this class.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CustomUserEvent
    {
        /// <summary>
        /// The name of the event
        /// </summary>
        private string name;
        /// <summary>
        /// Get or set the name of the event
        /// </summary>
        [CategoryAttribute("General")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The content of the event
        /// </summary>
        private string[] content;
        /// <summary>
        /// Get or set the content of the event
        /// </summary>
        [CategoryAttribute("General")]
        public string[] Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomUserEvent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        public CustomUserEvent(string name, string[] content)
        {
            this.name = name;
            this.content = content;
        }
    }
}
