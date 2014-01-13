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

namespace ARdevKit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Form for viewing the editor. This is the main form of the program.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class EditorWindow : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// if true the debug window will be opened when starting the test mode on the device.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool startDebugModeDevice;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// if true the debug window will be opened when starting the test mode locally.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool startDebugModeLocal;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Categories the element belongs to.
        /// List of scene element categories. 
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        //TODO: implement private List<SceneElementCategory> elementCategories;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Pathname of the project file.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private string projectPath;

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
        /// Gets or sets a value indicating whether to start debug mode locally.
        /// </summary>
        ///
        /// <value>
        /// if true the debug window will be opened when starting the test mode locally.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool StartDebugModeLocal
        {
            get { return startDebugModeLocal; }
            set { startDebugModeLocal = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the categories the element belongs to.
        /// </summary>
        ///
        /// <value>
        /// The element categories.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        //TODO: implement
        //private List<SceneElementCategory> ElementCategories
        //{
        //    get { return elementCategories; }
        //    set { elementCategories = value; }
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the full pathname of the project file.
        /// </summary>
        ///
        /// <value>
        /// The full pathname of the project file.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor. initializes components on startup.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public EditorWindow()
        {
            InitializeComponent();
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
            //stub
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
            //stub
            throw new NotImplementedException();
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
            TestController.StartWithImage();
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
            TestController.StartWithVirtualCamera();
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
    }
}
