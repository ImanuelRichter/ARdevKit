using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
