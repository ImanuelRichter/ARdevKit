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
    /// An <see cref="AbstractEvent"/> is a <see cref="JavaScriptBlock"/>
    /// that represents a function block (Event) triggered through the metaioSDK.
    /// </summary>
    [Serializable]
    public abstract class AbstractEvent : JavaScriptBlock
    {
        /// <summary>
        /// The content of the <see cref="AbstractEvent"/>.
        /// That means the stuff written by the user.
        /// </summary>
        protected string[] content;

        /// <summary>
        /// Gets or sets the <see cref="content"/>.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
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

        /// <summary>
        /// Gets or sets the augmentation identifier.
        /// </summary>
        /// <value>
        /// The augmentation identifier.
        /// </value>
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

        /// <summary>
        /// Returns the header (Signature of the function).
        /// </summary>
        /// <returns></returns>
        public string GetHeadLine()
        {
            if (head != null)
                return head + " " + (blockMarker == null ? "" : blockMarker.Start);
            else
                return "";
        }

        /// <summary>
        /// Gets the last line (currently it allways returns "};").
        /// </summary>
        /// <returns></returns>
        public string GetLastLine()
        {
            if (blockMarker != null)
                return blockMarker.End;
            else
                return "";
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string output = GetHeadLine() + Environment.NewLine;
            if (content != null)
                foreach (string s in Content)
                    output += s + Environment.NewLine;
            return output + GetLastLine() + Environment.NewLine;
        }

        /// <summary>
        /// Writes this Block (including sub-blocks and -lines) with the given writer.
        /// </summary>
        /// <param name="writer">The writer to write.</param>
        /// <remarks>
        /// Imanuel, 15.01.2014.
        /// </remarks>
        public override void Write(System.IO.StreamWriter writer)
        {
            writer.Write(this.ToString());
        }
    }
}
