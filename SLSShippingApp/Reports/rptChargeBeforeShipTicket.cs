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

        //private void lblProp651_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    Boolean isProp65 = Convert.ToBoolean(Detail.Report.GetCurrentColumnValue("IsProp65"));
        //    if (isProp65)
        //    {
        //        lblProp651.Visible = true;
        //        lblProp652.Visible = true;
        //        lblProp653.Visible = true;
        //        pbProp65.Visible = true;
        //    }
        //    String sBay = Detail.Report.GetCurrentColumnValue("BayLocation").ToString();
        //    Boolean bHold = Convert.ToBoolean(Detail.Report.GetCurrentColumnValue("Hold"));

        //    if (bHold)
        //        imgHold.Visible = true;
        //    else if (sBay != String.Empty && sBay != "-1" && sBay != "N/A")
        //        imgBay.Visible = true;
        //    else
        //        imgSingle.Visible = true;
        //}

        //private void pbItemImage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{

        //    String fullPathToFile = ConfigurationManager.AppSettings["ImagePath"].ToString();// + "! Image Not Found !.jpg";
        //    String sImage = @Detail.Report.GetCurrentColumnValue("StockNumber").ToString().Trim();//.Replace(@"\\",@"\");
        //    if (sImage != String.Empty)
        //        sImage += ".jpg";
        //    else
        //        sImage = "! Image Not Found !.jpg";

        //    fullPathToFile += sImage;

        //    (sender as XRPictureBox).ImageUrl = Convert.ToString(fullPathToFile);
        //    ((XRPictureBox)sender).PerformLayout();
        //}

        private void tableCell61_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String sDesc1 = Detail.Report.GetCurrentColumnValue("StockDescription1").ToString();
            String sDesc2 = Detail.Report.GetCurrentColumnValue("StockDescription2").ToString();
            (sender as XRTableCell).Text = sDesc1 + "\r\n" + sDesc2;
        }
    }
}
