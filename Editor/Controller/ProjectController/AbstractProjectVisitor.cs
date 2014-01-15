using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.ProjectController
{
    public abstract class AbstractProjectVisitor
    {
        /// <summary>
        /// The projectPath choosen by the user
        /// </summary>
        protected string projectPath;
        public abstract void visit(BarGraph graph);
        public abstract void visit(DbSource source);
        public abstract void visit(PictureMarker pictureMarker);
        public abstract void visit(IDMarker idMarker);
        public abstract void visit(Project project);
    }
}
