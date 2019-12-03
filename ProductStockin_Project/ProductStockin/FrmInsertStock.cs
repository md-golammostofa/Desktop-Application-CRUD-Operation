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
    public partial class FrmInsertStock : Form
    {
        public FrmInsertStock()
        {
            InitializeComponent();
        }

        private void FrmInsertStock_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT ProductId, ProductName FROM Products ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            comboBox1.DataSource = ds.Tables[0];

            SqlCommand cmd = new SqlCommand("SELECT MAX(Id)+1 FROM StockIns", conn);
            conn.Open();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            textBox1.Text = $"{id}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO StockIns VALUES (@i, @d, @p, @q)", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = textBox1.Text });
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.Date) { Value = dateTimePicker1.Value.Date });
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.Int) { Value = comboBox1.SelectedValue });
            cmd.Parameters.Add(new SqlParameter("@q", SqlDbType.Int) { Value = textBox2.Text });
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                MessageBox.Show("Data inserted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int x = int.Parse(textBox1.Text);
            textBox1.Text = $"{x + 1}";
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
