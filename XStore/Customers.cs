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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);
        int id;
        private void btn_Guncelle_Click(object sender, EventArgs e)
        {
            string updatequery = "Update Customers SET  Name = @name,Surname=@surname,Phone=@phone where CustomerID = @cusId";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@surname", textBox2.Text);
            cmd.Parameters.AddWithValue("@phone",textBox3.Text);
            cmd.Parameters.AddWithValue("@cusId", id);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
            Temizle();
            MüsterileriGetir();

        }

        private void btn_Getir_Click(object sender, EventArgs e)
        {
            MüsterileriGetir();
        }

        private void MüsterileriGetir()
        {
            string sorgu = "Select * from Customers";
            SqlDataAdapter dap = new SqlDataAdapter(sorgu, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {
                try
                {
                    string insertCus = "Insert Into Customers(Name,Surname,Phone) Values(@name, @surname,@phone) ";
                    SqlCommand cmd = new SqlCommand(insertCus, con);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@surname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox3.Text);

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
            MüsterileriGetir();
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
            }
        }
       
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }
    }
}
