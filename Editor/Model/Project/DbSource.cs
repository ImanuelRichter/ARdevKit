using ARdevKit.Controller.Connections.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public class DbSource : AbstractSource
    {
        private DbConnection connection;

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
            return Properties.Resources.sourcePreview_normal_;
        }

        public override Bitmap getIcon()
        {
            return Properties.Resources.sourcePreview_small_;
        }
    }
}
