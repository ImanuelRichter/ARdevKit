using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    interface IPreviewable
    {
        Bitmap getPreview();
        List<AbstractProperty> getPropertyList();
    }
}
