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

        //I use static variable because this information will be used for the whole
        //program not just one specific class
        public static List<product> productList = new List<product>();
        public DollarComputerContext dollarComputerDB;
        public static product selectProduct;

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

        private void SelectForm_Load(object sender, EventArgs e)
        {
            try
            {
                dollarComputerDB = new DollarComputerContext();

                productList = (from product in dollarComputerDB.products
                               select product).ToList();

                HardwareListDataGridView.DataSource = productList;

            }
            catch (Exception err)
            {
                //The error message should be hide from the outside world, but
                //I print it out to the console for debugging purpose
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
                selectProduct = new product();
                selectProduct = (from product in productList
                                 where product.productID == currentId
                                 select product).FirstOrDefault();
            }
            catch (Exception err)
            {
                //The error message should be hide from the outside world,
                //but I print it out to the console for debugging purpose
                MessageBox.Show("Cannot retrieve data", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(err.Message);
            }
            
            if(selectProduct != null)
            {

            }
        }
    }
}
