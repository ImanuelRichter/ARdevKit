using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public class FileSource : AbstractSource
    {
        private String sourceFilePath;

        public FileSource(String sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
        }
        public override void accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the property list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the preview.
        /// </summary>
        /// <returns><see cref="Bitmap"/> representing a <see cref="FileSource"/></returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.FileSource_normal_;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns>small <see cref="Bitmap"/> representing a <see cref="FileSource"/></returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.FileSource_small_;
        }
    }
}
