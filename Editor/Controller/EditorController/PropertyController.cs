using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Model.Project;
using ARdevKit;

namespace Controller.EditorController
{
    /// <summary>
    /// The PropertyController contains static methods to add and edit customUserEvents.
    /// </summary>
    class PropertyController
    {
        private EditorWindow editorWindowController;
        
        public PropertyController(EditorWindow ew)
        {
            editorWindowController = ew;
        }

        [Obsolete("addCustomUserEvent() : File is completly invalid, because with the original method signature it is impossible to implement."
            + "Please use the method addCustomUserEvent(selectedElement : AbstractAugmentation, content : string[]) instead.", true)]
        public File addCustomUserEvent()
        { throw new NotImplementedException(); }

        [Obsolete("editCustomUserEvent(customUserEvent : File) is completly invalid, because with the original method signature it is impossible to implement."
            + "Please use the method setCustomUserEventContent(selectedElement : AbstractAugmentation, name : string, content : string[]) instead.", true)]
        public void editCustomUserEvent(File customUserEvent)
        { throw new NotImplementedException(); }

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

        public void updatePropertyPanel(IPreviewable selectedElement)
        {
            throw new NotImplementedException();
        }
    }
}
