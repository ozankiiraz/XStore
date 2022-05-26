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

        }
        int id;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                comboBox1.ValueMember = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                comboBox2.ValueMember = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[4].Value);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
