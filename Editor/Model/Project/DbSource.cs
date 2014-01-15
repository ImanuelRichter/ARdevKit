using ARdevKit.Controller.Connections.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    class DbSource : AbstractSource
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

        public override System.Drawing.Bitmap getPreview()
        {
            throw new NotImplementedException();
        }
    }
}
