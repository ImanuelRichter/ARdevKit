using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// The class CustomUserEvent mainly contains a reference to a
    /// file, which is in the /currentProject/ Folder. This file has ALL
    /// Events the user creates (inclusive the template events we provide)
    /// for ONE augmentation.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CustomUserEvent
    {
        /// <summary>
        /// ID of the augmentation
        /// </summary>
        private string augmentationID;
        
        /// <summary>
        /// File path of the customUserEvents
        /// </summary>
        private string filePath;
        /// <summary>
        /// Get the file path for the customUserEvents-File.
        /// </summary>
        public string FilePath
        {
            get 
            { 
                if (String.Compare(filePath, "NULL") == 0)
                {
                    filePath = getCustomUserFile();
                }
                
                return filePath; 
            }
        }

        /// <summary>
        /// Constructor of the CustomUserEvent.
        /// </summary>
        /// <param name="augmentationID">ID of the augmentation</param>
        public CustomUserEvent(string augmentationID)
        {
            this.augmentationID = augmentationID;
            filePath = "NULL";
        }

        /// <summary>
        /// Copies the template to the /currentProject/ - Folder
        /// and renames it.
        /// </summary>
        /// <returns>File path of the newly generated customUserEvent.</returns>
        private string getCustomUserFile()
        {
            var fileName = "customUserEventTemplate.txt";
            var endFileName = augmentationID + "customUserEvent.js";

            if (System.IO.File.Exists(@"currentProject\" + endFileName))
                System.IO.File.Delete(@"currentProject\" + endFileName);

            System.IO.File.Copy(@"res\templates\" + fileName, @"currentProject\" + endFileName);

            return System.IO.Path.GetFullPath("currentProject\\" + endFileName);
        }
    }
}
