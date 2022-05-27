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

            try
            {
                string customerquery = "Select CustomerID,Name from Customers";
                SqlDataAdapter dap = new SqlDataAdapter(customerquery, con);
                DataTable dt = new DataTable();
                dap.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "CustomerID";
                comboBox1.DisplayMember = "Name";
            }
            catch (Exception)
            {

                MessageBox.Show("Hata");
            }
            try
            {

                string storequery = "Select StoreID,StoreName from XStores";
                SqlDataAdapter dap1 = new SqlDataAdapter(storequery, con);
                DataTable dt1 = new DataTable();
                dap1.Fill(dt1);
                comboBox2.DataSource = dt1;
                comboBox2.ValueMember = "StoreID";
                comboBox2.DisplayMember = "StoreName";
            }
            catch (Exception)
            {

                MessageBox.Show("Hata");

            }


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
                    cmd.Parameters.AddWithValue("@customerid", (comboBox1.SelectedValue));
                    cmd.Parameters.AddWithValue("@orderdate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@shippeddate",dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@shipadress", textBox1.Text);
                    cmd.Parameters.AddWithValue("@storeid",comboBox2.SelectedValue);

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
        int id;
        private void button3_Click(object sender, EventArgs e)
        {
            //gUNCELLE
            string updatequery = "Update Orders SET CustomerID=@customerid,OrderDate=@orderdate,ShippedDate=@shippeddate,ShipAdress=@shipadress," +
                "StoreID=@storeid where OrderId = @Orderid";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            cmd.Parameters.AddWithValue("@customerid",comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@orderdate",dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@shippeddate", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@shipadress", textBox1.Text);
            cmd.Parameters.AddWithValue("@storeid", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@Orderid", id);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
            Temizle();
            SiparisleriGetir();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
               comboBox1.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
               dateTimePicker1.Value =Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                comboBox2.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());

            }
        }
    }
}
