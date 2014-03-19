using ARdevKit.Controller.ProjectController;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
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
        /// Full pathname of the project file before it was changed.
        /// </summary>
        private string oldProjectPath;

        /// <summary>
        /// Gets or sets the <see cref="oldProjectPath"/>.
        /// </summary>
        /// <value>
        /// <see cref="oldProjectPath"/>.
        /// </value>
        public string OldProjectPath
        {
            get { return oldProjectPath; }
            set { oldProjectPath = value; }
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
        public Project(string name)
            : this()
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// tests if all trackables in this.trackables are null. if there are one which is not null
        /// it's true.
        /// </summary>
        /// <returns>
        /// true if trackable, false if not.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool hasTrackable()
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
            foreach (AbstractTrackable track in this.trackables)
            {
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

        /// <summary>   true if an Trackable with the same Path/ID exists, false if not. </summary>
        ///
        /// <param name="matrixID"> Identifier for the matrix. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        public bool existTrackable(int matrixID)
        {
            int counter = 0;
            foreach (AbstractTrackable track in trackables)
            {
                if (((IDMarker)track).MatrixID == matrixID)
                {
                    counter++;
                    if (counter == 2)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the checksum of the project lying at the project path.
        /// </summary>
        /// <returns></returns>
        /// <remarks>geht 20.02.2014 13:36</remarks>
        public string getChecksum()
        {
           try
            {
                using (MD5 md5 = MD5.Create())
                {
                        // Create a new Stringbuilder to collect the bytes 
                        // and create a string.
                        StringBuilder sBuilder = new StringBuilder();

                        byte[] data = md5.ComputeHash(ObjectToByteArray(this));

                        // Loop through each byte of the hashed data  
                        // and format each one as a hexadecimal string. 
                        for (int i = 0; i < data.Length; i++)
                        {
                            sBuilder.Append(data[i].ToString("x2"));
                        }

                        // Return the hexadecimal string. 
                        return sBuilder.ToString();
                }
            }
            catch (FileNotFoundException fnfe)
            {
                Debug.WriteLine(ProjectPath + "\\" + Name + ".ardev" + " not found");
                Debug.WriteLine(fnfe.StackTrace);
            }
            catch (ArgumentNullException ane)
           {
               Debug.WriteLine(ProjectPath + "\\" + Name + ".ardev" + " not found");
               Debug.WriteLine(ane.StackTrace);
           }

            return null;
        }

        /// <summary>
        /// The locker. A little helper needed for the checksum creation.
        /// </summary>
        /// <remarks>geht 20.02.2014 14:10</remarks>
        private static readonly Object locker = new Object();

        private static byte[] ObjectToByteArray(Object objectToSerialize)
        {
            MemoryStream fs = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                //Here's the core functionality! One Line!
                //To be thread-safe we lock the object
                lock (locker)
                {
                    formatter.Serialize(fs, objectToSerialize);
                }
                return fs.ToArray();
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Error occurred during serialization. Message: " +
                se.Message);
                return null;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Removes the augmentation and deletes connected files if other augmentations dont need it.
        /// </summary>
        /// <param name="augmentation">The augmentation.</param>
        /// <param name="index">The index.</param>
        public void RemoveAugmentation(AbstractAugmentation augmentation, int index)
        {
            if (augmentation is Abstract2DAugmentation)
            {
                Abstract2DAugmentation augmentationToBeRemoved = (Abstract2DAugmentation)augmentation;
                bool deleteFile = true;
                int i = index;

                while (deleteFile && i < trackables.Count)
                {
                    int j = 0;
                    while (deleteFile && j < trackables[i].Augmentations.Count)
                    {
                        if (trackables[i].Augmentations[j] is Abstract2DAugmentation)
                        {
                            deleteFile = !(((Abstract2DAugmentation)trackables[i].Augmentations[j]).ResFilePath == augmentationToBeRemoved.ResFilePath);
                        }
                        j++;
                    }
                    i++;
                }
                if (deleteFile)
                    augmentation.CleanUp();
            }
            augmentation.Trackable.RemoveAugmentation(augmentation);
        }
    }

}
