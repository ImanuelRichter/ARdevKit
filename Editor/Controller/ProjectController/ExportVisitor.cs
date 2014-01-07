using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.ProjectController
{
    class ExportVisitor
    {
        private Stream stream;
        public abstract void visit(BarGraph graph)
        {
            throw new NotImplementedException();
        }
        public abstract void visit(DbSource source)
        {
            throw new NotImplementedException();
        }
        public abstract void visit(PictureMarker pictureMarker)
        {
            throw new NotImplementedException();
        }
        public abstract void visit(IDMarker idMarker)
        {
            throw new NotImplementedException();
        }
    }
}
