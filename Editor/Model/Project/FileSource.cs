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
    /// <summary>   A file source. </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class FileSource : AbstractSource
    {
        /// <summary>   Full pathname of the source file. </summary>
        private String sourceFilePath;
        /// <summary>   Gets or sets the file source. </summary>
        ///
        /// <value> The file source. </value>
        [CategoryAttribute("General")]
        public string SourceFilePath
        {
            get { return sourceFilePath; }
            set { sourceFilePath = value; }
        }

        public FileSource(string sourceFilePath) : base()
        {
            this.sourceFilePath = sourceFilePath;
        }

        public FileSource(string sourceId, string sourceFilePath) : base(sourceId)
        {
            this.sourceFilePath = sourceFilePath;
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <param name="visitor">  . </param>
        public override void accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
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
