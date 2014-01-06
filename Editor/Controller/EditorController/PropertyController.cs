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
        public static void addCustomUserEvent(AbstractAugmentation selectedElement, string[] content)
        {
            string name = "customUserEvent" + (selectedElement.listCounter() + 1);
            selectedElement.addCustomUserEvent(name, content);
        }

        public static string[] getCusomUserEvent(AbstractAugmentation selectedElement, string name)
        {
            return selectedElement.getCustomUserEvent(name);
        }

        public static void updatePropertyPanel(IPreviewable selectedElement)
        {

        }
    }
}
