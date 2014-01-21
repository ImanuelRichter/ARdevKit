using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// Interface for previewable elements from the Model.
    /// </summary>
    public interface IPreviewable : ICloneable
    {

        /// <summary>
        /// Gets the preview which is displayed by
        ///PreviewPanel.
        /// </summary>
        /// <returns></returns>
        Bitmap getPreview();

        /// <summary>
        /// Gets the icon which is displayed by
        /// ElementSelectionPanel.
        /// </summary>
        /// <returns>
        /// The icon.
        /// </returns>
        Bitmap getIcon();
    }
}
