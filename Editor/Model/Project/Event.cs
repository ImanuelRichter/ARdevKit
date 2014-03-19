using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// The class CustomUserEvent mainly contains a reference path to a
    /// file, which is in the /tmp/ Folder. This file has ALL
    /// Events the user creates (inclusive the template events we provide)
    /// for ONE <see cref="AbstractAugmentation"/>.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class Event : JavaScriptBlock
    {
        [Browsable(false)]
        public enum Types { ONTOUCHSTARTED, ONTOUCHENDED, ONVISIBLE, ONINVISIBLE, ONLOADED, ONUNLOADED, CUSTOM }

        private Types type;

        [Browsable(false)]
        public Types Type { get { return type; } set { type = value; } }

        private List<JavaScriptInLine> content;

        public List<JavaScriptInLine> Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// ID of the augmentation, which has these user events.
        /// </summary>
        private string augmentationID;

        [Browsable(false)]
        public string AugmentationID
        {
            get 
            {
                return augmentationID; 
            }
            set
            {
                if (augmentationID != value)
                    head.Replace(augmentationID, value);
                augmentationID = value;
            }
        }

        /// <summary>
        /// Constructor of the CustomUserEvent.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public Event(string augmentationID, Types type)
        {
            if (augmentationID == null)
            {
                throw new ArgumentNullException("augmentionID was null.");
            }
            else
            {
                this.augmentationID = augmentationID;
                this.type = type;
                switch (type)
                {
                    case (Types.ONTOUCHSTARTED):
                        head = augmentationID + ".onTouchStarted = function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                    case (Types.ONTOUCHENDED):
                        head = augmentationID + ".onTouchEnded = function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                    case (Types.ONVISIBLE):
                        head = augmentationID + ".onVisible = function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                    case (Types.ONINVISIBLE):
                        head = augmentationID + ".onInvisible = function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                    case (Types.ONLOADED):
                        head = augmentationID + ".onLoaded = function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                    case (Types.ONUNLOADED):
                        head = augmentationID + ".onUnloaded = function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                    case (Types.CUSTOM):
                        head = "function()";
                        blockMarker = new BlockMarker("{", "};");
                        break;
                }
                content = new List<JavaScriptInLine>();
                content.Add(new JavaScriptInLine("//TODO add your content here", false));
            }
        }

        /// <summary>
        /// Creates the file from a template. The string "#element" in the template
        /// will be replaced with the id of the augmentation
        /// </summary>
        /// <returns>
        /// File path of the newly generated customUserEvent.
        /// </returns>
        private string getCustomUserFile()
        {
            var fileName = "customUserEventTemplate.txt";
            var endFileName = augmentationID + "_Event.js";
            var dest = @"tmp\" + augmentationID + "\\";
            var source = @"res\templates\";

            if (!System.IO.File.Exists(source + fileName))
            {
                throw new System.IO.FileNotFoundException("Die Datei " + fileName + " kann nicht gefunden werden.");
            }

            string content = System.IO.File.ReadAllText(source + fileName);
            content = content.Replace("#element", augmentationID);

            System.IO.Directory.CreateDirectory(dest);

            if (System.IO.File.Exists(dest + endFileName))
                System.IO.File.Delete(dest + endFileName);

            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(dest + endFileName))
            {
                outfile.Write(content);
            }

            return System.IO.Path.GetFullPath(dest + endFileName);
        }

        public string GetHeadLine()
        {
            return head + " " + blockMarker.Start;
        }

        public string GetLastLine()
        {
            return blockMarker.End;
        }

        public override string ToString()
        {
            string output = head + " " + blockMarker + Environment.NewLine;
            if (content != null)
                foreach (JavaScriptInLine l in content)
                    output += l.ToString();
            return output + blockMarker + Environment.NewLine;
        }

        public override void Write(System.IO.StreamWriter writer)
        {
            writer.Write(this.ToString());
        }
    }
}
