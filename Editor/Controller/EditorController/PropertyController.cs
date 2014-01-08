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
            string name = "customUserEvent" + (selectedElement.listCounter() + 1);
            selectedElement.addCustomUserEvent(name, content);
        }

        /// <summary>
        /// Deletes a customUserEvent from a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        public void deleteCustomUserEvent(AbstractAugmentation selectedElement, string name)
        {
            selectedElement.deleteCustomUserEvent(name);
        }

        /// <summary>
        /// Gets the content of a customUserEvent from a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected element</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        /// <returns>Returns the content of the customUserEvent</returns>
        public string[] getCustomUserEventContent(AbstractAugmentation selectedElement, string name)
        {
            return selectedElement.getCustomUserEventContent(name);
        }

        /// <summary>
        /// Sets (or edit, whatever you want to call it) the content of a customUserEvent from the selected Element.
        /// </summary>
        /// <param name="selectedElement">Current selected element</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        /// <param name="content">The new/edited content of the customUserEvent</param>
        public void setCustomUserEventContent(AbstractAugmentation selectedElement, string name, string[] content)
        {
            selectedElement.editCustomUserEventContent(name, content);
        }

        public void updatePropertyPanel(IPreviewable selectedElement)
        {
            throw new NotImplementedException();
        }
    }
}
