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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comp1004_assignment04
{
    public partial class ProductInfoForm : Form
    {
        /*===============PROPERTIES================================*/
        public Form PreviousForm { get; set; }

        private StreamWriter _swriter;
        private StreamReader _sreader;

        public ProductInfoForm()
        {
            InitializeComponent();
        }

        /*--------------EVENT HANDLER-----------------------------*/

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
                        Program.selectedProduct = (from product in Program.dollarComputerDB.products
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
            if(Program.selectedProduct != null)
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
                        this._swriter.WriteLine(Program.selectedProduct.productID);
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
            this.PreviousForm.Close();
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
            if (Program.selectedProduct != null)
            {
                _fillProductInfoInForm();
            }
            else if (this.PreviousForm != null)
            {
                openToolStripMenuItem_Click(sender, e);
            }
        }
        /*==================FUNCTION===============================*/
        private void _fillProductInfoInForm()
        {
            if (Program.selectedProduct != null)
            {
                this.NextButton.Enabled = true;
                ProductIdTextBox.Text = Program.selectedProduct.productID.ToString();
                ConditionTextBox.Text = Program.selectedProduct.condition.ToString();
                CostTextBox.Text = "$" + Program.selectedProduct.cost.ToString();
                PlatformTextBox.Text = Program.selectedProduct.platform.ToString();
                OSTextBox.Text = Program.selectedProduct.OS.ToString();
                ManufacturerTextBox.Text = Program.selectedProduct.manufacturer.ToString();
                ModelTextBox.Text = Program.selectedProduct.model.ToString();
                MemoryTextBox.Text = Program.selectedProduct.RAM_size.ToString();
                CPUBrandTextBox.Text = Program.selectedProduct.CPU_brand.ToString();
                CPUTypeTextBox.Text = Program.selectedProduct.CPU_type.ToString();
                LCDSizeTextBox.Text = Program.selectedProduct.screensize.ToString();
                CPUNumberTextBox.Text = Program.selectedProduct.CPU_number.ToString();
                CPUSpeedTextBox.Text = Program.selectedProduct.CPU_speed.ToString();
                HDDTextBox.Text = Program.selectedProduct.HDD_size.ToString();
                GPUTypeTextBox.Text = Program.selectedProduct.GPU_Type.ToString();
                WebCamTextBox.Text = Program.selectedProduct.webcam.ToString();
            }
        }
    }
}
