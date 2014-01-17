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
        public string FileSource
        {
            get { return sourceFilePath; }
            set { sourceFilePath = value; }
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

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The property list. </returns>
        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }

        /// <summary>   Gets the preview. </summary>
        ///
        /// <returns>   The preview. </returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.FileSource_normal_;
        }

        /// <summary>   Gets the icon. </summary>
        ///
        /// <returns>   The icon. </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.FileSource_small_;
        }
    }
}
