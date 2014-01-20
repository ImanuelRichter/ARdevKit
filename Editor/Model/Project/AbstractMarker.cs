using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Describes an <see cref="AbstractMarker"/> which is a <see cref="AbstractTrackable"/>.
    /// specifying the type and the size.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractMarker : AbstractTrackable
    {
        /// <summary>
        /// The type, to differtiate between different Marker types
        /// and their way to be tracked.
        /// </summary>
        protected string type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [CategoryAttribute("General"), ReadOnly(true)]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The size of the Marker in mm
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

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractMarker"/> class,
        /// but can be used by inheriting classes.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        protected AbstractMarker(string type, int size) : base()
        {
            this.type = type;
            this.size = size;            
        }
    }
}
