using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptHSNPackingLables : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHSNPackingLables()
        {
            InitializeComponent();
        }

        private void lblToday_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblToday.Value = DateTime.Now.ToShortDateString();
        }
    }
}
