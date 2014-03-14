using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A <see cref="VideoAugmentation" /> is an Augmentation that contains a video file.
    /// It inherits from <see cref="Abstract2DAugmentation" />.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class VideoAugmentation : Abstract2DAugmentation
    {
        /// <summary>
        /// A cached preview to prevent access problems.
        /// </summary>
        private Bitmap cachePreview = null;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width, in mm.
        /// </value>

        [Browsable(false)]
        public new int Width
        {
            get { return base.Height; }
            set { base.Height = value; }
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
        public VideoAugmentation()
            : base()
        {
            resFilePath = null;
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
        public override Bitmap getPreview(string projectPath)
        {
            string absolutePath = Path.Combine(projectPath == null ? "" : projectPath, resFilePath);
            if (System.IO.File.Exists(absolutePath))
                return Controller.EditorController.ThumbCreator.CreateThumb(absolutePath);     
            else
                throw new ArgumentException("Projekt-Datei beschädigt");
            
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
            string dir = Path.GetDirectoryName(resFilePath);
            if (Directory.Exists(dir) && dir.Contains("Assets"))
                System.IO.File.Delete(resFilePath);
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
            return ObjectCopier.Clone<VideoAugmentation>(this);
        }

        /// <summary>
        /// This method is called by the previewController when a new instance of the element is added to the Scene. It sets "must-have" properties.
        /// </summary>
        /// <param name="ew">The ew.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public override bool initElement(EditorWindow ew)
        {
            if (ResFilePath == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\res\\testFiles\\augmentations";
                openFileDialog.Title = "Wählen sie ein Video";
                openFileDialog.Filter = "Video Files (*.3G2)|*.3g2";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ResFilePath = openFileDialog.FileName;
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
