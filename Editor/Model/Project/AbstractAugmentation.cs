using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    abstract class AbstractAugmentation : ISerializable, IPreviewable
    {
        private int coordinatesystemid;
        private List<String> costumUserEvents;
        private bool isVisible;
        private Vector3D[] vectors;
        public AbstractSource source { get; set; }

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
