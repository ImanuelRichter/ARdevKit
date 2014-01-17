using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class AbstractMarker : AbstractTrackable
    {
        /// <summary>
        /// The type
        /// </summary>
        protected string type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The size
        /// </summary>
        protected int size;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
