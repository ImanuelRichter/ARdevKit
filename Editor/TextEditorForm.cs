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
        private string[] backUp;
        
        public string[] Value
        {
            get { return richTextBox1.Lines; }
            set 
            {
                if (backUp.Length == 0)
                    backUp = value;
                richTextBox1.Lines = value; 
            
            }
        }

        public TextEditorForm()
        {
            InitializeComponent();
            backUp = new string[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Lines = backUp;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
