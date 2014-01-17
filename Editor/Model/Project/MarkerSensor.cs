using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    public class MarkerSensor : AbstractSensor
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Specifies the <see cref="trackingQuality"/>. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
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
        ///                 than robust. </summary>
        protected TrackingQualities trackingQuality = TrackingQualities.robust;
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
        protected int thresholdOffset = 110;
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
        protected int numberOfSearchIterations = 3;
        public int NumberOfSearchIterations
        {
            get { return numberOfSearchIterations; }
            set { numberOfSearchIterations = value; }
        }

        public MarkerSensor()
        {
            Name = "Marker";
            sensorIDBase = SensorIDBases.MarkerTracking;
            SensorIDString = IDFactory.getSensorID(this);
            sensorType = SensorTypes.MarkerBasedSensorSource;
        }

        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
