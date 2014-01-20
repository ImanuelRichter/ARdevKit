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
        /// The sensor cos identifier, used by AREL
        /// to specify the TrackingData
        /// </summary>
        protected string sensorCosID;
        /// <summary>
        /// Gets or sets the sensor cos identifier.
        /// </summary>
        [CategoryAttribute("Expert"), ReadOnly(true)]
        public string SensorCosID
        {
            get { return sensorCosID; }
            set { sensorCosID = value; }
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
        [CategoryAttribute("Expert"), DefaultValue(0.7)]
        public double SimilarityThreshold
        {
            get { return similarityThreshold; }
            set { similarityThreshold = value; }
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
        /// Initializes no new instance of the <see cref="AbstractTrackable"/> class,
        /// but can be used in inheriting classes
        /// No <see cref="AbstractAugmentation"/>s are associated.
        /// </summary>
        protected AbstractTrackable()
        {
            vector = new Vector3D(0, 0, 0);
            similarityThreshold = 0.7;
            Augmentations = new List<AbstractAugmentation>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a new Fuser that fits to the trackable. </summary>
        ///
        /// <remarks>   Imanuel, 20.01.2014. </remarks>
        ///
        /// <returns>   The fuser. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract MarkerFuser getFuser();

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
        /// Finds the augmentation, which is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is searched for</param>
        /// <returns>the augmentation which is found, otherwise null </returns>
        public AbstractAugmentation FindAugmentation(IPreviewable a)
        {
            return this.Augmentations[this.Augmentations.IndexOf((AbstractAugmentation)a)];
        }

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
    }
}
