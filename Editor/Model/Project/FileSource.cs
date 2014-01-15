using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    class FileSource : AbstractSource
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

        public override System.Drawing.Bitmap getPreview()
        {
            throw new NotImplementedException();
        }

    }
}
