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
        public void visit(BarGraph graph)
        {
            throw new NotImplementedException();
        }
        public void visit(DbSource source)
        {
            throw new NotImplementedException();
        }
        public void visit(PictureMarker pictureMarker)
        {
            throw new NotImplementedException();
        }
        public void visit(IDMarker idMarker)
        {
            throw new NotImplementedException();
        }
        public void visit(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
