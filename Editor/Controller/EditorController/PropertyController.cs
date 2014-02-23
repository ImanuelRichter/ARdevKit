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
    /// The PropertyController contains static methods to add and edit customUserEvents.
    /// </summary>
    class PropertyController
    {
        private EditorWindow ew;

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
                    ew.PreviewController.findBox(ew.CurrentElement).Load(e.ChangedItem.Value.ToString());

                return;
            }
            
            /*=================================================================================*/



            // Checks if the MatrixID has been changed. If changed, it checks, if the id is equal to another id in
            // the project to generate a new id. This should prevent having two identical id's. 
            if (string.Equals(e.ChangedItem.Label.ToString(), "MatrixID", StringComparison.Ordinal))
            {
                if (ew.project.existTrackable((int)e.ChangedItem.Value))
                {
                    IDMarker marker = (IDMarker)ew.CurrentElement;
                    marker.MatrixID = ew.project.nextID();
                }

                return;
            }

            /*=================================================================================*/

            // Checks if X/Y position has been changed
            if (String.Equals(e.ChangedItem.Label.ToString(), "X", StringComparison.Ordinal)
                || String.Equals(e.ChangedItem.Label.ToString(), "Y", StringComparison.Ordinal))
            {
                ew.PreviewController.updateTranslation();
                ew.PreviewController.updateScale();

                return;
            }

            // Checks if height/width has been changed
            if (ew.CurrentElement is Abstract2DAugmentation)
            {
                if (string.Equals(e.ChangedItem.Label.ToString(), "Height", StringComparison.Ordinal)
                    || string.Equals(e.ChangedItem.Label.ToString(), "Width", StringComparison.Ordinal))
                {
                    ew.PreviewController.updateScale();

                    return;
                }
            }

            // Checks if the size of a trackable has been changed.
            if (string.Equals(e.ChangedItem.Label.ToString(), "Size", StringComparison.Ordinal))
            {
                ew.PreviewController.reloadPreviewPanel(ew.PreviewController.index);

                return;
            }
        }
    }
}
