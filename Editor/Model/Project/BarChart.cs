using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BarChart : Chart
    {
        /// <summary>
        /// Gets or sets the maximum color of the value.
        /// </summary>
        /// <value>
        /// The maximum color of the value.
        /// </value>
        public Color MaxValueColor { get; set; }
        /// <summary>
        /// Gets or sets the minimum color of the value.
        /// </summary>
        /// <value>
        /// The minimum color of the value.
        /// </value>
        public Color MinValueColor { get; set; }
        /// <summary>
        /// Gets or sets the optimal value.
        /// </summary>
        /// <value>
        /// The optimal value.
        /// </value>
        public int OptimalValue { get; set; }
        /// <summary>
        /// Gets or sets the color of the optimal value.
        /// </summary>
        /// <value>
        /// The color of the optimal value.
        /// </value>
        public Color OptimalValueColor { get; set; }

        public BarChart()
        {
            OptimalValue = 50;
            OptimalValueColor = new Color();
            MinValueColor = new Color();
            MinValue = 0;
            MaxValue = 100;
            Scaling = 0;
            ScalingVector = new Vector3D(0, 0, 0);
        }
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Bitmap getPreview()
        {
            return Properties.Resources.coordinationAxis_normal_;
        }

        public override Bitmap getIcon()
        {
            return Properties.Resources.coordinationAxis_small_;
        }

        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
    }
}
