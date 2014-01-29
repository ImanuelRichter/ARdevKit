﻿using ARdevKit.View;
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
    /// Describes a Marker, which is very flexible, because it is also
    /// a Picture. It is an <see cref="AbstractMarker"/>
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class ImageTrackable : Abstract2DTrackable
    {
        private MarkerlessFuser fuser;
        public MarkerlessFuser Fuser
        {
            get { return fuser; }
            set { fuser = value; }
        }

        /// <summary>
        /// Full pathname of the image file.
        /// </summary>
        protected string imagePath;
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
        protected string imageName;
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
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ImageTrackable()
        {
            type = "Markerless";
            similarityThreshold = 0.7;
            vector = new Vector3D(0, 0, 0);
            translationVector = new Vector3D(0, 0, 0);
            rotationVector = new Vector3Di(0, 0, 0, 0);
            Augmentations = new List<AbstractAugmentation>();
            this.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            fuser = new MarkerlessFuser();
            imagePath = null;
            imageName = "";
            size = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="imagePath">Full pathname of the image file.</param>
        public ImageTrackable(string imagePath) : this()
        {
            size = new Bitmap(imagePath).Height * new Bitmap(imagePath).Width;
            this.imagePath = imagePath;
            imageName = Path.GetFileName(imagePath);
        }

        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmentation augmentation in Augmentations)
            {
                augmentation.Accept(visitor);
            }
            Fuser.Accept(visitor);
        }

        public override System.Drawing.Bitmap getPreview()
        {
            return new Bitmap(ImagePath);
        }

        public override System.Drawing.Bitmap getIcon()
        {
            return Properties.Resources.ARMarker_small_;
        }

        public override object Clone()
        {
            ImageTrackable n = ObjectCopier.Clone<ImageTrackable>(this);
            n.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            return n;
        }

        public override bool initElement(EditorWindow ew)
        {
            if (base.initElement(ew))
            {
                bool isInitOk = true;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|PPM Files (*.ppm)|*.ppm|PGM Files (*.pgm)|*.pgm";
                isInitOk = openFileDialog.ShowDialog() == DialogResult.OK;
                if (isInitOk)
                {
                    string path = openFileDialog.FileName;
                    ImagePath = path;
                }

                if (!ew.project.existTrackable(this))
                {
                    ew.project.Sensor = new MarkerlessSensor();
                }
                return isInitOk;
            }
            else
            {
                return false;
            }
        }
    }
}
