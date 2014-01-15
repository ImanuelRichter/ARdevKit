using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    public class IDMarker : AbstractMarker
    {
        private int matrixID;

        public override void accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
        public override Bitmap getPreview()
        {
            return Properties.Resources.ARRMarker_normal_;
        }

        public override Bitmap getIcon()
        {
            return Properties.Resources.ARRMarker_small_;
        }
    }
}
