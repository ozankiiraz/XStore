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
    public partial class XStorePanel : Form
    {
        public XStorePanel()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["XStoreDB"].ConnectionString);



        private void StoreGetir()
        {
            string shipperQuery = "Select  * from XStores";
            SqlDataAdapter dap = new SqlDataAdapter(shipperQuery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StoreGetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1!= null){
                string insertSup = "Insert Into XStores(StoreName, RegionID) Values(@storeName, @rId)";
                SqlCommand cmd = new SqlCommand(insertSup, con);
                cmd.Parameters.AddWithValue("storeName", textBox1.Text);
                MessageBox.Show(comboBox1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("rId", Convert.ToInt32(comboBox1.SelectedValue));


                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
                con.Close();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen Mağaza Adı Giriniz.");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                string insertReg = "Insert Into Region(RegionDescription) Values(@desc)";
                SqlCommand cmd = new SqlCommand(insertReg, con);
                cmd.Parameters.AddWithValue("desc", textBox2.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
                con.Close();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen Bölge Adı Giriniz.");
            }

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
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = (TextBox)item;
                    txt.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BolgeGetir();
        }

        private void BolgeGetir()
        {
            string regionrQuery = "Select  * from Region";
            SqlDataAdapter dap = new SqlDataAdapter(regionrQuery, con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "RegionDescription";
            comboBox1.ValueMember = "RegionID";
        }

        int id;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BolgeGetir();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string updatequery = "Update XStores  SET  StoreName=@name where  StoreID= @rId";
                SqlCommand cmd = new SqlCommand(updatequery, con);
                cmd.Parameters.AddWithValue("rId", id);
                cmd.Parameters.AddWithValue("name", textBox1.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı");
                con.Close();
                StoreGetir();
            }
            catch (Exception)
            {

                throw;
            }
            Temizle();

        }
    }
}
