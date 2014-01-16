using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    public class MarkerlessSensor : AbstractSensor
    {
        [Flags]
        public enum FeatureDescriptorAlignments { regular, upright, gravity, rectified };
        /// <summary>   The following feature descriptor types are available:
        ///				"regular", "upright", "gravity", "rectified".
        ///				- The "regular" feature descriptor type is the most
        ///				  general feature descriptor type and is used as
        ///				  default if the tag is not specified.
        ///				- The "upright" feature descriptor type assumes that
        ///				  the camera is not rotated with respect to the optical
        ///				  axis, i.e. is turned upside down, during the tracking
        ///				  process.
        ///				- The "gravity" feature descriptor type can only be
        ///				  used with devices with inertial sensors which
        ///				  measures gravity. It is used for localizing static
        ///				  objects that provide (close to) vertical surfaces,
        ///				  e.g. buildings or posters on a wall. The orientation
        ///				  of the features will then be aligned with gravity.
        ///				- The "rectified" feature descriptor type can only be
        ///				  used with devices with inertial sensors which
        ///				  measures gravity. It is used for planar objects on a
        ///				  horizontal surface, e.g. a magazine on a table.
        ///				  This will improve the result of the localization of
        ///				  planar objects under steep camera angles at the cost
        ///				  of a lower framerate during localization.
        ///				  This parameter is for expert usage only. In general it
        ///				  is advised to leave the value unchanged. </summary>
        protected FeatureDescriptorAlignments featureDescriptorAlignment = FeatureDescriptorAlignments.regular;
        public FeatureDescriptorAlignments FeatureDescriptorAlignment
        {
            get { return featureDescriptorAlignment; }
            set { featureDescriptorAlignment = value; }
        }

        /// <summary>   A restriction on the number of reference planar objects
        ///				to be localized per frame. Localization takes longer
        ///				than interframe tracking, and if the system tries to
        ///				localize too many objects at the same time, it might
        ///				cause a lower framerate. The default value for this is 5
        ///				and is used if the tag is not specified.
        ///				Another name that can be used for this parameter is
        ///				&lt;MultipleReferenceImagesFast&gt;. This name is however
        ///				deprecated and should not be used any more.
        ///				This parameter is for expert usage only. In general it
        ///				is advised to leave the value unchanged. </summary>
        protected int maxObjectsToDetectPerFrame = 5;
        public int MaxObjectsToDetectPerFrame
        {
            get { return maxObjectsToDetectPerFrame; }
            set { maxObjectsToDetectPerFrame = value; }
        }

        /// <summary>   The maximum number of objects that should be tracked in
        ///				parallel. Tracking many objects in parallel is quite
        ///				expensive and might lead to a lower framerate. As soon
        ///				as the maximum number of tracked objects is reached,
        ///				the system will no longer try to localize new objects.
        ///				The default value for this is 1 and is used if the tag
        ///				is not specified.
        ///				Another name that can be used for this parameter is
        ///				&lt;MaxNumCosesForInit&gt;. This name is however deprecated
        ///				and should not be used any more.
        ///				This parameter is for expert usage only. In general it
        ///				is advised to leave the value unchanged. </summary>
        protected int maxObjectsToTrackInParallel = 1;
        public int MaxObjectsToTrackInParallel
        {
            get { return maxObjectsToTrackInParallel; }
            set { maxObjectsToTrackInParallel = value; }
        }

        /// <summary>   Default similarity threshold for specifying whether
        ///				template tracking was successful or failed. The
        ///				tracking quality measure is defined between -1 and 1,
        ///				where 1 is the best	possible value. If the tracking
        ///				quality	is reported to be below the threshold, the
        ///				tracker will treat the corresponding frame as lost.
        ///				The default value for this is 0.7 and is used if the
        ///				tag is not specified. This setting can be overridden
        ///				for each "COS" if it is defined there.
        ///				This parameter is for expert usage only. In general it
        ///				is advised to leave the value unchanged. </summary>
        protected double similarityThreshold = 0.7;
        public double SimilarityThreshold
        {
            get { return similarityThreshold; }
            set { similarityThreshold = value; }
        }

        public MarkerlessSensor()
        {
            Name = "Markerless";
            SensorIDString = IDFactory.getSensorID(this);
            sensorType = SensorTypes.FeatureBasedSensorSource;
            sensorSubType = SensorSubTypes.Fast;
        }

        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
