﻿/*
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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        /*--------------EVENT HANDLER-----------------------------*/
        private void SplashScreenTimer_Tick(object sender, EventArgs e)
        {
            SplashScreenTimer.Enabled = false;
            StartForm startForm = new StartForm();
            startForm.PreviousForm = this;
            startForm.Show();
            this.Hide();
        }
    }
}
