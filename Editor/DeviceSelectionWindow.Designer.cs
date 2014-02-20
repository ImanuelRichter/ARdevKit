namespace ARdevKit
{
    partial class DeviceSelectionWindow
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
        public System.Windows.Forms.ListView DeviceList
        {
            get { return deviceList; }
            set { deviceList = value; }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.deviceList = new System.Windows.Forms.ListView();
            this.detectedDevices = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.sendTo = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceList
            // 
            this.deviceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.detectedDevices});
            this.deviceList.Location = new System.Drawing.Point(3, 3);
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(786, 368);
            this.deviceList.TabIndex = 0;
            this.deviceList.UseCompatibleStateImageBehavior = false;
            this.deviceList.View = System.Windows.Forms.View.List;
            this.deviceList.SelectedIndexChanged += new System.EventHandler(this.deviceList_SelectedIndexChanged);
            // 
            // detectedDevices
            // 
            this.detectedDevices.Text = "erkannte Geräte";
            this.detectedDevices.Width = -2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.deviceList, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.38318F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.61682F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 428);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 93.38422F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.615776F));
            this.tableLayoutPanel2.Controls.Add(this.sendTo, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.refresh, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 377);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(786, 48);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // sendTo
            // 
            this.sendTo.Location = new System.Drawing.Point(3, 3);
            this.sendTo.Name = "sendTo";
            this.sendTo.Size = new System.Drawing.Size(201, 42);
            this.sendTo.TabIndex = 0;
            this.sendTo.Text = "an Gerät senden";
            this.sendTo.UseVisualStyleBackColor = true;
            this.sendTo.Click += new System.EventHandler(this.sendTo_Click);
            // 
            // refresh
            // 
            this.refresh.BackgroundImage = global::ARdevKit.Properties.Resources.refresh;
            this.refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refresh.Location = new System.Drawing.Point(736, 3);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(45, 42);
            this.refresh.TabIndex = 0;
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // DeviceSelectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 427);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DeviceSelectionWindow";
            this.Text = "DeviceSelectionWindow";
            this.Load += new System.EventHandler(this.DeviceSelectionWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView deviceList;
        private System.Windows.Forms.ColumnHeader detectedDevices;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button sendTo;
        private System.Windows.Forms.Button refresh;
    }
}