using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public class Vector3D
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y { get; set; }
        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        /// <value>
        /// The z.
        /// </value>
        public int Z { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Lizzard, 1/15/2014. </remarks>
        ///
        /// <param name="x">    The x coordinate. </param>
        /// <param name="y">    The y coordinate. </param>
        /// <param name="z">    The z coordinate. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Vector3D(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    


}
