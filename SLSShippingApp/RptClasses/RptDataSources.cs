using System;
using System.Windows.Forms;
using System.Data;


namespace SLSShippingApp
{

    public partial class RptDataSources :BindingSource
    {

        public DataTable GetReportDataTable(String[] vals, String rptType)
        {
            DataTable dt = new DataTable();

            if (rptType == "Item")
            {


                dt.Columns.Add("OrderNumber", typeof(String));
                dt.Columns.Add("ShippingDate", typeof(DateTime));
                dt.Columns.Add("StockNumber", typeof(String));
                dt.Columns.Add("BayLocation", typeof(String));
                dt.Columns.Add("CustomerNum", typeof(String));
                dt.Columns.Add("ItemDesc1", typeof(String));
                dt.Columns.Add("ItemDesc2", typeof(String));
                dt.Columns.Add("ComponentItemNumber", typeof(String));
                dt.Columns.Add("Image", typeof(String));
                DataRow dr = dt.NewRow();
                dr[0] = vals[1];
                dr[1] = Convert.ToDateTime(vals[7]);
                dr[2] = vals[3];
                dr[3] = vals[2];
                dr[4] = vals[0];
                dr[5] = vals[4];
                dr[6] = vals[5];
                dr[7] = vals[6];
                dr[8] = DBNull.Value;

                dt.Rows.Add(dr);



            }


            return dt;


        }


    }



}
