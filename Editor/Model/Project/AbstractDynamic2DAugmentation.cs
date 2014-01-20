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
    public abstract class AbstractDynamic2DAugmentation : Abstract2DAugmentation
    {
        /// <summary>
        /// New Variable which is for link a Source with this Augmentation
        /// </summary>
        [CategoryAttribute("General")]
        public AbstractSource source { get; set; }

        protected AbstractDynamic2DAugmentation() : base()
        {
            this.source = null;
        }

        protected AbstractDynamic2DAugmentation(int coordSystemId, bool isVisible,
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable,
            AbstractSource source)
            : base(coordSystemId, isVisible, translationVector, scaling, trackable)
        {
            this.source = source;
        }
    }
}
