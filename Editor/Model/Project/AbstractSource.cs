using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     AbstractSource is no PictureBox in the PreviewPanel, so it doesn't need an bitmap and so we
    ///     don't need getPreview(),
    ///     though getIcon() is needed for the ElementSelectionPanel.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractSource : IPreviewable//, ISerializable
    {

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        [ReadOnly(true), CategoryAttribute("General")]
        public String sourceID { get; set; }

        /// <summary>
        /// Gets or sets the augmentions, which get their dynamic information from the <see cref="AbstractSource"/>
        /// </summary>
        [Browsable(false)]
        public List<Abstract2DAugmention> augmentions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSource"/> class.
        /// </summary>
        protected AbstractSource()
        {
            this.augmentions = new List<Abstract2DAugmention>();
        }

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <param name="visitor"></param>
        public abstract void accept(AbstractProjectVisitor visitor);

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <returns></returns>
        public abstract List<AbstractProperty> getPropertyList();

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap getPreview();

        /// <summary>
        /// ToDo Summary is missing
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap getIcon();

        /// <summary>
        ///     Is needed for Custom Serialization. And provides the Serializer with the needed information
        /// </summary>
        /// <param name="info">Serialization Information, which is modified to encapsulate the things to save</param>
        /// <param name="context">describes aim and source of a serialized stream</param>
        [Obsolete("GetObjectData is obsolete, serialization is done without customization.")]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the augmention, which gets information through this <see cref="AbstractSource"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is searched for</param>
        /// <returns>the augmention which is found, otherwise null </returns>
        public AbstractAugmention findAugmention(IPreviewable a)
        {
            return this.augmentions[this.augmentions.IndexOf((Abstract2DAugmention)a)];
        }

        /// <summary>
        /// Checks if the augmention is associated with this <see cref="AbstractSource"/>.
        /// </summary>
        /// <param name="a">the IPreviewable, which is checked existence for</param>
        /// <returns>true, if its associated with this <see cref="AbstractSource"/>
        ///          false, else</returns>
        public bool existAugmention(IPreviewable a)
        {
            foreach (Abstract2DAugmention aug in augmentions)
            {
                if (aug == (Abstract2DAugmention) a)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
