using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptPrintLabel : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPrintLabel()
        {
            InitializeComponent();
            this.txtDate.Text = DateTime.Now.ToShortDateString();
        }

    }
}
