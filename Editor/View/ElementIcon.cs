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
     * <summary>    An element icon is used to display a registered SceneElement in the SceneSelectionPanel. </summary>
     *
     * <remarks>    Robin, 14.01.2014. </remarks>
     */

    class ElementIcon : System.Windows.Forms.TableLayoutPanel
    {
        /**
         * <summary>    The SceneElement that this Element Icon represents. </summary>
         */
        private SceneElement element;

        /**
         * <summary>    The label that displays the name. </summary>
         */

        private Label label;

        /**
         * <summary>    The pictureBox that shows the icon. </summary>
         */

        private PictureBox picture;

        /**
         * <summary>    The editor window. </summary>
         */

        private EditorWindow editorWindow;

        /**
         * <summary>    Constructor. Creates the text label and the pictureBox and adds them to the Panel. Adds event handlers. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="element">   The SceneElement that this Element Icon represents. </param>
         */

        public ElementIcon(SceneElement element, EditorWindow ew) : base()
        {
            this.element = element;
            ColumnCount = 1;
            RowCount = 2;
            EventHandler clickHandler=new EventHandler(onClick);
            DragEventHandler dragEnterHandler = new DragEventHandler(onDragEnter);
            DragEventHandler dragDropHandler = new DragEventHandler(onDragDrop);
            Click += clickHandler;
            DragEnter += dragEnterHandler;
            DragDrop += dragDropHandler;

            label = new Label();
            label.Text = element.ToString();
            label.AutoSize = true;
            label.Anchor = AnchorStyles.None;
            label.Click += clickHandler;
            label.DragEnter += dragEnterHandler;
            label.DragDrop += dragDropHandler;

            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap MyImage = new Bitmap(element.Icon);
            picture.ClientSize = new Size(121, 121);
            picture.Image = (Image) MyImage;
            picture.Click += clickHandler;
            picture.DragEnter += dragEnterHandler;
            picture.DragDrop += dragDropHandler;

            Controls.Add(picture, 0, 0);
            Controls.Add(label, 0, 1);
            AutoSize = true;
        }

        /**
         * <summary>    Raises the click event of the panel, label and picturebox. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onClick(object sender, EventArgs e)
        {
            //TODO
        }

        /**
         * <summary>    Raises the drag event of the panel, label and picturebox. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onDragEnter(object sender, DragEventArgs e)
        {
            //TODO
        }

        /**
         * <summary>    Raises the drag event of the panel, label and picturebox. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onDragDrop(object sender, DragEventArgs e)
        {
            //TODO
        }
    }
}
