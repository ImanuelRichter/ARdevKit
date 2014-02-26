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
using System.IO;
using System.Runtime.CompilerServices;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// describes an <see cref="AbstractAugmentation"/>, which is bound
    /// to a certain <see cref="AbstractTrackable"/>.
    /// is <see cref="IPreviewable"/> 
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractAugmentation : IPreviewable
    {
        /// <summary>   The identifier. </summary>
        protected string id;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General"), ReadOnly(true)]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// describes if the <see cref="AbstractAugmentation"/>
        /// is seen using AREL, even if the associated <see cref="AbstractTrackable"/>
        /// is not recognized.
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// Get or set if the <see cref="AbstractAugmentation"/> is 
        /// visible the whole time using AREL or not.
        /// </summary>
        [CategoryAttribute("General"), DefaultValueAttribute(true)]
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }


        /// <summary>
        /// The customUserEvents contains a path to a file, which has
        /// all user-generated events of this augmentation.
        /// </summary>
        private CustomUserEvent cue;
        /// <summary>
        /// Get the CustomUserEvent.
        /// </summary>
        [Browsable(false)]
        public CustomUserEvent CustomUserEventReference
        {
            get 
            {
                if (cue == null)
                    cue = new CustomUserEvent(id);
                return cue; 
            }
            set { cue = value; }
        }


        /// <summary>
        /// Vector to describe the position on the PreviewPanel, and later
        /// to position it on the coordinatesystem given in AREL.
        /// </summar>
        private Vector3D translationVector;
        /// <summary>
        /// Get or set the position of the <see cref="AbstractAugmentation"/>.
        /// </summary>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public Vector3D Translation
        {
            get { return translationVector; }
            set { translationVector = value; }
        }

        /// <summary>
        /// Vector, to describes the scaling of the <see cref="AbstractAugmentation"/>
        /// in x, y and z direction. Is used in AREL.
        /// </summary>
        private Vector3D scalingVector;
        /// <summary>
        /// gets or sets the scaling which is applied to the original 
        /// <see cref="AbstractAugmentation"/>
        /// </summary>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public Vector3D Scaling
        {
            get { return scalingVector; }
            set 
            { 
                if (value.X <= (double)0)
                    value.X = 0.01;
                if (value.Y <= (double)0)
                    value.Y = 0.01;
                if (value.Z <= (double)0)
                    value.Z = 0.01;
                
                scalingVector = value; 
            }
        }

        /// <summary>
        /// Vector, to describes the rotation (euler) of the <see cref="AbstractAugmentation"/> in
        /// x, y and z direction.
        /// </summary>
        private Vector3D rotationVector;
        /// <summary>
        /// gets or sets the Vector
        /// </summary>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public Vector3D Rotation
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
        /// Get or set a trackable to the augmentation.
        /// </summary>
        [Browsable(false)]
        public AbstractTrackable Trackable
        {
            get { return trackable; }
            set { trackable = value; }
        }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractAugmentation"/> class,
        /// but can be used in inheriting classes.
        /// Using standard values, such as emptyLists, vectors with 0 as coordinate and null.
        /// </summary>
        protected AbstractAugmentation()
        {
            isVisible = true;
            translationVector = new Vector3D(0, 0, 0);
            scalingVector = new Vector3D(0, 0, 0);
            rotationVector = new Vector3D(0, 0, 0);
            trackable = null;

        }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractAugmentation"/> class,
        /// but can be used in inheriting classes.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible] using AREL.</param>
        /// <param name="translationVector">The translation vector.</param>
        /// <param name="scaling">The scaling.</param>
        /// <param name="trackable">The trackable.</param>
        protected AbstractAugmentation(bool isVisible,
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable)
        {
            this.isVisible = isVisible;
            this.translationVector = translationVector;
            scalingVector = scaling;
            this.trackable = trackable;
        }

        /// <summary>
        /// An abstract method, to accept a <see cref="AbstractProjectVisitor"/>
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this element</param>
        public virtual void Accept(AbstractProjectVisitor visitor)
        {
            if (cue != null && !Equals(cue.FilePath, "NULL"))
                cue.Accept(visitor);
        }

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative Bitmap</returns>
        public abstract Bitmap getPreview();

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative iconized Bitmap</returns>
        public abstract Bitmap getIcon();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clean up (remove created/copied files and directories). </summary>
        ///
        /// <remarks>   Imanuel, 31.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void CleanUp();

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 22.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public abstract object Clone();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool initElement(EditorWindow ew)
        {
            int count = 0;
            bool found = true;
            String newID = "";
            while (found)
            {
                found = false;
                count++;
                foreach (AbstractTrackable t in ew.project.Trackables)
                {
                    newID = this.GetType().Name + count;
                    //make first letter lowercase
                    newID = newID[0].ToString().ToLower() + newID.Substring(1);
                    foreach (AbstractAugmentation a in t.Augmentations)
                    {
                        if (this.GetType().Equals(a.GetType()))
                        {
                            if (a.ID == newID)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
            id = newID;
            return true;
        }

        /**
         * <summary>    Gibt eine Zeichenfolge zurück, die das aktuelle Objekt darstellt. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <returns>    Eine Zeichenfolge, die das aktuelle Objekt darstellt. </returns>
         */
        public override string ToString()
        {
            return id;
        }
    }
}

