using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductStockin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmViewProducts { MdiParent = this }.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmInsertProduct { MdiParent = this }.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmEditProduct { MdiParent = this }.Show();
        }

        private void newStockInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmInsertStock { MdiParent = this }.Show();
        }

        private void editDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmEditDeleteStock { MdiParent = this }.Show();
        }

        private void productStockSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmProductStockRpt { MdiParent = this }.Show();
        }

        private void stockInByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmStockInByDateRpt { MdiParent = this }.Show();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmProductStockRpt { MdiParent = this }.Show();
        }
    }
}
