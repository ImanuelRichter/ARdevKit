using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Model.Project;

namespace Controller.EditorController
{
    /// <summary>
    /// The PropertyController contains static methods to add and edit customUserEvents.
    /// </summary>
    class PropertyController
    {
        /// <summary>
        /// Adds a customUserEvent to a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="content">Content of the customUserEvent</param>
        public static void addCustomUserEvent(AbstractAugmentation selectedElement, string[] content)
        {
            string name = "customUserEvent" + (selectedElement.listCounter() + 1);
            selectedElement.addCustomUserEvent(name, content);
        }

        /// <summary>
        /// Deletes a customUserEvent from a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected AbstractAugmentation</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        public static void deleteCustomUserEvent(AbstractAugmentation selectedElement, string name)
        {
            selectedElement.deleteCustomUserEvent(name);
        }

        /// <summary>
        /// Gets a customUserEvent from a selected element.
        /// </summary>
        /// <param name="selectedElement">Current selected element</param>
        /// <param name="name">Name/ID of the customUserEvent</param>
        /// <returns>Returns the content of the customUserEvent</returns>
        public static string[] getCusomUserEvent(AbstractAugmentation selectedElement, string name)
        {
            return selectedElement.getCustomUserEvent(name);
        }

        public static void updatePropertyPanel(IPreviewable selectedElement)
        {
            throw new NotImplementedException();
        }
    }
}
