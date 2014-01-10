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
    abstract class AbstractTrackable : ISerializable, IPreviewable
    {
        private String sensorID;
        private String sensorSubType;
        private String sensorType;
        private AbstractAugmentation[] augmentations;

        public abstract void accept(AbstractProjectVisitor visitor);

        public Bitmap getPreview()
        {
            throw new NotImplementedException();
        }

        public abstract List<AbstractProperty> getPropertyList();

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
			
    }
}
