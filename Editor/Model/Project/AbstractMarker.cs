using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public abstract class AbstractMarker : AbstractTrackable
    {
        private String sensorCosID;
        private int size;

        private AbstractSensor sensor;
        public AbstractSensor Sensor
        {
            get { return sensor; }
            set { sensor = value; }
        }

        private Fuser markerFuser;
        public Fuser MarkerFuser
        {
            get { return markerFuser; }
            set { markerFuser = value; }
        }
    }
}
