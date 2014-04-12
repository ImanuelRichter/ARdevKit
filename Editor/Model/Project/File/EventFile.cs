using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ARdevKit.Model.Project.Event;

namespace ARdevKit.Model.Project.File
{
    /// <summary>
    /// An <see cref="EventFile"/> is an <see cref="AbstractFile"/> that represents a file containing
    /// all Events corresponding to an <see cref="AbstractAugmentation"/>.
    /// </summary>
    [Serializable]
    public class EventFile : AbstractFile
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="filePath"> The path where the file will be stored </param>
        public EventFile(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Deletes all <see cref="AbstractBlock"/>s within this <see cref="EventFile"/>.
        /// </summary>
        public void Clear()
        {
            blocks = new List<AbstractBlock>();
        }

        /// <summary>
        /// Saves this <see cref="EventFile"/> to the given path.
        /// </summary>
        /// <param name="projectPath"> The new path </param>
        public override void Save(string projectPath)
        {
            this.filePath = Path.Combine(projectPath, "Events", Path.GetFileName(filePath));
            Save();
        }

        /// <summary>
        /// Saves this <see cref="EventFile"/> to the <see cref="filePath"/>.
        /// </summary>
        public override void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            if (blocks != null)
            {
                foreach (AbstractEvent e in blocks)
                {
                    e.Write(writer);
                }
            }
            writer.Close();
        }
    }
}
