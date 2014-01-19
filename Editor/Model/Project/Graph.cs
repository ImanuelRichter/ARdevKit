using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// ToDo summary is missing
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class Graph : AbstractDynamic2DAugmention 
    {
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        [CategoryAttribute("General")]
        public int MaxValue {get; set;}
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        [CategoryAttribute("General")]
        public int MinValue { get; set; }
        /// <summary>
        /// Gets or sets the scaling.
        /// </summary>
        [CategoryAttribute("General")]
        public int Scaling { get; set; }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        private string graphPath;
        /// <summary>
        /// ToDo summary is missing
        /// </summary>
        [CategoryAttribute("General")]
        public string GraphPath
        {
            get { return graphPath; }
            set { graphPath = value; }
        }

        /// <summary>
        /// ToDo summary is missing
        /// </summary>
        protected Graph() : base()
        {
            ; // missing initialization
        }
    }
}
