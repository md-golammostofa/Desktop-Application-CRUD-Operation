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
    public partial class FrmViewProducts : Form
    {
        DataView dvProducts, dvStocks;
        DataSet ds;
        SqlConnection conn;
        public FrmViewProducts()
        {
            InitializeComponent();
        }

        private void dgProducts_SelectionChanged(object sender, EventArgs e)
        {
            if(dgProducts.SelectedRows.Count > 0)
            {
                dvStocks.RowFilter = $"ProductId={dgProducts.SelectedRows[0].Cells[0].Value}";
            }
        }

        private void FrmViewProducts_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT p.ProductId, ProductName, [Description], SUM(Quantity) AS Stock
                                                    FROM Products p LEFT OUTER JOIN StockIns s 
                                                    ON p.ProductId = s.ProductId
                                                    GROUP BY p.ProductId, ProductName, [Description]", conn);
            ds = new DataSet();
            da.Fill(ds, "Products");
            da.SelectCommand.CommandText = "SELECT * FROM StockIns";
            da.Fill(ds, "StockIns");
            dvProducts = ds.Tables["Products"].DefaultView;
           
            dvStocks = ds.Tables["StockIns"].DefaultView;
            dgProducts.DataSource = dvProducts;
            dgStock.DataSource = dvStocks;
        }
    }
}
