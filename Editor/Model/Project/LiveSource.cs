using ARdevKit.Controller.ProjectController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public class LiveSource : AbstractSource
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
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ///
        /// <param name="url">  URL of the source. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LiveSource(string url)
        {
            this.url = url;
        }

        public override void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the icon. </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ///
        /// <returns>   The icon. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override Bitmap getIcon()
        {
            return Properties.Resources.sourcePreview_small_;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Makes a deep copy of this object. </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ///
        /// <returns>   A copy of this object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override object Clone()
        {
            return ObjectCopier.Clone<LiveSource>(this);
        }
    }
}
