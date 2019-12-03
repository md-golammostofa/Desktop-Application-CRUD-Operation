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

namespace ProductStockin
{
    public partial class FrmInsertProduct : Form
    {
        public FrmInsertProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn= new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Products VALUES (@i, @n, @d)", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = textBox1.Text });
            cmd.Parameters.Add(new SqlParameter("@n", SqlDbType.NVarChar) { Value = textBox2.Text });
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.NVarChar) { Value = textBox3.Text });
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                MessageBox.Show("Data inserted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Text = $"{int.Parse(textBox1.Text) + 1}";
            textBox2.Clear();
            textBox3.Clear();
        }

        private void FrmInsertProduct_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT MAX(ProductId)+1 FROM Products", conn);
            conn.Open();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            textBox1.Text = $"{id}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
