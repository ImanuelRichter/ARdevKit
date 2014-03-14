using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// An Abstract2DTrackable is a two-dimensional trackable image, that can be tracked by the metaio SDK.
    /// </summary>
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
        [CategoryAttribute("Expert"), Description("Der Sensor cos Bezeichner, der im AREL benutzt wird um die verfolgten Daten zu definieren.")]
        public string SensorCosID
        {
            get { return sensorCosID; }
            set { sensorCosID = value; }
        }

        /// <summary>
        /// The width in mm.
        /// </summary>
        protected int widthMM;

        /// <summary>
        /// Gets or sets the width mm.
        /// </summary>
        /// <value>
        /// The width in mm.
        /// </value>
        [CategoryAttribute("General"), Description("Breite des realen Markers in mm.")]
        public int WidthMM
        {
            get { return widthMM; }
            set { widthMM = value;  }
        }

        /// <summary>
        /// The height in mm.
        /// </summary>
        protected int heightMM;

        /// <summary>
        /// Gets or sets the width mm.
        /// </summary>
        /// <value>
        /// The width in mm.
        /// </value>
        [CategoryAttribute("General"), Description("Höhe des realen Markers in mm.")]
        public int HeightMM
        {
            get { return heightMM; }
            set { heightMM = value; }
        }

        /// <summary>
        /// The size of the Marker in mm
        /// </summary>
        protected int size;
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public int Size
        {
            get { return (int) Math.Round(Math.Sqrt(widthMM * heightMM), 0); }
            set 
            {
                if (value < 1)
                {
                    widthMM = 1;
                    heightMM = 1;
                }
                else
                {
                    widthMM = value;
                    heightMM = value;
                }
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return sensorCosID;
        }
    }
}
