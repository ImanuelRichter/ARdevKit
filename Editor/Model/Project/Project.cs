using ARdevKit.Controller.ProjectController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    ///     Encapsulates everything, that is needed for an AR-Application
    ///     and so this the element, which the user saves, loads or exports
    /// </summary>
    [Serializable]
    public class Project
    {
        /// <summary>
        /// The screensize
        /// </summary>
        /// <remarks>geht 26.01.2014 20:20</remarks>
        private ScreenSize screensize;

        /// <summary>
        /// Gets or sets the screensize.
        /// </summary>
        /// <value>
        /// The screensize.
        /// </value>
        /// <remarks>geht 28.01.2014 14:43</remarks>
        public ScreenSize Screensize
        {
            get { return screensize; }
            set { screensize = value; }
        }


        /// <summary>
        /// The name of the project.
        /// </summary>
        private string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Full pathname of the project file.
        /// </summary>
        private string projectPath;
        /// <summary>
        /// Gets or sets the full pathname of the project file.
        /// </summary>
        /// <value>
        /// The full pathname of the project file.
        /// </value>
        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        /// <summary>
        /// The sensor, is depentend on the
        /// used trackables.
        /// </summary>
        protected AbstractSensor sensor;
        /// <summary>
        /// Gets or sets the sensor.
        /// </summary>
        /// <value>
        /// The sensor.
        /// </value>
        public AbstractSensor Sensor
        {
            get { return sensor; }
            set { sensor = value; }
        }

        /// <summary>
        /// The sources used in this <see cref="Project"/>.
        /// </summary>
        private List<AbstractSource> sources;
        /// <summary>
        /// Gets or sets the sources.
        /// </summary>
        /// <value>
        /// The sources.
        /// </value>
        public List<AbstractSource> Sources
        {
            get { return sources; }
            set { sources = value; }
        }

        /// <summary>
        /// The trackables used in this <see cref="Project"/>.
        /// </summary>
        private List<AbstractTrackable> trackables;
        /// <summary>
        /// Gets or sets the trackables.
        /// </summary>
        /// <value>
        /// The trackables.
        /// </value>
        public List<AbstractTrackable> Trackables
        {
            get { return trackables; }
            set { trackables = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project" /> class
        /// with default values.
        /// </summary>
        public Project()
        {
            name = "";
            projectPath = "";
            sensor = null; 
            trackables = new List<AbstractTrackable>();
            sources = new List<AbstractSource>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project" /> class
        /// with specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public Project(string name) : this()
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project" /> class
        /// with specified name and projectPath.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectPath">Full pathname of the project file.</param>
        public Project(string name, string projectPath)
            : this(name)
        {
            this.projectPath = projectPath;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public void Accept(AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractTrackable t in Trackables)
            {
                t.Accept(visitor);
            }
            sensor.Accept(visitor);
        }


        /// <summary>
        /// Returns the associated source, if it is associated with the project.
        /// </summary>
        /// <param name="source">The source, which is searched.</param>
        /// <returns>
        /// the associated source
        /// </returns>
        public AbstractSource findSource(AbstractSource source)
        {
            return this.sources[this.sources.IndexOf(source)];
        }

        /// <summary>
        /// Returns, if the specified source is associated with this project.
        /// </summary>
        /// <param name="source">The specified source.</param>
        /// <returns>
        /// true, if the source is associated with this project
        /// false, else
        /// </returns>
        public bool existSource(AbstractSource source)
        {
            foreach (AbstractSource s in sources)
            {
                if (s == source)
                {
                    return true;
                }
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// tests if all trackables in this.trackables are null. if there are one which is not null
        /// it's true.
        /// </summary>
        /// <returns>
        /// true if trackable, false if not.
        /// </returns>
        /// <remarks>
        /// Lizzard, 1/19/2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool isTrackable()
        {
            foreach (AbstractTrackable temp in this.trackables)
            {
                if (temp != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the next bigger Matrix ID.
        /// </summary>
        /// <returns></returns>
        public int nextID()
        {
            int i = 0;
            foreach(AbstractTrackable track in this.trackables) {
                if (track != null && track is IDMarker)
                {
                    if (((IDMarker)track).MatrixID > i)
                    {
                        i = ((IDMarker)track).MatrixID; 
                    }
                }
            }
            return i + 1;
        }

        /// <summary>
        /// true if an Trackable with the same Path/ID exists, false if not.
        /// </summary>
        /// <param name="prev">The previous.</param>
        /// <returns></returns>
        public bool existTrackable(IPreviewable prev)
        {
            foreach (AbstractTrackable track in this.trackables)
            {
                if (track != null && track is IDMarker && prev is IDMarker)
                {
                    if (((IDMarker)track).MatrixID == ((IDMarker)prev).MatrixID)
                    {
                        return true;
                    }
                }
                else if (track != null && track is PictureMarker && prev is PictureMarker)
                {
                    if (((PictureMarker)track).PicturePath == ((PictureMarker)prev).PicturePath)
                    {
                        return true;
                    }
                }
                else if (track != null && track is ImageTrackable && prev is ImageTrackable)
                {
                    if (((ImageTrackable)track).ImagePath == ((ImageTrackable)prev).ImagePath)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    
}
