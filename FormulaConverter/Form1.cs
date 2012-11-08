﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Formula
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtOutputFormula.Text = "";
            foreach (string str in Formula.InfixToPostfix(txtInputFormula.Text))
            {
                txtOutputFormula.Text += str + " ";
            }
        }
    }
}
