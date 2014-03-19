namespace ARdevKit
{
    partial class TextEditorForm
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
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.rtb_content = new System.Windows.Forms.RichTextBox();
            this.tb_head = new System.Windows.Forms.TextBox();
            this.tb_end = new System.Windows.Forms.TextBox();
            this.tlp_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 2;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main.Controls.Add(this.tb_end, 0, 2);
            this.tlp_main.Controls.Add(this.tb_head, 0, 0);
            this.tlp_main.Controls.Add(this.btn_close, 1, 3);
            this.tlp_main.Controls.Add(this.btn_save, 0, 3);
            this.tlp_main.Controls.Add(this.rtb_content, 1, 0);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(714, 522);
            this.tlp_main.TabIndex = 1;
            // 
            // btn_close
            // 
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Location = new System.Drawing.Point(360, 493);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Schließen";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_save.Location = new System.Drawing.Point(279, 493);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // rtb_content
            // 
            this.rtb_content.AcceptsTab = true;
            this.tlp_main.SetColumnSpan(this.rtb_content, 2);
            this.rtb_content.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_content.Location = new System.Drawing.Point(3, 29);
            this.rtb_content.Name = "rtb_content";
            this.rtb_content.Size = new System.Drawing.Size(708, 432);
            this.rtb_content.TabIndex = 0;
            this.rtb_content.Text = "";
            // 
            // tb_head
            // 
            this.tlp_main.SetColumnSpan(this.tb_head, 2);
            this.tb_head.Enabled = false;
            this.tb_head.Location = new System.Drawing.Point(3, 3);
            this.tb_head.Name = "tb_head";
            this.tb_head.Size = new System.Drawing.Size(708, 20);
            this.tb_head.TabIndex = 2;
            this.tb_head.Visible = false;
            // 
            // tb_end
            // 
            this.tlp_main.SetColumnSpan(this.tb_end, 2);
            this.tb_end.Enabled = false;
            this.tb_end.Location = new System.Drawing.Point(3, 467);
            this.tb_end.Name = "tb_end";
            this.tb_end.Size = new System.Drawing.Size(708, 20);
            this.tb_end.TabIndex = 3;
            this.tb_end.Visible = false;
            // 
            // TextEditorForm
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(714, 522);
            this.Controls.Add(this.tlp_main);
            this.Name = "TextEditorForm";
            this.Text = "TextEditor";
            this.tlp_main.ResumeLayout(false);
            this.tlp_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.RichTextBox rtb_content;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox tb_end;
        private System.Windows.Forms.TextBox tb_head;
    }
}