using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Project
{
    /// <summary>
    /// This class is an abstract Element which every AbstractTrackable has. Other elements like a diagramm
    /// or images must be succeeded by this class.
    /// </summary>
    public class AbstractAugmentation
    {
        /// <summary>
        /// A list of all customUserEvents the current selected Element has.
        /// </summary>
        private List<CustomUserEvent> customUserEvent;

        /// <summary>
        /// ToDo
        /// </summary>
        private int coordinateSystemId;

        /// <summary>
        /// ToDo
        /// </summary>
        private bool isVisible;

        public int listCounter()
        {
            return customUserEvent.Count;
        }

        /// <summary>
        /// Adds a User generated customUserEvent to the List.
        /// </summary>
        /// <param name="name">Name of the customUserEvent</param>
        /// <param name="content">The content of the customUserEvent</param>
        public void addCustomUserEvent(string name, string[] content)
        {
            customUserEvent.Add(new CustomUserEvent(name, content));
        }

        /// <summary>
        /// Method for getting the content of an customUserEvent
        /// </summary>
        /// <param name="name">Name of the customUserEvent</param>
        /// <returns>Content of the customUserEvent</returns>
        public string[] getCustomUserEvent(string name)
        {
            foreach (CustomUserEvent cue in customUserEvent)
            {
                if (cue.Name == name)
                    return cue.Content;
            }
            return null;
        }

        /// <summary>
        /// Just an intern class for AbstractAugmentations. With this we are able to List all the customUserEvents.
        /// </summary>
        private class CustomUserEvent
        {
            private string name;
            private string[] content;

            public CustomUserEvent(string name, string[] content)
            {
                this.name = name;
                this.content = content;
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string[] Content
            {
                get { return content; }
                set { content = value; }
            }
        }
    }
}

