﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.Reflection;
using System.Resources;
using System.IO;
using ARdevKit.Model.Project.Event;

namespace ARdevKit
{
    /// <summary>
    /// Form-Class for the TextEditor. This text editor provides only basic operation and functionallity
    /// to edit a .txt or .js file.
    /// </summary>
    public partial class TextEditorForm : Form
    {
        /// <summary>
        /// The file path.
        /// </summary>
        private string filePath;

        /// <summary>
        /// The selected event.
        /// </summary>
        private AbstractEvent selectedEvent;

        /// <summary>
        /// Gets or sets the selected event.
        /// </summary>
        /// <value>
        /// The selected event.
        /// </value>
        public AbstractEvent SelectedEvent
        {
            get { return selectedEvent; }
            set { selectedEvent = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string[] Value
        {
            get { return rtb_content.Lines; }
            set { rtb_content.Lines = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorForm"/> class.
        /// </summary>
        public TextEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorForm"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public TextEditorForm(string filePath) : this()
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("Parameter filePath was null");
            }
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("The file you want to load doesn't exist.");
            }
            if (new FileInfo(filePath).Length > 52428800)
            {
                throw new ArgumentException("The file you want to load is bigger than 50 MB.");
            }
            this.filePath = filePath;
            rtb_content.LoadFile(this.filePath, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorForm"/> class.
        /// </summary>
        /// <param name="selectedEvent">The selected event.</param>
        public TextEditorForm(AbstractEvent selectedEvent)
            : this()
        {
            this.selectedEvent = selectedEvent;
            tb_head.Visible = true;
            tb_head.Text = selectedEvent.GetHeadLine();
            tb_end.Visible = true;
            tb_end.Text = selectedEvent.GetLastLine();
            rtb_content.Lines = selectedEvent.Content;
        }
       
        /// <summary>
        /// This Click-Event closes the Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This Click-Event saves the content of the RichTextBox to its appropriate file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (filePath != null)
                rtb_content.SaveFile(this.filePath, RichTextBoxStreamType.PlainText);
            else
            {
                SelectedEvent.Content = rtb_content.Lines;
            }
            tb_head.Visible = false;
            tb_end.Visible = false;
            Close();
        }
    }
}
