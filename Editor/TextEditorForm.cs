using System;
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

namespace ARdevKit
{
    public partial class TextEditorForm : Form
    {
        private string filePath;

        public string[] Value
        {
            get { return richTextBox1.Lines; }
            set { richTextBox1.Lines = value; }
        }

        public TextEditorForm()
        {
            InitializeComponent();
        }

        public TextEditorForm(string filePath) : this()
        {
            this.filePath = filePath;
            richTextBox1.LoadFile(this.filePath, RichTextBoxStreamType.PlainText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(this.filePath, RichTextBoxStreamType.PlainText);
        }
    }
}
