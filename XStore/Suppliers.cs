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
    public partial class Suppliers : Form
    {
        public Suppliers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);

        private void button1_Click(object sender, EventArgs e)
        {
            TedarikcileriGetir();
        }

        private void TedarikcileriGetir()
        {
            string shipperQuery = "Select  * from Suppliers";
            SqlDataAdapter dap = new SqlDataAdapter(shipperQuery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string insertSup = "Insert Into Suppliers(CompanyName, ContactName, Phone, Country, City, Address, Status) Values(@compName, @contName, @phone, @country, @city, @address,1)";
            SqlCommand cmd = new SqlCommand(insertSup, con);
            cmd.Parameters.AddWithValue("compName", textBox1.Text);
            cmd.Parameters.AddWithValue("contName", textBox2.Text);
            cmd.Parameters.AddWithValue("phone", textBox3.Text);
            cmd.Parameters.AddWithValue("country", textBox4.Text);
            cmd.Parameters.AddWithValue("city", textBox5.Text);
            cmd.Parameters.AddWithValue("address", textBox6.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
        }
        int id;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string updatequery = "Update Suppliers  SET  CompanyName=@compName, ContactName=@contName, Phone=@phone, Country=@country, City=@city, Address=@address where SupplierID = @supId";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            cmd.Parameters.AddWithValue("supId", id);
            cmd.Parameters.AddWithValue("compName", textBox1.Text);
            cmd.Parameters.AddWithValue("contName", textBox2.Text);
            cmd.Parameters.AddWithValue("phone", textBox3.Text);
            cmd.Parameters.AddWithValue("country", textBox4.Text);
            cmd.Parameters.AddWithValue("city", textBox5.Text);
            cmd.Parameters.AddWithValue("address", textBox6.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
            Temizle();
            TedarikcileriGetir();
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
                if (item is CheckBox)
                {
                    CheckBox cb = (CheckBox)item;
                    cb.Checked = false;

                }
            }
        }


    }
}
