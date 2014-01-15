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
        public String Name { get; set; }

        public List<AbstractSource> sources { get; set; }

        public List<AbstractTrackable> trackables { get; set; }

        public Project()
        {
            this.trackables = new List<AbstractTrackable>();
            this.sources = new List<AbstractSource>();
            this.Name = null;  
        }
        public void accept(AbstractProjectVisitor visitor)
        {
            visitor.visit(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    
}
