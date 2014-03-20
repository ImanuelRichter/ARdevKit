using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Model.Project.Event
{
    /// <summary>
    /// The class CustomUserEvent mainly contains a reference path to a
    /// file, which is in the /tmp/ Folder. This file has ALL
    /// Events the user creates (inclusive the template events we provide)
    /// for ONE <see cref="AbstractAugmentation"/>.
    /// </summary>
    [Serializable]
    public abstract class AbstractEvent : JavaScriptBlock
    {
        protected string[] content;

        [Browsable(false)]
        public string[] Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>
        /// ID of the augmentation, which has these user events.
        /// </summary>
        protected string augmentationID;

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
        public AbstractEvent(string augmentationID)
        {
            if (augmentationID == null)
            {
                throw new ArgumentNullException("augmentionID was null.");
            }
            else
            {
                this.augmentationID = augmentationID;
                this.blockMarker = new BlockMarker("{", "};");
            }
        }

        public string GetHeadLine()
        {
            if (head != null)
                return head + " " + (blockMarker == null ? "" : blockMarker.Start);
            else
                return "";
        }

        public string GetLastLine()
        {
            if (blockMarker != null)
                return blockMarker.End;
            else
                return "";
        }

        public override string ToString()
        {
            string output = GetHeadLine() + Environment.NewLine;
            if (content != null)
                foreach (string s in Content)
                    output += s + Environment.NewLine;
            return output + GetLastLine() + Environment.NewLine;
        }

        public override void Write(System.IO.StreamWriter writer)
        {
            writer.Write(this.ToString());
        }
    }
}
