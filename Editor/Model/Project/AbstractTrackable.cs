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
        protected MarkerFuser fuser;
        public MarkerFuser Fuser
        {
            get { return fuser; }
            set { fuser = value; }
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

        private double similarityThreshold = 0.7;
        protected double SimilarityThreshold
        {
            get { return similarityThreshold; }
            set { similarityThreshold = value; }
        }

        public Vector3D vector { get; set; }
        public List<AbstractAugmention> Augmentions { get; set; }

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
            this.Augmentions = new List<AbstractAugmention>();
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
        /// Finds the augmention, which is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is searched for</param>
        /// <returns>the augmention which is found, otherwise null </returns>
        public AbstractAugmention FindAugmention(IPreviewable a)
        {
            return this.Augmentions[this.Augmentions.IndexOf((AbstractAugmention)a)];
        }
        /// <summary>
        /// Checks if the augmention is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is checked existence for</param>
        /// <returns>true, if its associated with this <see cref="AbstractTrackable"/>
        ///          false, else</returns>
        public bool existAugmention(IPreviewable a)
        {
            foreach (AbstractAugmention aug in Augmentions)
            {
                if (aug == (AbstractAugmention)a)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
