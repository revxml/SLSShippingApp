using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;


namespace SLSShippingApp
{
    public partial class CommonAPI
    {
        //Set the application into test mode for thing like image location or Customer profile       
        Boolean cInTestMode;
        String ShippingCon;
        String MacolaCon;
        String ShippingAppCon;
        Int16 conVersion;

        public CommonAPI()
        {

            if (ConfigurationManager.AppSettings["IsTesting"].ToString() == "true")
                InTestMode = true;
            else
                InTestMode = false;

            if (InTestMode)
            {
                //DEBUG/TEST MODE
                ShippingConnection = ConfigurationManager.ConnectionStrings["DevShippingConnection"].ToString();
                MacolaConnection = ConfigurationManager.ConnectionStrings["DevMacolaConnection"].ToString();
            }
            else
            {
                //Production Mode Settings
                ShippingConnection = ConfigurationManager.ConnectionStrings["ShippingConnection"].ToString();
                MacolaConnection = ConfigurationManager.ConnectionStrings["MacolaConnection"].ToString();
            }

            ShippingAppCon = ConfigurationManager.ConnectionStrings["SLSShippingAppConnection"].ToString();

            Version = Convert.ToInt16(ConfigurationManager.AppSettings["AppVersion"].ToString());
        }

        public Boolean InTestMode
        {
            get
            {
                return cInTestMode;
            }
            set
            {
                cInTestMode = value;
            }
        }

        public String ShippingConnection
        {
            get
            {
                return ShippingCon;
            }
            set
            {
                ShippingCon = value;
            }
        }

        public String MacolaConnection
        {
            get
            {
                return MacolaCon;
            }
            set
            {
                MacolaCon = value;
            }
        }

        public String SLSShippingAppConnection
        {
            get
            {
                return ShippingAppCon;
            }
            set
            {
                ShippingAppCon = value;
            }
        }

        public Int16 Version
        {
            get
            {
                return conVersion;
            }
            set
            {
                conVersion = value;
            }
        }

        public Int32 RunUpdateSQL(String sSQL)
        {
            SqlConnection cnn = new SqlConnection(ShippingCon);
            Int32 iRecordsAffected = 0;
            cnn.Open();
            SqlCommand sCmd = new SqlCommand(sSQL, cnn);
            iRecordsAffected = sCmd.ExecuteNonQuery();
            cnn.Close();
            sCmd.Dispose();
            cnn.Dispose();
            return iRecordsAffected;
        }


        public Int32 FindRecord_RS(String SQLWhere, System.Data.DataSet ds)
        {
            System.Data.DataSet dsClone;
            dsClone = ds.Copy();
            Int32 iMatches = 0;

            System.Data.DataRow[] drMatches;
            drMatches = dsClone.Tables[0].Select(SQLWhere);

            if (drMatches.Count() > 0)
                iMatches = drMatches.Count();

            return iMatches;
        }
    }
}
