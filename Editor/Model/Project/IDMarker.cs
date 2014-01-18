using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class IDMarker : AbstractMarker
    {
        /// <summary>
        /// The matrix identifier
        /// </summary>
        private int matrixID;
        /// <summary>
        /// Gets or sets the matrix identifier.
        /// </summary>
        /// <value>
        /// The matrix identifier.
        /// </value>
        public int MatrixID
        {
            get { return matrixID; }
            set { matrixID = value; }
        }

        private MarkerSensor idMarkerTrackingSensor;
        public MarkerSensor IdMarkerTrackingSensor
        {
            get { return idMarkerTrackingSensor; }
            set { idMarkerTrackingSensor = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IDMarker"/> class.
        /// </summary>
        /// <param name="matrixID">The matrix identifier.</param>
        public IDMarker(int matrixID)
        {
            this.matrixID = matrixID;
            size = 60;
            type = "IDMarker";
            idMarkerTrackingSensor = new MarkerSensor();
            sensorCosID = IDFactory.createNewSensorCosID(this);
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

        /// <summary>
        /// Gets the property list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the preview.
        /// </summary>
        /// <returns></returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.ARRMarker_normal_;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns></returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.ARRMarker_small_;
        }
    }
}
