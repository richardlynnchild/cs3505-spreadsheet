﻿namespace SpreadsheetGUI
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.FormulaBox = new System.Windows.Forms.TextBox();
            this.CellName = new System.Windows.Forms.Label();
            this.LabelContents = new System.Windows.Forms.Label();
            this.EnterButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_New = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMoveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpSetMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpOtherMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMoveText = new System.Windows.Forms.TextBox();
            this.HelpSetText = new System.Windows.Forms.TextBox();
            this.HelpOtherText = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelValue = new System.Windows.Forms.Label();
            this.CellNameOutput = new System.Windows.Forms.TextBox();
            this.CellValueOutput = new System.Windows.Forms.TextBox();
            this.ReturnColumn = new System.Windows.Forms.Button();
            this.ReturnRow = new System.Windows.Forms.Button();
            this.OutputColumnInfo = new System.Windows.Forms.TextBox();
            this.OutputRowInfo = new System.Windows.Forms.TextBox();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.FilePanel = new System.Windows.Forms.Panel();
            this.MovementBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FileMenuLabel = new System.Windows.Forms.Label();
            this.Open_FileMenu = new System.Windows.Forms.Button();
            this.FileTextSelect = new System.Windows.Forms.TextBox();
            this.FileList = new System.Windows.Forms.ListView();
            this.NameColHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateColHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.undo_button = new System.Windows.Forms.Button();
            this.ColumnExit = new System.Windows.Forms.Button();
            this.RowExit = new System.Windows.Forms.Button();
            this.CloseSet = new System.Windows.Forms.Button();
            this.CloseOther = new System.Windows.Forms.Button();
            this.CloseMove = new System.Windows.Forms.Button();
            this.revert_button = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.FilePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovementBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.AutoSize = true;
            this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 114);
            this.spreadsheetPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(1178, 635);
            this.spreadsheetPanel1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormulaBox
            // 
            this.FormulaBox.Location = new System.Drawing.Point(394, 62);
            this.FormulaBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FormulaBox.Name = "FormulaBox";
            this.FormulaBox.Size = new System.Drawing.Size(296, 26);
            this.FormulaBox.TabIndex = 6;
            // 
            // CellName
            // 
            this.CellName.Location = new System.Drawing.Point(0, 0);
            this.CellName.Name = "CellName";
            this.CellName.Size = new System.Drawing.Size(100, 23);
            this.CellName.TabIndex = 0;
            // 
            // LabelContents
            // 
            this.LabelContents.AutoSize = true;
            this.LabelContents.Location = new System.Drawing.Point(290, 68);
            this.LabelContents.Name = "LabelContents";
            this.LabelContents.Size = new System.Drawing.Size(104, 20);
            this.LabelContents.TabIndex = 7;
            this.LabelContents.Text = "Cell Contents";
            // 
            // EnterButton
            // 
            this.EnterButton.Location = new System.Drawing.Point(700, 60);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(75, 34);
            this.EnterButton.TabIndex = 8;
            this.EnterButton.Text = "Enter";
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 49);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1178, 49);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu_New,
            this.FileMenu_Save,
            this.FileMenu_Open,
            this.FileMenu_Close});
            this.fileToolStripMenuItem.Enabled = false;
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 43);
            this.fileToolStripMenuItem.Visible = false;
            // 
            // FileMenu_New
            // 
            this.FileMenu_New.Name = "FileMenu_New";
            this.FileMenu_New.Size = new System.Drawing.Size(210, 30);
            this.FileMenu_New.Text = "New";
            // 
            // FileMenu_Save
            // 
            this.FileMenu_Save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.FileMenu_Save.Name = "FileMenu_Save";
            this.FileMenu_Save.Size = new System.Drawing.Size(210, 30);
            this.FileMenu_Save.Text = "Save";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // FileMenu_Open
            // 
            this.FileMenu_Open.Name = "FileMenu_Open";
            this.FileMenu_Open.Size = new System.Drawing.Size(210, 30);
            this.FileMenu_Open.Text = "Open";
            // 
            // FileMenu_Close
            // 
            this.FileMenu_Close.Name = "FileMenu_Close";
            this.FileMenu_Close.Size = new System.Drawing.Size(210, 30);
            this.FileMenu_Close.Text = "Close";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1178, 65);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenu});
            this.menuStrip2.Location = new System.Drawing.Point(135, 5);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip2.Size = new System.Drawing.Size(202, 24);
            this.menuStrip2.TabIndex = 11;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.Visible = false;
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMoveMenu,
            this.HelpSetMenu,
            this.HelpOtherMenu});
            this.HelpMenu.Enabled = false;
            this.HelpMenu.Image = ((System.Drawing.Image)(resources.GetObject("HelpMenu.Image")));
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(81, 29);
            this.HelpMenu.Text = "Help";
            this.HelpMenu.Visible = false;
            // 
            // HelpMoveMenu
            // 
            this.HelpMoveMenu.Name = "HelpMoveMenu";
            this.HelpMoveMenu.Size = new System.Drawing.Size(200, 30);
            this.HelpMoveMenu.Text = "Cell Selection";
            this.HelpMoveMenu.Click += new System.EventHandler(this.HelpMoveMenu_Click);
            // 
            // HelpSetMenu
            // 
            this.HelpSetMenu.Name = "HelpSetMenu";
            this.HelpSetMenu.Size = new System.Drawing.Size(200, 30);
            this.HelpSetMenu.Text = "Cell Editing";
            this.HelpSetMenu.Click += new System.EventHandler(this.HelpSetMenu_Click);
            // 
            // HelpOtherMenu
            // 
            this.HelpOtherMenu.Name = "HelpOtherMenu";
            this.HelpOtherMenu.Size = new System.Drawing.Size(200, 30);
            this.HelpOtherMenu.Text = "File Menu";
            this.HelpOtherMenu.Click += new System.EventHandler(this.HelpOtherMenu_Click);
            // 
            // HelpMoveText
            // 
            this.HelpMoveText.Location = new System.Drawing.Point(33, 132);
            this.HelpMoveText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.HelpMoveText.Multiline = true;
            this.HelpMoveText.Name = "HelpMoveText";
            this.HelpMoveText.ReadOnly = true;
            this.HelpMoveText.Size = new System.Drawing.Size(606, 199);
            this.HelpMoveText.TabIndex = 12;
            this.HelpMoveText.Text = resources.GetString("HelpMoveText.Text");
            this.HelpMoveText.Visible = false;
            // 
            // HelpSetText
            // 
            this.HelpSetText.Location = new System.Drawing.Point(33, 132);
            this.HelpSetText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.HelpSetText.Multiline = true;
            this.HelpSetText.Name = "HelpSetText";
            this.HelpSetText.ReadOnly = true;
            this.HelpSetText.Size = new System.Drawing.Size(606, 199);
            this.HelpSetText.TabIndex = 14;
            this.HelpSetText.Text = resources.GetString("HelpSetText.Text");
            this.HelpSetText.Visible = false;
            // 
            // HelpOtherText
            // 
            this.HelpOtherText.Location = new System.Drawing.Point(33, 132);
            this.HelpOtherText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.HelpOtherText.Multiline = true;
            this.HelpOtherText.Name = "HelpOtherText";
            this.HelpOtherText.ReadOnly = true;
            this.HelpOtherText.Size = new System.Drawing.Size(606, 376);
            this.HelpOtherText.TabIndex = 17;
            this.HelpOtherText.Text = resources.GetString("HelpOtherText.Text");
            this.HelpOtherText.Visible = false;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(16, 54);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(81, 20);
            this.LabelName.TabIndex = 0;
            this.LabelName.Text = "Cell Name";
            // 
            // LabelValue
            // 
            this.LabelValue.AutoSize = true;
            this.LabelValue.Location = new System.Drawing.Point(150, 54);
            this.LabelValue.Name = "LabelValue";
            this.LabelValue.Size = new System.Drawing.Size(80, 20);
            this.LabelValue.TabIndex = 18;
            this.LabelValue.Text = "Cell Value";
            // 
            // CellNameOutput
            // 
            this.CellNameOutput.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CellNameOutput.Location = new System.Drawing.Point(20, 74);
            this.CellNameOutput.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CellNameOutput.Name = "CellNameOutput";
            this.CellNameOutput.ReadOnly = true;
            this.CellNameOutput.Size = new System.Drawing.Size(78, 26);
            this.CellNameOutput.TabIndex = 19;
            this.CellNameOutput.Text = "A1";
            this.CellNameOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CellValueOutput
            // 
            this.CellValueOutput.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CellValueOutput.Location = new System.Drawing.Point(110, 74);
            this.CellValueOutput.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CellValueOutput.Name = "CellValueOutput";
            this.CellValueOutput.ReadOnly = true;
            this.CellValueOutput.Size = new System.Drawing.Size(150, 26);
            this.CellValueOutput.TabIndex = 20;
            this.CellValueOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReturnColumn
            // 
            this.ReturnColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnColumn.Location = new System.Drawing.Point(294, 2);
            this.ReturnColumn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ReturnColumn.Name = "ReturnColumn";
            this.ReturnColumn.Size = new System.Drawing.Size(171, 45);
            this.ReturnColumn.TabIndex = 21;
            this.ReturnColumn.Text = "Col Information";
            this.ReturnColumn.UseVisualStyleBackColor = true;
            this.ReturnColumn.Click += new System.EventHandler(this.ReturnCollumn_Click);
            // 
            // ReturnRow
            // 
            this.ReturnRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnRow.Location = new System.Drawing.Point(472, 2);
            this.ReturnRow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ReturnRow.Name = "ReturnRow";
            this.ReturnRow.Size = new System.Drawing.Size(162, 45);
            this.ReturnRow.TabIndex = 22;
            this.ReturnRow.Text = "Row Information";
            this.ReturnRow.UseVisualStyleBackColor = true;
            this.ReturnRow.Click += new System.EventHandler(this.ReturnRow_Click);
            // 
            // OutputColumnInfo
            // 
            this.OutputColumnInfo.Location = new System.Drawing.Point(248, 106);
            this.OutputColumnInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OutputColumnInfo.Multiline = true;
            this.OutputColumnInfo.Name = "OutputColumnInfo";
            this.OutputColumnInfo.ReadOnly = true;
            this.OutputColumnInfo.Size = new System.Drawing.Size(211, 162);
            this.OutputColumnInfo.TabIndex = 23;
            this.OutputColumnInfo.Text = " \r\n \r\n \r\n \r\n ";
            this.OutputColumnInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OutputColumnInfo.Visible = false;
            // 
            // OutputRowInfo
            // 
            this.OutputRowInfo.Location = new System.Drawing.Point(399, 109);
            this.OutputRowInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OutputRowInfo.Multiline = true;
            this.OutputRowInfo.Name = "OutputRowInfo";
            this.OutputRowInfo.ReadOnly = true;
            this.OutputRowInfo.Size = new System.Drawing.Size(210, 156);
            this.OutputRowInfo.TabIndex = 25;
            this.OutputRowInfo.Text = " \r\n \r\n \r\n \r\n ";
            this.OutputRowInfo.Visible = false;
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.ServerTextBox.Location = new System.Drawing.Point(856, 9);
            this.ServerTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(182, 26);
            this.ServerTextBox.TabIndex = 27;
            this.ServerTextBox.Text = "Enter Hostname";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(1050, 8);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(110, 32);
            this.ConnectButton.TabIndex = 28;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // FilePanel
            // 
            this.FilePanel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.FilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilePanel.Controls.Add(this.MovementBox);
            this.FilePanel.Controls.Add(this.panel1);
            this.FilePanel.Controls.Add(this.Open_FileMenu);
            this.FilePanel.Controls.Add(this.FileTextSelect);
            this.FilePanel.Controls.Add(this.FileList);
            this.FilePanel.Location = new System.Drawing.Point(135, 132);
            this.FilePanel.Name = "FilePanel";
            this.FilePanel.Size = new System.Drawing.Size(844, 588);
            this.FilePanel.TabIndex = 29;
            // 
            // MovementBox
            // 
            this.MovementBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MovementBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.MovementBox.Location = new System.Drawing.Point(740, 532);
            this.MovementBox.Name = "MovementBox";
            this.MovementBox.Size = new System.Drawing.Size(100, 49);
            this.MovementBox.TabIndex = 5;
            this.MovementBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.FileMenuLabel);
            this.panel1.Location = new System.Drawing.Point(20, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 46);
            this.panel1.TabIndex = 4;
            // 
            // FileMenuLabel
            // 
            this.FileMenuLabel.AutoSize = true;
            this.FileMenuLabel.Font = new System.Drawing.Font("Cambria", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileMenuLabel.Location = new System.Drawing.Point(300, 5);
            this.FileMenuLabel.Name = "FileMenuLabel";
            this.FileMenuLabel.Size = new System.Drawing.Size(215, 37);
            this.FileMenuLabel.TabIndex = 0;
            this.FileMenuLabel.Text = "Spreadsheets";
            // 
            // Open_FileMenu
            // 
            this.Open_FileMenu.Location = new System.Drawing.Point(572, 534);
            this.Open_FileMenu.Name = "Open_FileMenu";
            this.Open_FileMenu.Size = new System.Drawing.Size(110, 38);
            this.Open_FileMenu.TabIndex = 3;
            this.Open_FileMenu.Text = "Open";
            this.Open_FileMenu.UseVisualStyleBackColor = true;
            // 
            // FileTextSelect
            // 
            this.FileTextSelect.Location = new System.Drawing.Point(20, 538);
            this.FileTextSelect.Name = "FileTextSelect";
            this.FileTextSelect.Size = new System.Drawing.Size(546, 26);
            this.FileTextSelect.TabIndex = 2;
            // 
            // FileList
            // 
            this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColHeader,
            this.DateColHeader});
            this.FileList.Location = new System.Drawing.Point(20, 80);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(802, 432);
            this.FileList.TabIndex = 1;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.View = System.Windows.Forms.View.Details;
            // 
            // NameColHeader
            // 
            this.NameColHeader.Text = "Name";
            this.NameColHeader.Width = 480;
            // 
            // DateColHeader
            // 
            this.DateColHeader.Text = "Date";
            this.DateColHeader.Width = 320;
            // 
            // undo_button
            // 
            this.undo_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.undo_button.Image = global::SpreadsheetGUI.Properties.Resources.undo1;
            this.undo_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.undo_button.Location = new System.Drawing.Point(13, 6);
            this.undo_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.undo_button.Name = "undo_button";
            this.undo_button.Size = new System.Drawing.Size(112, 43);
            this.undo_button.TabIndex = 30;
            this.undo_button.Text = "Undo";
            this.undo_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.undo_button.UseVisualStyleBackColor = true;
            this.undo_button.Click += new System.EventHandler(this.undo_button_Click);
            // 
            // ColumnExit
            // 
            this.ColumnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ColumnExit.BackgroundImage")));
            this.ColumnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ColumnExit.Location = new System.Drawing.Point(438, 111);
            this.ColumnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ColumnExit.Name = "ColumnExit";
            this.ColumnExit.Size = new System.Drawing.Size(22, 25);
            this.ColumnExit.TabIndex = 24;
            this.ColumnExit.UseVisualStyleBackColor = true;
            this.ColumnExit.Visible = false;
            this.ColumnExit.Click += new System.EventHandler(this.ColumnExit_Click);
            // 
            // RowExit
            // 
            this.RowExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RowExit.BackgroundImage")));
            this.RowExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RowExit.Location = new System.Drawing.Point(588, 108);
            this.RowExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RowExit.Name = "RowExit";
            this.RowExit.Size = new System.Drawing.Size(22, 25);
            this.RowExit.TabIndex = 26;
            this.RowExit.UseVisualStyleBackColor = true;
            this.RowExit.Visible = false;
            this.RowExit.Click += new System.EventHandler(this.RowExit_Click);
            // 
            // CloseSet
            // 
            this.CloseSet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseSet.BackgroundImage")));
            this.CloseSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseSet.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.CloseSet.Location = new System.Drawing.Point(616, 132);
            this.CloseSet.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CloseSet.Name = "CloseSet";
            this.CloseSet.Size = new System.Drawing.Size(22, 25);
            this.CloseSet.TabIndex = 15;
            this.CloseSet.Text = "_";
            this.CloseSet.UseVisualStyleBackColor = true;
            this.CloseSet.Visible = false;
            this.CloseSet.Click += new System.EventHandler(this.CloseSet_Click);
            // 
            // CloseOther
            // 
            this.CloseOther.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseOther.BackgroundImage")));
            this.CloseOther.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseOther.Location = new System.Drawing.Point(616, 134);
            this.CloseOther.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CloseOther.Name = "CloseOther";
            this.CloseOther.Size = new System.Drawing.Size(22, 25);
            this.CloseOther.TabIndex = 16;
            this.CloseOther.Text = "_";
            this.CloseOther.UseVisualStyleBackColor = true;
            this.CloseOther.Visible = false;
            this.CloseOther.Click += new System.EventHandler(this.CloseOther_Click_1);
            // 
            // CloseMove
            // 
            this.CloseMove.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.CloseMove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseMove.BackgroundImage")));
            this.CloseMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseMove.Location = new System.Drawing.Point(616, 134);
            this.CloseMove.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CloseMove.Name = "CloseMove";
            this.CloseMove.Size = new System.Drawing.Size(22, 25);
            this.CloseMove.TabIndex = 13;
            this.CloseMove.Text = "_";
            this.CloseMove.UseVisualStyleBackColor = true;
            this.CloseMove.Visible = false;
            this.CloseMove.Click += new System.EventHandler(this.CloseMove_Click_1);
            // 
            // revert_button
            // 
            this.revert_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revert_button.Location = new System.Drawing.Point(135, 5);
            this.revert_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.revert_button.Name = "revert_button";
            this.revert_button.Size = new System.Drawing.Size(106, 44);
            this.revert_button.TabIndex = 31;
            this.revert_button.Text = "Revert";
            this.revert_button.UseVisualStyleBackColor = true;
            this.revert_button.Click += new System.EventHandler(this.revert_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1178, 749);
            this.Controls.Add(this.undo_button);
            this.Controls.Add(this.revert_button);
            this.Controls.Add(this.FilePanel);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.ColumnExit);
            this.Controls.Add(this.RowExit);
            this.Controls.Add(this.OutputColumnInfo);
            this.Controls.Add(this.OutputRowInfo);
            this.Controls.Add(this.ReturnRow);
            this.Controls.Add(this.ReturnColumn);
            this.Controls.Add(this.CellValueOutput);
            this.Controls.Add(this.CellNameOutput);
            this.Controls.Add(this.LabelValue);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.CloseSet);
            this.Controls.Add(this.CloseOther);
            this.Controls.Add(this.CloseMove);
            this.Controls.Add(this.HelpMoveText);
            this.Controls.Add(this.HelpOtherText);
            this.Controls.Add(this.HelpSetText);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.LabelContents);
            this.Controls.Add(this.FormulaBox);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.FilePanel.ResumeLayout(false);
            this.FilePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovementBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox FormulaBox;
        private System.Windows.Forms.Label CellName;
        private System.Windows.Forms.Label LabelContents;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_New;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Save;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Open;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpMoveMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpSetMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpOtherMenu;
        private System.Windows.Forms.TextBox HelpMoveText;
        private System.Windows.Forms.TextBox HelpOtherText;
        private System.Windows.Forms.TextBox HelpSetText;
        private System.Windows.Forms.Button CloseMove;
        private System.Windows.Forms.Button CloseSet;
        private System.Windows.Forms.Button CloseOther;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label LabelValue;
        private System.Windows.Forms.TextBox CellNameOutput;
        private System.Windows.Forms.TextBox CellValueOutput;
        private System.Windows.Forms.Button ReturnColumn;
        private System.Windows.Forms.Button ReturnRow;
        private System.Windows.Forms.TextBox OutputColumnInfo;
        private System.Windows.Forms.Button ColumnExit;
        private System.Windows.Forms.TextBox OutputRowInfo;
        private System.Windows.Forms.Button RowExit;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Panel FilePanel;
        private System.Windows.Forms.Button Open_FileMenu;
        private System.Windows.Forms.TextBox FileTextSelect;
        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FileMenuLabel;
        private System.Windows.Forms.ColumnHeader NameColHeader;
        private System.Windows.Forms.ColumnHeader DateColHeader;
        private System.Windows.Forms.PictureBox MovementBox;
        private System.Windows.Forms.Button undo_button;
        private System.Windows.Forms.Button revert_button;
    }
}

