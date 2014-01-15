using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARdevKit.Model.Project.File
{
    public class TrackingConfigurationFile : ConfigFile
    {
        private List<Section>.Enumerator sectionPointer;
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TrackingConfigurationFile(Tag tag)
        {
            header = "<?xml version=\"1.0\"?>";
            this.tag = tag;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a section. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="cs">   The section to be added. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void AddSection(Section cs)
        {
            if (sections == null)
            {
                sections = new List<Section>();
                sectionPointer = sections.GetEnumerator();
            }
            cs.ParentFile = this;
            sections.Add(cs);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the file at the projectPath. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="projectPath"> The project path to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Write(string projectPath)
        {
            string assetsPath = Path.Combine(projectPath, "Assets");
            if (!Directory.Exists(assetsPath))
                Directory.CreateDirectory(assetsPath);
            StreamWriter writer = new StreamWriter(Path.Combine(assetsPath, "TrackingConfiguration.xml"));
            writer.WriteLine(header);
            writer.WriteLine(tag);
            if (sections != null)
            {
                foreach (Section cs in sections)
                {
                    cs.Write(writer);
                }
            }
            writer.WriteLine(tag);
            writer.Close();
        }
    }
}
