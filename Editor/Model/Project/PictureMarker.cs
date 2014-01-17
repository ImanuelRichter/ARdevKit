using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using ARdevKit.Controller.ProjectController;
using ARdevKit.View;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class PictureMarker : AbstractMarker
    {
        /// <summary>   Full pathname of the image file. </summary>
        private string imagePath;
        /// <summary>   Gets or sets the full pathname of the image file. </summary>
        ///
        /// <value> The full pathname of the image file. </value>
        [CategoryAttribute("General")]
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        /// <summary>   Name of the image. </summary>
        private string imageName;
        /// <summary>   Gets or sets the name of the image. </summary>
        ///
        /// <value> The name of the image. </value>
        [CategoryAttribute("General")]
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        /// <summary>   The similarity threshhold. </summary>
        private double similarityThreshhold = 0.7;
        /// <summary>   Gets or sets the similarity threshhold. </summary>
        ///
        /// <value> The similarity threshhold. </value>
        [CategoryAttribute("General")]
        public double SimilarityThreshhold
        {
            get { return similarityThreshhold; }
            set { similarityThreshhold = value; }
        }

        /// <summary>   The picture marker tracking sensor. </summary>
        private MarkerlessSensor pictureMarkerTrackingSensor;
        /// <summary>   Gets or sets the picture marker tracking sensor. </summary>
        ///
        /// <value> The picture marker tracking sensor. </value>
        [CategoryAttribute("General")]
        public MarkerlessSensor PictureMarkerTrackingSensor
        {
            get { return pictureMarkerTrackingSensor; }
            set { pictureMarkerTrackingSensor = value; }
        }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="imagePath">    Full pathname of the image file. </param>
        public PictureMarker(string imagePath) : base() // maye needs to redo because of base() ?
        {
            this.imagePath = imagePath;
            imageName = Path.GetFileName(imagePath);
            type = "PictureMarker";
            Fuser = new MarkerlessFuser();
            sensorCosID = IDFactory.createNewSensorCosID(this);
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <param name="visitor">  . </param>
        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmention augmentation in Augmentions)
            {
                augmentation.Accept(visitor);
            }
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The property list. </returns>
        public override List<AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <returns>   The preview. </returns>
        public override System.Drawing.Bitmap getPreview()
        {
           return Properties.Resources.ARMarker_normal_;
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <returns>   The icon. </returns>
        public override System.Drawing.Bitmap getIcon()
        {
            return Properties.Resources.ARMarker_small_;
        }
    }
}
