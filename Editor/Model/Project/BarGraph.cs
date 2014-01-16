using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public class BarGraph : Graph
    {
        private Color maxValueColor;
        private Color minValueColor;
        private int optimalValue;
        private Color optimalValueColor;

        public override void accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
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
