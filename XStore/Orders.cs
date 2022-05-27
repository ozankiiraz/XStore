using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XStore
{
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'xStoreDBDataSet2.XStores' table. You can move, or remove it, as needed.
            this.xStoresTableAdapter1.Fill(this.xStoreDBDataSet2.XStores);
            // TODO: This line of code loads data into the 'xStoreDBDataSet1.XStores' table. You can move, or remove it, as needed.
            this.xStoresTableAdapter.Fill(this.xStoreDBDataSet1.XStores);
            // TODO: This line of code loads data into the 'xStoreDBDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.xStoreDBDataSet.Customers);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SiparisleriGetir();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);
        private void SiparisleriGetir()
        {
            string OrderSelect = "Select * from Orders";
            SqlDataAdapter dap = new SqlDataAdapter(OrderSelect, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Ekle
            if (textBox1.Text != null ) // KONTROLLER YAZILACAK
            {
                try
                {
                    string insertOrders = "Insert Into Orders(CustomerID,OrderDate,ShippedDate,ShipAdress,StoreID) " +
                        "Values(@customerid,@orderdate,@shippeddate,@shipadress,@storeid)";
                    SqlCommand cmd = new SqlCommand(insertOrders, con);
                    cmd.Parameters.AddWithValue("@customerid", comboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("@orderdate", Convert.ToDateTime(dateTimePicker1.Value));
                    cmd.Parameters.AddWithValue("@shippeddate",Convert.ToDateTime(dateTimePicker2.Value));
                    cmd.Parameters.AddWithValue("@shipadress", textBox1.Text);
                    cmd.Parameters.AddWithValue("@storeid",Convert.ToInt32(comboBox2.SelectedItem));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("İşlem Başarılı");
                    con.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri doldurunuz.");
            }
            Temizle();
            SiparisleriGetir();
        }

        private void Temizle()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = (TextBox)item;
                    txt.Clear();
                }
                if (item is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)item;
                    dtp.Value = DateTime.Now;
                }
            }
            }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
