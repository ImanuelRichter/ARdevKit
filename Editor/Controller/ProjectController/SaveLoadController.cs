using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.ProjectController
{
    /// <summary>
    ///     Provides static methods for saving and loading a <see cref="Project"/> 
    ///     to or from a certain path
    /// </summary>
    static class SaveLoadController
    {
        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="project">The <see cref="Project" />, which need to be serialized</param>
        public static void saveProject(Project project)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream((Path.Combine(project.ProjectPath, project.Name.Replace(" ", "_")) + ".ardev"), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, project);
            stream.Close();
        }

        /// <summary>
        /// Loads the project.
        /// </summary>
        /// <param name="path">The path, where the serialized <see cref="Project"/> is saved</param>
        /// <returns>the deserialized <see cref="Project"/></returns>
        public static Project loadProject(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Project deserializedProject = (Project)formatter.Deserialize(stream);
            stream.Close();
            deserializedProject.ProjectPath = Path.GetDirectoryName(path);
            deserializedProject.Name = Path.GetFileNameWithoutExtension(path);
            return deserializedProject;
        }
    }
}
