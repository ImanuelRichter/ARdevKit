using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;

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

        /// <summary>   The list of formated data. </summary>
        protected List<BarChartData> data;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the data. </summary>
        ///
        /// <value> The data. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General")]
        public List<BarChartData> Data
        {
            get { return data; }
            set { data = value; }
        }

        /*
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
            StreamReader optionsfile = System.IO.File.OpenText(@"res\\highcharts\\barChartColumn\\options.json");
            options = optionsfile.ReadToEnd();
            //OptimalValue = 50;
            data = new List<BarChartData>();
            data.Add(new BarChartData("Name 1", new double[] { 33.1, 66.9 }, ColorTranslator.FromHtml("0x55aa22"), ColorTranslator.FromHtml("0xdd210e")));
            data.Add(new BarChartData("Name 2", new double[] { 41.7, 58.3 }, ColorTranslator.FromHtml("0x55aa22"), ColorTranslator.FromHtml("0xdd210e")));
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

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 21.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            return ObjectCopier.Clone<BarChart>(this);
        }
    }
}
