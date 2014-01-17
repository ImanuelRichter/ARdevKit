using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Just a class for AbstractAugmentations. With this we are able to List all the customUserEvents. 
    /// See issue #13 for reason of this class.
    /// </summary>
    [Serializable]
    public class CustomUserEvent
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The content
        /// </summary>
        private string[] content;

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

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string[] Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
