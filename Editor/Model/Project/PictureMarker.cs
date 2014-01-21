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
            sensorCosID = IDFactory.createNewSensorCosID(this);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="imagePath">Full pathname of the image file.</param>
        public PictureMarker(string imagePath)
            : base("PictureMarker", new Bitmap(imagePath).Height * new Bitmap(imagePath).Width)
        {
            this.imagePath = imagePath;
            imageName = Path.GetFileName(imagePath);
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
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a new MarkerFuser. </summary>
        ///
        /// <remarks>   Imanuel, 20.01.2014. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <returns>   The fuser. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override MarkerFuser getFuser()
        {
            return new MarkerFuser();
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
    }
}
