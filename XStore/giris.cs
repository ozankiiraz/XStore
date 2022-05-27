using System;
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
            XStorePanel xs = new XStorePanel();
            xs.Show();
            this.Hide();
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
            Customers cus = new Customers();
            cus.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ORDERS
            Orders or = new Orders();
            or.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers sup = new Suppliers();
            sup.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Order_Details ORD = new Order_Details();
            ORD.Show();
            this.Hide();
        }
    }
}
