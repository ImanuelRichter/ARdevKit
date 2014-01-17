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

        private string projectPath;
        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        protected AbstractSensor sensor;
        public AbstractSensor Sensor
        {
            get { return sensor; }
            set { sensor = value; }
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

        public Project()
        {
            this.name = "";
            trackables = new List<AbstractTrackable>();
            sources = new List<AbstractSource>();
        }

        public Project(string name) : this()
        {
            this.name = name;
        }

        public Project(string name, string projectPath)
            : this(name)
        {
            this.projectPath = projectPath;
        }

        public void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractTrackable t in Trackables)
            {
                t.Accept(visitor);
            }
            sensor.Accept(visitor);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public AbstractSource findSource(AbstractSource source)
        {
            return this.sources[this.sources.IndexOf(source)];
        }

        public bool existSource(AbstractSource source)
        {
            foreach (AbstractSource s in sources)
            {
                if (s == source)
                {
                    return true;
                }
            }
            return false;
        }
    }

    
}
