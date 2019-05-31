using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptTargetPackingLabels : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTargetPackingLabels()
        {
            InitializeComponent();
        }

        private void lblPageNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {            
        }
    }
}
