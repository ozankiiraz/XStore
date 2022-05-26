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
    public partial class Kategoriler : Form
    {
        public Kategoriler()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);

        private void button1_Click(object sender, EventArgs e)
        {
            ShowCategories();
        }

        private void ShowCategories()
        {
            string categoryQuery = "Select * from Categories";
            SqlDataAdapter dap = new SqlDataAdapter(categoryQuery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {
                try
                {
                    string insertCat = "Insert Into Categories(CategoryName,Description) Values(@catName, @desc) ";
                    SqlCommand cmd = new SqlCommand(insertCat, con);
                    cmd.Parameters.AddWithValue("catName", textBox1.Text);
                    cmd.Parameters.AddWithValue("desc", textBox2.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("İşlem Başarılı");
                    con.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata: " +ex);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri doldurunuz.");
            }
            Temizle();
            ShowCategories();
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
            //foreach (Control item in groupBox2.Controls)
            //{
            //    if (item is TextBox)
            //    {
            //        TextBox txt = (TextBox)item;
            //        txt.Clear();
            //    }
            //}
        }
        int id;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string updatequery = "Update Categories SET  CategoryName = @catName, Description = @desc where CategoryID = @catId";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            cmd.Parameters.AddWithValue("@catName", textBox1.Text);
            cmd.Parameters.AddWithValue("@desc", textBox2.Text);
            cmd.Parameters.AddWithValue("@catId", id);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            con.Close();
            Temizle();
            ShowCategories();
        }
    }
}
