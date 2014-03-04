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
        /// <summary>   Prototype-Element. </summary>
        ///
        /// <value> The prototype. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private IPreviewable prototype;

        /**
         * <summary>    Gets or sets the prototype. </summary>
         *
         * <value>  The prototype. </value>
         */

        public IPreviewable Prototype
        {
            get { return prototype; }
            set { prototype = value; }
        }

        /// <summary>
        /// The icon of the element.
        /// </summary>
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
        ///     Konstruktor. Create a new SceneElement, takes a prototype of Type
        ///     IPreviewable, an icon of Typ Bitmap and a name of Typ String.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// <param name="name">    The name of the Element. </param>
        /// <param name="prototype">    The prototype. </param>
        /// <param name="ew">     The Editor window. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SceneElement(String name, IPreviewable prototype, EditorWindow ew)
        {
            this.name = name;
            this.prototype = prototype;
            this.icon = prototype.getIcon();
            this.elementIcon = new ElementIcon(this, ew);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return name;
        }
    }
}
