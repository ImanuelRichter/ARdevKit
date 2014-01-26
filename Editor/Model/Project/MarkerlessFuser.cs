using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The <see cref="MarkerlessFuser"/> is a <see cref="MarkerFuser"/> that additionally
    ///             has, <see cref="gravityAssistance"/>, <see cref="gammaTranslation"/>,
    ///             <see cref="gammaRotation"/> and <see cref="continueLostTrackingWithOrientationSensor"/> value. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class MarkerlessFuser : MarkerFuser
    {
        /// <summary>
        /// The gravity assistance.
        /// </summary>
        private string gravityAssistance;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the gravity assistance. </summary>
        ///
        /// <value> The gravity assistance. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string GravityAssistance
        {
            get { return gravityAssistance; }
            set { gravityAssistance = value; }
        }

        /// <summary>   The gamma translation. </summary>
        private double gammaTranslation;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the gamma translation. </summary>
        ///
        /// <value> The gamma translation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public double GammaTranslation
        {
            get { return gammaTranslation; }
            set { gammaTranslation = value; }
        }

        /// <summary>
        /// The gamma rotation.
        /// </summary>
        private double gammaRotation;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the gamma rotation. </summary>
        ///
        /// <value> The gamma rotation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public double GammaRotation
        {
            get { return gammaRotation; }
            set { gammaRotation = value; }
        }

        /// <summary>
        /// true to continue lost tracking with orientation sensor.
        /// </summary>
        private bool continueLostTrackingWithOrientationSensor;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets a value indicating whether the tracking should be continued with orientation
        /// sensor.
        /// </summary>
        ///
        /// <value> true if continue lost tracking with orientation sensor, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool ContinueLostTrackingWithOrientationSensor
        {
            get { return continueLostTrackingWithOrientationSensor; }
            set { continueLostTrackingWithOrientationSensor = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerlessFuser"/> class.
        /// </summary>
        public MarkerlessFuser() : base()
        {
            gravityAssistance = "";
            gammaTranslation = 0.8;
            gammaRotation = 0.5;
            continueLostTrackingWithOrientationSensor = false;
        }

        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
