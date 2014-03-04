using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller;

namespace ARdevKit.Controller.EditorController
{
    /**
     * <summary>    A category for scene elements. </summary>
     *
     * <remarks>    Robin, 19.01.2014. </remarks>
     */
    public enum MetaCategory { Source, Augmentation, Trackable }

    /**
     * <summary>    A category for scene elements. </summary>
     *
     * <remarks>    Robin, 19.01.2014. </remarks>
     */

    class SceneElementCategory
    {
        /// <summary>
        /// The meta category of the of all IPreviewables.
        /// </summary>
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

        /// <summary>
        /// The name of a category. This will be shown in the ComboBox of the ElementSelectionPanel.
        /// </summary>
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

        /// <summary>
        /// List of SceneElements in this particular category.
        /// </summary>
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
         * <param name="metaCategory">  Holds the Meta Category of the IPreviewables of this category. </param>
         * <param name="name">          Holds a name for the category that is shown in the ComboBox. </param>
         */

        public SceneElementCategory(MetaCategory metaCategory, String name)
        {
            this.metaCategory = metaCategory;
            this.name = name;
            sceneElements = new List<SceneElement>();
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
