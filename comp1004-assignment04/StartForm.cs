/*
 App name: DollarComputer
 Author's name: Kannika Bhatia
 Student ID: 200332992
 App Creation Date: 30 March 2017
 App Description: Connect to file or database and load information into form so
                    user can buy computer they like. Save their selection into file.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1004_assignment04
{
    public partial class StartForm : Form
    {
        /*===============PROPERTIES================================*/
        public SplashScreen PreviousForm { get; set; }

        public StartForm()
        {
            InitializeComponent();
        }

        /*--------------EVENT HANDLER-----------------------------*/

        private void StartNewOrderButton_Click(object sender, EventArgs e)
        {
            SelectForm selectForm = new SelectForm();
            selectForm.PreviousForm = this;
            selectForm.Show();
            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenSavedOrderButton_Click(object sender, EventArgs e)
        {
            ProductInfoForm productInfoForm = new ProductInfoForm();
            productInfoForm.PreviousForm = this;
            productInfoForm.Show();
            this.Hide();
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                Environment.Exit(1);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
