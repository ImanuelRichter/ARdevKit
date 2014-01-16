using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public class Fuser
    {
        public enum FuserTypes { SmoothingFuser, BestQualityFuser };
        private FuserTypes fuserType;
        public FuserTypes FuserType
        {
            get { return fuserType; }
            set { fuserType = value; }
        }

        private int keepPoseForNumberOfFrames = 2;
        public int KeepPoseForNumberOfFrames
        {
            get { return keepPoseForNumberOfFrames; }
            set { keepPoseForNumberOfFrames = value; }
        }

        private string gravityAssistance = "";
        public string GravityAssistance
        {
            get { return gravityAssistance; }
            set { gravityAssistance = value; }
        }

        private double alphaTranslation = 0.8;
        public double AlphaTranslation
        {
            get { return alphaTranslation; }
            set { alphaTranslation = value; }
        }

        private double gammaTranslation = 0.8;
        public double GammaTranslation
        {
            get { return gammaTranslation; }
            set { gammaTranslation = value; }
        }

        private double alphaRotation = 0.5;
        public double AlphaRotation
        {
            get { return alphaRotation; }
            set { alphaRotation = value; }
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
