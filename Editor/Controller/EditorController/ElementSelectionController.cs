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
        /// <summary>  Holds the SceneElementCategoryPanels of the SceneSelectionPanels. </summary>
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
        /// <param name="ew">   The Editor Window. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ElementSelectionController(EditorWindow ew)
        {
            this.editorWindow = ew;
            categoryPanels = new List<SceneElementCategoryPanel>();
            createCategoryPanels();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the ElementSelectionPanel. </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// 
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updateElementSelectionPanel()
        {
            foreach (Control c in editorWindow.Pnl_editor_selection.Controls)
            {
                if (c != editorWindow.Cmb_editor_selection_toolSelection)
                {
                    editorWindow.Pnl_editor_selection.Controls.Remove(c);
            }
            }
            SceneElementCategoryPanel panel = ((SceneElementCategoryPanel)editorWindow.Cmb_editor_selection_toolSelection.SelectedItem);
            editorWindow.Pnl_editor_selection.Controls.Add(panel);
            panel.Location = new Point(0, 25);
            panel.Size = new Size(editorWindow.Pnl_editor_selection.Size.Width, editorWindow.Pnl_editor_selection.Size.Height - 25);
            panel.Visible = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates the SceneCategoryElementPanels and holds them in a list. </summary>
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
                n.Size = new Size(editorWindow.Pnl_editor_selection.Size.Width, editorWindow.Pnl_editor_selection.Size.Height - 25);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Adds the SceneElementCategories to the ComboBox of the ElementSelectionPanel.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/13/2014. </remarks>
        ///
        /// 
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void populateComboBox()
        {
            editorWindow.Cmb_editor_selection_toolSelection.Items.Clear();
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
                    if (element.IsInstanceOfType(e.Dummy))
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
