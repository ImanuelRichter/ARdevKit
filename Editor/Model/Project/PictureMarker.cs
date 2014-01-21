using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Describes a Marker, which is very flexible, because it is also
    /// a Picture. It is an <see cref="AbstractMarker"/>
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class PictureMarker : AbstractMarker
    {
        /// <summary>
        /// Describes how  different elements are
        /// combined and connected in AREL.
        /// </summary>
        protected MarkerFuser fuser;
        /// <summary>
        /// Gets or sets the fuser.
        /// Is not Browsable, therefore not editable in 
        /// the PropertyPanel
        /// </summary>
        /// <value>
        /// The fuser.
        /// </value>
        public MarkerFuser Fuser
        {
            get { return fuser; }
            set { fuser = value; }
        }

        /// <summary>
        /// Full pathname of the image file.
        /// </summary>
        private string imagePath;
        /// <summary>
        /// Gets or sets the full pathname of the image file.
        /// </summary>
        /// <value>
        /// The full pathname of the image file.
        /// </value>
        [CategoryAttribute("General"), EditorAttribute(typeof(FileSelectorTypeEditor), 
            typeof(System.Drawing.Design.UITypeEditor))]
        public string ImagePath
        {
            get { return imagePath; }
            set 
            { 
                imagePath = value;
                imageName = Path.GetFileNameWithoutExtension(imagePath);
            }
        }

        /// <summary>
        /// Name of the image.
        /// </summary>
        private string imageName;
        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        [CategoryAttribute("General"), ReadOnly(true)]
        public string ImageName
        {
            get { return imageName; }
            //set { imageName = value; }
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PictureMarker()
            : base("PictureMarker", 0)
        {
            imagePath = null;
            imageName = "";
            this.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            fuser = new MarkerFuser();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="imagePath">Full pathname of the image file.</param>
        public PictureMarker(string imagePath)
            : base("PictureMarker", new Bitmap(imagePath).Height * new Bitmap(imagePath).Width)
        {
            this.imagePath = imagePath;
            this.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            imageName = Path.GetFileName(imagePath);
            fuser = new MarkerFuser();
        }

        /// <summary>
        /// An overwriting method, to accept a <see cref="AbstractProjectVisitor" />
        /// which must be implemented according to the visitor design pattern.
        /// It lets the visitor visit every augmentation associated with it.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        /// which is performed on this element</param>
        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmentation augmentation in Augmentations)
            {
                augmentation.Accept(visitor);
            }
            fuser.Accept(visitor);
        }

        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative Bitmap
        /// </returns>
        /// <exception cref="FileNotFoundException">If ImagePath is
        ///     not correct.</exception>
        public override Bitmap getPreview()
        {
           return new Bitmap(ImagePath);
        }


        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override System.Drawing.Bitmap getIcon()
        {
            return Properties.Resources.ARMarker_small_;
        }

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 22.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            PictureMarker n = new PictureMarker();
            n.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            return n;
        }
    }
}
