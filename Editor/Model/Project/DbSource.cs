using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;
using ARdevKit.Controller.ProjectController;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A database source for <see cref="AbstractDynamic2DAugmentation"/>. 
    /// It can also be used of other DynamicAugmentation, if the program will be extended.
    /// It inherits from <see cref="AbstractSource"/>.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class DbSource : AbstractSource
    {
        /// <summary>   URL of the source. </summary>
        protected string url;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets URL of the source. </summary>
        ///
        /// <value> The URL. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSource() { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ///
        /// <param name="url">  URL of the source. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSource(string url)
        {
            this.url = url;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An abstract method, to accept a <see cref="AbstractProjectVisitor"/> which must be implemented
        /// according to the visitor design pattern.
        /// </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ///
        /// <param name="visitor">  the visitor which encapsulates the action which is performed on this
        ///                         element. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.sourcePreview_small_;
        }

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 21.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            return ObjectCopier.Clone<DbSource>(this);
        }
    }
}
