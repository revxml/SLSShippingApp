using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptPackingLabels : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPackingLabels()
        {
            InitializeComponent();
            this.PageHeight = 200;
            this.PageWidth = 400;
        }

    }
}
