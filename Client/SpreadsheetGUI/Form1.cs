using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SS;
using SpreadsheetUtilities;
using System.Net.Sockets;
using Networking;
using System.Runtime.InteropServices;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        public Spreadsheet ss1;
        private Socket theServer;
        private bool connected;
        private Dictionary<string, string> clientFocus;
        private string previousSelection;
        private System.Timers.Timer pingTimer;
        private int pingMisses;
        //private System.Timers.Timer serverTimer;
        private SocketState serverSock;
        public Form1()
        {
            //
            //Set up for a new spreadsheet.
            //

            ss1 = new Spreadsheet(validVar, s => s.ToUpper(), "ps6");

            InitializeComponent();
            this.KeyPreview = true;

            clientFocus = new Dictionary<string, string>();

            //Set up valid open/save file types
            openFileDialog1.Filter = "Spreadsheet Files (*.sprd)|*.sprd|Text Files (*.txt)|*.txt";
            saveFileDialog1.Filter = "Spreadsheet Files (*.sprd)|*.sprd|Text Files (*.txt)|*.txt";

            //set up delegate listeners for the events.
            spreadsheetPanel1.SelectionChanged += MakeWriteable;
            this.KeyDown += ProcessKeyStroke;
            this.FormClosing += OnExit;
            this.MouseMove += FilePanelMove;
            this.FileList.SelectedIndexChanged += FileSelected;
            this.Open_FileMenu.Click += SendSpreadsheetSelection;
            this.spreadsheetPanel1.SelectionChanged += HandleSelectionChange;
            this.ServerTextBox.Enter += ServerTextBoxEntered;
            this.ServerTextBox.LostFocus += ServerTextBoxLeft;
            this.FormulaBox.GotFocus += HandleFomrulaBoxFocus;

            this.Width = 800;
            this.Height = 600;

            this.FilePanel.Visible = false;

            this.spreadsheetPanel1.SetSelection(0, 0);
            this.previousSelection = GetCellName(0, 0);


            //serverTimer = new System.Timers.Timer();
            //serverTimer.Interval = 60000; //60 s?
            //serverTimer.Elapsed += DisconnectDetector;

            pingMisses = -1;
            pingTimer = new System.Timers.Timer();
            pingTimer.Interval = 10000;
            pingTimer.Elapsed += SendPing;

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

            int[] loc = GetCellPosition(this.previousSelection);
            int myCol = loc[0];
            int myRow = loc[1];
            //spreadsheetPanel1.SetUnfocus(this.previousSelection, myRow, myCol);
            string unfocusMessage = "unfocus " + ((char)3);
            SendMessage(unfocusMessage);

            //string focusMessage = "focus " + cellName + ((char)3);
            //SendMessage(focusMessage);

            //set the contents of the formula box and set focus to it.
            FormulaBox.Text = ss1.GetCellContents(cellName).ToString();
            this.ActiveControl = FormulaBox;


        }


        /// <summary>
        /// Processes keystrokes from the user and updates the spreadsheet accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessKeyStroke(object sender, KeyEventArgs e)
        {
            if (!ServerTextBox.Focused && !FilePanel.Visible && connected)
            {
                if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Oemplus)
                    OperatorKey("+");
                else if (e.KeyCode == Keys.Oemplus)
                    OperatorKey("=");
                else if (e.KeyCode == Keys.OemMinus)
                    OperatorKey("-");
                else if (e.KeyData == Keys.Oem2)
                    OperatorKey("/");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D8)
                    OperatorKey("*");
                //pretty much none of the keys below actually send to the value of the cell, but they can be entered
                else if (e.KeyData == Keys.Space)
                    OperatorKey(" ");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.OemQuestion)
                    OperatorKey("?");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.OemSemicolon)
                    OperatorKey(":");
                else if (e.KeyData == Keys.OemSemicolon)
                    OperatorKey(";");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D3)
                    OperatorKey("#");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D1)
                    OperatorKey("!");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D2)
                    OperatorKey("@");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D4)
                    OperatorKey("$");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D5)
                    OperatorKey("%");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D7)
                    OperatorKey("&");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D9)
                    OperatorKey("(");
                else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.D0)
                    OperatorKey(")");

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
                    SendMessage(focusMessage);

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
            else if (!connected && !ServerTextBox.Focused)
            {
                MessageBox.Show("Please connect to a server before editing a spreadsheet");
                ServerTextBox.Focus();
            }

        }


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
            if (!connected && ServerTextBox.Text.Length < 1)
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
                    theServer = Network.ConnectToServer(SendRegisterMessage, ServerTextBox.Text);
                    ServerTextBox.Enabled = false;
                    ConnectButton.Enabled = false;
                    connected = true;
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
            if (FileList.SelectedItems.Count > 0)
            {
                ListViewItem fileSelected = FileList.SelectedItems[0];
                FileTextSelect.Text = fileSelected.Text;
            }
        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        /// <summary>
        /// Hides the caret for the formula box and returns focus to spreadsheet panel.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public void HandleFomrulaBoxFocus(object sender, EventArgs e)
        {
            HideCaret(FormulaBox.Handle);
            spreadsheetPanel1.Focus();
        }

        /// <summary>
        /// Sends a disconnect message to the server when the client is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExit(object sender, EventArgs e)
        {
            if (connected)
            {
                SendMessage("disconnnect " + (char)3);
            }
        }

        /// <summary>
        /// Requests the server to undo the most recent change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undo_button_MouseClick(object sender, MouseEventArgs e)
        {
            if (connected)
            {
                SendMessage("undo " + (char)3);
            }
        }

        /// <summary>
        /// Requests the server to revert the currently selected cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void revert_button_MouseClick(object sender, MouseEventArgs e)
        {
            if (connected)
            {
                spreadsheetPanel1.GetSelection(out int col, out int row);
                string cellName = GetCellName(col, row);

                SendMessage("revert " + CellName + (char)3);
            }
        }


        #endregion

        #region Networking Control

        private void Disconnect()
        {
            Network.Send(theServer, "disconnect " + (char)3);
            HandleDisconnect();
        }

        private void HandleDisconnect()
        {
            connected = false;
            previousSelection = "A1";

            MethodInvoker FMInvoker = new MethodInvoker(() =>
            {
                FilePanel.Visible = false;
                FileList.Items.Clear();

                serverSock = null;
                theServer = null;
                ServerTextBox.Enabled = true;
                ConnectButton.Enabled = true;

                spreadsheetPanel1.Clear();

                MessageBox.Show("Disconnected Successfully");
            });

            this.Invoke(FMInvoker);
        }

        /// <summary>
        /// Sends the server the name of the spreadsheet the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendSpreadsheetSelection(object sender, EventArgs e)
        {
            Open_FileMenu.Enabled = false;
            string message = "load " + FileTextSelect.Text + (char)3;
            FileList.Items.Clear();
            Network.Send(theServer, message);
            Network.GetData(serverSock);
        }

        /// <summary>
        /// Sends a string message to the server.
        /// </summary>
        /// <param name="msg"></param>
        private void SendMessage(string msg)
        {
            Network.Send(theServer, msg);
        }


        /// <summary>
        /// Sends the register message to the server after a connection is established.
        /// </summary>
        /// <param name="state"></param>
        private void SendRegisterMessage(SocketState state)
        {
            state.callMe = ActivateFileMenu;
            string message = "register" + (char)3;
            Network.Send(state.sock, message);

            Network.GetData(state);
        }

        private void SendPing(object sender, EventArgs e)
        {
            pingMisses++;
            if (pingMisses >= 6)
                Disconnect();
            string pingMsg = "ping " + ((char)3);
            SendMessage(pingMsg);
        }


        /// <summary>
        /// Processes a full state message. Assigns processMessage() as the socket state's callme when finished.
        /// </summary>
        /// <param name="state"></param>
        private void HandleFullState(SocketState state)
        {
            state.callMe = ProcessMessage;

            string message;
            lock (state) { message = state.builder.ToString(); }
            state.builder.Clear();
            //SOOOO many bugs in this area!
            //goes into line 364 like 5 times and comes out with a different message every time
            //starts the if(message.Contains) with the right message, by the time it gets to else if ping,
            //the message was ""

            //ALSO half the time the message is just full state (char)3, and half of the time it has anywhere
            //from 1 to like 6 ping messages included still...
            MethodInvoker FMInvoker = new MethodInvoker(() =>
            {
                FilePanel.Visible = false;
            });

            if (message.Contains(((char)3).ToString()))
            {
                if (message.Length < 20 && message.Contains("file_load_error"))
                {
                    MessageBox.Show("Could not open/create spreadsheet!");
                    Open_FileMenu.Enabled = true;
                    state.callMe = HandleFullState;
                    //TO DO
                    //does file_load_error need to call getData still? or can we return?
                    //assumption is opening up the file menu again does sendspreadsheetselection and get data
                    return;
                }
                if (message.Contains("ping " + ((char)3)))
                {
                    //we can *probably* discard the pings that are in this message, the server
                    //will send more before we time out
                    message.Remove(message.IndexOf("ping " + ((char)3)));
                }
                if (message.Contains("full_state "))
                {
                    //empty full state message ("full_state char3") is 12
                    if(message.Length > 12)
                    {
                        //full_state A6:3\nB4:2\n((char3))
                        //remove "full_state " and the terminating character
                        message = message.Substring(10, message.Length - 11);

                        string[] cells = message.Split('\n');

                        //split the cell name and value then set the cell.
                        foreach (string cell in cells)
                        {
                            if (cell == "")
                                continue;
                            string[] cellAndval = cell.Split(':');
                            string cellName = cellAndval[0];
                            string cellVal = cellAndval[1];

                            //cellName = cellName.Trim(' ');
                            cellVal = cellVal.Trim(' ');

                            int[] colRow = GetCellPosition(cellName);

                            SetCell(colRow[1], colRow[0], cellVal);
                        }

                    }
                    //TO DO put back (also on server)
                    pingTimer.Start();
                    //serverTimer.Start();
                    this.Invoke(FMInvoker);
                }
            }
            Network.GetData(state);
        }

        /// <summary>
        /// Process incoming messages from the server.
        /// </summary>
        /// <param name="state"></param>
        private void ProcessMessage(SocketState state)
        {
            string message;

            lock (state)
            {
                message = state.builder.ToString();
                string[] messages = message.Split((char)3);

                foreach (string msg in messages)
                {
                    if (msg == "")
                        break;
                    string[] msg2 = msg.Split(' ');
                    string command = msg2[0];
                    switch (command)
                    {
                        case "change":
                            //get cell name and contents from message
                            char[] delimiters = new char[] { ' ', ':' };
                            string[] msg_parts = msg.Split(delimiters);
                            string cell_name = msg_parts[1];
                            string contents = msg_parts[2];

                            //set the contents of the cell
                            GetCellPosition(cell_name, out int row, out int col);
                            SetCell(row, col, contents);
                            break;

                        case "ping":
                            //if (msg == "ping ")
                            //{
                            SendMessage("ping_response " + ((char)3));
                            //}
                            break;

                            //else if (msg == "ping_response ")
                            //{
                                //timer reset -- not sure this is right
                                //serverTimer.Stop();
                                //serverTimer.Start();
                                //pingMisses = 0;
                            //}
                            //break;

                        case "ping_response":
                            pingMisses = 0;
                            break;
                        case "disconnect":
                            HandleDisconnect();
                            break;
                        case "unfocus":
                            char[] delimiters2 = new char[] { ' ' };
                            string[] msg_parts2 = msg.Split(delimiters2);
                            string user_id = msg_parts2[1];

                            if (clientFocus.ContainsKey(user_id))
                            {
                                cell_name = clientFocus[user_id];
                                GetCellPosition(cell_name, out row, out col);
                                spreadsheetPanel1.SetUnfocus(cell_name, row, col);
                            }
                            break;
                        case "focus":
                            char[] delimiters3 = new char[] { ' ', ':' };
                            string[] msg_parts3 = msg.Split(delimiters3);
                            cell_name = msg_parts3[1];
                            user_id = msg_parts3[2];

                            clientFocus[user_id] = cell_name;

                            GetCellPosition(cell_name, out row, out col);
                            spreadsheetPanel1.SetFocus(cell_name, row, col);
                            break;
                    }


                    state.builder.Remove(0, message.Length);
                }
            }
            Network.GetData(state);
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
        /// Displays a custom file menu containing all spreadsheet name from a string.
        /// </summary>
        /// <param name="fileString"></param>
        private void ShowFileMenu(string fileString)
        {
            FilePanel.Show();
            string[] files = fileString.Split('\n');
            foreach (string file in files)
            {
                ListViewItem item = new ListViewItem(file);
                item.Text = file;
                FileList.Items.Add(item);
            }
        }

        /// <summary>
        /// Retrieves the list of spreadhseet names from the server and displays a file menu with the names.
        /// </summary>
        /// <param name="state"></param>
        private void ActivateFileMenu(SocketState state)
        {
            state.callMe = HandleFullState;

            serverSock = state;

            string message;
            lock (state)
            {
                message = state.builder.ToString();
            }

            //remove the first 17 ("connect_accepted \n") characters from the string.
            //And don't take the last two terminating characters at the end of the message.
            message = message.Substring(18, message.Length - 20);

            MethodInvoker FMInvoker = new MethodInvoker(() =>
            {
                ShowFileMenu(message);
            });

            this.Invoke(FMInvoker);

            state.builder.Clear();
            Network.GetData(state);
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

        private void DisconnectButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (connected)
                Disconnect();
            else
                MessageBox.Show("Not yet connected");
        }

        /// <summary>
        /// Listener to detect when the ping loop expires. Calls HandleDisconnnect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectDetector(object sender, EventArgs e)
        {
            HandleDisconnect();
        }

        /// <summary>
        /// Removes one character from the cell's text.
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Processes a standard key and append its char value to the text in the cell.
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Sets the value of a cell based on the text in the cell.
        /// </summary>
        /// <param name="e"></param>
        private void EnterKey(KeyEventArgs e)
        {
            if (connected)
            {
                spreadsheetPanel1.GetSelection(out int col, out int row);
                spreadsheetPanel1.GetValue(col, row, out string value);
                SetCell(row, col, value);
                e.SuppressKeyPress = true; //disables the annoying *ding* sound when pressing enter.

                //set the formula box to the contents of the cell
                string contents = ss1.GetCellContents(GetCellName(col, row)).ToString();
                FormulaBox.Text = contents;
                if (FormulaBox.Text.Length > 0)
                    FormulaBox.SelectionStart = FormulaBox.Text.Length;

                //unfocus from the current cell
                string unfocusMessage = "unfocus " + ((char)3);
                SendMessage(unfocusMessage);

                //tell server to edit the cell's contents
                string cellName = GetCellName(col, row);
                string editMsg = "edit " + cellName + ":" + contents + ((char)3);
                SendMessage(editMsg);
            }
        }

        /// <summary>
        /// Appends the char value of an operator key to the text in the cell.
        /// </summary>
        /// <param name="key"></param>
        private void OperatorKey(string key)
        {
            spreadsheetPanel1.GetSelection(out int col, out int row);
            spreadsheetPanel1.GetValue(col, row, out string value);
            string newVal = value + key.ToLower();
            spreadsheetPanel1.SetValue(col, row, newVal);
        }

        /// <summary>
        /// Resets the value display of a cell when a cell is exited.
        /// </summary>
        /// <param name="sender"></param>
        private void HandleSelectionChange(SpreadsheetPanel sender)
        {
            ResetCell(previousSelection);

            spreadsheetPanel1.GetSelection(out int col, out int row);
            spreadsheetPanel1.GetValue(col, row, out string value);

            previousSelection = GetCellName(col, row);

            string name = GetCellName(col, row);

            CellValueOutput.Text = value;
            string contents = ss1.GetCellContents(name).ToString();

            FormulaBox.Text = contents;

            spreadsheetPanel1.SetValue(col, row, contents);
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
        /// Returns an array containing the numerical [col, row] position of a cell name.
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns>int[row, col]</returns>
        private int[] GetCellPosition(string cellName)
        {
            cellName = cellName.Trim();
            int[] colRow = new int[2];
            colRow[0] = (int)cellName[0] - 65;
            colRow[1] = int.Parse(cellName.Substring(1).ToString()) - 1;

            return colRow;
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
        /// Sets the specified cell back to its original value.
        /// </summary>
        /// <param name="cellName"></param>
        private void ResetCell(string cellName)
        {
            int[] rowCol = GetCellPosition(cellName);
            int col = rowCol[0];
            int row = rowCol[1];

            string value = ss1.GetCellValue(cellName).ToString();

            if (ss1.GetCellValue(cellName).GetType() == typeof(FormulaError))
                spreadsheetPanel1.SetValue(col, row, "Formula Error!");
            else
                spreadsheetPanel1.SetValue(col, row, ss1.GetCellValue(cellName).ToString());

        }


        /// <summary>
        /// Writes the given contents to a specified cell location.
        /// </summary>
        private void SetCell(int row, int col, string cellVal)
        {
            try
            {
                string cellName = GetCellName(col, row);
                ss1.SetContentsOfCell(cellName, cellVal);

                //if the result is a formula error display a formula error message, otherwise set the cell with the result.
                if (ss1.GetCellValue(cellName).GetType() == typeof(FormulaError))
                    spreadsheetPanel1.SetValue(col, row, "Formula Error!");

                else
                {
                    spreadsheetPanel1.SetValue(col, row, ss1.GetCellValue(cellName).ToString());
                    UpdateCells(new HashSet<string>(ss1.getDependentCells(cellName)));
                }
            }

            catch (CircularException)
            {
                spreadsheetPanel1.SetValue(col, row, "Formula Error!");
            }

            catch (FormulaFormatException)
            {
                spreadsheetPanel1.SetValue(col, row, "Formula Error!");
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

        private void NewSpreadsheetButton_Click(object sender, EventArgs e)
        {
            SpreadsheetApplicationContext.getAppContext().RunForm(new Form1());
        }


        #endregion


    }
}
