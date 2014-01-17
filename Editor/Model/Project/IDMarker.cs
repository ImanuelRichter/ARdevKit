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

        /// <summary>
        /// The identifier marker tracking sensor
        /// </summary>
        private IDMarkerSensor idMarkerTrackingSensor;
        /// <summary>
        /// Gets or sets the identifier marker tracking sensor.
        /// </summary>
        /// <value>
        /// The identifier marker tracking sensor.
        /// </value>
        public IDMarkerSensor IdMarkerTrackingSensor
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
            type = "IDMarker";
            idMarkerTrackingSensor = new IDMarkerSensor();
            sensorCosID = IDFactory.getSensorCosID(this);
        }
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
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
