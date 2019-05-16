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

        public Int32 GetStateTranslation(String sState)
        {
            String[] arrayStates = new string[]
            {       "XX" ,//zero index dummy for Array.IndexOf(value) function
                "AL","AK","AZ","AR","CA","CO","CT","DE","DC","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA",
                "ME","MD","MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR",
                "PA","RI","SC","SD","TN","TX","UT","VT","VA","WA","WV","WI","WY",
                "AB","BC","MB","NB","NL","NT","NS","ON","PE","QC","SK","YT" };

            return Convert.ToInt32(Array.IndexOf(arrayStates, sState));
        }
        public Int32 GetCountryTranslation(String sCountry)
        {
            String[] arrayCountries = new string[]
                {   "XX", //Zero Index for IndexOf function
                    "US","CA","AL","DZ","AS","AD","AO","AI","AG","AR","AM","AW","AU","AT","AZ","BS","BH","BD","BB","BY",
                    "BE","BZ","BJ","BM","BO","BA","BW","BR","VG","BN","BG","BF","BI","KH","CM","CV","KY","CF","TD","JE",
                    "CL","CN","CO","CG","CK","CR","CI","HR","AN","CY","CZ","DK","DJ","DM","DO","EC","EG","SV","GQ","ER",
                    "EE","ET","FO","FJ","FI","FR","GF","PF","GA","GM","GE","DE","GH","GI","GR","GL","GD","GP","GU","GT",
                    "GN","GW","GY","HT","HN","HK","HU","IS","IN","ID","IE","IL","IT","JM","JP","JO","KZ","KE","KI","KR",
                    "KW","KG","LA","LV","LB","LS","LR","LI","LT","LU","MO","MK","MG","MW","MY","MV","ML","MT","MH","MQ",
                    "MR","MU","MX","FM","MD","MC","MN","MS","MA","MZ","NA","NR","NP","NL","NC","NZ","NI","NE","NG","NF",
                    "NP2","NO","OM","PK","PW","PA","PG","PY","PE","PH","PL","PT","PR","QA","RE","RO","RU","RW","SM","SA",
                    "SN","SC","SL","SG","SK","SI","SB","ZA","ES","LK","KN","LC","VC","SR","SZ","SE","CH","SY","TW","TJ",
                    "TZ","TW2","TG","TO","TT","TN","TR","TM","TC","TV","UG","UA","AE","GB","UY","VI","UZ","VU","VE","VN",
                    "WF","EH","YE","03","CD","ZM","ZW","AX","RS","IQ","AF","LY","TP","ME","BT","KM","WS","XB","IC","CU",
                    "XC","FK","IR","KP","MM","NU","MP","ST","SO","SD","XS","BL","XE","XM","TH","00","01","02","VA","GG",
                    "PS","CC","CX","GS","IM","MF","SJ","TK","YT","BQ"};

            switch (sCountry)
            {
                case "U.S.":
                case "US":
                case "U.S.A.":
                case "USA":
                case "US of A":
                case "United States":
                case "United States of America":
                    sCountry = "US";
                    break;
                case "Canada":
                case "CAN":
                case "CA":
                    sCountry = "CA";
                    break;
            }
            return Convert.ToInt32(Array.IndexOf(arrayCountries, sCountry));


        }

        public Int32 GetShipViaTranslation(String sShipVia)
        {
            /* Macola Ship Via for SLS
             * ship_via_cd
                ABF = ABF Freight ????
                F2D = FedEx 2 day
                FDX = FedEx
                FES = FX Express Saver
                FHD = FX Home Delivery
                FSO = FX Std Ovr Night
                FSP = FX Smart Post
                FXG = Fed Ex Ground
                LTL = Freight
                PU = Pick Up
                U2A = UPS 2nd Day Air
                U3D = UPS 3 Day Select
                UFM = USPS First Class
                UNA = UPS Next Day Air
                UPG = UPS Ground
                UPM = USPS Priority Mail
                */

            /*ShipRush Ship Service
             * UPS      0
             * FedEx    1
             * DHL      2 (do not use)
             * USPS     3
             * Endicia  4
             * Stamps   5 */
            Int32 iShipService = 0;

            switch (sShipVia)
            {
                case "U2A":// = UPS 2nd Day Air
                case "U3D":// = UPS 3 Day Select
                case "UNA":// = UPS Next Day Air
                case "UPG":// = UPS Ground
                    iShipService = 0;
                    break;
                case "F2D":// = FedEx 2 day
                case "FDX":// = FedEx
                case "FES":// = FX Express Saver
                case "FHD":// = FX Home Delivery
                case "FSO":// = FX Std Ovr Night
                case "FSP":// = FX Smart Post
                case "FXG":// = Fed Ex Ground
                    iShipService = 1;
                    break;
                case "UFM":// = USPS First Class"
                case "UPM":// = USPS Priority Mail"
                    iShipService = 3;
                    break;

            }

            return iShipService;
        }

    }
}
