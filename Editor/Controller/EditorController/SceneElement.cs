using ARdevKit.Model.Project;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Controller.EditorController
{
    /**
     * <summary>    An element that can be added to a Scene. </summary>
     *
     * <remarks>    Robin, 19.01.2014. </remarks>
     */

    class SceneElement
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Dummyelement. </summary>
        ///
        /// <value> The dummy. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private IPreviewable dummy;

        /**
         * <summary>    Gets or sets the dummy. </summary>
         *
         * <value>  The dummy. </value>
         */

        public IPreviewable Dummy
        {
            get { return dummy; }
            set { dummy = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   grafische Repräsentation des Elements. </summary>
        ///
        /// <value> The icon. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private Bitmap icon;

        /**
         * <summary>    Gets or sets the icon. </summary>
         *
         * <value>  The icon. </value>
         */

        public Bitmap Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> Name des Elements. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private String name;

        /**
         * <summary>    Gets or sets the name. </summary>
         *
         * <value>  The name. </value>
         */

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private ElementIcon elementIcon;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The ElementIcon. </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ElementIcon ElementIcon
        {
            get { return elementIcon; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Konstruktor. Erzeugt ein neues SceneElement Objekt, akzeptiert dafür ein dummy vom Typ
        ///     IPreviewable, ein icon vom Typ Bitmap, sowie name vom Typ String.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// <param name="name">    The name of the Element. </param>
        /// <param name="dummy">    The dummy. </param>
        /// <param name="ew">     The Editor window. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SceneElement(String name, IPreviewable dummy, EditorWindow ew)
        {
            this.name = name;
            this.dummy = dummy;
            this.icon = dummy.getIcon();
            this.elementIcon = new ElementIcon(this, ew);
        }

        /**
         * <summary>    Gibt eine Zeichenfolge zurück, die das aktuelle Objekt darstellt. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <returns>    Eine Zeichenfolge, die das aktuelle Objekt darstellt. </returns>
         */

        public override string ToString()
        {
            return name;
        }
    }
}
