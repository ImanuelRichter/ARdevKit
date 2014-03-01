using ARdevKit.Controller.EditorController;
using ARdevKit.Model.Project;
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
         * <summary>    Gets the element. </summary>
         *
         * <value>  The element. </value>
         */

        public SceneElement Element {
            get { return element; }
        }

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
         * <summary>    The point where the user clicked on the control. </summary>
         */

        private Point clickPoint;

        /**
         * <summary>    true if the mouse is pressed. </summary>
         */

        private Boolean isMousePressed = false;

        /**
         * <summary>    Gets the editor window. </summary>
         *
         * <value>  The editor window. </value>
         */

        public EditorWindow EditorWindow
        {
            get { return editorWindow; }
        }

        /// <summary>    Constructor. Creates the text label and the pictureBox and adds them to the Panel. Adds event handlers. </summary>
        /// <param name="element">The element.</param>
        /// <param name="ew">The ew.</param>
        /// <remarks>    Robin, 14.01.2014. </remarks>
        public ElementIcon(SceneElement element, EditorWindow ew) : base()
        {
            this.element = element;
            this.editorWindow = ew;
            ColumnCount = 1;
            RowCount = 2;
            MouseEventHandler mouseDownHandler = new MouseEventHandler(onMouseDown);
            MouseEventHandler mouseUpHandler = new MouseEventHandler(onMouseUp);
            MouseEventHandler mouseMoveHandler = new MouseEventHandler(onMouseMove);
            EventHandler mouseLeaveHandler = new EventHandler(onMouseLeave);
            MouseMove += mouseMoveHandler;
            MouseDown += mouseDownHandler;
            MouseUp += mouseUpHandler;
            MouseLeave += mouseLeaveHandler;

            label = new Label();
            label.Text = element.ToString();
            label.AutoSize = true;
            label.Anchor = AnchorStyles.None;
            label.MouseMove += mouseMoveHandler;
            label.MouseDown += mouseDownHandler;
            label.MouseUp += mouseUpHandler;
            label.MouseLeave += mouseLeaveHandler;

            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            Bitmap MyImage = new Bitmap(element.Icon);
            picture.ClientSize = new Size(121, 121);
            picture.Image = (System.Drawing.Image) MyImage;
            picture.MouseMove += mouseMoveHandler;
            picture.MouseDown += mouseDownHandler;
            picture.MouseUp += mouseUpHandler;
            picture.MouseLeave += mouseLeaveHandler;

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
            if (editorWindow.PreviewController.currentMetaCategory != MetaCategory.Source)
            {
                int y = editorWindow.Pnl_editor_preview.Height / 2;
                int x = editorWindow.Pnl_editor_preview.Width / 2;
                IPreviewable element = (IPreviewable)this.element.Prototype.Clone();
                editorWindow.PreviewController.addPreviewable(element, new ARdevKit.Model.Project.Vector3D(x, y, 1));
            }
        }

        /**
         * <summary>    Raises the mouse down event. Initiates drag&drop. </summary>
         *
         * <remarks>    Robin, 18.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onMouseDown(object sender, EventArgs e)
        {
            clickPoint = PointToClient(Cursor.Position);
            isMousePressed = true;
        }

        /**
         * <summary>    Raises the mouse up event. </summary>
         *
         * <remarks>    Robin, 19.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onMouseUp(object sender, EventArgs e)
        {
            isMousePressed = false;
            Point current = PointToClient(Cursor.Position);
            if (Math.Abs(clickPoint.X - current.X) < 10 && Math.Abs(clickPoint.Y - current.Y) < 10)
            {
                onClick(sender, e);
            }
            else
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
        }

        /**
         * <summary>    Raises the mouse move event. </summary>
         *
         * <remarks>    Robin, 19.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onMouseMove(object sender, EventArgs e)
        {
            if (isMousePressed)
            {
                Point current = PointToClient(Cursor.Position);
                if (Math.Abs(clickPoint.X - current.X) > 10 || Math.Abs(clickPoint.Y - current.Y) > 10)
                {
                    DoDragDrop(this, DragDropEffects.Move);
                }
            }
        }

        /**
         * <summary>    Raises the mouse leave event. </summary>
         *
         * <remarks>    Robin, 19.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information to send to registered event handlers. </param>
         */

        public void onMouseLeave(object sender, EventArgs e)
        {
            isMousePressed = false;
        }
    }
}
