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
    }
}
