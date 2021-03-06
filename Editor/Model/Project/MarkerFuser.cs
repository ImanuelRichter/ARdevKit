﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A <see cref="MarkerFuser"/> has a <see cref="fuserType"/>,
    ///             an <see cref="alphaTranslation"/>, an <see cref="alphaRotation"/>
    ///             and a <see cref="keepPoseForNumberOfFrames"/> value. </summary>
    ///
    /// <remarks>   Imanuel, 17.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class MarkerFuser
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Values that represent FuserTypes. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Flags]
        public enum FuserTypes { SmoothingFuser, BestQualityFuser };
        /// <summary>
        /// Type of the fuser.
        /// </summary>
        private FuserTypes fuserType;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the type of the fuser. </summary>
        ///
        /// <value> The type of the fuser. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public FuserTypes FuserType
        {
            get { return fuserType; }
            set { fuserType = value; }
        }

        /// <summary>
        /// The alpha translation.
        /// </summary>
        private double alphaTranslation;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the alpha translation. </summary>
        ///
        /// <value> The alpha translation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public double AlphaTranslation
        {
            get { return alphaTranslation; }
            set { alphaTranslation = value; }
        }

        /// <summary>
        /// The alpha rotation.
        /// </summary>
        private double alphaRotation;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the alpha rotation. </summary>
        ///
        /// <value> The alpha rotation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public double AlphaRotation
        {
            get { return alphaRotation; }
            set { alphaRotation = value; }
        }

        /// <summary>
        /// The keep pose for number of frames value.
        /// </summary>
        private int keepPoseForNumberOfFrames;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the keep pose for number of frames. </summary>
        ///
        /// <value> The keep pose for number of frames. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public int KeepPoseForNumberOfFrames
        {
            get { return keepPoseForNumberOfFrames; }
            set { keepPoseForNumberOfFrames = value; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerFuser"/> class.
        /// </summary>
        public MarkerFuser()
        {
            fuserType = FuserTypes.SmoothingFuser;
            alphaTranslation = 0.8;
            alphaRotation = 0.5;
            keepPoseForNumberOfFrames = 2;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Accepts the given visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
