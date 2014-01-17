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
    [Serializable]
    public abstract class AbstractAugmentation : IPreviewable//, ISerializables
    {
        private int coordinatesystemid;
        public int Coordinatesystemid
        {
            get { return coordinatesystemid; }
            set { coordinatesystemid = value; }
        }

        /// <summary>
        /// A list of all customUserEvents the current selected Element has.
        /// </summary>
        private List<CustomUserEvent> customUserEvent;

        /// <summary>
        /// ToDo
        /// </summary>
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// Vector to know the Position on the PreviewPanel.
        /// </summar>
        private Vector3D translationVector;
        public Vector3D TranslationVector
        {
            get { return translationVector; }
            set { translationVector = value; }
        }

        private Vector3Di rotationVector;
        public Vector3Di RotationVector
        {
            get { return rotationVector; }
            set { rotationVector = value; }
        }

        private Vector3D scalingVector;
        public Vector3D ScalingVector
        {
            get { return scalingVector; }
            set { scalingVector = value; }
        }

        /// <summary>
        /// New Variable which is for link a Source with this Augmentation
        /// </summary>
        public AbstractSource source { get; set; }

        /// <summary>
        /// Gets the list of all customUserEvent of this augmentation. (Only readable)
        /// </summary>
        public List<CustomUserEvent> CustomUserEventList
        {
            get { return customUserEvent; }
        }

        /// <summary>
        /// The AbstractTrackable with which this AbstractAugmentation is linked.
        /// It is visible in the same Scene as the trackable.
        /// </summary>
        protected AbstractTrackable trackable;
        /// <summary>
        /// Gets or sets the trackable
        /// </summary>
        /// <value>
        /// The trackable.
        /// </value>
        public AbstractTrackable Trackable
        {
            get { return trackable; }
            set { trackable = value; }
        }

        public abstract void Accept(AbstractProjectVisitor visitor);

        abstract public Bitmap getPreview();

        abstract public Bitmap getIcon();

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
    }
}

