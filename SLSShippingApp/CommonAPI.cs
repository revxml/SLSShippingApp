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
            String[] AddressSplitOnComma = sState.Split(',');
            String[] addressParts = AddressSplitOnComma[1].ToString().TrimStart(' ').Split(' ');
            String pieceState = addressParts[0];

            String[] arrayStates = new string[]
            {       "XX" ,//zero index dummy for Array.IndexOf(value) function
                "AL","AK","AZ","AR","CA","CO","CT","DE","DC","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA",
                "ME","MD","MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR",
                "PA","RI","SC","SD","TN","TX","UT","VT","VA","WA","WV","WI","WY",
                "AB","BC","MB","NB","NL","NT","NS","ON","PE","QC","SK","YT" };

            return Convert.ToInt32(Array.IndexOf(arrayStates, pieceState));
        }

        public String GetCityFromAddress(String sAddress)
        {
            String[] addressParts = sAddress.Split(',');
            return addressParts[0];
        }

        public Int32 GetCountryTranslation(String sCountry)
        {
            String[] arrayCountries = new string[]
                {    //Zero Index for IndexOf function - the ShipRush Index value for US is actually 0, so no need to pad the Array with an empty 0 index
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

        public String GetZipFromAddress(String sCityStateZip)
        {
            String[] addressParts = sCityStateZip.Split(',');
            String stateZip = addressParts[1];
            String[] arrStateZip = stateZip.Split(' ');
            String zip = arrStateZip[arrStateZip.Length - 1];
            return zip;

        }

        public Int32 GetShippingService(String sShipVia)
        {
            Int32 iShipService = 6;
            switch(sShipVia)
            {
                case "F2D":
                    iShipService = 16;
                    break;
                case "FES":
                    iShipService = 17;
                    break;
                case "FDX":
                case "ABF":
                case "LTL":
                    iShipService = -1; //Can't find translation
                    break;
                case "FHD":
                    iShipService = 22;
                    break;
                case "FSO":
                    iShipService = 15;
                    break;
                case "FSP":
                    iShipService = 58;
                    break;
                case "FXG":
                case "FXG: FX Ground":
                    iShipService = 21;
                    break;
                case "PU":
                    iShipService = 64;
                    break;
                case "U2A":
                    iShipService = 3;
                    break;
                case "U3D":
                    iShipService = 5;
                    break;
                case "UFM":
                    iShipService = 43;
                    break;
                case "UNA":
                    iShipService = 0;
                    break;
                case "UPG":
                    iShipService = 6;
                    break;
                case "UPM":
                    iShipService = 44;
                    break;
            }
            return iShipService;
            #region Shiprush Shipping Services Values
            /* Available Values listed in Shiprush API 
                ActiveX Value	UPS	
                0	UPS Next Day Air	
                1	UPS Next Day Air Early	
                2	UPS Next Day Air Saver	
                3	UPS 2nd Day Air	
                4	UPS 2nd Day Air A.M.	
                5	UPS 3 Day Select	
                6	UPS Ground	
                7	UPS Worldwide Express Plus	
                8	UPS Worldwide Express	
                9	UPS Worldwide Express Saver	
                10	UPS Worldwide Expedited	
                11	UPS Economy	
                12	UPS Standard	
                13	FedEx First Overnight	
                14	FedEx Priority Overnight	
                15	FedEx Standard Overnight	
                16	FedEx 2Day	
                17	FedEx Express Saver	
                18	FedEx 1Day Freight	
                19	FedEx 2Day Freight	
                20	FedEx 3Day Freight	
                21	FedEx Ground	
                22	FedEx Home Delivery	
                23	OBSOLETE: (FedEx Extra Hours Discontinued)	
                24	FedEx International First	
                25	FedEx International Priority	
                26	FedEx International Economy	
                27	OBSOLETE: (FedEx Extra Hours Discontinued)	
                28	FedEx International Priority Freight	
                29	FedEx International Economy Freight	
                30	FedEx International Ground	
                31	OBSOLETE: (DHL Next Day 10:30 am)	
                32	OBSOLETE: (DHL Next Day 12:00 pm)	
                33	OBSOLETE: (DHL Next Day 3:00 pm)	
                34	OBSOLETE: (DHL Second Day)	
                35	OBSOLETE: (DHL Ground)	
                36	<no service>	
                37	UPS Expedited	
                38	UPS Express Saver	
                39	UPS Express	
                40	UPS Express Plus	
                41	UPS Standard	
                42	UPS 3 Day Select	
                43	USPS First Class	
                44	USPS Priority	
                45	USPS Media Mail	
                46	USPS Retail Ground	
                47	USPS Express	
                48	OBSOLETE: (USPS BPM - DO NOT USE!)	
                49	USPS Library Mail	
                50	DHL International Express	
                51	OBSOLETE: (DHL @Home Standard)	
                52	OBSOLETE: (DHL @Home Deferred)	
                53	USPS Intl Express	
                54	USPS Intl Priority	
                55	USPS Intl First Class	
                56	Generic Standard	
                57	Generic Express	
                58	FedEx SmartPost	
                59	USPS Parcel Select	
                60	Critical Mail	
                61	Domestic Address Label	
                62	International Address Label	
                63	FedEx SmartPost International	
                64	Local Pickup	
                65	DHL GM Parcel Plus Expedited	
                66	DHL GM Parcel Plus Standard	
                67	DHL GM BPM Expedited	
                68	DHL GM BPM Standard	
                69	DHL GM Catalog BPM Expedited	
                70	DHL GM Catalog BPM Standard	
                71	DHL GM Media Mail Ground	
                72	DHL GM Parcel Expedited	
                73	DHL GM Parcel Ground	
                74	DHL GM Priority Mail	
                75	DHL GM First Class Product	
                76	DHL GM First Class Parcel	
                77	International Priority Airmail	
                78	International Surface Air Lift	
                79	FedEx 2Day A.M.	
                80	FedEx Freight Priority	
                81	FedEx Freight Economy	
                82	UPS SurePost Less than 1 lb	
                83	UPS SurePost 1 lb or Greater
                84	UPS SurePost BPM	
                85	UPS SurePost Media	
                86	UPS First Class Mail	
                87	UPS Priority Mail	
                88	UPS Expedited Mail Innovations	
                89	UPS Priority Mail Innovations	
                90	UPS Economy Mail Innovations
                91	FedEx First Overnight Freight	
                92	DHL Packet Plus Priority	
                93	DHL Packet Priority	
                94	DHL Packet Standard	
                95	DHL Packet IPA	
                96	DHL Packet ISAL	
                97	Consolidator International	
                98	USPSAutoselected	
                99	CollectPlusEconomy	
                100	CollectPlusStandard	
                101	DHLPackage1kg	
                102	DHLPackage2kg	
                103	DHLPackage5kg	
                104	DHLPackage10kg	
                105	DHLParcel2kg	
                106	DHLExpressEasyNational5kg	
                107	DHLExpressEasyNational10kg	
                108	DHLExpressEasyNational20kg	
                109	DHLExpressEasyNational31kg	
                110	DPParcelGross	
                111	DPParcelMaxi	
                112	DPPackageGross	
                113	DPPackageKompakt	
                114	DHLExpressEasyNational1kg	
                115	DHLExpressEasyNational2kg	
                116	Commercial ePacket	
                117	DHLParcel1kg	
                118	DHLExpressEasyNational500g	
                119	DPGross	
                120	DPKompakt	
                121	RoyalMailFirstClass	
                122	RoyalMailSecondClass	
                123	RoyalMailSignedFirstClass	
                124	RoyalMailSignedSecondClass	
                125	RoyalMailGuaranteedBy1PM	
                126	RoyalMailGuaranteedBy9AM	
                127	DYNAMEXSameDay	
                128	DHL GM Parcel Priority	
                129	DHL GM Parcel Standard	
                130	DHL GM Direct Express (DDP)	
                131	DHL GM Direct Express (DDU)	
                132	Asendia PMI	
                133	Asendia PMEI	
                134	Asendia Priority Tracked	
                135	Asendia International Express	
                136	Asendia Other	
                137	DHL GM Parcel Direct (DDP)	
                138	DHL GM Parcel Direct (DDU)	
                139	DHL GM Business Priority	
                140	DHL GM Business Standard
                */
            #endregion
        }

        public Int32 GetShipViaTranslation(String sShipVia)
        {
            #region Macola ShipVia Codes
            /* Macola Ship Via for SLS
             * ship_via_cd
                ABF = ABF Freight ????
             16   F2D = FedEx 2 day
                FDX = FedEx
            17    FES = FX Express Saver
            22    FHD = FX Home Delivery
            15    FSO = FX Std Ovr Night
             58   FSP = FX Smart Post
            21    FXG = Fed Ex Ground
                LTL = Freight
            64    PU = Pick Up
            3    U2A = UPS 2nd Day Air
            5    U3D = UPS 3 Day Select
            43    UFM = USPS First Class
            0    UNA = UPS Next Day Air
             6   UPG = UPS Ground
            44    UPM = USPS Priority Mail
                */
            #endregion

            #region ShipRush Ship Carriers
            /*ShipRush Ship Carrier
             * UPS      0
             * FedEx    1
             * DHL      2 (do not use)
             * USPS     3
             * Endicia  4
             * Stamps   5 */
            #endregion

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
                case "FXG: FX Ground": // = Fex Ex Ground
                    iShipService = 1;
                    break;
                case "UFM":// = USPS First Class"
                case "UPM":// = USPS Priority Mail"
                    iShipService = 3;
                    break;
            }
            return iShipService;
        }

        public String GetCarrierCode(String sShipVia)
        {
            String sCode = "U";

            switch(sShipVia)
            {
                case "UPG":
                case "USP":
                case "U2A":
                case "U3D":
                case "UAS":
                case "UNA":
                case "U2M":
                case "UAM":
                    sCode = "U";
                    break;
                case "FXG":
                case "FXG: FX Ground":
                case "FSP":
                case "FHD":
                case "F2D":
                case "FSO":
                case "FES":
                case "FPO":
                case "F3F":
                case "FFO":
                    sCode = "F";
                    break;
                case "UPM":
                case "UFM":
                case "UMM":
                case "UPP":
                    sCode = "P";
                    break;
            }
            return sCode;
        }

        public String GetCarrierMode(String sShipVia)
        {
            String sMode = "G";

            switch (sShipVia)
            {
                case "USP":
                case "FHD":
                    sMode = "A";
                    break;
                case "UPG":
                    sMode = "G";
                    break;
                case "FXG":
                case "UAM":
                case "FXG: FX Ground":
                    sMode = "B";
                    break;
                case "FSP":
                    sMode = "C";
                    break;
                case "F2D":
                case "U2A":
                    sMode = "2";
                    break;
                case "UPM":
                    sMode = "R";
                    break;
                case "FSO":
                case "UMM":
                    sMode = "S";
                    break;
                case "U3D":
                    sMode = "3";
                    break;
                case "UAS":
                    sMode = "V";
                    break;
                case "UFM":
                case "FFO":
                    sMode = "F";
                    break;
                case "UNA":
                    sMode = "1";
                    break;
                case "FES":
                    sMode = "X";
                    break;
                case "FPO":
                case "UPP":
                    sMode = "P";
                    break;
                case "F3F":
                    sMode = "5";
                    break;
                case "U2M":
                    sMode = "M";
                    break;
            }
            return sMode;
        }
    }
}