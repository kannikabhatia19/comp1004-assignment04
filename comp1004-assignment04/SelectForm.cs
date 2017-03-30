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
    public partial class SelectForm : Form
    {
        public Form PreviousForm { get; set; }

        public SelectForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                this.PreviousForm.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ProductInfoForm productInfoForm = new ProductInfoForm();
            productInfoForm.PreviousForm = this;
            productInfoForm.Show();
            this.Hide();
        }
    }
}
