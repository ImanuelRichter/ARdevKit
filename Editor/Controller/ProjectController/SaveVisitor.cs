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
        public SaveVisitor()
        {            
            formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Visits the specified project and saves it to the projectPath.
        /// </summary>
        /// <param name="project">The project, which is serializable</param>
        public override void Visit(Project project)
        {
            Stream stream = new FileStream((Path.Combine(project.ProjectPath, project.Name.Replace(" ", "_")) + ".bin"), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, project);
            stream.Close();
        }

        /// <summary>
        /// Visits the specified graph and serializes it to the projectPath.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Visit(BarChart graph)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the specified source and serializes it to the projectPath.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Visit(DbSource source)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the specified picture marker and serializes it to the projectPath.
        /// </summary>
        /// <param name="pictureMarker">The picture marker.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Visit(PictureMarker pictureMarker)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the specified identifier marker and serializes it to the projectPath.
        /// </summary>
        /// <param name="idMarker">The identifier marker.</param>
        public override void Visit(IDMarker idMarker)
        {
            //do nothing
        }        

        /// <summary>
        /// Visits the given <see cref="ImageAugmention" />.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Visit(ImageAugmention image)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the given <see cref="MarkerlessFuser" />.
        /// </summary>
        /// <param name="markerlessFuser">The markerless fuser.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Visit(MarkerlessFuser markerlessFuser)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the given <see cref="MarkerFuser" />.
        /// </summary>
        /// <param name="markerFuser">The marker fuser.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Visit(MarkerFuser markerFuser)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the given <see cref="MarkerlessSensor" />.
        /// </summary>
        /// <param name="MarkerlessSensor">The markerless sensor.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Visit(MarkerlessSensor MarkerlessSensor)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the given <see cref="PictureMarkerSensor" />.
        /// </summary>
        /// <param name="pictureMarkerSensor">The picture marker sensor.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Visit(PictureMarkerSensor pictureMarkerSensor)
        {
            //do nothing
        }

        /// <summary>
        /// Visits the given <see cref="MarkerSensor" />.
        /// </summary>
        /// <param name="idMarkerSensor">The identifier marker sensor.</param>
        /// <remarks>
        /// Imanuel, 17.01.2014.
        /// </remarks>
        public override void Visit(MarkerSensor idMarkerSensor)
        {
            //do nothing
        }
    }
}
