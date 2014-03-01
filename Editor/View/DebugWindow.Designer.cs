namespace ARdevKit.View
{
    partial class DebugWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb_out = new System.Windows.Forms.RichTextBox();
            this.tlp_debugWindow_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_debugWindow_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_out
            // 
            this.rtb_out.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_out.Enabled = false;
            this.rtb_out.Location = new System.Drawing.Point(3, 3);
            this.rtb_out.Name = "rtb_out";
            this.rtb_out.Size = new System.Drawing.Size(618, 435);
            this.rtb_out.TabIndex = 0;
            this.rtb_out.Text = "";
            // 
            // tlp_debugWindow_main
            // 
            this.tlp_debugWindow_main.ColumnCount = 1;
            this.tlp_debugWindow_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_debugWindow_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_debugWindow_main.Controls.Add(this.rtb_out, 0, 0);
            this.tlp_debugWindow_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_debugWindow_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_debugWindow_main.Name = "tlp_debugWindow_main";
            this.tlp_debugWindow_main.RowCount = 1;
            this.tlp_debugWindow_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_debugWindow_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_debugWindow_main.Size = new System.Drawing.Size(624, 441);
            this.tlp_debugWindow_main.TabIndex = 1;
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.tlp_debugWindow_main);
            this.Name = "DebugWindow";
            this.Text = "Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
            this.tlp_debugWindow_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_out;
        /// <summary>
        /// Gets the rtb_out.
        /// </summary>
        /// <value>
        /// The rtb_out.
        /// </value>
        public System.Windows.Forms.RichTextBox Rtb_out
        {
            get { return rtb_out; }
        }
        private System.Windows.Forms.TableLayoutPanel tlp_debugWindow_main;
    }
}