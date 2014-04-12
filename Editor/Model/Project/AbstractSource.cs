using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     An <see cref="AbstractSource"/> is an <see cref="IPreviewable"/> and can be a <see cref="DbSource"/>
    ///     or a <see cref="FileSource"/> and is used by <see cref="AbstractDynamic2DAugmentations"/>
    ///     to provide their content.
    ///     Every <see cref="AbstractSource"/> has its own <see cref="sourceID"/> and a <see cref="queryFilePath"/>
    ///     which leads to a file containing the query to grant access to the data.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractSource : IPreviewable
    {
        /// <summary>   The source identifier. </summary>
        private String sourceID;

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        [ReadOnly(true), CategoryAttribute("General")]
        public String SourceID
        {
            get { return sourceID; }
            set { sourceID = value; }
        }

        /// <summary>   The query to the source. </summary>
        protected string queryFilePath;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the query. </summary>
        ///
        /// <value> The query. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General"), EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Query
        {
            get { return queryFilePath; }
            set { queryFilePath = value; }
        }

        AbstractDynamic2DAugmentation augmentation;

        /// <summary>
        /// Gets or sets the augmentations, which get their dynamic information from the <see cref="AbstractSource" />
        /// </summary>
        /// <value>
        /// The augmentations.
        /// </value>
        [Browsable(false)]
        public virtual AbstractDynamic2DAugmentation Augmentation 
        {
            get { return augmentation; }
            set
            {
                if (augmentation!=null && !value.Equals(augmentation))
                {
                    string newPath = Path.Combine(Environment.CurrentDirectory, "tmp\\" + value.ID);
                    if (!System.IO.Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    System.IO.File.Copy(queryFilePath, newPath + "\\query.js");
                    queryFilePath = newPath + "\\query.js";
                }
                augmentation = value;
            }
        }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractSource" /> class,
        /// </summary>
        protected AbstractSource() { }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractSource"/> class.
        /// but can be used from inheriting classes.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        protected AbstractSource(string sourceId)
        {
            sourceID = sourceId;
        }

        /// <summary>
        /// An abstract method, to accept an <see cref="AbstractProjectVisitor"/>
        ///  which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this element</param>
        public abstract void Accept(AbstractProjectVisitor visitor);

        /// <summary>
        /// returns NO <see cref="Bitmap" /> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public Bitmap getPreview(string projectPath)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative iconized Bitmap</returns>
        public abstract Bitmap getIcon();

        /// <summary>
        /// This method is called by the previewController when a new instance of the element is added to the Scene. It sets "must-have" properties.
        /// </summary>
        /// <param name="ew">The EditorWindow.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public virtual bool initElement(EditorWindow ew)
        {
            int count = 0;
            bool found = true;
            String newID = "";
            while (found)
            {
                found = false;
                count++;
                newID = this.GetType().Name + count;
                //make first letter lowercase
                newID = newID[0].ToString().ToLower() + newID.Substring(1);
                foreach (AbstractSource s in ew.project.Sources)
                {
                    if (s != null)
                    {
                        if (this.GetType().Equals(s.GetType()))
                        {
                            if (s.sourceID == newID)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
            sourceID = newID;
            if (!ew.project.Sources.Contains(this))
            {
                ew.project.Sources.Add(this);
            }
            return true;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return sourceID;
        }

        /// <summary>
        /// Makes a deep copy of this object.
        /// </summary>
        /// <returns>
        /// A copy of this object.
        /// </returns>
        public virtual object Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
