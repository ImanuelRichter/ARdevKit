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
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private List<AbstractSource> sources;
        private List<AbstractTrackable> trackables;

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
