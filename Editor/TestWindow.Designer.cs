namespace ARdevKit
{
    partial class TestWindow
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
            this.pnl_TestWindowMain = new System.Windows.Forms.Panel();
            this.pnl_TestWindowMetaioRenderer = new System.Windows.Forms.Panel();
            this.pnl_TestWindowStatus = new System.Windows.Forms.Panel();
            this.lbl_TestWindowVersion = new System.Windows.Forms.Label();
            this.pnl_TestWindowMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_TestWindowMain
            // 
            this.pnl_TestWindowMain.Controls.Add(this.pnl_TestWindowMetaioRenderer);
            this.pnl_TestWindowMain.Controls.Add(this.pnl_TestWindowStatus);
            this.pnl_TestWindowMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TestWindowMain.Location = new System.Drawing.Point(0, 0);
            this.pnl_TestWindowMain.Name = "pnl_TestWindowMain";
            this.pnl_TestWindowMain.Size = new System.Drawing.Size(784, 561);
            this.pnl_TestWindowMain.TabIndex = 0;
            this.pnl_TestWindowMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_TestWindowView_Paint);
            // 
            // pnl_TestWindowMetaioRenderer
            // 
            this.pnl_TestWindowMetaioRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TestWindowMetaioRenderer.Location = new System.Drawing.Point(0, 0);
            this.pnl_TestWindowMetaioRenderer.Name = "pnl_TestWindowMetaioRenderer";
            this.pnl_TestWindowMetaioRenderer.Size = new System.Drawing.Size(784, 538);
            this.pnl_TestWindowMetaioRenderer.TabIndex = 2;
            // 
            // pnl_TestWindowStatus
            // 
            this.pnl_TestWindowStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_TestWindowStatus.Location = new System.Drawing.Point(0, 538);
            this.pnl_TestWindowStatus.Name = "pnl_TestWindowStatus";
            this.pnl_TestWindowStatus.Size = new System.Drawing.Size(784, 23);
            this.pnl_TestWindowStatus.TabIndex = 1;
            // 
            // lbl_TestWindowVersion
            // 
            this.lbl_TestWindowVersion.Location = new System.Drawing.Point(655, 541);
            this.lbl_TestWindowVersion.Name = "lbl_TestWindowVersion";
            this.lbl_TestWindowVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_TestWindowVersion.Size = new System.Drawing.Size(126, 17);
            this.lbl_TestWindowVersion.TabIndex = 1;
            this.lbl_TestWindowVersion.Text = "Version";
            this.lbl_TestWindowVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lbl_TestWindowVersion);
            this.Controls.Add(this.pnl_TestWindowMain);
            this.Name = "TestWindow";
            this.Text = "TestWindow";
            this.pnl_TestWindowMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_TestWindowMain;
        private System.Windows.Forms.Label lbl_TestWindowVersion;
        private System.Windows.Forms.Panel pnl_TestWindowMetaioRenderer;
        private System.Windows.Forms.Panel pnl_TestWindowStatus;
    }
}