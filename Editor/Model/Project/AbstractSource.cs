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
    [Serializable]
    public abstract class AbstractSource : IPreviewable//, ISerializable
    {
        public String sourceID { get; set; }

        public List<AbstractDynamic2DAugmentation> augmentions {get; set; }
        abstract public void accept(AbstractProjectVisitor visitor);

        public abstract List<AbstractProperty> getPropertyList();

        public AbstractSource()
        {
            this.augmentions = new List<AbstractDynamic2DAugmentation>();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public abstract Bitmap getPreview();

        public abstract Bitmap getIcon();

        public AbstractAugmentation findAugmentation(IPreviewable a)
        {
            return this.augmentions[this.augmentions.IndexOf((AbstractDynamic2DAugmentation)a)];
        }

        public bool existAugmentation(IPreviewable a)
        {
            foreach (AbstractDynamic2DAugmentation aug in augmentions)
            {
                if (aug == (AbstractDynamic2DAugmentation) a)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
