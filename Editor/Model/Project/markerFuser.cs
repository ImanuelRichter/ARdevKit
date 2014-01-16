using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    public abstract class MarkerFuser
    {
        public enum FuserTypes { SmoothingFuser, BestQualityFuser };
        private FuserTypes fuserType;
        public FuserTypes FuserType
        {
            get { return fuserType; }
            set { fuserType = value; }
        }

        private double alphaTranslation = 0.8;
        public double AlphaTranslation
        {
            get { return alphaTranslation; }
            set { alphaTranslation = value; }
        }

        private double alphaRotation = 0.5;
        public double AlphaRotation
        {
            get { return alphaRotation; }
            set { alphaRotation = value; }
        }

        private int keepPoseForNumberOfFrames = 2;
        public int KeepPoseForNumberOfFrames
        {
            get { return keepPoseForNumberOfFrames; }
            set { keepPoseForNumberOfFrames = value; }
        }

        public void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
