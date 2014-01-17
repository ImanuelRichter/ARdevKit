using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public class ImageAugmention : Abstract2DAugmention
    {
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override System.Drawing.Bitmap getPreview()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Bitmap getIcon()
        {
            throw new NotImplementedException();
        }

        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
    }
}
