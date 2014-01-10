using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.ProjectController
{
    abstract class AbstractProjectVisitor
    {
        public abstract void visit(BarGraph graph);
        public abstract void visit(DbSource source);
        public abstract void visit(PictureMarker pictureMarker);
        public abstract void visit(IDMarker idMarker);
        public abstract void visit(Project project);
    }
}
