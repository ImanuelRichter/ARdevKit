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
        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        [ReadOnly(true), CategoryAttribute("General")]
        public String sourceID { get; set; }

        /// <summary>   The query to the source. </summary>
        protected string queryFilePath;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the query. </summary>
        ///
        /// <value> The query. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General"), EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string QueryFilePath
        {
            get { return queryFilePath; }
            set { queryFilePath = value; }
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
        /// An abstract method, to accept a <see cref="AbstractProjectVisitor"/>
        /// which must be implemented according to the visitor design pattern.
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
    }
}
