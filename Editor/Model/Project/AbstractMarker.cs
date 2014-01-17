using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractMarker : AbstractTrackable
    {
        /// <summary>
        /// The type
        /// </summary>
        protected string type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [CategoryAttribute("General")]
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
        [CategoryAttribute("General")]
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public AbstractMarker() : base()
        {
            ; // some missing initialization?
        }
    }
}
