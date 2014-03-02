using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// The class CustomUserEvent mainly contains a reference path to a
    /// file, which is in the /tmp/ Folder. This file has ALL
    /// Events the user creates (inclusive the template events we provide)
    /// for ONE augmentation.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CustomUserEvent
    {
        /// <summary>
        /// ID of the augmentation, which has this user event.
        /// </summary>
        private string augmentationID;
        
        /// <summary>
        /// File path of the customUserEvents.
        /// </summary>
        private string filePath;
        /// <summary>
        /// Get or set the file path for the customUserEvents-File.
        /// </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        /// <summary>
        /// Constructor of the CustomUserEvent.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public CustomUserEvent(string augmentationID)
        {
            this.augmentationID = augmentationID;
            filePath = getCustomUserFile();
        }

        /// <summary>
        /// Creates the file from a template. The element in the template
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

        /// <summary>
        /// A method, to accept a <see cref="AbstractProjectVisitor" />
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this <see cref="CustomUserEvent"/></param>
        public void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
