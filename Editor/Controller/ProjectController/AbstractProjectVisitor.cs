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
        public abstract void Visit(BarGraph graph);
        public abstract void Visit(DbSource source);
        public abstract void Visit(PictureMarker pictureMarker);
        public abstract void Visit(IDMarker idMarker);
        public abstract void Visit(IDMarkerSensor idMarkerSensor);
        public abstract void Visit(PictureMarkerSensor pictureMarkerSensor);
        public abstract void Visit(Project project);
    }
}
