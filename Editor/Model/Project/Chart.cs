using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;
using System.Drawing.Design;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Describes a <see cref="Chart"/> with its
    /// Colors and OptimalValues. It is a <see cref="Chart"/>.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class Chart : AbstractDynamic2DAugmentation
    {
        /// <summary>
        /// The style used by HighChart.
        /// </summary>
        protected ChartPositioning positioning;
        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public ChartPositioning Positioning
        {
            get { return positioning; }
            set { positioning = value; }
        }

        /// <summary>   Full pathname of the options file. </summary>
        protected string optionsFilePath;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets <see cref="optionsFilePath"/>. </summary>
        ///
        /// <value> The options. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General"), EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Options
        {
            get { return optionsFilePath; }
            set { optionsFilePath = value; }
        }

        /// <summary>   Default constructor. </summary>
        public Chart()
        {
            Positioning = new ChartPositioning(ChartPositioning.PositioningModes.RELATIVE);
            optionsFilePath = null;
            Width = 200;
            Height = 200;
            Scaling = new Vector3D(0, 0, 0);
        }


        /// <summary>
        /// An overwriting method, to accept a <see cref="AbstractProjectVisitor" />
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this <see cref="Chart"/></param>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            if (Source != null)
                Source.Accept(visitor);
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
            return Properties.Resources.highcharts_normal_;
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
            return Properties.Resources.highcharts_normal_;
        }

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 21.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            return ObjectCopier.Clone<Chart>(this);
        }
    }
}
