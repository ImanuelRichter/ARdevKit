using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using ARdevKit.View;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Describes an abstract twodimensional augmentation with its 
    /// additional features height and width. It inherits from
    /// <see cref="AbstractAugmentation"/>.
    /// </summary>
    [Serializable]
    public abstract class Abstract2DAugmentation : AbstractAugmentation
    {
        /// <summary>
        /// The height, in mm
        /// </summary>
        private int height;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height, in mm.
        /// </value>
        [Browsable(false)]
        [Editor(typeof(SliderEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public int Height
        {
            get { return height; }
            set 
            {
                if (value < 1)
                    height = 1;
                else if (value > 1000)
                    height = 1000;
                else
                    height = value; 
            }
        }

        /// <summary>
        /// The width, in mm
        /// </summary>
        private int width;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width, in mm.
        /// </value>
        [Browsable(false)]
        [Editor(typeof(SliderEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public int Width
        {
            get { return width; }
            set 
            {
                if (value < 1)
                    width = 1;
                else if (value > 1000)
                    width = 1000;
                else 
                    width = value; 
            }
        }

        /// <summary>
        /// Initializes no new instance of the <see cref="Abstract2DAugmentation"/> class,
        /// but can be used in inheriting classes.
        /// sets <see cref="height" /> and <see cref="width" /> = 0
        /// </summary>
        protected Abstract2DAugmentation() : base()
        {
            height = 0;
            width = 0;
        }

        /// <summary> 
        /// constructor, which sets every member of the class as specified,
        /// for use from inheriting classes
        /// </summary>
        ///
        /// <param name="isVisible">            true if this object is visible. </param>
        /// <param name="translationVector">    The translation vector. </param>
        /// <param name="scaling">              The scaling. </param>
        /// <param name="trackable">            The trackable. </param>
        /// <param name="width">                The width. </param>
        /// <param name="height">               The height. </param>
        protected Abstract2DAugmentation(bool isVisible,
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable,
            int width, int height)
            : base(isVisible, translationVector, scaling, trackable)
        {
            this.height = height;
            this.width = width;
        }
    }
}
