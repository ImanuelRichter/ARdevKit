using ARdevKit.Controller.EditorController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARdevKit.View
{
    /**
     * <summary>    Panel the scene element category. Is used to display multiple ElementIcons in a row. </summary>
     *
     * <remarks>    Robin, 14.01.2014. </remarks>
     */

    class SceneElementCategoryPanel : System.Windows.Forms.FlowLayoutPanel 
    {
        /**
         * <summary>    The category of the elements in the panel. </summary>
         */

        private SceneElementCategory category;

        /**
         * <summary>    Gets or sets the category. </summary>
         *
         * <value>  The category. </value>
         */

        public SceneElementCategory Category
        {
            get { return category; }
            set { category = value; }
        }

        /**
         * <summary>    Constructor. Sets the category. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="category">  The category of the elements in the panel. </param>
         */

        public SceneElementCategoryPanel(SceneElementCategory category)
            : base()
        {
            AutoScroll = true;
            //Visible = false;
            Size = new Size(0, 0);
            FlowDirection = FlowDirection.TopDown;
           
            this.category = category;
            foreach (SceneElement e in category.SceneElements)
            {
                add(e.ElementIcon);
            }
            WrapContents = false;
        }

        public void add(ElementIcon icon)
        {
            Controls.Add(icon);
        }

        public override string ToString()
        {
            return category.ToString();
        }

        public string CategoryName{
            get { return category.Name; }
        }
    }
}
