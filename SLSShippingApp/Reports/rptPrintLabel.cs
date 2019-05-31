using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Configuration;

namespace SLSShippingApp.Reports
{
    public partial class rptPrintLabel : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPrintLabel()
        {
            InitializeComponent();
            this.PageHeight = 200;
            this.PageWidth = 400;
            lblDate.Text = DateTime.Now.ToShortDateString();           
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String sImagePath = @Detail.Report.GetCurrentColumnValue("Image").ToString();//.Replace(@"\\",@"\");
            (sender as XRPictureBox).ImageUrl = sImagePath;
           ((XRPictureBox)sender).PerformLayout();            
        }

        private void txtBayLoc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Detail.Report.GetCurrentColumnValue("BayLocation").ToString() == "-1")
                ((XRLabel)sender).Text = String.Empty;
        }
    }
}
