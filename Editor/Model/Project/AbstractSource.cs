using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     AbstractSource is no PictureBox in the Panel, so it doesn't need an bitmap and so we
    ///     don't need getPreview(),
    ///     though this IPreviewable can't be a Interface for AbstractSource.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public abstract class AbstractSource : ISerializable, IPreviewable
    {
        private String sourceID;


        private List<AbstractDynamic2DAugmentation> augmentions;


        abstract public void accept(AbstractProjectVisitor visitor);

        public abstract List<AbstractProperty> getPropertyList();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public abstract Bitmap getPreview();

        public abstract Bitmap getIcon();
    }
}
