using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An <see cref="IDFactory"/> produces the ids for <see cref="AbstractSensor"/>s
    ///             and <see cref="AbstractMarker"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class IDFactory
    {
        /// <summary>   The sensorID counter. </summary>
        private static int sensorIDcounter = 1;
        /// <summary>   The sensorCosID counter. </summary>
        private static int sensorCosIDcounter = 0;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new sensorIDstring. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="sensor">   The sensor. </param>
        ///
        /// <returns>   The new new sensorIDstring. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string CreateNewSensorID(AbstractSensor sensor)
        {
            return sensor.SensorIDBase.ToString() + sensorIDcounter++;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new sensorCosIDstring. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="trackable">   The marker. </param>
        ///
        /// <returns>   The new new sensorCosIDstring. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string CreateNewSensorCosID(AbstractTrackable trackable)
        {
            return trackable.Type + sensorCosIDcounter++;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            sensorIDcounter = 1;
            sensorCosIDcounter = 1;
        }
    }
}
