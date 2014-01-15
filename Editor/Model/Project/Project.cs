using ARdevKit.Controller.ProjectController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public class Project : ISerializable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<AbstractSource> sources;
        public List<AbstractSource> Sources
        {
            get { return sources; }
            set { sources = value; }
        }

        private List<AbstractTrackable> trackables;
        public List<AbstractTrackable> Trackables
        {
            get { return trackables; }
            set { trackables = value; }
        }

        public Project(string name)
        {
            this.name = name;
            trackables = new List<AbstractTrackable>();
            sources = new List<AbstractSource>();
        }

        public void Accept(AbstractProjectVisitor visitor)
        {
            visitor.visit(this);
            foreach (AbstractTrackable t in Trackables)
            {
                t.Accept(visitor);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    
}
