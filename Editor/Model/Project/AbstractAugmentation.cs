using ARdevKit.Controller.ProjectController;
using ARdevKit.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// describes an <see cref="AbstractAugmentation"/>, which is bound
    /// to a certain <see cref="AbstractTrackable"/>.
    /// is <see cref="IPreviewable"/> 
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class AbstractAugmentation : IPreviewable
    {
        /// <summary>   The identifier. </summary>
        protected string id;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General"), ReadOnly(true)]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Describes if the <see cref="AbstractAugmentation"/>
        /// is seen using AREL, even if the associated <see cref="AbstractTrackable"/>
        /// is not recognized.
        /// </summary>
        private bool isVisible;
        /// <summary>
        /// Get or set if the <see cref="AbstractAugmentation"/> is 
        /// visible the whole time using AREL or not.
        /// </summary>
        [CategoryAttribute("General"), DefaultValueAttribute(true)]
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        private EventFile eventFile;

        [Browsable(false)]
        public EventFile EventFile
        {
            get 
            {
                if (eventFile == null)
                {
                    if (Events != null)
                    {
                        eventFile = new EventFile(Path.Combine("Events", ID + "_events.js"));
                        foreach (Event e in Events)
                            eventFile.AddBlock(e);
                        return eventFile;
                    }
                    else
                        return null;
                }
                else
                {
                    eventFile.Clear();
                    foreach (Event e in Events)
                        eventFile.AddBlock(e);
                    return eventFile;
                }
            }
            set { eventFile = value; }
        }

        private List<Event> events;
        [Browsable(false)]
        public List<Event> Events
        {
            get
            {
                events = new List<Event>();
                if (OnTouchStarted != null)
                    events.Add(OnTouchStarted);
                if (OnTouchEnded != null)
                    events.Add(OnTouchEnded);
                if (OnVisible != null)
                    events.Add(OnVisible);
                if (OnInvisible != null)
                    events.Add(OnInvisible);
                if (OnLoaded != null)
                    events.Add(OnLoaded);
                if (OnUnloaded != null)
                    events.Add(OnUnloaded);
                if (CustomEvents != null)
                    foreach (Event e in CustomEvents)
                        events.Add(e);
                return events;
            }
            set { events = value; }
        }

        private Event onTouchStarted;

        [CategoryAttribute("Events")]
        public Event OnTouchStarted
        {
            get { return onTouchStarted; }
            set { onTouchStarted = value; }
        }

        private Event onTouchEnded;

        [CategoryAttribute("Events")]
        public Event OnTouchEnded
        {
            get { return onTouchEnded; }
            set { onTouchEnded = value; }
        }

        private Event onVisible;

        [CategoryAttribute("Events")]
        public Event OnVisible
        {
            get { return onVisible; }
            set { onVisible = value; }
        }

        private Event onInvisible;

        [CategoryAttribute("Events")]
        public Event OnInvisible
        {
            get { return onInvisible; }
            set { onInvisible = value; }
        }

        private Event onLoaded;

        [CategoryAttribute("Events")]
        public Event OnLoaded
        {
            get { return onLoaded; }
            set { onLoaded = value; }
        }

        private Event onUnloaded;

        [CategoryAttribute("Events")]
        public Event OnUnloaded
        {
            get { return onUnloaded; }
            set { onUnloaded = value; }
        }

        private List<Event> customEvents;

        [CategoryAttribute("Events")]
        public List<Event> CustomEvents
        {
            get { return customEvents; }
            set { customEvents = value; }
        }


        /// <summary>
        /// Vector to describe the position on the PreviewPanel, and later
        /// to position it on the coordinatesystem given in AREL.
        /// </summary>
        private Vector3D translationVector;
        /// <summary>
        /// Get or set the position of the <see cref="AbstractAugmentation"/>.
        /// </summary>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public Vector3D Translation
        {
            get { return translationVector; }
            set { translationVector = value; }
        }

        /// <summary>
        /// Vector, to describes the scaling of the <see cref="AbstractAugmentation"/>
        /// in x, y and z direction. Is used in AREL.
        /// </summary>
        private Vector3D scalingVector;
        /// <summary>
        /// gets or sets the scaling which is applied to the original 
        /// <see cref="AbstractAugmentation"/>
        /// </summary>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public Vector3D Scaling
        {
            get { return scalingVector; }
            set { scalingVector = value; }
        }

        /// <summary>
        /// Vector, to describes the rotation (euler) of the <see cref="AbstractAugmentation"/> in
        /// x, y and z direction.
        /// </summary>
        private Vector3D rotationVector;
        /// <summary>
        /// gets or sets the Vector
        /// </summary>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public Vector3D Rotation
        {
            get { return rotationVector; }
            set { rotationVector = value; }
        }

        /// <summary>
        /// The AbstractTrackable with which this AbstractAugmentation is linked.
        /// It is visible in the same Scene as the trackable.
        /// </summary>
        protected AbstractTrackable trackable;
        /// <summary>
        /// Get or set a trackable to the augmentation.
        /// </summary>
        [Browsable(false)]
        public AbstractTrackable Trackable
        {
            get { return trackable; }
            set { trackable = value; }
        }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractAugmentation"/> class,
        /// but can be used in inheriting classes.
        /// Using standard values, such as emptyLists, vectors with 0 as coordinate and null.
        /// </summary>
        protected AbstractAugmentation()
        {
            isVisible = true;
            translationVector = new Vector3D(0, 0, 0);
            scalingVector = new Vector3D(0, 0, 0);
            rotationVector = new Vector3D(0, 0, 0);
            trackable = null;

        }

        /// <summary>
        /// Initializes no new instance of the <see cref="AbstractAugmentation"/> class,
        /// but can be used in inheriting classes.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible] using AREL.</param>
        /// <param name="translationVector">The translation vector.</param>
        /// <param name="scaling">The scaling.</param>
        /// <param name="trackable">The trackable.</param>
        protected AbstractAugmentation(bool isVisible,
            Vector3D translationVector, Vector3D scaling, AbstractTrackable trackable)
        {
            this.isVisible = isVisible;
            this.translationVector = translationVector;
            scalingVector = scaling;
            this.trackable = trackable;
        }

        /// <summary>
        /// Method to create an instance of the CustomUserEvent.
        /// </summary>
        public Event createEvent(Event.Types type)
        {
            Event newEvent = null;
            switch (type)
            {
                case (Event.Types.ONTOUCHSTARTED):
                    return OnTouchStarted = new Event(id, type);
                    break;
                case (Event.Types.ONTOUCHENDED):
                    return OnTouchEnded = new Event(id, type);
                    break;
                case (Event.Types.ONVISIBLE):
                    return OnVisible = new Event(id, type);
                    break;
                case (Event.Types.ONINVISIBLE):
                    return OnInvisible = new Event(id, type);
                    break;
                case (Event.Types.ONLOADED):
                    return OnLoaded = new Event(id, type);
                    break;
                case (Event.Types.ONUNLOADED):
                    return OnUnloaded = new Event(id, type);
                    break;
                case (Event.Types.CUSTOM):
                    Event customEvent = new Event(id, type);
                    if (CustomEvents == null)
                        CustomEvents = new List<Event>();
                    CustomEvents.Add(customEvent);
                    return customEvent;
                    break;
                default:
                    return null;
                    break;
            }
        }

        /// <summary>
        /// An abstract method, to accept a <see cref="AbstractProjectVisitor"/>
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this element</param>
        public virtual void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(EventFile);
        }

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative Bitmap</returns>
        public abstract Bitmap getPreview(string projectPath);

        /// <summary>
        /// returns a <see cref="Bitmap"/> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable"/>
        /// </summary>
        /// <returns>a representative iconized Bitmap</returns>
        public abstract Bitmap getIcon();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clean up (remove created/copied files and directories). </summary>
        ///
        /// <remarks>   Imanuel, 31.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void CleanUp();

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 22.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public abstract object Clone();

        /// <summary>
        /// This method is called by the previewController when a new instance of the element is added to the Scene. It sets "must-have" properties.
        /// </summary>
        /// <param name="ew">The ew.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool initElement(EditorWindow ew)
        {
            int count = 0;
            bool found = true;
            String newID = "";
            while (found)
            {
                found = false;
                count++;
                foreach (AbstractTrackable t in ew.project.Trackables)
                {
                    newID = this.GetType().Name + count;
                    //make first letter lowercase
                    newID = newID[0].ToString().ToLower() + newID.Substring(1);
                    if (t != null)
                    {
                        foreach (AbstractAugmentation a in t.Augmentations)
                        {
                            if (this.GetType().Equals(a.GetType()))
                            {
                                if (a.ID == newID)
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            id = newID;
            if (Events != null)
            {
                foreach (Event e in Events)
                {
                    if (e != null)
                    {
                        e.AugmentationID = id;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return id;
        }
    }
}

