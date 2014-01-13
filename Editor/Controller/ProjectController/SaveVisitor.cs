using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace ARdevKit.Controller.ProjectController
{
    class SaveVisitor
    {
        private System.Runtime.Serialization.Formatters.Soap.SoapFormatter formatter;
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
