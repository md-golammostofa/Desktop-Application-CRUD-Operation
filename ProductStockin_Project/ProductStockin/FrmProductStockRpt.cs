using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductStockin
{
    public partial class FrmProductStockRpt : Form
    {
        public FrmProductStockRpt()
        {
            InitializeComponent();
        }

        private void FrmProductStockRpt_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT * FROM Products", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Products");
            da.SelectCommand.CommandText = "SELECT * FROM StockIns";
            da.Fill(ds, "StockIns");
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Path.GetFullPath("..\\..\\") + "ProductStockRpt.rpt");
            cryRpt.SetDataSource(ds);
            

            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
