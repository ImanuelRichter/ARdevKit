using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    public class MarkerlessFuser : MarkerFuser
    {
        private string gravityAssistance = "";
        public string GravityAssistance
        {
            get { return gravityAssistance; }
            set { gravityAssistance = value; }
        }

        private double gammaTranslation = 0.8;
        public double GammaTranslation
        {
            get { return gammaTranslation; }
            set { gammaTranslation = value; }
        }

        private double gammaRotation = 0.5;
        public double GammaRotation
        {
            get { return gammaRotation; }
            set { gammaRotation = value; }
        }

        private bool continueLostTrackingWithOrientationSensor = false;
        public bool ContinueLostTrackingWithOrientationSensor
        {
            get { return continueLostTrackingWithOrientationSensor; }
            set { continueLostTrackingWithOrientationSensor = value; }
        }
    }
}
