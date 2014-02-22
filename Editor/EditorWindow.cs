////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	editorwindow.cs
//
// summary:	Implements the editorwindow class
// This is the main window of the entire program, as well as the main entrance point.
// The Editor Window controlls the main tasks and delegates complex task to the other dedicated controllers.
// It also controls the communication between the model and the view, meaning it builds the view and saves/deletes/changes data in the model.
// 
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using ARdevKit.Model.Project;
using Controller.EditorController;
using ARdevKit.Controller.ProjectController;
using ARdevKit.Controller.EditorController;
using ARdevKit.Controller.Connections.DeviceConnection;
using ARdevKit.Controller.TestController;
using ARdevKit.View;
using System.IO;
using ARdevKit.Model.Project.File;
using System.Drawing.Printing;
using System.Security.Cryptography;

namespace ARdevKit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Form for viewing the editor. This is the main form of the program.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class EditorWindow : Form
    {
        /// <summary>
        /// The checksum of the project. Is needed to determine whether there has been made changes to the project.
        /// </summary>
        /// <remarks>geht 20.02.2014 13:06</remarks>
        private string checksum;

        /// <summary>
        /// The minscreenwidht
        /// </summary>
        /// <remarks>geht 28.01.2014 15:12</remarks>
        private const uint MINSCREENWIDHT = 320;

        /// <summary>
        /// The minscreenheight
        /// </summary>
        /// <remarks>geht 28.01.2014 15:12</remarks>
        private const uint MINSCREENHEIGHT = 240;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// if true the debug window will be opened when starting the test mode on the device.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool startDebugModeDevice;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets a value indicating whether to start debug mode if test mode is started on the
        /// device.
        /// </summary>
        ///
        /// <value>
        /// if true the debug window will be opened when starting the test mode on the device.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool StartDebugModeDevice
        {
            get { return startDebugModeDevice; }
            set { startDebugModeDevice = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// if true the debug window will be opened when starting the test mode locally.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool startDebugModeLocal;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets a value indicating whether to start debug mode locally.
        /// </summary>
        ///
        /// <value>
        /// if true the debug window will be opened when starting the test mode locally.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal bool StartDebugModeLocal
        {
            get { return startDebugModeLocal; }
            set { startDebugModeLocal = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Categories the element belongs to.
        /// List of scene element categories. 
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<SceneElementCategory> elementCategories;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the categories the element belongs to.
        /// </summary>
        ///
        /// <value>
        /// The element categories.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal List<SceneElementCategory> ElementCategories
        {
            get { return elementCategories; }
            set { elementCategories = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Linked list containing all IPreviewables.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private LinkedList<IPreviewable> allElements;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets allElements.
        /// </summary>
        ///
        /// <value>
        /// Linked list containing elements.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal LinkedList<IPreviewable> AllElements
        {
            get { return allElements; }
            set { allElements = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The element selection controller.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private ElementSelectionController elementSelectionController;

        /// <summary>
        /// Gets the element selection controller.
        /// </summary>
        /// <value>
        /// The element selection controller.
        /// </value>
        /// <remarks>geht 19.01.2014 23:06</remarks>
        internal ElementSelectionController ElementSelectionController
        {
            get { return elementSelectionController; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The preview controller.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private PreviewController previewController;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the previewController. </summary>
        ///
        /// <value> The previewController. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public PreviewController PreviewController
        {
            get { return previewController; }
            set { previewController = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the project. </summary>
        ///
        /// <value> The project. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Project project { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The property controller.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private PropertyController propertyController;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The device connection controller.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private DeviceConnectionController deviceConnectionController;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The export visitor.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private ExportVisitor exportVisitor;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the export visitor.
        /// </summary>
        ///
        /// <value>
        /// The export visitor.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal ExportVisitor ExportVisitor
        {
            get { return exportVisitor; }
            set { exportVisitor = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The current element.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private IPreviewable currentElement;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the current element.
        /// </summary>
        ///
        /// <value>
        /// The current element.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal IPreviewable CurrentElement
        {
            get { return currentElement; }
            set { currentElement = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor. initializes components on startup.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public EditorWindow()
        {
            InitializeComponent();
            createNewProject("");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by Editor for load events. actions to do when the editor is loaded.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_file_new for click events.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void tsm_editor_menu_file_new_Click(object sender, EventArgs e)
        {
            if (projectChanged())
            {
                if (MessageBox.Show("Möchten Sie das aktuelle Projekt abspeichern, bevor ein neues angelegt wird?", "Projekt speichern?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        this.saveProject();
                    }
                    catch (ArgumentNullException ae)
                    {
                        Debug.WriteLine(ae.StackTrace);
                    }
                }
            }

                createNewProject("");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_file_exit for click events. exits the program.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void tsm_editor_menu_file_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_test_startWithImage for click events. Starts the test
        /// mode for images.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tsm_editor_menu_test_startImage_Click(object sender, EventArgs e)
        {
            if (project.Trackables != null && project.Trackables.Count > 0 && project.Trackables[0] != null)
            {
                try
                {
                    TestController.StartPlayer(project, TestController.IMAGE, (int)project.Screensize.Width, (int)project.Screensize.Height, tsm_editor_menu_test_togleDebug.Checked);
                }
                catch (OperationCanceledException oae)
                {
                    MessageBox.Show("Vorgang wurde abgebrochen");
                }
            }
            else
                MessageBox.Show("Keine Szene zum Testen vorhanden");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_test_startWithVideo for click events. Starts the test
        /// mode for images.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tsm_editor_menu_test_startVideo_Click(object sender, EventArgs e)
        {
            if (project.Trackables != null && project.Trackables.Count > 0 && project.Trackables[0] != null)
                TestController.StartPlayer(project, TestController.VIDEO, (int)project.Screensize.Width, (int)project.Screensize.Height, tsm_editor_menu_test_togleDebug.Checked);
            else
                MessageBox.Show("Keine Szene zum Testen vorhanden");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_test_startWithVirtualCamera for click events. Starts the test
        /// mode using a virtual camera.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void tsm_editor_menu_test_startWithVirtualCamera_Click(object sender, EventArgs e)
        {
            if (project.Trackables != null && project.Trackables.Count > 0 && project.Trackables[0] != null)
                TestController.StartPlayer(project, TestController.CAMERA, (int)project.Screensize.Width, (int)project.Screensize.Height, tsm_editor_menu_test_togleDebug.Checked);
            else
                MessageBox.Show("Keine Szene zum Testen vorhanden");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Event handler. This eventHandler is to change the choosen scene from the
        ///     SceneSelectionPanel. The handler will load an existent scene, which was created in the
        ///     past. If you change the scene from a new created scene, which is empty this scene will be
        ///     delete.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/16/2014. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_editor_scene_scene_change(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(((Button)sender).Text);
            if (this.project.Trackables.Count > 1)
            {
                this.previewController.reloadPreviewPanel(temp - 1);
                this.PropertyGrid1.SelectedObject = null;

            }
            else
            {
                this.previewController.reloadPreviewPanel(0);
                this.PropertyGrid1.SelectedObject = null;
            }

            this.resetButton();
            this.setButton(((Button)sender).Text);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Event handler. This eventHandler is to add a scene to the SceneSelectionPanel. This
        ///     funktion adds a new Button to the SceneSelectionPanel and set a new Scene to the
        ///     PreviewPanel.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/16/2014. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_editor_scene_scene_new(object sender, EventArgs e)
        {
            if (this.project.Trackables.Count < 10)
            {
                Button tempButton = new Button();
                tempButton.Location = new System.Drawing.Point(54 + (52 * project.Trackables.Count), 34);
                tempButton.Name = "btn_editor_scene_scene_" + (this.project.Trackables.Count + 1);
                tempButton.Size = new System.Drawing.Size(46, 45);
                tempButton.TabIndex = 1;
                tempButton.Text = Convert.ToString(this.project.Trackables.Count + 1);
                tempButton.UseVisualStyleBackColor = true;
                tempButton.Click += new System.EventHandler(this.btn_editor_scene_scene_change);
                tempButton.ContextMenu = new ContextMenu();
                tempButton.ContextMenu.Tag = tempButton;
                tempButton.ContextMenu.MenuItems.Add("Duplicate", new EventHandler(this.pnl_editor_scene_duplicate));

                this.pnl_editor_scenes.Controls.Add(tempButton);
                this.previewController.reloadPreviewPanel(this.project.Trackables.Count);
                this.PropertyGrid1.SelectedObject = null;
                this.resetButton();
                this.setButton(tempButton.Text);
            }
            else
            {
                MessageBox.Show("Sie können nicht mehr als 10 Szenen pro Project haben.");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Event handler. This eventHandler is to remove a scene from the SceneSelectionPanel. This
        ///     Functions clean the scene, if there is only one scene, else the funktion removes the
        ///     panel and set scene 1 to the current scene.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/16/2014. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_editor_scene_scene_remove(object sender, EventArgs e)
        {
            if (this.project.Trackables.Count > 1)
            {
                this.project.Trackables.Remove(this.previewController.trackable);
                this.previewController.trackable = this.project.Trackables[0];
                this.reloadSelectionPanel();
                this.previewController.index = -1;
                this.previewController.reloadPreviewPanel(0);
                if (!this.project.hasTrackable())
                {
                    this.ElementSelectionController.setElementEnable(typeof(PictureMarker), true);
                    this.ElementSelectionController.setElementEnable(typeof(IDMarker), true);
                }
            }
            else
            {
                this.project.Trackables[0] = null;
                this.previewController.currentMetaCategory = MetaCategory.Trackable;
                this.previewController.removePreviewable(this.previewController.trackable);
                if (!this.project.hasTrackable())
                {
                    this.ElementSelectionController.setElementEnable(typeof(PictureMarker), true);
                    this.ElementSelectionController.setElementEnable(typeof(IDMarker), true);
                }
            }
            this.resetButton();
            this.setButton(Convert.ToString("1"));
            this.PropertyGrid1.SelectedObject = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_file_open for click events.
        /// </summary>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void tsm_editor_menu_file_open_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void addDevice()
        {
            //TODO: implement addDevice()
        }

        public void createNewProject(String name)
        {
            this.initializeEmptyProject(name);
            this.initializeControllers();
            this.updatePanels();
            this.checksum = project.getChecksum();
        }

        /// <summary>
        /// Exports the project. saves the project first and then exports to project path
        /// </summary>
        /// <remarks>geht 19.01.2014 22:10</remarks>
        public void exportProject()
        {
            try
            {
                saveProject();

                try
                {
                    exportVisitor = new ExportVisitor();
                    project.Accept(exportVisitor);
                }
                catch (DirectoryNotFoundException de)
                {
                    Debug.WriteLine(de.StackTrace);
                }
                catch (OperationCanceledException oce)
                {
                    MessageBox.Show("Exportvorgang abgebrochen");
                }
                try
                {
                    foreach (AbstractFile file in exportVisitor.Files)
                    {
                        file.Save();
                    }
                }
                catch (NullReferenceException ne)
                {
                    Debug.WriteLine(ne.StackTrace);
                }

                MessageBox.Show("Projekt wurde exportiert!", "Export");
            }
            catch (ArgumentNullException ae)
            {
                Debug.WriteLine(ae.StackTrace);
            }
        }

        /// <summary>
        /// Loads the project. Opens a file dialog to select a saved project.
        /// </summary>
        /// <remarks>geht 19.01.2014 17:55</remarks>
        public void loadProject()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ARdevkit Projektdatei|*.ardev";
            openFileDialog1.Title = "Projekt öffnen";
            openFileDialog1.ShowDialog();
            try
            {
                initializeLoadedProject(SaveLoadController.loadProject(openFileDialog1.FileName));
                this.initializeControllers();
                this.updatePanels();
                previewController.index = -1;
                previewController.reloadPreviewPanel(0);
                this.updateSceneSelectionPanel();
                this.updateScreenSize();
                this.checksum = project.getChecksum();
            }
            catch (System.ArgumentException)
            {

            }
        }

        /// <summary>
        /// Should open the DebugWindow, but isn't used...
        /// </summary>
        /// <remarks>geht 19.01.2014 22:38</remarks>
        [System.Obsolete("this method is not used...", false)]
        public void openDebugWindow()
        {
            //TODO: implement openDebugWindow()
        }

        /// <summary>
        /// Should open the TestWindow, but isn't used...
        /// </summary>
        /// <remarks>geht 19.01.2014 22:38</remarks>
        [System.Obsolete("this method is not used...", false)]
        public void openTestWindow()
        {
            //TODO: implement openTestWindow()
        }

        /**
         * <summary>    Registers all SceneElements that are available. </summary>
         *
         * <remarks>    Robin, 14.01.2014. </remarks>
         */

        public void registerElements()
        {
            SceneElementCategory sources = new SceneElementCategory(MetaCategory.Source, "Sources");
            sources.addElement(new SceneElement("Database Source", new DbSource(), this));
            sources.addElement(new SceneElement("FileSource", new FileSource(""), this));
            SceneElementCategory augmentations = new SceneElementCategory(MetaCategory.Augmentation, "Augmentations");
            augmentations.addElement(new SceneElement("Chart", new Chart(), this));
            augmentations.addElement(new SceneElement("Image Augmentation", new ImageAugmentation(), this));
            augmentations.addElement(new SceneElement("Video Augmentation", new VideoAugmentation(), this));
            SceneElementCategory trackables = new SceneElementCategory(MetaCategory.Trackable, "Trackables");
            trackables.addElement(new SceneElement("Picture Marker", new PictureMarker(), this));
            trackables.addElement(new SceneElement("IDMarker", new IDMarker(1), this));
            trackables.addElement(new SceneElement("Image Trackable", new ImageTrackable(), this));
            addCategory(trackables);
            addCategory(augmentations);
            addCategory(sources);
            IDFactory.Reset();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Saves the project. Opens file save dialog if project Path isn't set yet. calls save(String path).
        /// </summary>
        ///
        /// <remarks>
        /// geht, 17.01.2014.
        /// </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void saveProject()
        {
            if (project.Sensor == null)
            {
                MessageBox.Show("Sie müssen mindestens ein Trackable hinzufügen!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Debug.WriteLine("You have to add at least one trackable first!");
                throw new ArgumentNullException();
            }
            else
            {
                if (project.ProjectPath == null || project.Name.Equals(""))
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "ARdevkit Projektdatei|*.ardev";
                    saveFileDialog1.Title = "Projekt speichern";
                    saveFileDialog1.ShowDialog();
                    try
                    {
                        project.ProjectPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                        project.Name = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                        this.save(project.ProjectPath);
                    }
                    catch (System.ArgumentException)
                    {
                        project.ProjectPath = null;
                    }
                }
                else
                {
                    this.save(project.ProjectPath);
                }
            }
        }

        private void save(String path)
        {
            SaveLoadController.saveProject(this.project);
            checksum = this.project.getChecksum();
        }

        public void sendToDevice()
        {
            //TODO: implement sendToDevice()
        }

        public void updateElementSelectionPanel()
        {
            this.Cmb_editor_selection_toolSelection.Items.Clear();
            this.elementSelectionController.populateComboBox();
            this.elementSelectionController.updateElementSelectionPanel();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     This functions Updates the scene PreviewPanel. Alle elements will be removed and
        ///     all current elements will add again to the panel.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/16/2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updatePreviewPanel()
        {
            this.previewController.updatePreviewPanel();
        }

        /// <summary>
        /// Should update the PropertyPanel, but isn't used anyways...
        /// </summary>
        /// <param name="selectedElement">The selected element.</param>
        /// <remarks>geht 19.01.2014 22:37</remarks>
        [System.Obsolete("this method is not used...", false)]
        internal void updatePropertyPanel(IPreviewable selectedElement)
        {
            //TODO: implement updatePropertyPanel(IPreviewable selectedElement)
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     This functions Updates the scene SceneSelectionPanel. Alle elements will be removed and
        ///     all current elements will add again to the panel.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/16/2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updateSceneSelectionPanel()
        {
            for (int i = 0; i < this.project.Trackables.Count; i++)
            {
                if (this.project.Trackables[i] == null)
                {
                    this.project.Trackables.Remove(this.project.Trackables[i]);
                }
            }
            this.pnl_editor_scenes.Controls.Clear();
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_new);
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_delete);

            for (int i = 0; i < this.project.Trackables.Count; i++)
            {
                Button tempButton = new Button();
                tempButton.Location = new System.Drawing.Point(54 + (i * 52), 34);
                tempButton.Name = "btn_editor_scene_scene_" + (this.project.Trackables.Count + 1);
                tempButton.Size = new System.Drawing.Size(46, 45);
                tempButton.Text = Convert.ToString(i + 1);
                tempButton.UseVisualStyleBackColor = true;
                tempButton.Click += new System.EventHandler(this.btn_editor_scene_scene_change);
                tempButton.ContextMenu = new ContextMenu();
                tempButton.ContextMenu.Tag = tempButton;
                tempButton.ContextMenu.MenuItems.Add("Duplicate", new EventHandler(this.pnl_editor_scene_duplicate));

                this.pnl_editor_scenes.Controls.Add(tempButton);
            }
            this.setButton("1");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Reload selection panel. </summary>
        ///
        /// <remarks>   Lizzard, 1/19/2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void reloadSelectionPanel()
        {
            this.pnl_editor_scenes.Controls.Clear();
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_new);
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_delete);

            for (int i = 0; i < this.project.Trackables.Count; i++)
            {
                Button tempButton = new Button();
                tempButton.Location = new System.Drawing.Point(54 + (i * 52), 34);
                tempButton.Name = "btn_editor_scene_scene_" + (this.project.Trackables.Count + 1);
                tempButton.Size = new System.Drawing.Size(46, 45);
                tempButton.Text = Convert.ToString(i + 1);
                tempButton.UseVisualStyleBackColor = true;
                tempButton.Click += new System.EventHandler(this.btn_editor_scene_scene_change);
                tempButton.ContextMenu = new ContextMenu();
                tempButton.ContextMenu.Tag = tempButton;
                tempButton.ContextMenu.MenuItems.Add("Duplicate", new EventHandler(this.pnl_editor_scene_duplicate));

                this.pnl_editor_scenes.Controls.Add(tempButton);
            }
        }

        public void updateStatusBar()
        {
            //TODO: implement updateStatusBar()
        }

        /**
         * <summary>    Adds a category to the element categories. </summary>
         *
         * <remarks>    Robin, 18.01.2014. </remarks>
         *
         * <param name="category">  The category. </param>
         */

        private void addCategory(SceneElementCategory category)
        {
            elementCategories.Add(category);
        }

        /**
         * <summary>
         *  Event handler. Called by cmb_editor_selection_toolSelection for selected index changed
         *  events.
         * </summary>
         *
         * <remarks>    Robin, 18.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Event information. </param>
         */

        private void cmb_editor_selection_toolSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            elementSelectionController.updateElementSelectionPanel();
            previewController.currentMetaCategory = ((SceneElementCategoryPanel)cmb_editor_selection_toolSelection.SelectedItem).Category.Category;
        }

        /**
         * <summary>    Event handler. Called by pnl_editor_preview for drag enter events. </summary>
         *
         * <remarks>    Robin, 18.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Drag event information. </param>
         */

        private void pnl_editor_preview_DragEnter(object sender, DragEventArgs e)
        {
            if (previewController.currentMetaCategory != MetaCategory.Source)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        /**
         * <summary>    Event handler. Called by pnl_editor_preview for drag drop events when an element is droped on the preview. </summary>
         *
         * <remarks>    Robin, 18.01.2014. </remarks>
         *
         * <param name="sender">    Source of the event. </param>
         * <param name="e">         Drag event information. </param>
         */

        private void pnl_editor_preview_DragDrop(object sender, DragEventArgs e)
        {
            if (previewController.currentMetaCategory != MetaCategory.Source)
            {
                ElementIcon icon = (ElementIcon)e.Data.GetData(typeof(ElementIcon));
                Point p = pnl_editor_preview.PointToClient(Cursor.Position);
                IPreviewable element = (IPreviewable)icon.Element.Prototype.Clone();
                icon.EditorWindow.PreviewController.addPreviewable(element, new Vector3D(p.X, p.Y, 0));
            }
        }

        private void initializeControllers()
        {
            try
            {
                this.elementSelectionController = new ElementSelectionController(this);
            }
            catch (Exception)
            {

                Debug.WriteLine("ElementSelectionController is not implemented yet...");
            }

            this.previewController = new PreviewController(this);
            this.propertyController = new PropertyController(this);

            try
            {
                this.deviceConnectionController = new DeviceConnectionController(this);
            }
            catch (Exception)
            {

                Debug.WriteLine("DeviceConnectionController is not implemented yet...");
            }
        }

        private void initializeEmptyProject(String projectname)
        {
            this.project = new Project(projectname);
            this.project.ProjectPath = null;
            this.startDebugModeDevice = false;
            this.startDebugModeLocal = false;
            this.elementCategories = new List<SceneElementCategory>();
            this.allElements = new LinkedList<IPreviewable>();
            this.exportVisitor = new ExportVisitor();
            this.currentElement = null;
            this.project.Screensize = new ScreenSize();
            this.project.Screensize.Height = Convert.ToUInt32(pnl_editor_preview.Size.Height);
            this.project.Screensize.Width = Convert.ToUInt32(pnl_editor_preview.Size.Width);
            this.project.Screensize.SizeChanged += new System.EventHandler(this.pnl_editor_preview_SizeChanged); 
            registerElements();
        }

        private void initializeLoadedProject(Project p)
        {
            this.project = p;
            this.startDebugModeDevice = false;
            this.startDebugModeLocal = false;
            this.elementCategories = new List<SceneElementCategory>();
            this.allElements = new LinkedList<IPreviewable>();
            this.exportVisitor = new ExportVisitor();
            this.currentElement = null;
            registerElements();
        }

        private void updatePanels()
        {
            this.updateElementSelectionPanel();
            this.updatePreviewPanel();
            this.updateSceneSelectionPanel();
            //this.updatePropertyPanel(currentElement);
            this.updateStatusBar();
        }

        /// <summary>
        /// Updates the size of the screen.
        /// </summary>
        /// <remarks>geht 26.01.2014 20:20</remarks>
        private void updateScreenSize()
        {
            if (project.Screensize.Width < MINSCREENWIDHT)
            {
                this.project.Screensize.Width = MINSCREENWIDHT;
                this.pnl_editor_preview.Size = new Size((int)project.Screensize.Width, (int)project.Screensize.Height);
                this.previewController.rescalePreviewPanel();
            }
            else if (project.Screensize.Height < MINSCREENHEIGHT)
            {
                this.project.Screensize.Height = MINSCREENHEIGHT;
                this.pnl_editor_preview.Size = new Size((int)project.Screensize.Width, (int)project.Screensize.Height);
                this.previewController.rescalePreviewPanel();
            }
            else
            {
                this.pnl_editor_preview.Size = new Size((int)project.Screensize.Width, (int)project.Screensize.Height);
                this.previewController.rescalePreviewPanel();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_file_save for click events.
        /// Click on "Speichern" dialog.
        /// </summary>
        ///
        /// <remarks>
        /// geht, 17.01.2014.
        /// </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void tsm_editor_menu_file_save_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveProject();
            }
            catch (ArgumentNullException ae)
            {
                Debug.WriteLine(ae.StackTrace);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event handler. Called by tsm_editor_menu_file_saveAs for click events.
        /// Click on "Speichern unter" dialog.
        /// </summary>
        ///
        /// <remarks>
        /// geht, 17.01.2014.
        /// </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void tsm_editor_menu_file_saveAs_Click(object sender, EventArgs e)
        {
            this.project.ProjectPath = null;
            try
            {
                this.saveProject();
            }
            catch (ArgumentNullException ae)
            {
                Debug.WriteLine(ae.StackTrace);
            }
        }

        private void tsm_editor_menu_file_open_Click_1(object sender, EventArgs e)
        {
            if (projectChanged())
            {
                if (MessageBox.Show("Möchten Sie das aktuelle Projekt abspeichern, bevor ein anderes geöffnet wird?", "Projekt speichern?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        this.saveProject();
                    }
                    catch (ArgumentNullException ae)
                    {
                        Debug.WriteLine(ae.StackTrace);
                    }
                }
            }
            
            this.loadProject();
        }

        private void tsm_editor_menu_file_export_Click(object sender, EventArgs e)
        {
            this.exportProject();
        }

        private void tsm_editor_menu_help_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ARdevKit Version 0.1 alpha \n\n Rüdiger Heres \n Jonas Lachowitzer \n Robin Lamberti \n Tuong-Vu Mai \n Imanuel Richter\n Marwin Rieger \n\n Nutzung auf eigene Gefahr! \n Das Programm könnte Ihre Kekse auffressen...", "Info");
        }


        /// <summary>
        /// Sets the current scene button to the Color ControlDark.
        /// </summary>
        /// <param name="text">The text.</param>
        private void setButton(string text)
        {
            foreach (Control comp in pnl_editor_scenes.Controls)
            {
                if (comp.Text == text)
                {
                    ((Button)comp).BackColor = SystemColors.ControlDark;
                }
            }
        }

        /// <summary>
        /// Resets the SceneSelectionsButtons to normal Color.
        /// </summary>
        private void resetButton()
        {
            foreach (Control comp in pnl_editor_scenes.Controls)
            {
                if (((Button)comp).BackColor == SystemColors.ControlDark)
                {
                    ((Button)comp).BackColor = SystemColors.Control;
                }
            }
        }

        /// <summary>
        /// Sets the PasteButton enabled.
        /// </summary>
        public void setPasteButtonEnabled()
        {
            this.tsm_editor_menu_edit_paste.Enabled = true;
            this.pnl_editor_preview.ContextMenu.MenuItems[0].Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the pnl_editor_preview control.
        /// Used for changing the screensize.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>geht 26.01.2014 20:21</remarks>
        private void pnl_editor_preview_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = project.Screensize;
            propertyGrid1.PropertySort = PropertySort.NoSort;
            this.previewController.setCurrentElement(null);
        }

        /// <summary>
        /// Handles the SizeChanged event of the pnl_editor_preview control.
        /// Is called when the screensize has been changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>geht 26.01.2014 20:21</remarks>
        private void pnl_editor_preview_SizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("ScreenSize has been changed!");
            this.updateScreenSize();
        }

        private void pnl_editor_scene_duplicate(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(((Button)((ContextMenu)((MenuItem)sender).Parent).Tag).Text);
            AbstractTrackable tempTrack;

            if (this.project.Trackables[temp - 1] != null)
            {
                tempTrack = (AbstractTrackable)this.project.Trackables[temp - 1].Clone();
                if (tempTrack.initElement(this))
                {
                    for (int i = 0; i < tempTrack.Augmentations.Count; i++)
                    {
                        tempTrack.Augmentations[i] = (AbstractAugmentation)tempTrack.Augmentations[i].Clone();
                    }
                    if (!this.project.existTrackable(tempTrack))
                    {
                        tempTrack.vector = new Vector3D(this.pnl_editor_preview.Size.Width / 2, this.pnl_editor_preview.Size.Height / 2, 0);
                        this.project.Trackables.Add(tempTrack);
                        this.updateSceneSelectionPanel();
                    }
                }
            }
        }

        int trackablePCounter = 0;

        /// <summary>
        /// Handles the Click event of the trackableDruckenToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>geht 04.02.2014 15:14</remarks>
        private void trackableDruckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (project.hasTrackable())
            {
                Debug.WriteLine("printing out trackables");

                trackablePCounter = 0;
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(Print_Page);

                PrintPreviewDialog dlg = new PrintPreviewDialog();
                dlg.Document = pd;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pd.Print();
                }
            }
            else
            {
                Debug.WriteLine("there are no trackables to print out...");
            }
        }

        /// <summary>
        /// Handles the Page event of the Print control.
        /// prints one page for each trackable.
        /// </summary>
        /// <param name="o">The source of the event.</param>
        /// <param name="e">The <see cref="PrintPageEventArgs"/> instance containing the event data.</param>
        /// <remarks>geht 04.02.2014 15:14</remarks>
        private void Print_Page(object o, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            e.Graphics.DrawImage(project.Trackables[trackablePCounter].getPreview(), x, y);

            if (project.Trackables[trackablePCounter] != project.Trackables.Last())
            {
                trackablePCounter++;
                e.HasMorePages = true;
                return;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the EditorWindow control.
        /// Displays a save dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        /// <remarks>geht 04.02.2014 15:15</remarks>
        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!projectChanged())
                return;

            DialogResult dlg = MessageBox.Show("Möchten Sie das aktuelle Projekt abspeichern, bevor ARdevKit beendet wird?", "Projekt speichern?", MessageBoxButtons.YesNoCancel);
            if (dlg == DialogResult.Yes)
            {
                e.Cancel = true;
                try
                {
                    this.saveProject();
                    e.Cancel = false;
                }
                catch (ArgumentNullException ae)
                {
                    Debug.WriteLine(ae.StackTrace);
                }
            }
            else if (dlg == DialogResult.No)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// reports whether the project has been changed or not after the last saving.
        /// returns false when the project hasn't been changed.
        /// returns true when the project has been changed.
        /// </summary>
        /// <returns></returns>
        /// <remarks>geht 20.02.2014 14:15</remarks>
        private bool projectChanged()
        {
            if (checksum.Equals(this.project.getChecksum()))
                return false;
            else
                return true;
        }

        private void tsm_editor_menu_help_help_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + "\\Documentation.chm", HelpNavigator.TableOfContents);
        }
    }
}
