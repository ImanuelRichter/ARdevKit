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
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractTrackable : IPreviewable//, ISerializable 
    {
        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        protected MarkerFuser fuser;
        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        [Browsable(false)]
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
        [CategoryAttribute("Expert"), ReadOnly(true)]
        public string SensorCosID
        {
            get { return sensorCosID; }
            set { sensorCosID = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        protected double similarityThreshold = 0.7;
        [CategoryAttribute("Expert"), DefaultValue(0.7)]
        public double SimilarityThreshold
        {
            get { return similarityThreshold; }
            set { similarityThreshold = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        [CategoryAttribute("General"), Browsable(false)]
        public Vector3D vector { get; set; }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        [Browsable(false)]
        public List<AbstractAugmentation> Augmentions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTrackable"/> class.
        /// With no trackables associated.
        /// </summary>
        protected AbstractTrackable()
        {
            this.Augmentions = new List<AbstractAugmentation>();
            /* could it be here are some missing initialization? */
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <param name="visitor"></param>
        public abstract void Accept(AbstractProjectVisitor visitor);

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap getPreview();

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap getIcon();

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <returns></returns>
        public abstract List<AbstractProperty> getPropertyList();

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
        public AbstractAugmentation FindAugmention(IPreviewable a)
        {
            return this.Augmentions[this.Augmentions.IndexOf((AbstractAugmentation)a)];
        }

        /// <summary>
        /// Checks if the augmention is associated with this <see cref="AbstractTrackable"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is checked existence for</param>
        /// <returns>true, if its associated with this <see cref="AbstractTrackable"/>
        ///          false, else</returns>
        public bool existAugmention(IPreviewable a)
        {
            foreach (AbstractAugmentation aug in Augmentions)
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
