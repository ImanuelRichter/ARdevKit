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

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     AbstractSource has no PictureBox in the PreviewPanel, so it doesn't need a getPreview() method,
    ///     though getIcon() is needed for the ElementSelectionPanel.
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
            set
            {
                if (File.Helper.FileExists(@"res\", value))
                {
                    var endFileName = sourceID + "_" + System.IO.Path.GetFileName(value);
                    File.Helper.Copy(value, @"tmp\source\", endFileName);
                    queryFilePath = System.IO.Path.GetFullPath(@"tmp\source\" + endFileName);
                }
                else
                    queryFilePath = value;
            }
        }

        /// <summary>
        /// Gets or sets the augmentations, which get their dynamic information from the <see cref="AbstractSource" />
        /// </summary>
        /// <value>
        /// The augmentations.
        /// </value>
        [Browsable(false)]
        public AbstractDynamic2DAugmentation Augmentation { get; set; }

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
        /// returns NO <see cref="Bitmap"/> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        public Bitmap getPreview()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative iconized Bitmap</returns>
        public abstract Bitmap getIcon();

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 22.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public abstract object Clone();

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
            return true;
        }

        /**
         * <summary>    Gibt eine Zeichenfolge zurück, die das aktuelle Objekt darstellt. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <returns>    Eine Zeichenfolge, die das aktuelle Objekt darstellt. </returns>
         */
        public override string ToString()
        {
            return sourceID;
        }
    }
}
