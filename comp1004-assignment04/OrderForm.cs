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
    public partial class OrderForm : Form
    {
        /*===============PROPERTIES================================*/
        public Form PreviousForm { get; set; }

        /*===============INSTANCE VARIABLES=======================*/
        private const decimal _taxRate = 0.13m;
        private decimal _price;
        private decimal _salesTax;
        private decimal _totalPrice;
        
        public OrderForm()
        {
            InitializeComponent();
        }

        /*--------------EVENT HANDLER-----------------------------*/

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            ConditionTextBox.Text = Program.selectedProduct.condition.ToString();
            PlatformTextBox.Text = Program.selectedProduct.platform.ToString();
            ModelTextBox.Text = Program.selectedProduct.model.ToString();
            ManufacturerTextBox.Text = Program.selectedProduct.manufacturer.ToString();
            LCDSizeTextBox.Text = Program.selectedProduct.screensize.ToString();
            MemoryTextBox.Text = Program.selectedProduct.RAM_size.ToString();
            CPUBrandTextBox.Text = Program.selectedProduct.CPU_brand.ToString();
            CPUTypeTextBox.Text = Program.selectedProduct.CPU_type.ToString();
            CPUNumberTextBox.Text = Program.selectedProduct.CPU_number.ToString();
            CPUSpeedTextBox.Text = Program.selectedProduct.CPU_speed.ToString();
            HDDTextBox.Text = Program.selectedProduct.HDD_size.ToString();
            GPUTypeTextBox.Text = Program.selectedProduct.GPU_Type.ToString();
            WebCamTextBox.Text = Program.selectedProduct.webcam.ToString();
            OSTextBox.Text = Program.selectedProduct.OS.ToString();

            _price = (decimal) Program.selectedProduct.cost;
            PriceTextBox.Text = _price.ToString();
            _salesTax = (decimal)Program.selectedProduct.cost * _taxRate;
            SalesTaxTextBox.Text = _salesTax.ToString();
            _totalPrice = _price + _salesTax;
            TotalTextBox.Text = _totalPrice.ToString();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your selected order is printing...", "Printing", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ProductInfoForm productInfoForm = new ProductInfoForm();
            productInfoForm.PreviousForm = this;
            productInfoForm.Show();
            this.Hide();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.PreviousForm.Close();
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Thank you for shopping at DollarComputer.\nYour order will be divered to you within 7-10 business day", 
                "Thank you", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                this.PreviousForm.Close();
            }
        }
    }
}
