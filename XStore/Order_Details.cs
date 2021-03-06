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
    public partial class Order_Details : Form
    {
        public Order_Details()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);
        private void Order_Details_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'xStoreDBDataSet4.Orders' table. You can move, or remove it, as needed.
            this.ordersTableAdapter.Fill(this.xStoreDBDataSet4.Orders);
            // TODO: This line of code loads data into the 'xStoreDBDataSet3.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.xStoreDBDataSet3.Products);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //getir
            SiparisDetaylarınıGetir();

        }
     
        private void SiparisDetaylarınıGetir()
        {
            string odquery = "Select  * from OrderDetails";
            SqlDataAdapter dap = new SqlDataAdapter(odquery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
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
               
                if (item is NumericUpDown)
                {
                    NumericUpDown cb = (NumericUpDown)item;
                    cb.Value = 0;

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kaydet
            if (numericUpDown1.Value != 0)
            {
                try
                {
                    string insertod = "Insert Into OrderDetails(OrderID,ProductID,UnitPrice,Quantity,Discount) Values(@orderId, @productid, @unitprice,@quantity,@discount) ";
                    SqlCommand cmd = new SqlCommand(insertod, con);
                    cmd.Parameters.AddWithValue("@orderId", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@productid",comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@unitprice",numericUpDown1.Value );
                    cmd.Parameters.AddWithValue("@quantity",numericUpDown2.Value );
                    cmd.Parameters.AddWithValue("@discount", numericUpDown3.Value); 
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("İşlem Başarılı");
                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();

                    MessageBox.Show("Hata: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri doldurunuz.");
            }
            Temizle();
        }
        int id,id2;
        private void button2_Click(object sender, EventArgs e)
        {
            //Güncelle
            string updatequery = "Update OrderDetails SET ProductID = @productid ,UnitPrice = @unitprice ,Quantity = @quantity, Discount = @discount where OrderID = @orderid ";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            cmd.Parameters.AddWithValue("@unitprice",numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@quantity", numericUpDown2.Value);
            cmd.Parameters.AddWithValue("@discount", numericUpDown3.Value);
            cmd.Parameters.AddWithValue("@orderid", id);
            cmd.Parameters.AddWithValue("@productid", comboBox1.SelectedValue);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
            Temizle();
            SiparisDetaylarınıGetir();
        }
    
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                //id2 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
                comboBox1.SelectedValue=Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
                comboBox2.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value);
                numericUpDown2.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[3].Value);
                numericUpDown3.Value= Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[4].Value);
            }
        }
    }
}
