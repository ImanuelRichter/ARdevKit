using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class AbstractTrackable : IPreviewable//, ISerializable 
    {

        /// <summary>
        /// The marker fuser
        /// </summary>
        private Fuser markerFuser;
        /// <summary>
        /// Gets or sets the marker fuser.
        /// </summary>
        /// <value>
        /// The marker fuser.
        /// </value>
        public Fuser MarkerFuser
        {
            get { return markerFuser; }
            set { markerFuser = value; }
        }

        /// <summary>
        /// The sensor cos identifier
        /// </summary>
        protected string sensorCosID;
        /// <summary>
        /// Gets or sets the sensor cos identifier.
        /// </summary>
        /// <value>
        /// The sensor cos identifier.
        /// </value>
        public string SensorCosID
        {
            get { return sensorCosID; }
            set { sensorCosID = value; }
        }

        /// <summary>
        /// Gets or sets the vector,
        /// </summary>
        /// <value>
        /// The vector.
        /// </value>
        public Vector3D vector { get; set; }

        /// <summary>
        /// Gets or sets the augmentations.
        /// </summary>
        /// <value>
        /// The augmentations.
        /// </value>
        public List<AbstractAugmentation> augmentations { get; set; }

        public abstract void Accept(AbstractProjectVisitor visitor);

        public abstract Bitmap getPreview();

        public abstract Bitmap getIcon();

        public abstract List<AbstractProperty> getPropertyList();


        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTrackable"/> class.
        /// With no trackables associated.
        /// </summary>
        public AbstractTrackable()
        {
            this.augmentations = new List<AbstractAugmentation>();
        }

        /// <summary>
        ///     Is needed for Custom Serialization. And provides the Serializer with the needed information
        /// </summary>
        /// <param name="info">Serialization Information, which is modified to encapsulate the things to save</param>
        /// <param name="context">describes aim and source of a serialized stream</param>
        [Obsolete("GetObjectData is obsolete, serialization is done without customization.")]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Finds the augmentation, which is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is searched for</param>
        /// <returns>the Augmentation which is found, otherwise null </returns>
        public AbstractAugmentation findAugmentation(IPreviewable a)
        {
            return this.augmentations[this.augmentations.IndexOf((AbstractAugmentation)a)];
        }

        /// <summary>
        /// Checks if the augmentation is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is checked existence for</param>
        /// <returns>true, if its associated with this <see cref="AbstractTrackable"/>
        ///          false, else</returns>
        public bool existAugmentation(IPreviewable a)
        {
            foreach (AbstractAugmentation aug in augmentations)
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
