using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SLSShippingApp.Reports
{
    public partial class rptPickingTicket : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPickingTicket()
        {
            InitializeComponent();
        }

        private void lblProp651_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Boolean isProp65 = Convert.ToBoolean(Detail.Report.GetCurrentColumnValue("IsProp65"));
            if (isProp65)
            {
                lblProp651.Visible = true;
                lblProp652.Visible = true;
                lblProp653.Visible = true;
                pbProp65.Visible = true;
            }
            String sBay =Detail.Report.GetCurrentColumnValue("BayLocation").ToString();
            Boolean bHold = Convert.ToBoolean(Detail.Report.GetCurrentColumnValue("Hold"));
            if (bHold)
                imgHold.Visible = true;
            else if (sBay != String.Empty && sBay != "-1")
                imgBay.Visible = true;
            else
                imgSingle.Visible = true;
        }
    }
}
