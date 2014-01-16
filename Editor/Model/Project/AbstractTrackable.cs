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
    public abstract class AbstractTrackable : ISerializable, IPreviewable
    {
        protected MarkerFuser fuser;
        public MarkerFuser Fuser
        {
            get { return fuser; }
            set { fuser = value; }
        }

        protected string sensorCosID;
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
        public List<AbstractAugmention> augmentations { get; set; }

        public abstract void Accept(AbstractProjectVisitor visitor);

        abstract public Bitmap getPreview();

        abstract public Bitmap getIcon();

        public abstract List<AbstractProperty> getPropertyList();

        public AbstractTrackable()
        {
            this.augmentations = new List<AbstractAugmention>();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public AbstractAugmention findAugmentation(IPreviewable a)
        {
            return this.augmentations[this.augmentations.IndexOf((AbstractAugmention)a)];
        }

        public bool existAugmentation(IPreviewable a)
        {
            foreach (AbstractAugmention aug in augmentations)
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
