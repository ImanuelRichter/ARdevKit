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
    public abstract class Graph : AbstractDynamic2DAugmention 
    {
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int MaxValue {get; set;}
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public int MinValue { get; set; }
        /// <summary>
        /// Gets or sets the scaling.
        /// </summary>
        /// <value>
        /// The scaling.
        /// </value>
        public int Scaling { get; set; }

        private string graphPath;
        public string GraphPath
        {
            get { return graphPath; }
            set { graphPath = value; }
        }
    }
}
