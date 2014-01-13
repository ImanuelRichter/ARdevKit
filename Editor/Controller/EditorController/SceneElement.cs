using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.EditorController
{
    class SceneElement
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Dummyelement. </summary>
        ///
        /// <value> The dummy. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IPreviewable dummy { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   grafische Repräsentation des Elements. </summary>
        ///
        /// <value> The icon. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Bitmap icon { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> Name des Elements. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String name { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Erstellt das ElementIcon. </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void createElementIcon()
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Konstruktor. Erzeugt ein neues SceneElement Objekt, akzeptiert dafür ein dummy vom Typ
        ///     IPreviewable, ein icon vom Typ Bitmap, sowie name vom Typ String.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ///
        /// <param name="dummy">    The dummy. </param>
        /// <param name="icon">     The icon. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SceneElement(IPreviewable dummy, Bitmap icon)
        {
            throw new NotImplementedException();
        }
    }
}
