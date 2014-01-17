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
    /// <summary>   A database source. </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class DbSource : AbstractSource
    {
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

        /// <summary>   Default constructor. </summary>
        public DbSource()
        {
            ; // missing initialization
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

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <returns>   The property list. </returns>
        public override List<View.AbstractProperty> getPropertyList()
        {
            throw new NotImplementedException();
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <returns>   The preview. </returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.sourcePreview_normal_;
        }

        /// <summary>   ToDo Summary is missing. </summary>
        ///
        /// <returns>   The icon. </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.sourcePreview_small_;
        }
    }
}
