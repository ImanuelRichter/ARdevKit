using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Just a class for AbstractAugmentations. With this we are able to List all the customUserEvents. 
    /// See issue #13 for reason of this class.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class CustomUserEvent
    {
        private string augmentationID;
        
        private string filePath;

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
        /// Initializes a new instance of the <see cref="CustomUserEvent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        public CustomUserEvent(string augmentationID)
        {
            this.augmentationID = augmentationID;
            filePath = "NULL";
        }

        private string getCustomUserFile()
        {
            var fileName = "customUserEventTemplate.txt";
            var endFileName = augmentationID + "customUserEvent.txt";

            if (System.IO.File.Exists(@"currentProject\" + endFileName))
                System.IO.File.Delete(@"currentProject\" + endFileName);

            System.IO.File.Copy(@"res\templates\" + fileName, @"currentProject\" + endFileName);

            return System.IO.Path.GetFullPath("currentProject\\" + endFileName);
        }
    }
}
