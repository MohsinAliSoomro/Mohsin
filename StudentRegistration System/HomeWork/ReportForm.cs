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
namespace HomeWork
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            ReportDocument report = new ReportDocument();
            report.Load(@"C:\Users\Mohsin_Ali\documents\visual studio 2015\Projects\HomeWork\HomeWork\CrystalReport1.rpt");
            crystalReportViewer1.ReportSource = report;
        }
    }
}
