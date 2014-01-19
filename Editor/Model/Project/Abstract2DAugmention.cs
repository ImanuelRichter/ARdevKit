﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class Abstract2DAugmention : AbstractAugmention
    {
        /// <summary>
        /// The height, in mm
        /// </summary>
        private int height;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height, in mm.
        /// </value>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// The width, in mm
        /// </summary>
        private int width;
        private int coordSystemId;
        private Vector3D scaling;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width, in mm.
        /// </value>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        protected Abstract2DAugmention() : base()
        {
            height = 0;
            width = 0;
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <param name="coordSystemId">        Identifier for the coordinate system. </param>
        /// <param name="isVisible">            true if this object is visible. </param>
        /// <param name="translationVector">    The translation vector. </param>
        /// <param name="scaling">              The scaling. </param>
        /// <param name="trackable">            The trackable. </param>
        /// <param name="width">                The width. </param>
        /// <param name="height">               The height. </param>
        protected Abstract2DAugmention(int coordSystemId, bool isVisible,
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable,
            int width, int height)
            : base(coordSystemId, isVisible, translationVector, scaling, trackable)
        {
            this.height = height;
            this.width = width;
        }

        public Abstract2DAugmention(int coordSystemId, bool isVisible, Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable)
        {
            // TODO: Complete member initialization
            this.coordSystemId = coordSystemId;
            this.IsVisible = isVisible;
            this.TranslationVector = translationVector;
            this.scaling = scaling;
            this.trackable = trackable;
        }
    }
}
