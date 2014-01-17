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

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractAugmention : IPreviewable//, ISerializables
    {
        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        private int coordinatesystemid;
        /// <summary>
        /// Get or set the CoordinateSystemId.
        /// </summary>
        [CategoryAttribute("General")]
        public int Coordinatesystemid
        {
            get { return coordinatesystemid; }
            set { coordinatesystemid = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// Get or set if the augmention is visible or not.
        /// </summary>
        [CategoryAttribute("General"), DefaultValueAttribute(true)]
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// A list of all customUserEvents the current selected Element has.
        /// </summary>
        private List<CustomUserEvent> customUserEvent;
        /// <summary>
        /// Get a List with all custom user-generated code.
        /// </summary>
        [CategoryAttribute("Expert")]
        public List<CustomUserEvent> CustomUserEventList
        {
            get { return customUserEvent; }
        }

        /// <summary>
        /// Vector to know the Position on the PreviewPanel.
        /// </summar>
        private Vector3D translationVector;
        /// <summary>
        /// Get or set the position of the augmention.
        /// </summary>
        [CategoryAttribute("General")]
        public Vector3D TranslationVector
        {
            get { return translationVector; }
            set { translationVector = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        private Vector3D scalingVector;
        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        [CategoryAttribute("General")]
        public Vector3D ScalingVector
        {
            get { return scalingVector; }
            set { scalingVector = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        private Vector3Di rotationVector;
        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        [CategoryAttribute("General")]
        public Vector3Di RotationVector
        {
            get { return rotationVector; }
            set { rotationVector = value; }
        }

        /// <summary>
        /// The AbstractTrackable with which this AbstractAugmentation is linked.
        /// It is visible in the same Scene as the trackable.
        /// </summary>
        protected AbstractTrackable trackable;
        /// <summary>
        /// Get or set a trackable to the augmention.
        /// </summary>
        [Browsable(false)]
        public AbstractTrackable Trackable
        {
            get { return trackable; }
            set { trackable = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// Body must still be implemented
        /// </summary>
        protected AbstractAugmention()
        {
            // todo!
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
    }
}

