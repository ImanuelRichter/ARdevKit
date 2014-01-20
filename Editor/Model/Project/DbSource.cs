using ARdevKit.Controller.Connections.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// A database source 
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class DbSource : AbstractSource
    {
        /*
        /// <summary>   The connection. </summary>
        private DbConnection connection;
        /// <summary>   Gets or sets the connection. </summary>
        ///
        /// <value> The connection. </value>
        [CategoryAttribute("General")]
        public DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
         * */

        /// <summary>   Default constructor. </summary>
        /// <remarks>   TODO                 </remarks>
        public DbSource()
        {
            ; // ToDo
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <param name="visitor">  . </param>
        public override void accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            throw new NotImplementedException();
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
    }
}
