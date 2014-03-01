using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using ARdevKit;
using ARdevKit.Model.Project;

namespace Controller.EditorController
{
    /// <summary>
    /// The PropertyController contains events for the propertyGrid
    /// </summary>
    class PropertyController
    {
        /// <summary>
        /// Instance of the EditorWindow(Controller)
        /// </summary>
        private EditorWindow ew;

        /// <summary>
        /// Constructor of the class. It adds automatically all events which belongs to the propertyGrid.
        /// </summary>
        /// <param name="ew"></param>
        public PropertyController(EditorWindow ew)
        {
            this.ew = ew;
            ew.PropertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(changedProperty);
        }

        /// <summary>
        /// See issue #13 for reason of these invalid methods etc.
        /// </summary>
        [Obsolete("addCustomUserEvent() : File is completly invalid, because with the original method signature it is impossible to implement.", true)]
        public /*File*/void addCustomUserEvent()
        { throw new NotImplementedException(); }

        /// <summary>
        /// See issue #13 for reason of these invalid methods etc.
        /// </summary>
        [Obsolete("editCustomUserEvent(customUserEvent : File) is completly invalid, because with the original method signature it is impossible to implement.", true)]
        public void editCustomUserEvent(/*File customUserEvent*/)
        { throw new NotImplementedException(); }

        /// <summary>
        /// Event which catches all changes of the propertyGrid, which should have an impact of the elements on the preview panel.
        /// </summary>
        /// <param name="sender">Sender ~...</param>
        /// <param name="e">Event argument</param>
        private void changedProperty(object sender, PropertyValueChangedEventArgs e)
        {
            /*==============================================================================*/
            // Changes which should be undone if set to null

            // Checks if a image/option path is set to null
            if (string.Equals(e.ChangedItem.Label.ToString(), "Options", StringComparison.Ordinal))
            {
                if (string.Equals((string)e.ChangedItem.Value, "", StringComparison.Ordinal))
                {
                    ((Chart)ew.CurrentElement).Options = e.OldValue.ToString();

                    return;
                }
            }

            // Checks if picturePath has been changed
            if (String.Equals(e.ChangedItem.Label.ToString(), "PicturePath", StringComparison.Ordinal))
            {
                if (string.Equals((string)e.ChangedItem.Value, "", StringComparison.Ordinal))
                    ((PictureMarker)ew.CurrentElement).PicturePath = e.OldValue.ToString();
                else
                    ew.PreviewController.findBox(ew.CurrentElement).Load(e.ChangedItem.Value.ToString());

                return;
            }

            // Checks if imagePath has been changed (it's indifferent if it's an augmentation or trackable)
            if (String.Equals(e.ChangedItem.Label.ToString(), "ImagePath", StringComparison.Ordinal))
            {
                if (string.Equals((string)e.ChangedItem.Value, "", StringComparison.Ordinal))
                {
                    if (ew.CurrentElement is ImageAugmentation)
                        ((ImageAugmentation)ew.CurrentElement).ImagePath = e.OldValue.ToString();
                    if (ew.CurrentElement is ImageTrackable)
                        ((ImageTrackable)ew.CurrentElement).ImagePath = e.OldValue.ToString();
                }
                else
                {
                    ((ImageAugmentation)ew.CurrentElement).ImagePath = e.ChangedItem.Value.ToString();
                    this.ew.PreviewController.reloadPreviewable((AbstractAugmentation)this.ew.CurrentElement);
                    PictureBox temp = this.ew.PreviewController.findBox(this.ew.CurrentElement);
                    temp.BorderStyle = BorderStyle.Fixed3D;
                    temp.BringToFront();
                }
                    
                    

                return;
            }

            // Checks if Data has been changed (only for FileSource for now)
            if (string.Equals(e.ChangedItem.Label.ToString(), "Data", StringComparison.Ordinal))
            {
                if (string.Equals((string)e.ChangedItem.Value, "", StringComparison.Ordinal))
                {
                    if (((Chart)ew.CurrentElement).Source is FileSource)
                        ((FileSource)((Chart)ew.CurrentElement).Source).Data = e.OldValue.ToString();

                    return;
                }
            }

            /*=================================================================================*/
            // Changes which enable/disables button if set to null or was null before the change

            // Checks if Query has been changed
            if (String.Equals(e.ChangedItem.Label.ToString(), "Query", StringComparison.Ordinal))
            {
                if (string.Equals((string)e.ChangedItem.Value, "", StringComparison.Ordinal))
                {
                    (ew.PreviewController.findBox(ew.CurrentElement).ContextMenu).MenuItems[6].Enabled = false;

                    return;
                }

                if (e.OldValue == null || string.Equals(e.OldValue.ToString(), "", StringComparison.Ordinal))
                {
                    (ew.PreviewController.findBox(ew.CurrentElement).ContextMenu).MenuItems[6].Enabled = true;

                    return;
                }
            }

            /*=================================================================================*/
            // Changes which changes things in the previewPanel

            // Checks if X/Y position has been changed
            if ((String.Equals(e.ChangedItem.Label.ToString(), "X", StringComparison.Ordinal)
                || String.Equals(e.ChangedItem.Label.ToString(), "Y", StringComparison.Ordinal))
                && !(String.Equals(e.ChangedItem.Parent.Label.ToString(), "Rotating", StringComparison.Ordinal)))
            {
                this.ew.PreviewController.reloadPreviewable((AbstractAugmentation)this.ew.CurrentElement);
                PictureBox temp = this.ew.PreviewController.findBox(this.ew.CurrentElement);
                        temp.BorderStyle = BorderStyle.Fixed3D;
                        temp.BringToFront();

                return;
            }

            // Checks if Z has been changed
            if (String.Equals(e.ChangedItem.Label.ToString(), "Z", StringComparison.Ordinal)
                && String.Equals(e.ChangedItem.Parent.Label.ToString(), "Rotation", StringComparison.Ordinal))
            {
                ew.PreviewController.rotateAugmentation(this.ew.CurrentElement);

                return;
            }

            // Checks if height/width has been changed
            if (ew.CurrentElement is Abstract2DAugmentation)
            {
                if (string.Equals(e.ChangedItem.Label.ToString(), "Height", StringComparison.Ordinal)
                    || string.Equals(e.ChangedItem.Label.ToString(), "Width", StringComparison.Ordinal))
                {
                    this.ew.PreviewController.reloadPreviewable((AbstractAugmentation)this.ew.CurrentElement);
                    PictureBox temp = this.ew.PreviewController.findBox(this.ew.CurrentElement);
                        temp.BorderStyle = BorderStyle.Fixed3D;
                        temp.BringToFront();
                    return;
                }
            }

            // Checks if the size of a trackable has been changed.
            if (string.Equals(e.ChangedItem.Label.ToString(), "Size", StringComparison.Ordinal))
            {
                ew.PreviewController.reloadPreviewPanel(ew.PreviewController.index);

                return;
            }

            /*=================================================================================*/
            // Miscellaneous changes


            // Checks if the MatrixID has been changed. If changed, it checks, if the id is equal to another id in
            // the project to generate a new id. This should prevent having two identical id's. 
            if (string.Equals(e.ChangedItem.Label.ToString(), "MatrixID", StringComparison.Ordinal))
            {
                if (ew.project.existTrackable((int)e.ChangedItem.Value))
                {
                    IDMarker marker = (IDMarker)ew.CurrentElement;
                    marker.MatrixID = ew.project.nextID();
                }
                this.ew.PreviewController.findBox(this.ew.CurrentElement).Image = this.ew.PreviewController.scaleIPreviewable(this.ew.CurrentElement);
                this.ew.PreviewController.reloadPreviewPanel(this.ew.PreviewController.index);
                return;
            }
        }
    }
}
