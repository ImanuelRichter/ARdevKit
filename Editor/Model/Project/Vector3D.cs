using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A 3D vektor.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class Vector3D
    {
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        /// <value>
        /// The x coordinate.
        /// </value>
        [CategoryAttribute("General")]
        public double X { get; set; }
        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        /// <value>
        /// The y coordinate.
        /// </value>
        [CategoryAttribute("General")]
        public double Y { get; set; }
        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        /// <value>
        /// The z coordinate.
        /// </value>
        [CategoryAttribute("General")]
        public double Z { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <remarks>
        /// Lizzard, 1/15/2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    


}
