using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller.ProjectController;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An <see cref="AbstractSensor"/> has a <see cref="name"/>, a <see cref="sensorType"/>,
    ///             and can have a <see cref="sensorSubType"/>. Moreover it has
    ///             a <see cref="sensorIDBase"/> which is used to create the <see cref="sensorIDString"/>. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractSensor
    {
        /// <summary>   The name. </summary>
        private string name;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [CategoryAttribute("General")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Flags for specifying SensorTypes. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Flags]
        public enum SensorTypes { FeatureBasedSensorSource, MarkerBasedSensorSource };
        /// <summary>   Type of the sensor. </summary>
        protected SensorTypes sensorType;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the type of the sensor. </summary>
        ///
        /// <value> The type of the sensor. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [CategoryAttribute("General")]
        public SensorTypes SensorType
        {
            get { return sensorType; }
            set { sensorType = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Flags for specifying SensorSubTypes. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Flags]
        public enum SensorSubTypes { None, Fast, Robust };
        /// <summary>   SubType of the sensor. </summary>
        protected SensorSubTypes sensorSubType;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the SubType of the sensor. </summary>
        ///
        /// <value> The type of the sensor sub. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [CategoryAttribute("General")]
        public SensorSubTypes SensorSubType
        {
            get { return sensorSubType; }
            set { sensorSubType = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Flags for specifying SensorIDBases. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Flags]
        public enum SensorIDBases { FeatureTracking, MarkerTracking };
        /// <summary>   The sensor identifier base. </summary>
        protected SensorIDBases sensorIDBase;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sensor identifier base. </summary>
        ///
        /// <value> The sensor identifier base. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [CategoryAttribute("General")]
        public SensorIDBases SensorIDBase
        {
            get { return sensorIDBase; }
            set { sensorIDBase = value; }
        }

        /// <summary>   The sensor identifier string. </summary>
        protected string sensorIDString;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sensor identifier string. </summary>
        ///
        /// <value> The sensor identifier string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [CategoryAttribute("General")]
        public string SensorIDString
        {
            get { return sensorIDString; }
            set { sensorIDString = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        public AbstractSensor()
        {
            ; // initialization missing
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Accepts the given visitor. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="visitor">  The visitor. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void Accept(AbstractProjectVisitor visitor);
    }
}
