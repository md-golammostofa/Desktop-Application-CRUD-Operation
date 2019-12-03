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
    public partial class FrmEditDeleteStock : Form
    {
        public FrmEditDeleteStock()
        {
            InitializeComponent();
        }

        private void FrmEditDeleteStock_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT ProductId, ProductName FROM Products ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            da.SelectCommand.CommandText = "SELECT Id FROM StockIns";
            da.Fill(ds, "StockIns");
            comboBox1.DataSource = ds.Tables[0];
            comboBox2.DataSource = ds.Tables[1];
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM StockIns WHERE Id=@i", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = comboBox1.SelectedValue });
            conn.Open();
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                dateTimePicker1.Value = r.GetDateTime(1).Date;
                comboBox1.SelectedValue = r.GetInt32(2).ToString();
                textBox2.Text = r.GetInt32(3).ToString();
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE StockIns SET [Date]= @d, [ProductId]=@p, Quantity=@q WHERE Id=@i", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = comboBox2.SelectedValue });
            cmd.Parameters.Add(new SqlParameter("@d", SqlDbType.Date) { Value = dateTimePicker1.Value.Date });
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.Int) { Value = comboBox1.SelectedValue });
            cmd.Parameters.Add(new SqlParameter("@q", SqlDbType.Int) { Value = textBox2.Text });
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                MessageBox.Show("Data Updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM StockIns  WHERE Id=@i", conn);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.Int) { Value = comboBox2.SelectedValue });
            
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                MessageBox.Show("Data Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            SqlDataAdapter da = new SqlDataAdapter("SELECT Id FROM StockIns", conn);
            DataSet ds = new DataSet();
            
            da.Fill(ds, "StockIns");
            
            comboBox2.DataSource = ds.Tables[0];
        }
    }
}
