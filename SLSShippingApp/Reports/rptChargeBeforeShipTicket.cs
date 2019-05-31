using System;
using System.Drawing;
using System.Configuration;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptChargeBeforeShipTicket : DevExpress.XtraReports.UI.XtraReport
    {
        public rptChargeBeforeShipTicket()
        {
            InitializeComponent();
        }

        private void tableCell61_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String sDesc1 = Detail.Report.GetCurrentColumnValue("StockDescription1").ToString();
            String sDesc2 = Detail.Report.GetCurrentColumnValue("StockDescription2").ToString();
            (sender as XRTableCell).Text = sDesc1 + "\r\n" + sDesc2;
        }
    }
}
