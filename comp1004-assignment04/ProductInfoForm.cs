using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1004_assignment04
{
    public partial class ProductInfoForm : Form
    {

        public Form PreviousForm { get; set; }
        private product _selectedProduct;

        private StreamWriter _swriter;
        private StreamReader _sreader;

        public ProductInfoForm()
        {
            InitializeComponent();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string fileName;

            OpenProductFileDialog.Filter = "Text Files (*.txt)|*.txt";
            OpenProductFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            result = OpenProductFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileName = OpenProductFileDialog.FileName;
                int productID;
                try
                {
                    this._sreader = new StreamReader(fileName);

                    if (this._sreader.Peek() != -1)
                    {
                        productID = Convert.ToInt32(_sreader.ReadLine());
                        this._selectedProduct = (from product in Program.dollarComputerDB.products
                                         where product.productID == productID
                                         select product).FirstOrDefault();
                        _fillProductInfoInForm();
                    }
                    else
                    {
                        MessageBox.Show("File Empty", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine(err.Message);
                    MessageBox.Show("Cannot open file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this._sreader.Close();
                }
            }  
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this._selectedProduct != null)
            {
                DialogResult result;
                string fileName;

                SaveProductFileDialog.Filter = "Text Files (*.txt)|*.txt";
                SaveProductFileDialog.FileName = "Product.txt";
                SaveProductFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                result = SaveProductFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = SaveProductFileDialog.FileName;

                    try
                    {
                        this._swriter = new StreamWriter(fileName);
                        this._swriter.WriteLine(this._selectedProduct.productID);
                        MessageBox.Show("Successfully writing to file", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine(err.Message);
                        MessageBox.Show("Cannot write to file", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this._swriter.Close();
                    }
                }
            }
        }

        private void SelectAnotherProductButton_Click(object sender, EventArgs e)
        {
            SelectForm selectForm = new SelectForm();
            selectForm.PreviousForm = this;
            selectForm.Show();
            this.Hide();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductInfoForm_FormClosing(object sender, FormClosingEventArgs e)
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
            OrderForm orderForm = new OrderForm();
            orderForm.PreviousForm = this;
            orderForm.Show();
            this.Hide();
        }

        private void ProductInfoForm_Load(object sender, EventArgs e)
        {
            if (SelectForm.selectProduct != null)
            {
                this._selectedProduct = SelectForm.selectProduct;
                _fillProductInfoInForm();
            }
            else if (this.PreviousForm != null)
            {
                openToolStripMenuItem_Click(sender, e);
            }
        }

        private void _fillProductInfoInForm()
        {
            if (this._selectedProduct != null)
            {
                this.NextButton.Enabled = true;
                ProductIdTextBox.Text = this._selectedProduct.productID.ToString();
                ConditionTextBox.Text = this._selectedProduct.condition.ToString();
                CostTextBox.Text = "$" + this._selectedProduct.cost.ToString();
                PlatformTextBox.Text = this._selectedProduct.platform.ToString();
                OSTextBox.Text = this._selectedProduct.OS.ToString();
                ManufacturerTextBox.Text = this._selectedProduct.manufacturer.ToString();
                ModelTextBox.Text = this._selectedProduct.model.ToString();
                MemoryTextBox.Text = this._selectedProduct.RAM_size.ToString();
                CPUBrandTextBox.Text = this._selectedProduct.CPU_brand.ToString();
                CPUTypeTextBox.Text = this._selectedProduct.CPU_type.ToString();
                LCDSizeTextBox.Text = this._selectedProduct.screensize.ToString();
                CPUNumberTextBox.Text = this._selectedProduct.CPU_number.ToString();
                CPUSpeedTextBox.Text = this._selectedProduct.CPU_speed.ToString();
                HDDTextBox.Text = this._selectedProduct.HDD_size.ToString();
                GPUTypeTextBox.Text = this._selectedProduct.GPU_Type.ToString();
                WebCamTextBox.Text = this._selectedProduct.webcam.ToString();
            }
        }
    }
}
