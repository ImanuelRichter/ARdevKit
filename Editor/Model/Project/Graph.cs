using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class Graph : AbstractDynamic2DAugmentation 
    {
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int maxValue {get; set;}
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public int minValue { get; set; }
        /// <summary>
        /// Gets or sets the scaling.
        /// </summary>
        /// <value>
        /// The scaling.
        /// </value>
        public int scaling { get; set; }
    }
}
