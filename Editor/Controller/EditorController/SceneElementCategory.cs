using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller;

namespace ARdevKit.Controller.EditorController
{
    class SceneElementCategory
    {
        public enum MetaCategory { Source, Augmentation, Trackable };

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Hält die Kategorie der in den SceneElements enthaltenen IPreviewables. </summary>
        ///
        /// <value> The meta category. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private MetaCategory metaCategory;

        /**
         * <summary>    Gets or sets the category the meta belongs to. </summary>
         *
         * <value>  The meta category. </value>
         */

        public MetaCategory Category
        {
            get { return metaCategory; }
            set { metaCategory = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Hält einen Namen, der die Kategorie beschreibt. Dieser wird in der ComboBox des
        ///     ElementSelectionPanels angezeigt.
        /// </summary>
        ///
        /// <value> The name. </value>
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Hält die zur Kategorie gehörenden SceneElements. </summary>
        ///
        /// <value> The scene elements. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<SceneElement> sceneElements;

        /**
         * <summary>    Gets or sets the scene elements. </summary>
         *
         * <value>  The scene elements. </value>
         */

        public List<SceneElement> SceneElements
        {
            get { return sceneElements; }
            set { sceneElements = value; }
        }

        /**
         * <summary>    Constructor. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="metaCategory">  Hält die Kategorie der in den SceneElements enthaltenen
         *                              IPreviewables. </param>
         * <param name="name">          Hält einen Namen, der die Kategorie beschreibt. Dieser wird in
         *                              der ComboBox des ElementSelectionPanels angezeigt. </param>
         */

        public SceneElementCategory(MetaCategory metaCategory, String name)
        {
            this.metaCategory = metaCategory;
            this.name = name;
            sceneElements = new List<SceneElement>();
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

        /**
         * <summary>    Adds an element to the category. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="e"> The SceneElement to process. </param>
         */

        public void addElement(SceneElement e)
        {
            sceneElements.Add(e);
        }
    }
}
