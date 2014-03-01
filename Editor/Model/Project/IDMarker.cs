using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// <see cref="IDMarker"/> is a <see cref="AbstractMarker"/>
    /// adding an matrixID.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class IDMarker : Abstract2DTrackable
    {
        /// <summary>
        /// The matrix identifier, describes the Markers, which
        /// are deployed by the metaio SDK.
        /// They reach from 1 to 255.
        /// </summary>
        private int matrixID;
        /// <summary>
        /// Gets or sets the matrix identifier.
        /// </summary>
        /// <value>
        /// The matrix identifier.
        /// </value>
        [CategoryAttribute("General")]
        [ Description("The matrix identifies describes the marker, which are deployed by the metaio SDK. The range is from 1 to 512.")]
        public int MatrixID
        {
            get { return matrixID; }
            set 
            {
                if (value < 1)
                    matrixID = 1;
                else if (value > 512)
                    matrixID = 512;
                else
                    matrixID = value; 
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IDMarker"/> class.
        /// </summary>
        /// <param name="matrixID">The matrix identifier.</param>
        public IDMarker(int matrixID)
        {
            type = "IDMarker";
            similarityThreshold = 0.7;
            translationVector = new Vector3D(0, 0, 0);
            rotationVector = new Vector3Di(0, 0, 0, 0);
            Augmentations = new List<AbstractAugmentation>();
            this.matrixID = matrixID;
            this.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            fuser = new MarkerFuser();
            size = 60;
        }


        /// <summary>
        /// An method, to accept a <see cref="AbstractProjectVisitor" />
        /// and let the visitor visit the associated fuser.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        /// which is performed on this element</param>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmentation augmentation in Augmentations)
            {
                augmentation.Accept(visitor);
            }
            fuser.Accept(visitor);
        }

        /// <summary>
        /// Gets the preview.
        /// </summary>
        /// <returns>
        /// a representative Bitmap
        /// </returns>
        public override Bitmap getPreview()
        {
            StringBuilder markerName = new StringBuilder("IDMarker");
            if(matrixID < 100)
            {
                 markerName.Append("0");
            }
            if(matrixID < 10)
            {
                markerName.Append("0");
            }
            markerName.Append(matrixID);
            return (Bitmap)Properties.Resources.ResourceManager.GetObject(markerName.ToString());
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.ARRMarker_small_;
        }

        public override object Clone()
        {
            IDMarker n = ObjectCopier.Clone<IDMarker>(this);
            n.fuser = new MarkerFuser();
            n.sensorCosID = IDFactory.CreateNewSensorCosID(this);
            return n;
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
            if (ew.project.existTrackable(this))
            {
                MatrixID = ew.project.nextID();
            }
            else
            {
                ew.project.Sensor = new MarkerSensor();
            }
            return base.initElement(ew);
        }
    }
}
