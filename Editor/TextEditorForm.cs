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
        public string[] Value
        {
            get { return richTextBox1.Lines; }
            set { richTextBox1.Lines = value; }
        }

        public TextEditorForm()
        {
            InitializeComponent();
        }

        public TextEditorForm(string path)
            : this()
        {
            richTextBox1.LoadFile(Path.Combine(Application.StartupPath, "res/templates/customUserEventTemplate.txt"), RichTextBoxStreamType.PlainText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
