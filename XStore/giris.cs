﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XStore
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //STORE
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //PORUDUCT
            Product pro = new Product();
            pro.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {//CUSTOMERS

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ORDERS

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SUPPLİERS
        }

        private void button6_Click(object sender, EventArgs e)
        {//REGİON

        }
    }
}