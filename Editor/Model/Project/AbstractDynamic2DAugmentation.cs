using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Inherits from <see cref="Abstract2DAugmentation"/> and adds
    /// <see cref="AbstractSource"/>, in order to show dynamic content.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractDynamic2DAugmentation : Abstract2DAugmentation
    {
        /// <summary>
        /// variable which links an <see cref="AbstractSource"/> to 
        /// this <see cref="Abstract2DAugmentation"/>.
        /// </summary>
        [CategoryAttribute("General")] [Browsable(false)]
        public AbstractSource Source { get; set; }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractDynamic2DAugmentation"/> class,
        /// but can be used by inheriting classes. It is
        /// using the standard constructor from <see cref="Abstract2DAugmentation"/>.
        /// </summary>
        protected AbstractDynamic2DAugmentation() : base() { }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractDynamic2DAugmentation" /> class,
        /// but can be used by inheriting classes. It is
        /// using the constructor from <see cref="Abstract2DAugmentation" />.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible] using AREL.</param>
        /// <param name="translationVector">The translation vector.</param>
        /// <param name="scaling">The scaling.</param>
        /// <param name="trackable">The trackable.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="source">The source.</param>
        protected AbstractDynamic2DAugmentation(bool isVisible,
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable, int width, int height,
            AbstractSource source)
            : base(isVisible, translationVector, scaling, trackable, width, height)
        {
            this.Source = source;
        }

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 30.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            AbstractDynamic2DAugmentation n = ObjectCopier.Clone<AbstractDynamic2DAugmentation>(this);
            if (Source != null)
            {
                n.Source = (AbstractSource)Source.Clone();
            }
            return n;
        }

        /// <summary>
        /// This method is called by the previewController when a new instance of the element is added to the Scene. It sets "must-have" properties.
        /// </summary>
        /// <param name="ew">The ew.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public override bool initElement(ARdevKit.EditorWindow ew)
        {
            bool ok = base.initElement(ew);
            if (this.Source != null)
            {
                this.Source.initElement(ew);
                this.Source.Augmentation = this;
            }
            return ok;
        }
    }
}
