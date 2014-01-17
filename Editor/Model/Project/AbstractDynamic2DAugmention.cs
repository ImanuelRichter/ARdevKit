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
    public abstract class AbstractDynamic2DAugmention : AbstractAugmention
    {
        /// <summary>
        /// New Variable which is for link a Source with this Augmentation
        /// </summary>
        [CategoryAttribute("General")]
        public AbstractSource source { get; set; }

        protected AbstractDynamic2DAugmention() : base()
        {
            ; // missing initialization
        }
    }
}
