using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Describes an <see cref="AbstractTrackable"/> with its
    /// associated <see cref="AbstractAugmentation"/>s and 
    /// further details used for AREL.
    /// Is <see cref="IPreviewable"/>
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractTrackable : IPreviewable
    {
        /// <summary>
        /// The type, to differtiate between different Marker types
        /// and their way to be tracked.
        /// </summary>
        protected string type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [CategoryAttribute("General"), Description("Type of the marker"), ReadOnly(true)]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Describes at which similarity,
        /// a picture recorded by the camera is recognized
        /// to be the desired one. Only experts usage.
        /// </summary>
        protected double similarityThreshold;
        /// <summary>
        /// Gets or sets the similarity threshold.
        /// </summary>
        /// <value>
        /// The similarity threshold.
        /// </value>
        [CategoryAttribute("Expert"), DefaultValue(0.7), Editor(typeof(SliderEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("Descibes at which similarty a picture recorded by the camera is recognized.")]
        public double SimilarityThreshold
        {
            get { return similarityThreshold; }
            set 
            {
                if (value < (double)0)
                    similarityThreshold = (double)0;
                else if (value > (double)1)
                    similarityThreshold = (double)1;
                else
                    similarityThreshold = value; 
            }
        }

        /// <summary>
        /// Describes the position of the Trackable
        /// in the coordinatesystem used by metaio.
        /// </summary>
        /// <value>
        /// The vector.
        /// </value>
        [CategoryAttribute("General"), Browsable(false)]
        public Vector3D vector { get; set; }

        /// <summary>
        /// Lists all associated <see cref="AbstractAugmentations"/>.
        /// </summary>
        /// <value>
        /// The augmentations.
        /// </value>
        [Browsable(false)]
        public List<AbstractAugmentation> Augmentations { get; set; }

        /// <summary>
        /// An abstract method, to accept a <see cref="AbstractProjectVisitor"/>
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this element</param>
        public abstract void Accept(AbstractProjectVisitor visitor);

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative Bitmap</returns>
        public abstract Bitmap getPreview();

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative iconized Bitmap</returns>
        public abstract Bitmap getIcon();

        /// <summary>
        /// Checks if the augmentation is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is checked existence for</param>
        /// <returns>true, if its associated with this <see cref="AbstractTrackable"/>
        ///          false, else</returns>
        public bool existAugmentation(IPreviewable a)
        {
            foreach (AbstractAugmentation aug in Augmentations)
            {
                if (aug == (AbstractAugmentation)a)
                {
                    return true;
                }
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Removes the augmentation described by augmentation. </summary>
        ///
        /// <remarks>   Imanuel, 31.01.2014. </remarks>
        ///
        /// <param name="augmentation"> The augmentation. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void RemoveAugmentation(AbstractAugmentation augmentation)
        {
            augmentation.CleanUp();
            Augmentations.Remove(augmentation);
        }

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 22.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public abstract object Clone();

        /// <summary>
        /// This method is called by the previewController when a new instance of the element is added to the Scene. It sets "must-have" properties.
        /// </summary>
        /// <param name="ew">The ew.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public virtual bool initElement(EditorWindow ew)
        {
            //do nothing if not overwritten.
            return true;
        }
    }
}
