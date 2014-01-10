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
    /// 
    /// This class is till TODO!!! Set and Get methods from some variables are still missing, also some methods from the 
    /// design patern and comments for the documentation.
    /// 
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

        public AbstractAugmentation()
        {
            customUserEvent = new List<CustomUserEvent>();
            coordinateSystemId = 0;
            isVisible = false;
        }

        public AbstractAugmentation(int coordId, bool isVisible)
        {
            customUserEvent = new List<CustomUserEvent>();
            coordinateSystemId = coordId;
            this.isVisible = isVisible;
        }

        /// <summary>
        /// Gets the list of all customUserEvent of this augmentation. (Only readable)
        /// </summary>
        public List<CustomUserEvent> CustomUserEventList
        {
            get { return customUserEvent; }

        }
    }

    /// <summary>
    /// Just a class for AbstractAugmentations. With this we are able to List all the customUserEvents.
    /// </summary>
    public class CustomUserEvent
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

