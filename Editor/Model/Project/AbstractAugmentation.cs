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
using System.Collections;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractAugmentation : IPreviewable//, ISerializables
    {
        /// <summary>
        /// is used within AREL, to define which <see cref="AbstractAugmentation"/> is bound to 
        /// which <see cref="AbstractTrackable"/>s. it is set 
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
        /// describes if the <see cref="AbstractAugmentation"/>
        /// is seen using AREL, even if the associated <see cref="AbstractTrackable"/>
        /// is not recognized.
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// Get or set if the augmention is visible the whole time using AREl or not.
        /// </summary>
        [CategoryAttribute("General"), DefaultValueAttribute(false)]
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// A list of all customUserEvents the current <see cref="AbstractAugmentation"/> has.
        /// The user can write a javascript based code for the augmention.
        /// </summary>
        private ArrayList customUserEvent;
        /// <summary>
        /// Get the content of the customUserEvent. Each element in the List represents a line of the code.
        /// </summary>
        [CategoryAttribute("Expert")]
        [Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, 
            PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public ArrayList CustomUserEventList
        {
            get { return customUserEvent; }
        }

        /// <summary>
        /// Vector to describe the position on the PreviewPanel, and later
        /// to position it on the coordinatesystem given in AREL.
        /// </summar>
        private Vector3D translationVector;
        /// <summary>
        /// Get or set the position of the <see cref="AbstractAugmentation"/>.
        /// </summary>
        [CategoryAttribute("General")]
        public Vector3D TranslationVector
        {
            get { return translationVector; }
            set { translationVector = value; }
        }

        /// <summary>
        /// Vector, to describes the scaling of the Augmention in
        /// x, y and z direction. Is used in AREL.
        /// </summary>
        private Vector3D scalingVector;
        /// <summary>
        /// gets or sets the scaling which is applied to the original 
        /// <see cref="AbstractAugmentation"/>
        /// </summary>
        [CategoryAttribute("General")]
        public Vector3D ScalingVector
        {
            get { return scalingVector; }
            set { scalingVector = value; }
        }

        /// <summary>
        /// Vector, to describes the rotation of the Augmention in
        /// x, y and z direction. Is used in AREL.
        /// </summary>
        private Vector3Di rotationVector;
        /// <summary>
        /// gets or sets the Vector
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
        protected AbstractAugmentation()
        {
            coordinatesystemid = 0;
            isVisible = true;
            customUserEvent = new ArrayList();
            translationVector = new Vector3D(0, 0, 0);
            scalingVector = new Vector3D(0, 0, 0);
            rotationVector = new Vector3Di(0, 0, 0, 0);
            trackable = null;
        }

        /// <summary>   ToDo Summary is missing Body must still be implemented. </summary>
        ///
        /// <param name="coordSystemId">        Identifier for the coordinate system. </param>
        /// <param name="isVisible">            ToDo Summary is missing. </param>
        /// <param name="translationVector"> Vector to know the Position on the PreviewPanel.</summar> </param>
        /// <param name="scaling">              The scaling. </param>
        /// <param name="trackable">         The AbstractTrackable with which this AbstractAugmentation is
        ///     linked. It is visible in the same Scene as the trackable. </param>
        protected AbstractAugmentation(int coordSystemId, bool isVisible, 
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable)
        {
            coordinatesystemid = coordSystemId;
            this.isVisible = isVisible;
            customUserEvent = new ArrayList();
            this.translationVector = translationVector;
            scalingVector = scaling;
            this.trackable = trackable;
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

