﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Describes a <see cref="BarChart"/> with its
    /// Colors and OptimalValues. It is a <see cref="Chart"/>.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class BarChart : Chart
    {
        /*
        /// <summary>
        /// Gets or sets the color of the maximum value,
        /// which can be displayed.
        /// </summary>
        /// <value>
        /// The color of the maximum value.
        /// </value>
        [CategoryAttribute("Color")]
        public Color MaxValueColor { get; set; }
        
        /// <summary>
        /// Gets or sets the color of the minimal value,
        /// which can be displayed.
        /// </summary>
        /// <value>
        /// The color of the minimum value.
        /// </value>
        [CategoryAttribute("Color")]
        public Color MinValueColor { get; set; }
       
        /// <summary>
        /// Gets or sets the optimal value.
        /// </summary>
        /// <value>
        /// The optimal value.
        /// </value>
        [CategoryAttribute("General")]
        public int OptimalValue { get; set; }
        
        /// <summary>
        /// Gets or sets the color of the optimal value.
        /// </summary>
        /// <value>
        /// The color of the optimal value.
        /// </value>
        [CategoryAttribute("Color")]
        public Color OptimalValueColor { get; set; }
         */

        /// <summary>   Default constructor. </summary>
        public BarChart()
        {
            Style = new ChartStyle();
            //OptimalValue = 50;
            //OptimalValueColor = new Color();
            //MinValueColor = new Color();
            MinValue = 0;
            MaxValue = 100;
            ScalingVector = new Vector3D(0, 0, 0);
            Style = new ChartStyle();
        }


        /// <summary>
        /// An overwriting method, to accept a <see cref="AbstractProjectVisitor" />
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this <see cref="BarChart"/></param>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }


        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative Bitmap
        /// </returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.coordinationAxis_normal_;
        }


        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.coordinationAxis_small_;
        }
    }
}