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
        private String sensorID;
        private String sensorSubType;
        private String sensorType;
        public Vector3D vector { get; set; }
        public List<AbstractAugmentation> augmentations { get; set; }

        public abstract void accept(AbstractProjectVisitor visitor);

        abstract public Bitmap getPreview();

        abstract public Bitmap getIcon();

        public abstract List<AbstractProperty> getPropertyList();

        public AbstractTrackable()
        {
            this.augmentations = new List<AbstractAugmentation>();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public AbstractAugmentation findAugmentation(IPreviewable a)
        {
            return this.augmentations[this.augmentations.IndexOf((AbstractAugmentation)a)];
        }

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
