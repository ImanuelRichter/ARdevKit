using System.Drawing;
using System.Windows.Forms;
namespace ARdevKit
{
    partial class EditorWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWindow));
            this.mst_editor_menu = new System.Windows.Forms.MenuStrip();
            this.tsm_editor_menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_file_new = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_file_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tss_editor_menu_file_opnen_save = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_editor_menu_file_save = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_file_saveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_file_export = new System.Windows.Forms.ToolStripMenuItem();
            this.tss_editor_menu_file_export_sendTo = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_editor_menu_file_sendTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_sendTo_win8Device = new System.Windows.Forms.ToolStripMenuItem();
            this.tss_editor_menu_file_sendTo_win8Device_togleDebug = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_editor_menu_file_sendTo_togleDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_file_connection = new System.Windows.Forms.ToolStripMenuItem();
            this.tss_editor_menu_file_connection_exit = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_editor_menu_file_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_edit_copie = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_edit_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_test = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_test_startImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_test_startVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_test_startWithVirtualCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.tss_editor_meu_test_loadVideo_togleDebug = new System.Windows.Forms.ToolStripSeparator();
            this.tsm_editor_menu_test_togleDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_help_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_editor_menu_help_info = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_editor_selection = new System.Windows.Forms.Panel();
            this.cmb_editor_selection_toolSelection = new System.Windows.Forms.ComboBox();
            this.pnl_editor_preview = new System.Windows.Forms.Panel();
            this.pnl_editor_properties = new System.Windows.Forms.Panel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.cmb_editor_properties_objectSelection = new System.Windows.Forms.ComboBox();
            this.pnl_editor_scenes = new System.Windows.Forms.Panel();
            this.btn_editor_scene_delete = new System.Windows.Forms.Button();
            this.btn_editor_scene_scene_1 = new System.Windows.Forms.Button();
            this.btn_editor_scene_new = new System.Windows.Forms.Button();
            this.pnl_editor_status = new System.Windows.Forms.Panel();
            this.lbl_version = new System.Windows.Forms.Label();
            this.mst_editor_menu.SuspendLayout();
            this.pnl_editor_selection.SuspendLayout();
            this.pnl_editor_properties.SuspendLayout();
            this.pnl_editor_scenes.SuspendLayout();
            this.pnl_editor_status.SuspendLayout();
            this.SuspendLayout();
            // 
            // mst_editor_menu
            // 
            this.mst_editor_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_editor_menu_file,
            this.tsm_editor_menu_edit,
            this.tsm_editor_menu_test,
            this.tsm_editor_menu_help});
            this.mst_editor_menu.Location = new System.Drawing.Point(0, 0);
            this.mst_editor_menu.Name = "mst_editor_menu";
            this.mst_editor_menu.Size = new System.Drawing.Size(1008, 24);
            this.mst_editor_menu.TabIndex = 0;
            this.mst_editor_menu.Text = "menuStrip1";
            // 
            // tsm_editor_menu_file
            // 
            this.tsm_editor_menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_editor_menu_file_new,
            this.tsm_editor_menu_file_open,
            this.tss_editor_menu_file_opnen_save,
            this.tsm_editor_menu_file_save,
            this.tsm_editor_menu_file_saveAs,
            this.tsm_editor_menu_file_export,
            this.tss_editor_menu_file_export_sendTo,
            this.tsm_editor_menu_file_sendTo,
            this.tsm_editor_menu_file_connection,
            this.tss_editor_menu_file_connection_exit,
            this.tsm_editor_menu_file_exit});
            this.tsm_editor_menu_file.Name = "tsm_editor_menu_file";
            this.tsm_editor_menu_file.Size = new System.Drawing.Size(46, 20);
            this.tsm_editor_menu_file.Text = "Datei";
            // 
            // tsm_editor_menu_file_new
            // 
            this.tsm_editor_menu_file_new.Name = "tsm_editor_menu_file_new";
            this.tsm_editor_menu_file_new.ShortcutKeyDisplayString = "STRG+N";
            this.tsm_editor_menu_file_new.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsm_editor_menu_file_new.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_new.Text = "Neu";
            this.tsm_editor_menu_file_new.Click += new System.EventHandler(this.tsm_editor_menu_file_new_Click);
            // 
            // tsm_editor_menu_file_open
            // 
            this.tsm_editor_menu_file_open.Name = "tsm_editor_menu_file_open";
            this.tsm_editor_menu_file_open.ShortcutKeyDisplayString = "STRG+O";
            this.tsm_editor_menu_file_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsm_editor_menu_file_open.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_open.Text = "Öffnen";
            this.tsm_editor_menu_file_open.Click += new System.EventHandler(this.tsm_editor_menu_file_open_Click_1);
            // 
            // tss_editor_menu_file_opnen_save
            // 
            this.tss_editor_menu_file_opnen_save.Name = "tss_editor_menu_file_opnen_save";
            this.tss_editor_menu_file_opnen_save.Size = new System.Drawing.Size(189, 6);
            // 
            // tsm_editor_menu_file_save
            // 
            this.tsm_editor_menu_file_save.Name = "tsm_editor_menu_file_save";
            this.tsm_editor_menu_file_save.ShortcutKeyDisplayString = "STRG+S";
            this.tsm_editor_menu_file_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsm_editor_menu_file_save.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_save.Text = "Speichern";
            this.tsm_editor_menu_file_save.Click += new System.EventHandler(this.tsm_editor_menu_file_save_Click);
            // 
            // tsm_editor_menu_file_saveAs
            // 
            this.tsm_editor_menu_file_saveAs.Name = "tsm_editor_menu_file_saveAs";
            this.tsm_editor_menu_file_saveAs.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_saveAs.Text = "Speichern unter...";
            this.tsm_editor_menu_file_saveAs.Click += new System.EventHandler(this.tsm_editor_menu_file_saveAs_Click);
            // 
            // tsm_editor_menu_file_export
            // 
            this.tsm_editor_menu_file_export.Name = "tsm_editor_menu_file_export";
            this.tsm_editor_menu_file_export.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_export.Text = "Exportieren";
            this.tsm_editor_menu_file_export.Click += new System.EventHandler(this.tsm_editor_menu_file_export_Click);
            // 
            // tss_editor_menu_file_export_sendTo
            // 
            this.tss_editor_menu_file_export_sendTo.Name = "tss_editor_menu_file_export_sendTo";
            this.tss_editor_menu_file_export_sendTo.Size = new System.Drawing.Size(189, 6);
            // 
            // tsm_editor_menu_file_sendTo
            // 
            this.tsm_editor_menu_file_sendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_editor_menu_sendTo_win8Device,
            this.tss_editor_menu_file_sendTo_win8Device_togleDebug,
            this.tsm_editor_menu_file_sendTo_togleDebug});
            this.tsm_editor_menu_file_sendTo.Enabled = false;
            this.tsm_editor_menu_file_sendTo.Name = "tsm_editor_menu_file_sendTo";
            this.tsm_editor_menu_file_sendTo.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_sendTo.Text = "Senden an...";
            // 
            // tsm_editor_menu_sendTo_win8Device
            // 
            this.tsm_editor_menu_sendTo_win8Device.Name = "tsm_editor_menu_sendTo_win8Device";
            this.tsm_editor_menu_sendTo_win8Device.Size = new System.Drawing.Size(171, 22);
            this.tsm_editor_menu_sendTo_win8Device.Text = "Windows 8 - Gerät";
            // 
            // tss_editor_menu_file_sendTo_win8Device_togleDebug
            // 
            this.tss_editor_menu_file_sendTo_win8Device_togleDebug.Name = "tss_editor_menu_file_sendTo_win8Device_togleDebug";
            this.tss_editor_menu_file_sendTo_win8Device_togleDebug.Size = new System.Drawing.Size(168, 6);
            // 
            // tsm_editor_menu_file_sendTo_togleDebug
            // 
            this.tsm_editor_menu_file_sendTo_togleDebug.CheckOnClick = true;
            this.tsm_editor_menu_file_sendTo_togleDebug.Name = "tsm_editor_menu_file_sendTo_togleDebug";
            this.tsm_editor_menu_file_sendTo_togleDebug.Size = new System.Drawing.Size(171, 22);
            this.tsm_editor_menu_file_sendTo_togleDebug.Text = "Debug";
            // 
            // tsm_editor_menu_file_connection
            // 
            this.tsm_editor_menu_file_connection.Enabled = false;
            this.tsm_editor_menu_file_connection.Name = "tsm_editor_menu_file_connection";
            this.tsm_editor_menu_file_connection.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_connection.Text = "Verbindung einrichten";
            // 
            // tss_editor_menu_file_connection_exit
            // 
            this.tss_editor_menu_file_connection_exit.Name = "tss_editor_menu_file_connection_exit";
            this.tss_editor_menu_file_connection_exit.Size = new System.Drawing.Size(189, 6);
            // 
            // tsm_editor_menu_file_exit
            // 
            this.tsm_editor_menu_file_exit.Name = "tsm_editor_menu_file_exit";
            this.tsm_editor_menu_file_exit.ShortcutKeyDisplayString = "STRG+Q";
            this.tsm_editor_menu_file_exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.tsm_editor_menu_file_exit.Size = new System.Drawing.Size(192, 22);
            this.tsm_editor_menu_file_exit.Text = "Beenden";
            this.tsm_editor_menu_file_exit.Click += new System.EventHandler(this.tsm_editor_menu_file_exit_Click);
            // 
            // tsm_editor_menu_edit
            // 
            this.tsm_editor_menu_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_editor_menu_edit_copie,
            this.tsm_editor_menu_edit_paste});
            this.tsm_editor_menu_edit.Name = "tsm_editor_menu_edit";
            this.tsm_editor_menu_edit.Size = new System.Drawing.Size(75, 20);
            this.tsm_editor_menu_edit.Text = "Bearbeiten";
            // 
            // tsm_editor_menu_edit_copie
            // 
            this.tsm_editor_menu_edit_copie.Enabled = false;
            this.tsm_editor_menu_edit_copie.Name = "tsm_editor_menu_edit_copie";
            this.tsm_editor_menu_edit_copie.ShortcutKeyDisplayString = "STRG+C";
            this.tsm_editor_menu_edit_copie.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsm_editor_menu_edit_copie.Size = new System.Drawing.Size(172, 22);
            this.tsm_editor_menu_edit_copie.Text = "Kopieren";
            // 
            // tsm_editor_menu_edit_paste
            // 
            this.tsm_editor_menu_edit_paste.Enabled = false;
            this.tsm_editor_menu_edit_paste.Name = "tsm_editor_menu_edit_paste";
            this.tsm_editor_menu_edit_paste.ShortcutKeyDisplayString = "STRG+V";
            this.tsm_editor_menu_edit_paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsm_editor_menu_edit_paste.Size = new System.Drawing.Size(172, 22);
            this.tsm_editor_menu_edit_paste.Text = "Einfügen";
            // 
            // tsm_editor_menu_test
            // 
            this.tsm_editor_menu_test.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_editor_menu_test_startImage,
            this.tsm_editor_menu_test_startVideo,
            this.tsm_editor_menu_test_startWithVirtualCamera,
            this.tss_editor_meu_test_loadVideo_togleDebug,
            this.tsm_editor_menu_test_togleDebug});
            this.tsm_editor_menu_test.Name = "tsm_editor_menu_test";
            this.tsm_editor_menu_test.Size = new System.Drawing.Size(41, 20);
            this.tsm_editor_menu_test.Text = "Test";
            // 
            // tsm_editor_menu_test_startImage
            // 
            this.tsm_editor_menu_test_startImage.Name = "tsm_editor_menu_test_startImage";
            this.tsm_editor_menu_test_startImage.Size = new System.Drawing.Size(144, 22);
            this.tsm_editor_menu_test_startImage.Text = "Bild laden";
            this.tsm_editor_menu_test_startImage.Click += new System.EventHandler(this.tsm_editor_menu_test_startImage_Click);
            // 
            // tsm_editor_menu_test_startVideo
            // 
            this.tsm_editor_menu_test_startVideo.Enabled = false;
            this.tsm_editor_menu_test_startVideo.Name = "tsm_editor_menu_test_startVideo";
            this.tsm_editor_menu_test_startVideo.Size = new System.Drawing.Size(144, 22);
            this.tsm_editor_menu_test_startVideo.Text = "Video laden";
            this.tsm_editor_menu_test_startVideo.Click += new System.EventHandler(this.tsm_editor_menu_test_startVideo_Click);
            // 
            // tsm_editor_menu_test_startWithVirtualCamera
            // 
            this.tsm_editor_menu_test_startWithVirtualCamera.Name = "tsm_editor_menu_test_startWithVirtualCamera";
            this.tsm_editor_menu_test_startWithVirtualCamera.Size = new System.Drawing.Size(144, 22);
            this.tsm_editor_menu_test_startWithVirtualCamera.Text = "vCam nutzen";
            this.tsm_editor_menu_test_startWithVirtualCamera.Click += new System.EventHandler(this.tsm_editor_menu_test_startWithVirtualCamera_Click);
            // 
            // tss_editor_meu_test_loadVideo_togleDebug
            // 
            this.tss_editor_meu_test_loadVideo_togleDebug.Name = "tss_editor_meu_test_loadVideo_togleDebug";
            this.tss_editor_meu_test_loadVideo_togleDebug.Size = new System.Drawing.Size(141, 6);
            // 
            // tsm_editor_menu_test_togleDebug
            // 
            this.tsm_editor_menu_test_togleDebug.CheckOnClick = true;
            this.tsm_editor_menu_test_togleDebug.Enabled = false;
            this.tsm_editor_menu_test_togleDebug.Name = "tsm_editor_menu_test_togleDebug";
            this.tsm_editor_menu_test_togleDebug.Size = new System.Drawing.Size(144, 22);
            this.tsm_editor_menu_test_togleDebug.Text = "Debug";
            // 
            // tsm_editor_menu_help
            // 
            this.tsm_editor_menu_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_editor_menu_help_help,
            this.tsm_editor_menu_help_info});
            this.tsm_editor_menu_help.Name = "tsm_editor_menu_help";
            this.tsm_editor_menu_help.Size = new System.Drawing.Size(44, 20);
            this.tsm_editor_menu_help.Text = "Hilfe";
            // 
            // tsm_editor_menu_help_help
            // 
            this.tsm_editor_menu_help_help.Enabled = false;
            this.tsm_editor_menu_help_help.Name = "tsm_editor_menu_help_help";
            this.tsm_editor_menu_help_help.Size = new System.Drawing.Size(99, 22);
            this.tsm_editor_menu_help_help.Text = "Hilfe";
            // 
            // tsm_editor_menu_help_info
            // 
            this.tsm_editor_menu_help_info.Name = "tsm_editor_menu_help_info";
            this.tsm_editor_menu_help_info.Size = new System.Drawing.Size(99, 22);
            this.tsm_editor_menu_help_info.Text = "Info";
            this.tsm_editor_menu_help_info.Click += new System.EventHandler(this.tsm_editor_menu_help_info_Click);
            // 
            // pnl_editor_selection
            // 
            this.pnl_editor_selection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_editor_selection.Controls.Add(this.cmb_editor_selection_toolSelection);
            this.pnl_editor_selection.Location = new System.Drawing.Point(0, 27);
            this.pnl_editor_selection.Name = "pnl_editor_selection";
            this.pnl_editor_selection.Size = new System.Drawing.Size(135, 673);
            this.pnl_editor_selection.TabIndex = 1;
            // 
            // cmb_editor_selection_toolSelection
            // 
            this.cmb_editor_selection_toolSelection.DisplayMember = "CategoryName";
            this.cmb_editor_selection_toolSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_editor_selection_toolSelection.DropDownWidth = 128;
            this.cmb_editor_selection_toolSelection.FormattingEnabled = true;
            this.cmb_editor_selection_toolSelection.ItemHeight = 13;
            this.cmb_editor_selection_toolSelection.Location = new System.Drawing.Point(3, 3);
            this.cmb_editor_selection_toolSelection.MaxDropDownItems = 4;
            this.cmb_editor_selection_toolSelection.Name = "cmb_editor_selection_toolSelection";
            this.cmb_editor_selection_toolSelection.Size = new System.Drawing.Size(128, 21);
            this.cmb_editor_selection_toolSelection.TabIndex = 0;
            this.cmb_editor_selection_toolSelection.SelectedIndexChanged += new System.EventHandler(this.cmb_editor_selection_toolSelection_SelectedIndexChanged);
            // 
            // pnl_editor_preview
            // 
            this.pnl_editor_preview.AllowDrop = true;
            this.pnl_editor_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_editor_preview.Location = new System.Drawing.Point(141, 27);
            this.pnl_editor_preview.Name = "pnl_editor_preview";
            this.pnl_editor_preview.Size = new System.Drawing.Size(661, 553);
            this.pnl_editor_preview.TabIndex = 2;
            this.pnl_editor_preview.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnl_editor_preview_DragDrop);
            this.pnl_editor_preview.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnl_editor_preview_DragEnter);
            // 
            // pnl_editor_properties
            // 
            this.pnl_editor_properties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_editor_properties.Controls.Add(this.propertyGrid1);
            this.pnl_editor_properties.Controls.Add(this.cmb_editor_properties_objectSelection);
            this.pnl_editor_properties.Location = new System.Drawing.Point(808, 27);
            this.pnl_editor_properties.Name = "pnl_editor_properties";
            this.pnl_editor_properties.Size = new System.Drawing.Size(200, 673);
            this.pnl_editor_properties.TabIndex = 2;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(198, 650);
            this.propertyGrid1.TabIndex = 2;
            // 
            // cmb_editor_properties_objectSelection
            // 
            this.cmb_editor_properties_objectSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmb_editor_properties_objectSelection.FormattingEnabled = true;
            this.cmb_editor_properties_objectSelection.Items.AddRange(new object[] {
            "Objekt wählen..."});
            this.cmb_editor_properties_objectSelection.Location = new System.Drawing.Point(0, 0);
            this.cmb_editor_properties_objectSelection.MaxDropDownItems = 1;
            this.cmb_editor_properties_objectSelection.Name = "cmb_editor_properties_objectSelection";
            this.cmb_editor_properties_objectSelection.Size = new System.Drawing.Size(198, 21);
            this.cmb_editor_properties_objectSelection.TabIndex = 1;
            this.cmb_editor_properties_objectSelection.Text = "Objekt wählen...";
            // 
            // pnl_editor_scenes
            // 
            this.pnl_editor_scenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_delete);
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_scene_1);
            this.pnl_editor_scenes.Controls.Add(this.btn_editor_scene_new);
            this.pnl_editor_scenes.Location = new System.Drawing.Point(141, 586);
            this.pnl_editor_scenes.Name = "pnl_editor_scenes";
            this.pnl_editor_scenes.Size = new System.Drawing.Size(661, 114);
            this.pnl_editor_scenes.TabIndex = 2;
            // 
            // btn_editor_scene_delete
            // 
            this.btn_editor_scene_delete.Location = new System.Drawing.Point(611, 34);
            this.btn_editor_scene_delete.Name = "btn_editor_scene_delete";
            this.btn_editor_scene_delete.Size = new System.Drawing.Size(45, 45);
            this.btn_editor_scene_delete.TabIndex = 2;
            this.btn_editor_scene_delete.Text = "-";
            this.btn_editor_scene_delete.UseVisualStyleBackColor = true;
            this.btn_editor_scene_delete.Click += new System.EventHandler(this.btn_editor_scene_scene_remove);
            // 
            // btn_editor_scene_scene_1
            // 
            this.btn_editor_scene_scene_1.Location = new System.Drawing.Point(54, 34);
            this.btn_editor_scene_scene_1.Name = "btn_editor_scene_scene_1";
            this.btn_editor_scene_scene_1.Size = new System.Drawing.Size(46, 45);
            this.btn_editor_scene_scene_1.TabIndex = 1;
            this.btn_editor_scene_scene_1.Text = "1";
            this.btn_editor_scene_scene_1.UseVisualStyleBackColor = false;
            this.btn_editor_scene_scene_1.Click += new System.EventHandler(this.btn_editor_scene_scene_change);
            // 
            // btn_editor_scene_new
            // 
            this.btn_editor_scene_new.BackColor = System.Drawing.Color.DarkGray;
            this.btn_editor_scene_new.Location = new System.Drawing.Point(3, 34);
            this.btn_editor_scene_new.Name = "btn_editor_scene_new";
            this.btn_editor_scene_new.Size = new System.Drawing.Size(45, 45);
            this.btn_editor_scene_new.TabIndex = 0;
            this.btn_editor_scene_new.Text = "+";
            this.btn_editor_scene_new.UseVisualStyleBackColor = true;
            this.btn_editor_scene_new.Click += new System.EventHandler(this.btn_editor_scene_scene_new);
            // 
            // pnl_editor_status
            // 
            this.pnl_editor_status.Controls.Add(this.lbl_version);
            this.pnl_editor_status.Location = new System.Drawing.Point(0, 706);
            this.pnl_editor_status.Name = "pnl_editor_status";
            this.pnl_editor_status.Size = new System.Drawing.Size(1008, 23);
            this.pnl_editor_status.TabIndex = 3;
            // 
            // lbl_version
            // 
            this.lbl_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_version.AutoSize = true;
            this.lbl_version.Location = new System.Drawing.Point(931, 10);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(76, 13);
            this.lbl_version.TabIndex = 0;
            this.lbl_version.Text = "ARdevKit v0.1";
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.pnl_editor_status);
            this.Controls.Add(this.pnl_editor_scenes);
            this.Controls.Add(this.pnl_editor_properties);
            this.Controls.Add(this.pnl_editor_preview);
            this.Controls.Add(this.pnl_editor_selection);
            this.Controls.Add(this.mst_editor_menu);
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "EditorWindow";
            this.Text = "ARdevKit";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.mst_editor_menu.ResumeLayout(false);
            this.mst_editor_menu.PerformLayout();
            this.pnl_editor_selection.ResumeLayout(false);
            this.pnl_editor_properties.ResumeLayout(false);
            this.pnl_editor_scenes.ResumeLayout(false);
            this.pnl_editor_status.ResumeLayout(false);
            this.pnl_editor_status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mst_editor_menu;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_new;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_open;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_save;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_saveAs;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_sendTo;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_export;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_connection;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_exit;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_edit;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_test;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_test_startWithVirtualCamera;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_test_togleDebug;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_help;
        private System.Windows.Forms.ToolStripSeparator tss_editor_menu_file_opnen_save;
        private System.Windows.Forms.ToolStripSeparator tss_editor_menu_file_export_sendTo;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_sendTo_win8Device;
        private System.Windows.Forms.ToolStripSeparator tss_editor_menu_file_sendTo_win8Device_togleDebug;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_file_sendTo_togleDebug;
        private System.Windows.Forms.ToolStripSeparator tss_editor_menu_file_connection_exit;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_edit_copie;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_edit_paste;
        private System.Windows.Forms.ToolStripSeparator tss_editor_meu_test_loadVideo_togleDebug;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_help_help;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_help_info;
        private System.Windows.Forms.Panel pnl_editor_selection;
        private System.Windows.Forms.Panel pnl_editor_preview;
        private System.Windows.Forms.Panel pnl_editor_scenes;
        private System.Windows.Forms.Panel pnl_editor_properties;
        private System.Windows.Forms.Panel pnl_editor_status;
        private System.Windows.Forms.ComboBox cmb_editor_selection_toolSelection;
        private System.Windows.Forms.ComboBox cmb_editor_properties_objectSelection;
        private System.Windows.Forms.Button btn_editor_scene_new;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_test_startImage;
        private System.Windows.Forms.ToolStripMenuItem tsm_editor_menu_test_startVideo;
        private System.Windows.Forms.Button btn_editor_scene_scene_1;
        private System.Windows.Forms.Button btn_editor_scene_delete;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Label lbl_version;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the PropertyGrid1.
        /// </summary>
        ///
        /// <value>
        /// PropertyGrid.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public System.Windows.Forms.PropertyGrid PropertyGrid1
        {
            get { return propertyGrid1; }
            set { propertyGrid1 = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the pnl editor preview.
        /// </summary>
        ///
        /// <value>
        /// The pnl editor preview.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public System.Windows.Forms.Panel Pnl_editor_preview
        {
            get { return pnl_editor_preview; }
            set { pnl_editor_preview = value; }
        }

        /**
         * <summary>    Gets or sets the pnl editor selection. </summary>
         *
         * <value>  The pnl editor selection. </value>
         */

        public System.Windows.Forms.Panel Pnl_editor_selection
        {
            get { return pnl_editor_selection; }
            set { pnl_editor_selection = value; }
        }

        /**
         * <summary>    Gets or sets the cmb editor selection tool selection. </summary>
         *
         * <value>  The cmb editor selection tool selection. </value>
         */

        public System.Windows.Forms.ComboBox Cmb_editor_selection_toolSelection
        {
            get { return cmb_editor_selection_toolSelection; }
            set { cmb_editor_selection_toolSelection = value; }
        }

        /// <summary>
        /// Gets or sets the tsm_editor_menu_edit_copie.
        /// </summary>
        /// <value>
        /// The tsm_editor_menu_edit_copie.
        /// </value>
        public System.Windows.Forms.ToolStripMenuItem Tsm_editor_menu_edit_copie
        {
            get { return tsm_editor_menu_edit_copie; }
            set {tsm_editor_menu_edit_copie = value; }
        }

        /// <summary>
        /// Gets or sets the tsm_editor_menu_edit_paste.
        /// </summary>
        /// <value>
        /// The tsm_editor_menu_edit_paste.
        /// </value>
        public System.Windows.Forms.ToolStripMenuItem Tsm_editor_menu_edit_paste
        {
            get { return tsm_editor_menu_edit_paste; }
            set { tsm_editor_menu_edit_paste = value; }
        }
    }
}

