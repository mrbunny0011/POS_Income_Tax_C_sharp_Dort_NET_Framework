using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace POS_Income_Tax
{
    public partial class BillReport : Form
    {
        public BillReport()
        {
            InitializeComponent();
        }
        public DataTable data { get; set; }
        public string ReoprtName { get; set; }
        private void BillReport_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.SelectionMode= CrystalDecisions.Windows.Forms.SelectionMode.None;
            ReportDocument rd = new ReportDocument();
            rd.Load(this.ReoprtName);
            rd.SetDataSource(this.data);
            crystalReportViewer1.ReportSource = rd; 
        }
    }
}
