using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>   Interface for previewable. </summary>
    public interface IPreviewable
    {
        /// <summary>   Gets the preview. </summary>
        ///
        /// <returns>   The preview. </returns>
        Bitmap getPreview();

        /// <summary>   Gets property list. </summary>
        ///
        /// <returns>   The property list. </returns>
        List<AbstractProperty> getPropertyList();

        /// <summary>   Gets the icon. </summary>
        ///
        /// <returns>   The icon. </returns>
        Bitmap getIcon();
    }
}
