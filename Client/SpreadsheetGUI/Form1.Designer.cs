namespace SpreadsheetGUI
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
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.NewSpreadsheetButton = new System.Windows.Forms.ToolStripMenuItem();
            this.undo_button = new System.Windows.Forms.ToolStripMenuItem();
            this.revert_button = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ColumnExit = new System.Windows.Forms.Button();
            this.RowExit = new System.Windows.Forms.Button();
            this.CloseSet = new System.Windows.Forms.Button();
            this.CloseOther = new System.Windows.Forms.Button();
            this.CloseMove = new System.Windows.Forms.Button();
            this.menuStrip2.SuspendLayout();
            this.FilePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovementBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.AutoSize = true;
            this.spreadsheetPanel1.BackColor = System.Drawing.Color.DimGray;
            this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 0);
            this.spreadsheetPanel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(785, 487);
            this.spreadsheetPanel1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormulaBox
            // 
            this.FormulaBox.BackColor = System.Drawing.Color.Gainsboro;
            this.FormulaBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FormulaBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormulaBox.Location = new System.Drawing.Point(518, 25);
            this.FormulaBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FormulaBox.Name = "FormulaBox";
            this.FormulaBox.ReadOnly = true;
            this.FormulaBox.Size = new System.Drawing.Size(146, 20);
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
            this.LabelContents.BackColor = System.Drawing.Color.DimGray;
            this.LabelContents.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelContents.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.LabelContents.Location = new System.Drawing.Point(431, 28);
            this.LabelContents.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelContents.Name = "LabelContents";
            this.LabelContents.Size = new System.Drawing.Size(85, 15);
            this.LabelContents.TabIndex = 7;
            this.LabelContents.Text = "Cell Contents:";
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
            this.menuStrip2.Location = new System.Drawing.Point(90, 3);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
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
            this.HelpMenu.Size = new System.Drawing.Size(64, 24);
            this.HelpMenu.Text = "Help";
            this.HelpMenu.Visible = false;
            // 
            // HelpMoveMenu
            // 
            this.HelpMoveMenu.Name = "HelpMoveMenu";
            this.HelpMoveMenu.Size = new System.Drawing.Size(145, 22);
            this.HelpMoveMenu.Text = "Cell Selection";
            this.HelpMoveMenu.Click += new System.EventHandler(this.HelpMoveMenu_Click);
            // 
            // HelpSetMenu
            // 
            this.HelpSetMenu.Name = "HelpSetMenu";
            this.HelpSetMenu.Size = new System.Drawing.Size(145, 22);
            this.HelpSetMenu.Text = "Cell Editing";
            this.HelpSetMenu.Click += new System.EventHandler(this.HelpSetMenu_Click);
            // 
            // HelpOtherMenu
            // 
            this.HelpOtherMenu.Name = "HelpOtherMenu";
            this.HelpOtherMenu.Size = new System.Drawing.Size(145, 22);
            this.HelpOtherMenu.Text = "File Menu";
            this.HelpOtherMenu.Click += new System.EventHandler(this.HelpOtherMenu_Click);
            // 
            // HelpMoveText
            // 
            this.HelpMoveText.Location = new System.Drawing.Point(22, 86);
            this.HelpMoveText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.HelpMoveText.Multiline = true;
            this.HelpMoveText.Name = "HelpMoveText";
            this.HelpMoveText.ReadOnly = true;
            this.HelpMoveText.Size = new System.Drawing.Size(405, 131);
            this.HelpMoveText.TabIndex = 12;
            this.HelpMoveText.Text = resources.GetString("HelpMoveText.Text");
            this.HelpMoveText.Visible = false;
            // 
            // HelpSetText
            // 
            this.HelpSetText.Location = new System.Drawing.Point(22, 86);
            this.HelpSetText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.HelpSetText.Multiline = true;
            this.HelpSetText.Name = "HelpSetText";
            this.HelpSetText.ReadOnly = true;
            this.HelpSetText.Size = new System.Drawing.Size(405, 131);
            this.HelpSetText.TabIndex = 14;
            this.HelpSetText.Text = resources.GetString("HelpSetText.Text");
            this.HelpSetText.Visible = false;
            // 
            // HelpOtherText
            // 
            this.HelpOtherText.Location = new System.Drawing.Point(22, 86);
            this.HelpOtherText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.HelpOtherText.Multiline = true;
            this.HelpOtherText.Name = "HelpOtherText";
            this.HelpOtherText.ReadOnly = true;
            this.HelpOtherText.Size = new System.Drawing.Size(405, 246);
            this.HelpOtherText.TabIndex = 17;
            this.HelpOtherText.Text = resources.GetString("HelpOtherText.Text");
            this.HelpOtherText.Visible = false;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.BackColor = System.Drawing.Color.DimGray;
            this.LabelName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelName.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.LabelName.Location = new System.Drawing.Point(29, 30);
            this.LabelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(67, 15);
            this.LabelName.TabIndex = 0;
            this.LabelName.Text = "Cell Name:";
            this.LabelName.Click += new System.EventHandler(this.LabelName_Click);
            // 
            // LabelValue
            // 
            this.LabelValue.AutoSize = true;
            this.LabelValue.BackColor = System.Drawing.Color.DimGray;
            this.LabelValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelValue.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.LabelValue.Location = new System.Drawing.Point(192, 30);
            this.LabelValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelValue.Name = "LabelValue";
            this.LabelValue.Size = new System.Drawing.Size(65, 15);
            this.LabelValue.TabIndex = 18;
            this.LabelValue.Text = "Cell Value:";
            // 
            // CellNameOutput
            // 
            this.CellNameOutput.BackColor = System.Drawing.Color.Gainsboro;
            this.CellNameOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CellNameOutput.Location = new System.Drawing.Point(100, 27);
            this.CellNameOutput.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CellNameOutput.Name = "CellNameOutput";
            this.CellNameOutput.ReadOnly = true;
            this.CellNameOutput.Size = new System.Drawing.Size(88, 20);
            this.CellNameOutput.TabIndex = 19;
            this.CellNameOutput.Text = "A1";
            this.CellNameOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CellValueOutput
            // 
            this.CellValueOutput.BackColor = System.Drawing.Color.Gainsboro;
            this.CellValueOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CellValueOutput.Location = new System.Drawing.Point(261, 27);
            this.CellValueOutput.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CellValueOutput.Name = "CellValueOutput";
            this.CellValueOutput.ReadOnly = true;
            this.CellValueOutput.Size = new System.Drawing.Size(166, 20);
            this.CellValueOutput.TabIndex = 20;
            this.CellValueOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OutputColumnInfo
            // 
            this.OutputColumnInfo.Location = new System.Drawing.Point(165, 69);
            this.OutputColumnInfo.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.OutputColumnInfo.Multiline = true;
            this.OutputColumnInfo.Name = "OutputColumnInfo";
            this.OutputColumnInfo.ReadOnly = true;
            this.OutputColumnInfo.Size = new System.Drawing.Size(141, 107);
            this.OutputColumnInfo.TabIndex = 23;
            this.OutputColumnInfo.Text = " \r\n \r\n \r\n \r\n ";
            this.OutputColumnInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OutputColumnInfo.Visible = false;
            // 
            // OutputRowInfo
            // 
            this.OutputRowInfo.Location = new System.Drawing.Point(307, 69);
            this.OutputRowInfo.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.OutputRowInfo.Multiline = true;
            this.OutputRowInfo.Name = "OutputRowInfo";
            this.OutputRowInfo.ReadOnly = true;
            this.OutputRowInfo.Size = new System.Drawing.Size(141, 107);
            this.OutputRowInfo.TabIndex = 25;
            this.OutputRowInfo.Text = " \r\n \r\n \r\n \r\n ";
            this.OutputRowInfo.Visible = false;
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ServerTextBox.ForeColor = System.Drawing.SystemColors.MenuText;
            this.ServerTextBox.Location = new System.Drawing.Point(552, 3);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(142, 20);
            this.ServerTextBox.TabIndex = 27;
            this.ServerTextBox.Text = "Enter Hostname";
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.Gainsboro;
            this.ConnectButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ConnectButton.FlatAppearance.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.ConnectButton.FlatAppearance.BorderSize = 0;
            this.ConnectButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.Location = new System.Drawing.Point(700, 3);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(73, 21);
            this.ConnectButton.TabIndex = 28;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
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
            this.FilePanel.Location = new System.Drawing.Point(103, 88);
            this.FilePanel.Margin = new System.Windows.Forms.Padding(2);
            this.FilePanel.Name = "FilePanel";
            this.FilePanel.Size = new System.Drawing.Size(563, 383);
            this.FilePanel.TabIndex = 29;
            // 
            // MovementBox
            // 
            this.MovementBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MovementBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.MovementBox.Location = new System.Drawing.Point(493, 346);
            this.MovementBox.Margin = new System.Windows.Forms.Padding(2);
            this.MovementBox.Name = "MovementBox";
            this.MovementBox.Size = new System.Drawing.Size(67, 32);
            this.MovementBox.TabIndex = 5;
            this.MovementBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.FileMenuLabel);
            this.panel1.Location = new System.Drawing.Point(13, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 30);
            this.panel1.TabIndex = 4;
            // 
            // FileMenuLabel
            // 
            this.FileMenuLabel.AutoSize = true;
            this.FileMenuLabel.Font = new System.Drawing.Font("Cambria", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileMenuLabel.Location = new System.Drawing.Point(200, 3);
            this.FileMenuLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FileMenuLabel.Name = "FileMenuLabel";
            this.FileMenuLabel.Size = new System.Drawing.Size(148, 26);
            this.FileMenuLabel.TabIndex = 0;
            this.FileMenuLabel.Text = "Spreadsheets";
            // 
            // Open_FileMenu
            // 
            this.Open_FileMenu.Location = new System.Drawing.Point(381, 347);
            this.Open_FileMenu.Margin = new System.Windows.Forms.Padding(2);
            this.Open_FileMenu.Name = "Open_FileMenu";
            this.Open_FileMenu.Size = new System.Drawing.Size(73, 25);
            this.Open_FileMenu.TabIndex = 3;
            this.Open_FileMenu.Text = "Open";
            this.Open_FileMenu.UseVisualStyleBackColor = true;
            // 
            // FileTextSelect
            // 
            this.FileTextSelect.Location = new System.Drawing.Point(13, 350);
            this.FileTextSelect.Margin = new System.Windows.Forms.Padding(2);
            this.FileTextSelect.Name = "FileTextSelect";
            this.FileTextSelect.Size = new System.Drawing.Size(365, 20);
            this.FileTextSelect.TabIndex = 2;
            // 
            // FileList
            // 
            this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColHeader});
            this.FileList.Location = new System.Drawing.Point(13, 52);
            this.FileList.Margin = new System.Windows.Forms.Padding(2);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(536, 282);
            this.FileList.TabIndex = 1;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.View = System.Windows.Forms.View.Details;
            // 
            // NameColHeader
            // 
            this.NameColHeader.Text = "Name";
            this.NameColHeader.Width = 480;
            // 
            // menuStrip3
            // 
            this.menuStrip3.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip3.Location = new System.Drawing.Point(0, 24);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(785, 24);
            this.menuStrip3.TabIndex = 34;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSpreadsheetButton,
            this.undo_button,
            this.revert_button});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(785, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // NewSpreadsheetButton
            // 
            this.NewSpreadsheetButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewSpreadsheetButton.Image = global::SpreadsheetGUI.Properties.Resources.newspreadsheet;
            this.NewSpreadsheetButton.Name = "NewSpreadsheetButton";
            this.NewSpreadsheetButton.Size = new System.Drawing.Size(60, 20);
            this.NewSpreadsheetButton.Text = "New";
            this.NewSpreadsheetButton.Click += new System.EventHandler(this.NewSpreadsheetButton_Click);
            // 
            // undo_button
            // 
            this.undo_button.BackgroundImage = global::SpreadsheetGUI.Properties.Resources.Undo_icon;
            this.undo_button.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.undo_button.Image = global::SpreadsheetGUI.Properties.Resources.Undo_icon;
            this.undo_button.Name = "undo_button";
            this.undo_button.Size = new System.Drawing.Size(64, 20);
            this.undo_button.Text = "Undo";
            this.undo_button.Click += new System.EventHandler(this.undo_button_MouseClick);
            // 
            // revert_button
            // 
            this.revert_button.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revert_button.Name = "revert_button";
            this.revert_button.Size = new System.Drawing.Size(56, 20);
            this.revert_button.Text = "Revert";
            this.revert_button.Click += new System.EventHandler(this.revert_button_MouseClick);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.Gainsboro;
            this.DisconnectButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DisconnectButton.BackgroundImage")));
            this.DisconnectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DisconnectButton.Location = new System.Drawing.Point(520, 0);
            this.DisconnectButton.Margin = new System.Windows.Forms.Padding(2);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(27, 22);
            this.DisconnectButton.TabIndex = 32;
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DisconnectButton_MouseClick);
            // 
            // ColumnExit
            // 
            this.ColumnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ColumnExit.BackgroundImage")));
            this.ColumnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ColumnExit.Location = new System.Drawing.Point(291, 69);
            this.ColumnExit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ColumnExit.Name = "ColumnExit";
            this.ColumnExit.Size = new System.Drawing.Size(15, 16);
            this.ColumnExit.TabIndex = 24;
            this.ColumnExit.UseVisualStyleBackColor = true;
            this.ColumnExit.Visible = false;
            this.ColumnExit.Click += new System.EventHandler(this.ColumnExit_Click);
            // 
            // RowExit
            // 
            this.RowExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RowExit.BackgroundImage")));
            this.RowExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RowExit.Location = new System.Drawing.Point(433, 69);
            this.RowExit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.RowExit.Name = "RowExit";
            this.RowExit.Size = new System.Drawing.Size(15, 16);
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
            this.CloseSet.Location = new System.Drawing.Point(411, 86);
            this.CloseSet.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CloseSet.Name = "CloseSet";
            this.CloseSet.Size = new System.Drawing.Size(15, 16);
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
            this.CloseOther.Location = new System.Drawing.Point(411, 87);
            this.CloseOther.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CloseOther.Name = "CloseOther";
            this.CloseOther.Size = new System.Drawing.Size(15, 16);
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
            this.CloseMove.Location = new System.Drawing.Point(411, 87);
            this.CloseMove.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CloseMove.Name = "CloseMove";
            this.CloseMove.Size = new System.Drawing.Size(15, 16);
            this.CloseMove.TabIndex = 13;
            this.CloseMove.Text = "_";
            this.CloseMove.UseVisualStyleBackColor = true;
            this.CloseMove.Visible = false;
            this.CloseMove.Click += new System.EventHandler(this.CloseMove_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(785, 487);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.ColumnExit);
            this.Controls.Add(this.RowExit);
            this.Controls.Add(this.CellValueOutput);
            this.Controls.Add(this.CellNameOutput);
            this.Controls.Add(this.LabelValue);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.CloseSet);
            this.Controls.Add(this.CloseOther);
            this.Controls.Add(this.CloseMove);
            this.Controls.Add(this.LabelContents);
            this.Controls.Add(this.FormulaBox);
            this.Controls.Add(this.FilePanel);
            this.Controls.Add(this.OutputColumnInfo);
            this.Controls.Add(this.OutputRowInfo);
            this.Controls.Add(this.HelpMoveText);
            this.Controls.Add(this.HelpOtherText);
            this.Controls.Add(this.HelpSetText);
            this.Controls.Add(this.menuStrip3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.FilePanel.ResumeLayout(false);
            this.FilePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovementBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.PictureBox MovementBox;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NewSpreadsheetButton;
        private System.Windows.Forms.ToolStripMenuItem undo_button;
        private System.Windows.Forms.ToolStripMenuItem revert_button;
    }
}

