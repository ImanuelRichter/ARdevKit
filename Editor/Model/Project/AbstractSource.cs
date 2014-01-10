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
    abstract class AbstractSource : ISerializable, IPreviewable
    {
        private Bitmap icon;
        private String sourceID;
        private List<AbstractDynamic2DAugmentation> augmentions;


        abstract public void accept(AbstractProjectVisitor visitor);

        public Bitmap getPreview()
        {
            return icon;
        }

        public abstract List<AbstractProperty> getPropertyList();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
