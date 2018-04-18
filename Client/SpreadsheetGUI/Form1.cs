using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SS;
using SpreadsheetUtilities;
using System.Net.Sockets;
using Networking;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        public Spreadsheet ss1;
        public string filename;
        private Socket theServer;
        private bool connected;
        private Dictionary<string, KeyValuePair<bool, string>> cellStatus;
        public Form1()
        {
            //
            //Set up for a new spreadsheet.
            //

            ss1 = new Spreadsheet(validVar, s => s.ToUpper(), "ps6");

            InitializeComponent();
            this.KeyPreview = true;


            //Set up valid open/save file types
            openFileDialog1.Filter = "Spreadsheet Files (*.sprd)|*.sprd|Text Files (*.txt)|*.txt";
            saveFileDialog1.Filter = "Spreadsheet Files (*.sprd)|*.sprd|Text Files (*.txt)|*.txt";

            //set up delegate listeners for the selection changed event.
            spreadsheetPanel1.SelectionChanged += MakeWriteable;
            spreadsheetPanel1.SelectionChanged += setCellNameVal;

            //set up listeners for keydown and formclosing events.
            this.KeyDown += ProcessKeyStroke;
            this.FormClosing += OnExit;
            this.MouseMove += FilePanelMove;
            this.FileList.Click += FileSelected;
            this.Open_FileMenu.Click += SendSpreadsheetSelection;

            this.Width = 1000;
            this.Height = 600;

            FilePanel.Visible = false;

            ServerTextBox.Enter += ServerTextBoxEntered;
            ServerTextBox.LostFocus += ServerTextBoxLeft; 

            FormulaBox.Focus();

        }

        #region Spreadsheet Control

        /// <summary>
        /// Sets the formula box to the value of the selected cell and sets focus to the formula box.
        /// Prepares for user input to cell.
        /// </summary>
        /// <param name="sender"></param>
        private void MakeWriteable(SpreadsheetPanel sender)
        {
            sender.GetSelection(out int col, out int row);
            string cellName = GetCellName(col, row);

            //set the contents of the formula box and set focus to it.
            FormulaBox.Text = ss1.GetCellContents(cellName).ToString();
            FormulaBox.Focus();

            //put the cursor to the end of the text
            if (FormulaBox.Text.Length > 0)
                FormulaBox.SelectionStart = FormulaBox.Text.Length;
        }

        /// <summary>
        /// Delegate to handle the setting of cell name and cell value text boxes
        /// on cell selection change.
        /// </summary>
        /// <param name="sender"></param>
        private void setCellNameVal(SpreadsheetPanel sender)
        {
            sender.GetSelection(out int col, out int row);

            string cellName = GetCellName(col, row);
            object cellVal = ss1.GetCellValue(cellName);

            CellNameOutput.Text = cellName;

            //if the result is a formulaError display a Formula Error string, otherwise set the cell normally.
            if (cellVal.GetType() == typeof(FormulaError))
                CellValueOutput.Text = "Formula Error";
            else
                CellValueOutput.Text = cellVal.ToString();
        }

        /// <summary>
        /// Processes keystrokes from the user and updates the spreadsheet accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessKeyStroke(object sender, KeyEventArgs e)
        {
            if ( ! ServerTextBox.Focused)
            {
                //they are no longer editing
                if (e.KeyData == Keys.Enter)
                {
                    EnterKey(e);
                }

                //if they are currently editing
                else
                {
                    //once networking works...
                    spreadsheetPanel1.GetSelection(out int c, out int r);
                    string cellName = GetCellName(c, r);
                    string focusMessage = "focus " + cellName + ((char)3);
                    SendFocus(focusMessage);

                    //special case for backspace
                    if (e.KeyData == Keys.Back)
                    {
                        BackspaceKey(e);
                    }


                    else if ((e.KeyData >= Keys.A && e.KeyData <= Keys.Z) || (e.KeyData >= Keys.D0 && e.KeyData <= Keys.D9) || (e.KeyData >= Keys.NumPad0 && e.KeyData <= Keys.NumPad9))
                    {
                        StandardKey(e);
                    }

                    else
                    {
                        //not a valid key
                        return;
                    }
                }
            }
            
        }

        /*
        /// <summary>
        /// Overrides the ProcessCmdKey function in order to use the arrow keys and Tab to make
        /// Cell selections.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return spreadsheetPanel1.MovementKey(ref msg, keyData);
        }
        */

        /// <summary>
        /// EnterButton clicked event. Sets selected cell to contents of the formula box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterButton_Click(object sender, EventArgs e)
        {
            SetCell();
            FormulaBox.Focus();
        }

        /// <summary>
        /// Delegate to remove text and change color when ServerTextbox is entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerTextBoxEntered(object sender, EventArgs e)
        {
            ServerTextBox.Text = "";
            ServerTextBox.ForeColor = Color.Black;
        }

        /// <summary>
        /// Delegate to add text and change text color when ServerTextBox is left (if the client has not connected to a server yet).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerTextBoxLeft(object sender, EventArgs e)
        {
            if (!connected)
            {
                ServerTextBox.Text = "Enter Hostname";
                ServerTextBox.ForeColor = SystemColors.ScrollBar;
            }
        }

        ///<summary>
        /// checks that the the servename is valid and connects the client to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ServerTextBox.Text))
                MessageBox.Show("Please enter a server address.");
            else
            {
                try
                {
                    //theServer = Network.ConnectToServer(SendRegisterMessage, ServerTextBox.Text);
                    ServerTextBox.Enabled = false;
                    ConnectButton.Enabled = false;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("invalid server name");
                }
            }
        }

        /// <summary>
        /// Sets the FileTextBox contents to the name of the selected spreadsheet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSelected(object sender, EventArgs e)
        {
            ListViewItem fileSelected = FileList.SelectedItems[0];
            FileTextSelect.Text = fileSelected.Text;
        }

        private void OnExit(object sender, EventArgs e)
        {

        }


        private void undo_button_Click(object sender, EventArgs e)
        {

        }

        private void revert_button_Click(object sender, EventArgs e)
        {


        }


        #endregion

        #region Networking Control

        /// <summary>
        /// Sends the server the name of the spreadsheet the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendSpreadsheetSelection(object sender, EventArgs e)
        {
            Networking.Networking.Send(theServer, FileTextSelect.Text);
            //TODO: add full state message processing function.
            //HandleFullState();
        }
        private void Receive()
        {
            /*
            string message = Network.Recieve();
            if (message is focus)
                ReceiveFocus(message);
            */
        }

        /// <summary>
        /// Sends the focus message to the server when a cell is being edited.
        /// </summary>
        private void SendFocus(string message)
        {
            //hacking, but allows you to test diff cases
            //if in column c, receive will be column d
            //so you can test a case where someone else would have been editing a diff cell
            ReceiveFocus(message);
            //Network.Send(theServer, message);
        }

        private void ReceiveFocus(string message)
        {
            //color the cell

            //figure out what cell the focus is on
            //string[] msg_parts = message.Split(null);
            //string[] smaller_parts = msg_parts[1].Split(':');

            char[] delimiters = new char[] { ' ', ':', ((char)3) };
            string[] msg_parts = message.Split(delimiters);

            string cell_name = msg_parts[1];


            //KeyValuePair<bool, string> newStatus = new KeyValuePair<bool, string>(true, smaller_parts[1]);
            //cellStatus[cell_name] = newStatus;
            GetCellPosition(cell_name, out int row, out int col);
            spreadsheetPanel1.SetFocus(row, col);
        }

        private void SendMessage(string msg)
        {
            Networking.Networking.Send(theServer, msg);
        }

        #endregion

        #region Row/Col Info

        /// <summary>
        /// Calculates the sum, count and average of the current row. Displays the results in a textbox.
        /// If a cell contains a variable or formula that cannot be solved due to the dependency being unfinished it is left out of the calculations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnRow_Click(object sender, EventArgs e)
        {
            //handle logic of row info
            OnReturnRowClick();

            //set this menu visible
            RowExit.Visible = true;
            OutputRowInfo.Visible = true;

            //set other menus invisivle
            HelpSetText.Visible = false;
            CloseSet.Visible = false;
            HelpMoveText.Visible = false;
            CloseMove.Visible = false;
            HelpOtherText.Visible = false;
            CloseOther.Visible = false;
            ColumnExit.Visible = false;
            OutputColumnInfo.Visible = false;
        }

        /// <summary>
        /// Calculates the sum, count, and average of the values in the current column. Displays results in a textbox
        /// If a cell contains a variable or formula that cannot be solved due to the dependency being unfinished it is left out of the calculations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnCollumn_Click(object sender, EventArgs e)
        {
            //handle logic of column info
            OnRerutrnColumnClick();

            //set this menu visible
            ColumnExit.Visible = true;
            OutputColumnInfo.Visible = true;

            //set other menus invisible
            HelpSetText.Visible = false;
            CloseSet.Visible = false;
            HelpMoveText.Visible = false;
            CloseMove.Visible = false;
            HelpOtherText.Visible = false;
            CloseOther.Visible = false;
            OutputRowInfo.Visible = false;
            RowExit.Visible = false;
        }

        /// <summary>
        /// Helper Method for ReturnCollumClick
        /// </summary>
        private void OnRerutrnColumnClick()
        {
            double sum = 0;
            double count = 0;
            int startrow = 0;
            spreadsheetPanel1.GetSelection(out int col, out int row);

            // iterate through cells and get sum and count.
            while (startrow < 99)
            {
                double addto;
                string temp = ss1.GetCellValue(GetCellName(col, startrow)).ToString();
                if (double.TryParse(temp, out addto))
                {
                    sum += addto;
                    count += 1;
                }
                startrow += 1;
            }

            //set contents
            double average = sum / count;
            string letter = Convert.ToChar(col + 65).ToString();
            OutputColumnInfo.Text = " \r\nSum of column " + letter + "  =  " + sum + " \r\n \r\nCount of column " + letter + "  =  " + count + " \r\n \r\nAverage of column  " + letter + "  =  " + average + "";
        }


        /// <summary>
        /// Makes the information box for the column and the column button invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnExit_Click(object sender, EventArgs e)
        {
            ColumnExit.Visible = false;
            OutputColumnInfo.Visible = false;
        }

        /// <summary>
        /// Helper method for returnRow_Click
        /// </summary>
        private void OnReturnRowClick()
        {
            double sum = 0;
            double count = 0;
            int startcol = 0;
            spreadsheetPanel1.GetSelection(out int col, out int row);

            //iterate through cells and get sum and count.
            while (startcol < 26)
            {
                double addto;
                string temp = ss1.GetCellValue(GetCellName(startcol, row)).ToString();
                if (double.TryParse(temp, out addto))
                {
                    sum += addto;
                    count += 1;
                }
                startcol += 1;
            }

            //set contents
            double average = sum / count;
            int rowNum = row + 1;
            OutputRowInfo.Text = " \r\nSum of row " + rowNum + "  =  " + sum + "\r\n \r\nCount of row " + rowNum + "  =  " + count + " \r\n \r\nAverage of row " + rowNum + "  =  " + average + "";
        }

        /// <summary>
        /// Makes the information box for the row and the row button invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowExit_Click(object sender, EventArgs e)
        {
            OutputRowInfo.Visible = false;
            RowExit.Visible = false;
        }

        #endregion

        #region Help Menu

        /// <summary>
        /// If the user selects the Cell Selection help option, the help text box and exit button are made visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMoveMenu_Click(object sender, EventArgs e)
        {
            //make this menu visible
            CloseMove.Visible = true;
            HelpMoveText.Visible = true;

            //set the other menus invisible
            HelpSetText.Visible = false;
            CloseSet.Visible = false;
            HelpOtherText.Visible = false;
            CloseOther.Visible = false;
        }

        /// <summary>
        /// If the user selects the Cell Editing help option, the help text box and exit button are made visible. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpSetMenu_Click(object sender, EventArgs e)
        {
            //make this menu visible
            CloseSet.Visible = true;
            HelpSetText.Visible = true;

            //set the other help menus invisible
            HelpMoveText.Visible = false;
            CloseMove.Visible = false;
            HelpOtherText.Visible = false;
            CloseOther.Visible = false;
        }

        /// <summary>
        /// If the user selects the File Menu help option, the help text box and exit button are made visible. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpOtherMenu_Click(object sender, EventArgs e)
        {
            //make this menu visible
            CloseOther.Visible = true;
            HelpOtherText.Visible = true;

            //set teh other help menus invisible
            HelpSetText.Visible = false;
            CloseSet.Visible = false;
            HelpMoveText.Visible = false;
            CloseMove.Visible = false;
        }

        /// <summary>
        /// This makes the Cell Selection help text boxes and exit button invisible. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseSet_Click(object sender, EventArgs e)
        {
            HelpSetText.Visible = false;
            CloseSet.Visible = false;
        }

        /// <summary>
        /// This makes the Cell Editing help text boxes and exit button invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseOther_Click_1(object sender, EventArgs e)
        {
            HelpOtherText.Visible = false;
            CloseOther.Visible = false;
        }

        /// <summary>
        /// This makes the File menu help text boxes and exit button invisible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMove_Click_1(object sender, EventArgs e)
        {
            HelpMoveText.Visible = false;
            CloseMove.Visible = false;
        }

        #endregion

        #region File Menu

        /// <summary>
        /// Displays a custom file menu containing all the sent spreadsheet name from the server.
        /// </summary>
        /// <param name="fileString"></param>
        private void ShowFileMenu(string fileString)
        {
            FilePanel.Show();
            string[] files = fileString.Split('\n');
            //for some reason Split() does not remove the delimter from the last part of the string
            string lastString = files[files.Length - 1];
            lastString = lastString.Substring(0, lastString.Length - 2);
            files[files.Length - 1] = lastString;
            foreach (string file in files)
            {
                ListViewItem item = new ListViewItem(file);
                item.Text = file;
                FileList.Items.Add(item);
            }
        }

        /// <summary>
        /// Work in progress, low priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePanelMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.FilePanel.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            }
        }

        #endregion

        #region Helper Methods

        private void BackspaceKey(KeyEventArgs e)
        {
            spreadsheetPanel1.GetSelection(out int col, out int row);
            spreadsheetPanel1.GetValue(col, row, out string value);
            if (value.Length > 0)
            {
                int split_index = (value.Length - 1);
                string newVal = value.Substring(0, split_index);
                spreadsheetPanel1.SetValue(col, row, newVal);
            }
        }

        private void StandardKey(KeyEventArgs e)
        {
            spreadsheetPanel1.GetSelection(out int col, out int row);
            KeysConverter kc = new KeysConverter();
            string keyString = kc.ConvertToString(e.KeyData);
            keyString = keyString.ToLower();
            spreadsheetPanel1.GetValue(col, row, out string value);
            string newVal = value + keyString;
            spreadsheetPanel1.SetValue(col, row, newVal);
        }

        private void EnterKey(KeyEventArgs e)
        {
            SetCell();
            setCellNameVal(spreadsheetPanel1);
            e.SuppressKeyPress = true;

            //once networking is back up...
            string unfocusMessage = "unfocus " + ((char)3);
            SendMessage(unfocusMessage);
        }

        /// <summary>
        /// Returns a string Cell name based on a numeric row, column position.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetCellName(int col, int row)
        {
            return Convert.ToChar(col + 65).ToString() + (row + 1).ToString();
        }

        /// <summary>
        /// Helper method that returns the int row, col postiion of a cell name.
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void GetCellPosition(string cellName, out int row, out int col)
        {
            col = Convert.ToChar(cellName[0]) - 65;
            row = Convert.ToInt16(cellName.Substring(1)) - 1;
        }

        /// <summary>
        /// determines if a given variable is valid according to the PS6 specifications.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool validVar(string s)
        {
            if (s.Length > 3 || s.Length < 2)
                return false;
            if (!char.IsLetter(s[0]))
                return false;
            if (!char.IsDigit(s[1]))
                return false;
            if (s.Length == 3)
            {
                if (!char.IsDigit(s[2]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Writes the contents of the Formula Text box to the currently selected cell.
        /// </summary>
        private void SetCell()
        {
            //attempt to set the cell, if there is an error set the formula box to display an error message.
            try
            {
                spreadsheetPanel1.GetSelection(out int col, out int row);
                string cellName = GetCellName(col, row);

                //set the contents of the cell and update all the cells that need to be updated
                //UpdateCells(ss1.SetContentsOfCell(cellName, FormulaBox.Text));
                ss1.SetContentsOfCell(cellName, FormulaBox.Text);

                //if the result is a formula error display a formula error message, otherwise set the cell with the result.
                if (ss1.GetCellValue(cellName).GetType() == typeof(FormulaError))
                {
                    //FormulaError f = new FormulaError(ss1.GetCellValue(cellName));
                    spreadsheetPanel1.SetValue(col, row, "Formula Error!");
                }
                else
                {
                    spreadsheetPanel1.SetValue(col, row, ss1.GetCellValue(cellName).ToString());
                    UpdateCells(new HashSet<string>(ss1.getDependentCells(cellName)));
                }
            }

            catch (CircularException)
            {
                FormulaBox.Text = "Ciruclar Dependency!";
            }

            catch (FormulaFormatException)
            {
                FormulaBox.Text = "Invalid Formula!";
            }
        }

        /// <summary>
        /// Update the displayed value of all the cells that need to be changed after a cell value is set.
        /// </summary>
        /// <param name="cellsToChange"></param>
        private void UpdateCells(ISet<string> cellsToChange)
        {
            foreach (string cellName in cellsToChange)
            {
                //get the numeric row, col position of the cell
                int col = Convert.ToChar(cellName.Substring(0, 1)) - 65;
                int row = Convert.ToInt16(cellName.Substring(1)) - 1;

                //update the cell state in the spreadsheet
                ss1.UpdateCell(cellName);

                //update the GUI
                if (ss1.GetCellValue(cellName).GetType() != typeof(FormulaError))
                    spreadsheetPanel1.SetValue(col, row, ss1.GetCellValue(cellName).ToString());
                else
                    spreadsheetPanel1.SetValue(col, row, "Formula Error!");

                //update all dependent cells
                UpdateCells(new HashSet<string>(ss1.getDependentCells(cellName)));
            }
        }
        /*
        /// <summary>
        /// Sends the register message to the server after a connection is established.
        /// </summary>
        /// <param name="state"></param>
        private void SendRegisterMessage(SocketState state)
        {
            string message = "register" + (char)3;
            Network.Send(state.Socket, message);

        }
        */

        private void HandleFullState()
        {
            /*
             * string message = Network.Recieve();
             * if (message == ...)
             * MessageBox.Show(Spreadsheet not valid!);
             * 
             * else
             * 
             * POSSIBLE TODO: add recieve function to networking file if needed.
             * */
        }
        #endregion
    }
}
