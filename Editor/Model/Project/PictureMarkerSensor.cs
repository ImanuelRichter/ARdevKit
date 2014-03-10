using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A <see cref="PictureMarkerSensor"/> is a <see cref="AbstractSensor"/> used for <see cref="PictureMarker"/>.
    /// Contains the values, which are used from the MetaioSDK to populate the TrackingData.XML.
    /// They describe which Marker should be tracked, and at what quality and speed.
    /// </summary>
    [Serializable]
    public class PictureMarkerSensor : AbstractSensor
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Specifies the <see cref="trackingQuality" />.
        /// </summary>
        /// <remarks>
        /// Imanuel, 15.01.2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Flags]
        public enum TrackingQualities { robust, fast };
        /// <summary>   Strategy which is used for the marker detection.
        ///             There are two types available:
        ///             -   "robust" to use a robust approach to detect the
        ///                 markers, which usually gives the best results,
        ///                 but consumes more computational time, i.e. is 
        ///                 slower.
        ///             -   "fast" to use a more simple approach to detect
        ///                 the markers, which is less precise, but faster
        ///                 than robust. 
        /// </summary>
        protected TrackingQualities trackingQuality;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the tracking quality. </summary>
        ///
        /// <value> The tracking quality. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public TrackingQualities TrackingQuality
        {
            get { return trackingQuality; }
            set { trackingQuality = value; }
        }

        /// <summary>   The threshold which is used to binarize the camera
        ///             image. Binarizing is the process where each pixel
        ///             is converted to a grayscale value (between 0 and
        ///             255) and then is set to 0 when the value is below
        ///             the threshold and to 1 when the value is above.
        ///             This helps to clearly identify the marker and is
        ///             therefore important for the detection process. When
        ///             the tracking quality is set to "fast", then this
        ///             value is fixed and will not change during the
        ///             tracking process. When the tracking quality is set
        ///             to "robust", then the value is only the starting
        ///             value in the very first frame after loading the
        ///			    tracking.xml. Detecting markers using a fixed
        ///				threshold can lead to failure. The value range for
        ///				the threshold is between 0 and 255. </summary>
        protected int thresholdOffset;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the threshold offset. </summary>
        ///
        /// <value> The threshold offset. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public int ThresholdOffset
        {
            get { return thresholdOffset; }
            set { thresholdOffset = value; }
        }

        /// <summary>   Number of search iterations which controls the
        ///             number of attempts to find a marker with a new
        ///             ThresholdOffset. This parameter matters when "robust"
        ///				is set as "TrackingQuality", but is ignored for
        ///				"fast". The ThresholdOffset is adapted when no
        ///				marker was detected.
        ///             With a high number, the marker tracker is more
        ///             likely to detect a marker, but it also needs more
        ///             computational time, i.e. is slower. </summary>
        protected int numberOfSearchIterations;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the number of search iterations. </summary>
        ///
        /// <value> The total number of search iterations. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public int NumberOfSearchIterations
        {
            get { return numberOfSearchIterations; }
            set { numberOfSearchIterations = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public PictureMarkerSensor() : base()
        {
            Name = "PictureMarker";
            trackingQuality = TrackingQualities.robust;
            thresholdOffset = 128;
            numberOfSearchIterations = 3;
            sensorIDBase = SensorIDBases.MarkerTracking;
            SensorIDString = IDFactory.CreateNewSensorID(this);
            sensorType = SensorTypes.MarkerBasedSensorSource;
        }

        /// <summary>
        /// Accepts the given visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
