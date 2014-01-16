using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    [Serializable]
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
            type = "IDMarker";
            idMarkerTrackingSensor = new IDMarkerSensor();
            sensorCosID = IDFactory.getSensorCosID(this);
        }
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
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
