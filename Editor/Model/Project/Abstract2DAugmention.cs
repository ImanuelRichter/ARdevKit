using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class Abstract2DAugmention : AbstractAugmention
    {
        /// <summary>
        /// The height
        /// </summary>
        private int height;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// The width
        /// </summary>
        private int width;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        public Abstract2DAugmention() : base()
        {
            // todo!
        }
    }
}
