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
        [Obsolete("addCustomUserEvent() : File is completly invalid, because with the original method signature it is impossible to implement."
            + "Please use the method addCustomUserEvent(selectedElement : AbstractAugmentation, content : string[]) instead.", true)]
        public /*File*/void addCustomUserEvent()
        { throw new NotImplementedException(); }

        [Obsolete("editCustomUserEvent(customUserEvent : File) is completly invalid, because with the original method signature it is impossible to implement."
            + "Please use the method setCustomUserEventContent(selectedElement : AbstractAugmentation, name : string, content : string[]) instead.", true)]
        public void editCustomUserEvent(/*File customUserEvent*/)
        { throw new NotImplementedException(); }

        private void changedProperty(object sender, PropertyValueChangedEventArgs e)
        {
            if (String.Equals(e.ChangedItem.Label.ToString(), "X", StringComparison.Ordinal)
                || String.Equals(e.ChangedItem.Label.ToString(), "Y", StringComparison.Ordinal))
            {
                ew.PreviewController.updateTranslation();
                ew.PreviewController.updateScale();

                return;
            }

            if (String.Equals(e.ChangedItem.Label.ToString(), "PicturePath", StringComparison.Ordinal))
            {
                ew.PreviewController.findBox(ew.CurrentElement).Load(e.ChangedItem.Value.ToString());

                return;
            }

            if (String.Equals(e.ChangedItem.Label.ToString(), "ImagePath", StringComparison.Ordinal))
            {
                ew.PreviewController.findBox(ew.CurrentElement).Load(e.ChangedItem.Value.ToString());

                return;
            }

            if (string.Equals(e.ChangedItem.Label.ToString(), "MatrixID", StringComparison.Ordinal))
            {
                if (ew.project.existTrackable((int)e.ChangedItem.Value))
                {
                    IDMarker marker = (IDMarker)ew.CurrentElement;
                    marker.MatrixID = ew.project.nextID();
                }

                return;
            }

            if (ew.CurrentElement is Abstract2DAugmentation)
            {
                if (string.Equals(e.ChangedItem.Label.ToString(), "Height", StringComparison.Ordinal)
                    || string.Equals(e.ChangedItem.Label.ToString(), "Width", StringComparison.Ordinal))
                {
                    ew.PreviewController.updateScale();

                    return;
                }
            }

            if (string.Equals(e.ChangedItem.Label.ToString(), "Size", StringComparison.Ordinal))
            {
                ew.PreviewController.reloadPreviewPanel(ew.PreviewController.index);
                // ew.PreviewController.rescalePreviewPanel();

                return;
            }

        }

        /*
        /// <summary>
        /// Adds a customUserEvent to a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="content">Content of the customUserEvent</param>
        public void addCustomUserEvent(AbstractAugmentation selectedElement, string[] content)
        {
            int counter = selectedElement.CustomUserEventList.Count;
            selectedElement.CustomUserEventList.Add(new CustomUserEvent("customUserEvent" + counter, content));
        }

        /// <summary>
        /// Adds a customUserEvent to a selected element. 
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="name">Name of the customUserEvent</param>
        /// <param name="content">Content of the customUserEvent</param>
        public void addCustomUserEvent(AbstractAugmentation selectedElement, string name, string[] content)
        {
            selectedElement.CustomUserEventList.Add(new CustomUserEvent(name, content));
        }

        /// <summary>
        /// Deletes a customUserEvent from a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        public void deleteCustomUserEvent(AbstractAugmentation selectedElement, string name)
        {
            foreach(CustomUserEvent cue in selectedElement.CustomUserEventList)
            {
                if (String.Equals(cue.Name, name, StringComparison.Ordinal))
                    selectedElement.CustomUserEventList.Remove(cue);
            }
        }

        /// <summary>
        /// Sets (or change, whatever you want to call it) the name of the customUserEvent.
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="oldName">Current name of the customUserEvent</param>
        /// <param name="newName">The new name of the customUserEvent</param>
        public void setCustomUserEventName(AbstractAugmentation selectedElement, string oldName, string newName)
        {
            if (newName.Length == 0)
                throw new ArgumentException("The name must have at leat one symbol.");

            foreach (CustomUserEvent cue in selectedElement.CustomUserEventList)
            {
                if (String.Equals(cue.Name, oldName, StringComparison.Ordinal))
                    cue.Name = newName;
            }
        }

        /// <summary>
        /// Gets the content of a customUserEvent from a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected element</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        /// <returns>Returns the content of the customUserEvent. Returns NULL, if the customUserEvent was not found in the list.</returns>
        public string[] getCustomUserEventContent(AbstractAugmentation selectedElement, string name)
        {
            foreach (CustomUserEvent cue in selectedElement.CustomUserEventList)
            {
                if (string.Equals(cue.Name, name, StringComparison.Ordinal))
                    return cue.Content;
            }

            return null;
        }

        /// <summary>
        /// Sets (or edit, whatever you want to call it) the content of a customUserEvent from the selected Element.
        /// </summary>
        /// <param name="selectedElement">Current selected element</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        /// <param name="content">The new/edited content of the customUserEvent</param>
        public void setCustomUserEventContent(AbstractAugmentation selectedElement, string name, string[] content)
        {
            foreach (CustomUserEvent cue in selectedElement.CustomUserEventList)
            {
                if (string.Equals(cue.Name, name, StringComparison.Ordinal))
                    cue.Content = content;
            }
        }

        /* possible unnecessary method. see issue #13 for reason of it.
        public void updatePropertyPanel(IPreviewable selectedElement)
        {
            throw new NotImplementedException();
        }
         * */
    }
}
