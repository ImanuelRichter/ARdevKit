using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ARdevKit.Controller.ProjectController
{
    public class SaveVisitor : AbstractProjectVisitor
    {
        public SaveVisitor(string projectPath)
        {
            this.projectPath = projectPath;
            formatter = new BinaryFormatter();
        }
        private BinaryFormatter formatter;
        private Stream stream;

        public override void visit(BarGraph graph)
        {
            throw new NotImplementedException();
        }
        public override void visit(DbSource source)
        {
            throw new NotImplementedException();
        }
        public override void visit(PictureMarker pictureMarker)
        {
            throw new NotImplementedException();
        }
        public override void visit(IDMarker idMarker)
        {
            throw new NotImplementedException();
        }

        public override void visit(Project project)
        {
            Stream stream = new FileStream((Path.Combine(projectPath, project.Name.Replace(" ", "_"))+".bin"), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, project);
            stream.Close();
        }

        public Project load(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Project deserializedProject = (Project)formatter.Deserialize(stream);
            stream.Close();
            return deserializedProject;
        }
    }
}
