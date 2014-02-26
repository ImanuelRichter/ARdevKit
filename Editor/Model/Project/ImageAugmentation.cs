using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.ComponentModel;
using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System.IO;
using System.Windows.Forms;

namespace ARdevKit.Model.Project
{
    /// <summary>   
    /// An augmentation only described by an ImagePath.
    /// It is an <see cref="Abstract2DAugmentation"/>
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class ImageAugmentation : Abstract2DAugmentation
    {
        /// <summary>
        /// Full pathname of the image file.
        /// </summary>
        private string imagePath;

        //an instance of the preview to prevent access complications.
        private Bitmap cachePreview = null;

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
            set { imagePath = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width, in mm.
        /// </value>
        [Browsable(false)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height, in mm.
        /// </value>
        [Browsable(false)]
        public new int Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageAugmentation() : base()
        {
            imagePath = null;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAugmentation"/> class.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        public ImageAugmentation(string imagePath)
            : base()
        {
            this.imagePath = imagePath;
        }

        /// <summary>
        /// An overwriting method, to accept a <see cref="AbstractProjectVisitor" />
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        /// which is performed on this element</param>
        public override void Accept(AbstractProjectVisitor visitor)
        {
            base.Accept(visitor);
            visitor.Visit(this);
        }


        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative Bitmap
        /// </returns>
        /// <exception cref="FileNotFoundException">Thrown when the requested File is
        /// not found in <see cref="ImagePath" />.</exception>
        public override Bitmap getPreview()
        {
            if (cachePreview == null)
            {
                cachePreview = new Bitmap(ImagePath);
            }
            return cachePreview;
        }
                
        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        /// <exception cref="FileNotFoundException">If ImagePath is bad</exception>
        public override Bitmap getIcon()
        {
            return Properties.Resources.ImageAugmentation_small_; 
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clean up (remove created/copied files and directories). </summary>
        ///
        /// <remarks>   Imanuel, 31.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void CleanUp()
        {
            string dir = Path.GetDirectoryName(imagePath);
            if (Directory.Exists(dir) && dir.Contains("Assets"))
                System.IO.File.Delete(imagePath);
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
            return ObjectCopier.Clone<ImageAugmentation>(this);
        }

        public override bool initElement(EditorWindow ew)
        {
            if (ImagePath == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|PPM Files (*.ppm)|*.ppm|PGM Files (*.pgm)|*.pgm";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImagePath = openFileDialog.FileName;
                    return base.initElement(ew);
                }
                else
                {
                    return false;
                }
            }
            return base.initElement(ew);
        }
    }
}
