using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A vector 3 di. Is a <see cref="Vector3D"/> with
    /// an extra int variable.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class Vector3Di : Vector3D
    {
        /// <summary>
        /// Used by TrackingConfig from metaioSDK
        /// </summary>
        private int w;
        /// <summary>
        /// Gets or sets the w.
        /// </summary>
        /// <value>
        /// The w.
        /// </value>
        [CategoryAttribute("General")]
        public int W
        {
            get { return w; }
            set { w = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="w">Used by TrackingConfig from metaioSDK</param>
        public Vector3Di(int x, int y, int z, int w) : base(x, y, z)
        {
            this.w = w;
        }
    }
}
