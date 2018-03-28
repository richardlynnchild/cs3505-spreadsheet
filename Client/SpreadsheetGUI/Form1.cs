using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Form1's KeyDown event handler gets our new event handler.
            this.KeyDown += Form1_KeyDown;
        }

        //Im leaving this Method in for now, for a future refernce on event hanlders.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                MessageBox.Show("A pressed.");
            if (e.KeyCode == Keys.Tab)
                MessageBox.Show("Tab Pressed.");
        }

        /// <summary>
        /// Overrides the base ProcessCmdKey method to allow use of non-character keys
        /// in this case: up, down, right, left and tab.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //get the row, col postion of the currently selected cell.
            spreadsheetPanel1.GetSelection(out int col, out int row);

            //move the selected cell based on the key pressed
            switch (keyData)
            {
                case Keys.Up:
                    if (row - 1 < 0)
                        row = 99;
                    spreadsheetPanel1.SetSelection(col, row - 1);
                    break;

                case Keys.Down:
                    if (row + 1 > 98)
                        row = -1;
                    spreadsheetPanel1.SetSelection(col, row + 1);
                    break;

                case Keys.Right:
                    if (col + 1 > 25)
                    {
                        spreadsheetPanel1.AutoScrollPosition = new Point(0,0);
                        col = -1;
                    }
                    spreadsheetPanel1.SetSelection(col + 1, row);
                    break;

                case Keys.Left:
                    if (col - 1 < 0)
                        col = 26;
                    spreadsheetPanel1.SetSelection(col - 1, row);
                    break;

                case Keys.Tab:
                    if (col + 1 > 25)
                        col = -1;
                    spreadsheetPanel1.SetSelection(col + 1, row);
                    break;

                default:
                    return false;
            }
            return true;
        }

    }
}
