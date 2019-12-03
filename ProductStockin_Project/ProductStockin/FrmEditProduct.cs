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
    public partial class FrmEditProduct : Form
    {
        public FrmEditProduct()
        {
            InitializeComponent();
        }

        private void FrmEditProduct_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT ProductId FROM Products ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            comboBox1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Products SET ProductName= @n, [Description]=@d WHERE ProductId=@i", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = comboBox1.SelectedValue });
            cmd.Parameters.Add(new SqlParameter("@n", SqlDbType.NVarChar) { Value = textBox2.Text });
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.NVarChar) { Value = textBox3.Text });
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                MessageBox.Show("Data Updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE ProductId=@i", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = comboBox1.SelectedValue });
            conn.Open();
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                textBox2.Text = r.GetString(1);
                textBox3.Text = r.GetString(2);
            }
            conn.Close();
        }
    }
}
