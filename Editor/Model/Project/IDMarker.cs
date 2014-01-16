using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    public class IDMarker : AbstractMarker
    {
        private int matrixID;
        public int MatrixID
        {
            get { return matrixID; }
            set { matrixID = value; }
        }

        private IDMarkerSensor idMarkerTrackingSensor;
        public IDMarkerSensor IdMarkerTrackingSensor
        {
            get { return idMarkerTrackingSensor; }
            set { idMarkerTrackingSensor = value; }
        }

        public IDMarker(int matrixID)
        {
            this.matrixID = matrixID;
            size = 60;
            type = "IDMarker";
            idMarkerTrackingSensor = new IDMarkerSensor();
            sensorCosID = IDFactory.getSensorCosID(this);
            Fuser = new MarkerlessFuser();
        }
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmention augmentation in Augmentions)
            {
                augmentation.Accept(visitor);
            }
            fuser.Accept(visitor);
        }

        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
        public override Bitmap getPreview()
        {
            return Properties.Resources.ARRMarker_normal_;
        }

        public override Bitmap getIcon()
        {
            return Properties.Resources.ARRMarker_small_;
        }
    }
}
