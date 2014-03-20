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
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class EventFile : AbstractFile
    {
        public EventFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void Clear()
        {
            blocks = new List<AbstractBlock>();
        }

        public override void Save(string projectPath)
        {
            this.filePath = Path.Combine(projectPath, "Events", Path.GetFileName(filePath));
            Save();
        }

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
