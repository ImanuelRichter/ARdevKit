using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.ComponentModel;
using ARdevKit.Controller.ProjectController;
using ARdevKit.View;

namespace ARdevKit.Model.Project
{
    /// <summary>   An image augmention. </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class ImageAugmention : Abstract2DAugmention
    {
        /// <summary>   Full pathname of the image file. </summary>
        private string imagePath;
        /// <summary>   Gets or sets the full pathname of the image file. </summary>
        ///
        /// <value> The full pathname of the image file. </value>
        [CategoryAttribute("General")]
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        /// <summary>   Default constructor. </summary>
        public ImageAugmention() : base()
        {
            ; // missing initialization
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <param name="visitor">  . </param>
        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The preview. </returns>
        public override Bitmap getPreview()
        {
            throw new NotImplementedException();
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The icon. </returns>
        public override Bitmap getIcon()
        {
            throw new NotImplementedException();
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The property list. </returns>
        public override List<AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }
    }
}
