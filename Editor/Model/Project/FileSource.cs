using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
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

        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
        public override Bitmap getPreview()
        {
            return Properties.Resources.FileSource_normal_;
        }

        public override Bitmap getIcon()
        {
            return Properties.Resources.FileSource_small_;
        }
    }
}
