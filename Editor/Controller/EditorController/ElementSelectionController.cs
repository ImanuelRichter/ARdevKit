using ARdevKit.Model.Project;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit.Controller.EditorController
{
    class ElementSelectionController
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>  Hält die SceneElementCategoryPanels des SceneSelectionPanels. </summary>
        ///
        /// <value> The category panels. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<SceneElementCategoryPanel> categoryPanels;

        /**
         * <summary>    Gets or sets the category panels. </summary>
         *
         * <value>  The category panels. </value>
         */

        public List<SceneElementCategoryPanel> CategoryPanels
        {
            get { return categoryPanels; }
            set { categoryPanels = value; }
        }

        /**
         * <summary>    The editor window. </summary>
         */
        private readonly EditorWindow editorWindow;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// 
        ///
        /// <param name="ew">   The ew. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ElementSelectionController(EditorWindow ew)
        {
            this.editorWindow = ew;
            categoryPanels = new List<SceneElementCategoryPanel>();
            createCategoryPanels();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Aktualisiert das ElementSelectionPanel. </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// 
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updateElementSelectionPanel()
        {
            foreach (SceneElementCategoryPanel p in categoryPanels)
            {
                p.Visible = false;
            }
            ((SceneElementCategoryPanel) editorWindow.Cmb_editor_selection_toolSelection.SelectedItem).Visible = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Erstellt die SceneCategoryElementPanels und hält sie in einer Liste. </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// 
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void createCategoryPanels()
        {
            foreach (SceneElementCategory c in editorWindow.ElementCategories)
            {
                SceneElementCategoryPanel n = new SceneElementCategoryPanel(c);
                categoryPanels.Add(n);
                editorWindow.Pnl_editor_selection.Controls.Add(n);
                n.Location = new Point(0, 25);
                n.Size = new Size(editorWindow.Pnl_editor_selection.Size.Width, editorWindow.Pnl_editor_selection.Size.Height-25);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Fügt die SceneElementCategories der ComboBox des ElementSelectionPanels hinzu.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// 
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void populateComboBox()
        {
            foreach (SceneElementCategoryPanel p in categoryPanels)
            {
                editorWindow.Cmb_editor_selection_toolSelection.Items.Add(p);
            }
            editorWindow.Cmb_editor_selection_toolSelection.SelectedIndex = 0;
        }

        /**
         * <summary>    Disables or enables the given element in the Element Selection Panel. </summary>
         *
         * <remarks>    Robin, 19.01.2014. </remarks>
         *
         * <param name="element">   The element to disable or enable. Example: typeof(IDMarker) </param>
         * <param name="enable">   Whether the element should be dis or enabled. </param>
         */

        public void setElementEnable(Type element, Boolean enable)
        {
            foreach (SceneElementCategoryPanel p in categoryPanels)
            {
                foreach (SceneElement e in p.Category.SceneElements)
                {
                    if (element.IsInstanceOfType(e.Dummy) )
                    {
                        foreach (Control c in p.Controls)
                        {
                            if (((ElementIcon)c).Element.Dummy == e.Dummy)
                            {
                                c.Visible = enable;
                            }
                        }
                    }
                }
            }
        }
            
    }
}
