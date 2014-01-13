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
        /// <summary>
        /// ToDo
        /// </summary>
        private int coordinatesystemid;

        /// <summary>
        /// A list of all customUserEvents the current selected Element has.
        /// </summary>
        public List<String> costumUserEvents { get; set; }

        /// <summary>
        /// ToDo
        /// </summary>
        private bool isVisible;

        /// <summary>
        /// Vector of Augmentation
        /// </summar>
        public Vector3D vector { get; set; }

        /// <summary>
        /// Source which is linked to Augmentation
        /// </summary>
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
