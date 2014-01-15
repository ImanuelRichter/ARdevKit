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
        public AbstractAugmentation[] augmentations { get; set; }

        public abstract void accept(AbstractProjectVisitor visitor);

        abstract public Bitmap getPreview();

        abstract public Bitmap getIcon();

        public abstract List<AbstractProperty> getPropertyList();

        public AbstractTrackable()
        {
            this.augmentations = new AbstractAugmentation[3];
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public bool isAugmented()
        {
            for (int i = 0; i < augmentations.Length; i++)
            {
                if (augmentations[i] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public void addAugmentation(AbstractAugmentation augmentation)
        {
            for (int i = 0; i < augmentations.Length; i++)
            {
                if (augmentations[i] == null)
                {
                    augmentations[i] = augmentation;
                    return;
                }
            }
            throw new NotSupportedException("There are already 3 augmentations connected to this trackable.");
        }

        public void removeAugmentation(AbstractAugmentation augmentation)
        {
            for (int i = 0; i < augmentations.Length; i++)
            {
                if (augmentations[i] == augmentation)
                {
                    augmentations[i] = null;
                    return;
                }
            }
            throw new NotSupportedException("The augmentation which should be removed, could not be found.");
        }

        public bool isAugmentionFull()
        {
            if (augmentations.Length < 3)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public int findAugmentation(AbstractAugmentation a)
        {
            for (int i = 0; i < augmentations.Length; i++)
            {
                if (augmentations[i] == a)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
