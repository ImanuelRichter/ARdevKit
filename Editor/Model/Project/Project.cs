using ARdevKit.Controller.ProjectController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public class Project// : ISerializable
    {
        [Flags]
        public enum SensorTypes { FeatureBasedSensorSource };
        private SensorTypes sensorType = SensorTypes.FeatureBasedSensorSource;
        public SensorTypes SensorType
        {
            get { return sensorType; }
            set { sensorType = value; }
        }

        [Flags]
        public enum SensorSubTypes { Fast };
        private SensorSubTypes sensorSubType = SensorSubTypes.Fast;
        public SensorSubTypes SensorSubType
        {
            get { return sensorSubType; }
            set { sensorSubType = value; }
        }

        [Flags]
        public enum SensorIDs { FeatureTracking };
        private SensorIDs sensorID = SensorIDs.FeatureTracking;
        public SensorIDs SensorID
        {
            get { return sensorID; }
            set { sensorID = value; }
        }

        [Flags]
        public enum FeatureDescriptorAlignments { regular };
        private FeatureDescriptorAlignments featureDescriptorAlignment = FeatureDescriptorAlignments.regular;
        public FeatureDescriptorAlignments FeatureDescriptorAlignment
        {
            get { return featureDescriptorAlignment; }
            set { featureDescriptorAlignment = value; }
        }

        private int maxObjectsToDetectPerFrame = 5;
        public int MaxObjectsToDetectPerFrame
        {
            get { return maxObjectsToDetectPerFrame; }
            set { maxObjectsToDetectPerFrame = value; }
        }

        private int maxObjectsToTrackInParallel = 1;
        public int MaxObjectsToTrackInParallel
        {
            get { return maxObjectsToTrackInParallel; }
            set { maxObjectsToTrackInParallel = value; }
        }

        private double similarityThreshold = 0.7;
        public double SimilarityThreshold
        {
            get { return similarityThreshold; }
            set { similarityThreshold = value; }
        }

        private String name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<AbstractSource> sources;
        public List<AbstractSource> Sources
        {
            get { return sources; }
            set { sources = value; }
        }

        private List<AbstractTrackable> trackables;
        public List<AbstractTrackable> Trackables
        {
            get { return trackables; }
            set { trackables = value; }
        }

        public Project(string name)
        {
            this.name = name;
            trackables = new List<AbstractTrackable>();
            sources = new List<AbstractSource>();
        }

        public void Accept(AbstractProjectVisitor visitor)
        {
            visitor.visit(this);
            /*foreach (AbstractTrackable t in Trackables)
            {
                t.Accept(visitor);
            }*/
        }

/*        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }*/
    }

    
}
