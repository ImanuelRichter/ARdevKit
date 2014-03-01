using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class Abstract2DTrackable : AbstractTrackable
    {

        /// <summary>
        /// The sensor cos identifier, used by AREL
        /// to specify the TrackingData
        /// </summary>
        protected string sensorCosID;
        /// <summary>
        /// Gets or sets the sensor cos identifier.
        /// </summary>
        [CategoryAttribute("Expert"), Description("The sensor cos identifier, used by AREL to specify the tracking data.")]
        public string SensorCosID
        {
            get { return sensorCosID; }
            set { sensorCosID = value; }
        }

        /// <summary>
        /// The size of the Marker in mm
        /// </summary>
        protected int size;
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        [CategoryAttribute("General"), Description("Size of the actual marker in mm")]
        public int Size
        {
            get { return size; }
            set 
            {
                if (value < 1)
                    size = 1;
                else
                    size = value; 
            }
        }

        /// <summary>
        /// Vector to describe the position on the PreviewPanel, and later
        /// to position it on the coordinatesystem given in AREL.
        /// </summary>
        protected Vector3D translationVector;
        /// <summary>
        /// Get or set the position of the <see cref="AbstractAugmentation"/>.
        /// </summary>
        [Browsable(false)]
        public Vector3D Translation
        {
            get { return translationVector; }
            set { translationVector = value; }
        }

        /// <summary>
        /// Vector, to describe the rotation of the <see cref="AbstractAugmentation"/> in
        /// x, y and z direction. w is used for TrackingFile Offset in AREL.
        /// </summary>
        protected Vector3Di rotationVector;
        /// <summary>
        /// gets or sets the Vector
        /// </summary>
        [Browsable(false)]
        [Description("Vector which describes the rotation of the augmentation.")]
        public Vector3Di Rotation
        {
            get { return rotationVector; }
            set { rotationVector = value; }
        }

        /// <summary>
        /// Describes how  different elements are
        /// combined and connected in AREL.
        /// </summary>
        protected MarkerFuser fuser;
        /// <summary>
        /// Gets or sets the fuser.
        /// Is not Browsable, therefore not editable in 
        /// the PropertyPanel
        /// </summary>
        /// <value>
        /// The fuser.
        /// </value>
        [Browsable(false)]
        public MarkerFuser Fuser
        {
            get { return fuser; }
            set { fuser = value; }
        }

        /**
         * <summary>    Gibt eine Zeichenfolge zurück, die das aktuelle Objekt darstellt. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <returns>    Eine Zeichenfolge, die das aktuelle Objekt darstellt. </returns>
         */
        public override string ToString()
        {
            return sensorCosID;
        }
    }
}
