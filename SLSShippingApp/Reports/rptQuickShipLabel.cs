﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptQuickShipLabel : DevExpress.XtraReports.UI.XtraReport
    {
        public rptQuickShipLabel()
        {
            InitializeComponent();
        }

        private void lblToday_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblToday.Value = DateTime.Now.ToShortDateString();
        }

        private void pbImage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            pbImage.ImageUrl = Detail.Report.GetCurrentColumnValue("txtImageFile").ToString();
        }
    }
}
