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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1004_assignment04
{
    public partial class SelectForm : Form
    {
        /*===============PROPERTIES================================*/
        public Form PreviousForm { get; set; }

        public SelectForm()
        {
            InitializeComponent();
        }
        
        /*--------------EVENT HANDLER-----------------------------*/

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.PreviousForm.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ProductInfoForm productInfoForm = new ProductInfoForm();
            productInfoForm.PreviousForm = this;
            productInfoForm.Show();
            this.Hide();
        }

        private void SelectForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.productList = (from product in Program.dollarComputerDB.products
                               select product).ToList();

                HardwareListDataGridView.DataSource = Program.productList;

            }
            catch (Exception err)
            {
                MessageBox.Show("Cannot retrieve data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(err.Message);
            }
           
        }

        private void HardwareListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            NextButton.Enabled = true;

            int rowIndex = HardwareListDataGridView.CurrentRow.Index;

            short currentId = (short)HardwareListDataGridView.Rows[rowIndex].Cells[0].Value;
            
            try
            {
                Program.selectedProduct = new product();
                Program.selectedProduct = (from product in Program.productList
                                 where product.productID == currentId
                                 select product).FirstOrDefault();
            }
            catch (Exception err)
            {
                MessageBox.Show("Cannot retrieve data", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(err.Message);
            }
            
            if(Program.selectedProduct != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Program.selectedProduct.manufacturer);
                sb.Append(" ");
                sb.Append(Program.selectedProduct.model);
                sb.Append(" Priced at: $");
                sb.Append(Program.selectedProduct.cost);
                SelectionTextBox.Text = sb.ToString();
            }
        }

        
    }
}
