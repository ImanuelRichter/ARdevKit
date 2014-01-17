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

    /// <summary>
    ///     visits the Project and walks through the different elements, which are held by the project
    ///     in order to serialize them in binary form
    /// </summary>
    /// <remarks>
    ///     obsolete, because serialization automatically serializes every member of the containerclass
    ///     use <see cref="SaveLoadController"/> instead
    /// </remarks>
    [Obsolete("A Visitor is obsolete, because standard serialization serializes every serializable field")]
    public class SaveVisitor : AbstractProjectVisitor
    {
        private BinaryFormatter formatter;
        private Stream stream;
        private string projectPath;


        /// <summary>
        /// Initializes a new instance of the <see cref="SaveVisitor"/> class.
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        public SaveVisitor(string projectPath)
        {
            this.projectPath = projectPath;
            formatter = new BinaryFormatter();
        }
       

        /// <summary>
        /// Visits the specified graph and serializes it to the projectPath.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void visit(BarGraph graph)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Visits the specified source and serializes it to the projectPath.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void visit(DbSource source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Visits the specified picture marker and serializes it to the projectPath.
        /// </summary>
        /// <param name="pictureMarker">The picture marker.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void visit(PictureMarker pictureMarker)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Visits the specified identifier marker and serializes it to the projectPath.
        /// </summary>
        /// <param name="idMarker">The identifier marker.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void visit(IDMarker idMarker)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Visits the specified project and saves it to the projectPath.
        /// </summary>
        /// <param name="project">The project, which is serializable</param>
        public override void visit(Project project)
        {
            Stream stream = new FileStream((Path.Combine(projectPath, project.Name.Replace(" ", "_"))+".bin"), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, project);
            stream.Close();
        }
    }
}
