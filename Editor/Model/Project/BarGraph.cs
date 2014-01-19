using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// ToDo the summary is missing.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class BarGraph : Graph
    {
        /// <summary>
        /// Gets or sets the maximum color of the value.
        /// </summary>
        /// <value>
        /// The maximum color of the value.
        /// </value>
        [CategoryAttribute("General")]
        public Color MaxValueColor { get; set; }
        /// <summary>
        /// Gets or sets the minimum color of the value.
        /// </summary>
        /// <value>
        /// The minimum color of the value.
        /// </value>
        [CategoryAttribute("General")]
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
        [CategoryAttribute("General")]
        public Color OptimalValueColor { get; set; }

        /// <summary>   Default constructor. </summary>
        public BarGraph()
        {
            OptimalValue = 50;
            OptimalValueColor = new Color();
            MinValueColor = new Color();
            MinValue = 0;
            MaxValue = 100;
            Scaling = 0;
            ScalingVector = new Vector3D(0, 0, 0);
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <param name="visitor">  . </param>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <returns>   The preview. </returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.coordinationAxis_normal_;
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <returns>   The icon. </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.coordinationAxis_small_;
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The property list. </returns>
        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
    }
}
