using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A file source for <see cref="AbstractDynamic2DAugmentation"/>. 
    /// It can also be used of other DynamicAugmentation, if the program will be extended.
    /// It inherits from <see cref"AbstractSource">.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class FileSource : AbstractSource
    {
        /// <summary>   Full pathname of the source file. </summary>
        private String sourceFilePath;
        /// <summary>
        /// Gets or sets the data file path. If the file exists, it will be copied to a temporary directore 
        /// /tmp/ of the program directory. If not, nothing happens.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [CategoryAttribute("General")]
        [EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Data
        {
            get { return sourceFilePath; }
            set { sourceFilePath = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSource"/> class.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        public FileSource(string sourceFilePath) : base()
        {
            this.sourceFilePath = sourceFilePath;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An abstract method, to accept an <see cref="AbstractProjectVisitor"/> which must be
        /// implemented according to the visitor design pattern.
        /// </summary>
        ///
        /// <remarks>   Imanuel, 27.01.2014. </remarks>
        ///
        /// <param name="visitor">  the visitor which encapsulates the action which is performed on this
        ///                         element. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.FileSource_small_;
        }

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 21.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            return ObjectCopier.Clone<FileSource>(this);
        }
    }
}
