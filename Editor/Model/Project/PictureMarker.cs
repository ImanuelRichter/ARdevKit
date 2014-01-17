using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public class PictureMarker : AbstractMarker
    {
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        private double similarityThreshhold = 0.7;
        public double SimilarityThreshhold
        {
            get { return similarityThreshhold; }
            set { similarityThreshhold = value; }
        }

        private MarkerlessSensor pictureMarkerTrackingSensor;
        public MarkerlessSensor PictureMarkerTrackingSensor
        {
            get { return pictureMarkerTrackingSensor; }
            set { pictureMarkerTrackingSensor = value; }
        }

        public PictureMarker(string imagePath)
        {
            this.imagePath = imagePath;
            imageName = Path.GetFileName(imagePath);
            type = "PictureMarker";
            Fuser = new MarkerlessFuser();
            sensorCosID = IDFactory.createNewSensorCosID(this);
        }

        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmention augmentation in Augmentions)
            {
                augmentation.Accept(visitor);
            }
        }

        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Bitmap getPreview()
        {
           return Properties.Resources.ARMarker_normal_;
        }

        public override System.Drawing.Bitmap getIcon()
        {
            return Properties.Resources.ARMarker_small_;
        }
    }
}
