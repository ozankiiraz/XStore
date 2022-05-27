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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);
        private void button1_Click(object sender, EventArgs e)
        {
            UrunleriGetir();

        }

        private void UrunleriGetir()
        {
            string stockquery = "Select  * from Products";
            SqlDataAdapter dap = new SqlDataAdapter(stockquery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Product_Load(object sender, EventArgs e)
        {
            string categoriesquery = "Select CategoryID,CategoryName from Categories";
            SqlDataAdapter dap = new SqlDataAdapter(categoriesquery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "CategoryID";
            comboBox1.DisplayMember = "CategoryName";


            string suppliersquery = "Select SupplierID, CompanyName from Suppliers";
            SqlDataAdapter dap1 = new SqlDataAdapter(suppliersquery, con);
            DataTable dt1 = new DataTable();
            dap1.Fill(dt1);
            comboBox2.DataSource = dt1;
            comboBox2.ValueMember = "SupplierID";
            comboBox2.DisplayMember = "CompanyName";

            button3.Enabled = false;

        }
     


        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != null) 
            {
                try
                {
                    string insertProd = "Insert Into Products(CategoryID, SupplierID, ProductName, UnitPrice, UnitsInStock) " +
                        "Values(@catId,@supId, @Name, @up, @uis)";
                    SqlCommand cmd = new SqlCommand(insertProd, con);
                    cmd.Parameters.AddWithValue("@catId", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@supId", (comboBox2.SelectedValue));
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@up",numericUpDown1.Value );
                    cmd.Parameters.AddWithValue("@uis", numericUpDown1.Value);


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
            UrunleriGetir();
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
                    NumericUpDown n = (NumericUpDown)item;
                    n.Value = 0;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string updatequery = "Update Products  SET  CategoryID = @catId, SupplierID = @supId, ProductName = @Name, UnitPrice =  @up, UnitsInStock = @uis";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            cmd.Parameters.AddWithValue("@catId", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@supId", (comboBox2.SelectedValue));
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@up", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@uis", numericUpDown1.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
            Temizle();
            UrunleriGetir();
            button3.Enabled = false;

        }
        int id;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                comboBox1.SelectedValue = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                comboBox2.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[4].Value);
                numericUpDown2.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[4].Value);
            }
            button3.Enabled = true;
        }
    }
}
