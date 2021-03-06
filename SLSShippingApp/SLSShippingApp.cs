﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Management;
using System.Drawing.Printing;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using SLSShippingApp.Reports;
using ZRush_ShipRush;

    /*
     * KEB - 20190620
     * After Installing this app on a local pc, any pc, you have to open a CMD prompt (as administrator), 
     * change directories until you get into this applications installation directory.
     * use "dir" to see the contents of the directory
     * then type "ShipRush-COM-Plus-register.vbs" and hit <Enter>
     * you should get a prompt that says it was succesfully registered as a COM+ component.
     * CONVERSELY:
     * If you need to uninstall the actual ShipRush application, go to Control Panel - Adminsitrative Tools - 
     * COmponent Service - then expand COmponent Services, and COmputers, and My Computer and COM+ Applications.
     * Then right-click on the [ShipRush] object and select <Delete>. You may have to <Stop> the object first
     */

namespace SLSShippingApp
{
    public partial class SLSShippingApp : Form
    {
       // Boolean bVerbose = Convert.ToBoolean(ConfigurationManager.AppSettings["VerboseError"].ToString());

        CommonAPI comAPI;
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        DataSet ds;
        DataTable dt;
        String sSQL = String.Empty;
        SqlDataReader reader;
        String gScanType = "PR";
        Boolean bGotError = false;
        String sSFOItemNo = String.Empty;
        String sSFOItemDesc1 = String.Empty;
        String sSFOItemDesc2 = String.Empty;
        String sSFOUPC = String.Empty;
        String sSFOScheduleNo = String.Empty;
        BindingSource bsQSMaintenance = new BindingSource();
        String tblItemMasterDB = "DATA_09";
        String sScanToOrder = String.Empty;
        String sScanToCustomer = String.Empty;
        Boolean bSingleItemOrders = false;
        //String g_TicketPrinter = ConfigurationManager.AppSettings.Get("PickTicketPrinter").ToString();
        //String g_LabelPrinter = ConfigurationManager.AppSettings.Get("LabelPrinter").ToString();
        //String g_ShipLabelPrinter = ConfigurationManager.AppSettings.Get("ShipLabelPrinter").ToString();
        System.Diagnostics.Stopwatch watch;
        String timeMsg = String.Empty;
        //Boolean doTimeCheck = Convert.ToBoolean( ConfigurationManager.AppSettings.Get("DoTimeCheck").ToString());
        ZFShippingPanel shiprushPanel = new ZFShippingPanel();
        static bool clockwise = false;

        public SLSShippingApp()
        {
            InitializeComponent();
            comAPI = new CommonAPI();

           // if (ConfigurationManager.AppSettings["IsTesting"].ToString() == "true")
            if(comAPI.InTestMode)
            {
                //  iIsTest = 1;
                tblItemMasterDB = "DATA_309";
            }

            try
            {
                this.txtOperator.Text = Environment.UserName.ToString();
                this.txtAdminOperator.Text = Environment.UserName.ToString();
                LoadAvailablePrinters();
                if (!CheckDatabaseExists())
                {
                    GenerateDatabase();
                }

                if (!CheckPrinterConnected(comAPI.TicketPrinter))
                {
                    MessageBox.Show(String.Format("{0} is either not connected, or not a valid printer", comAPI.TicketPrinter), "Invalid Printer");
                    return;
                }
                if (!CheckPrinterConnected(comAPI.LabelPrinter))
                {
                    MessageBox.Show(String.Format("{0} is either not connected, or not a valid printer", comAPI.LabelPrinter), "Invalid Printer");
                    return;
                }
                if (!CheckPrinterConnected(comAPI.ShipLabelPrinter))
                {
                    MessageBox.Show(String.Format("{0} is either not connected, or not a valide printer", comAPI.ShipLabelPrinter), "Invalid Printer");
                    return;
                }
                /* this is for generating customer packing slips, when customer needs to approve them*/
                // DataTable dtPrintInfo = GetPrintInfo();
                // LoadPrintData(dtPrintInfo);
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("Error on load: {0}\r\n{1}",ex.Message,ex.InnerException.ToString()));
            }
        }

        private bool CheckDatabaseExists()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=SLSShippingApp;Integrated Security=True");
            command.CommandType = System.Data.CommandType.Text;
            try
            {
                command.Connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GenerateDatabase()
        {

            List<string> cmds = new List<string>();

            if (File.Exists(Application.StartupPath + "\\SLSShippingAppDB.sql"))
            {
                TextReader tr = new StreamReader(Application.StartupPath + "\\SLSShippingAppDB.sql");
                string line = String.Empty;
                string cmd = String.Empty;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line.Trim().ToUpper() == "60")
                    {
                        cmds.Add(cmd);
                        cmd = String.Empty;
                    }
                    else
                    {
                        cmd += line + "\r\n";
                    }
                }
                if (cmd.Length > 0)
                {
                    cmds.Add(cmd);
                    cmd = String.Empty;
                }
                tr.Close();
            }
            else
            {
                MessageBox.Show(String.Format("Database file not found at {0}",Application.StartupPath + "\\SLSShippingAppDB.sql"),"On Load - Database Creation Failure");
            }
            if (cmds.Count > 0)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=MASTER;Integrated Security=True");
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                for (int i = 0; i < cmds.Count; i++)
                {
                    command.CommandText = cmds[i];
                    command.ExecuteNonQuery();
                }
                command.Connection.Close();
                command.Dispose();
            }
        }

        private DataTable GetPrintInfo()
        {
            DataTable dt = new DataTable();
            comAPI = new CommonAPI();
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = "app_PullPrintInfoOnly";
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataAdapter da = new SqlDataAdapter
            {
                SelectCommand = sqlCmd
            };
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        private void SLSShipping_Load(object sender, EventArgs e)
        {
            comAPI = new CommonAPI();
            GetScannedHistory();
            this.Show();
            Application.DoEvents();
            SetStartScreen();
            //  AddHoverNotes(); //comment once unit testing is completed
        }

        /// <summary>
        /// if there is a control, or are controls, that have not passed unit testing,-
        /// use this to bind the control(s) to the hover event that will display
        /// an explaniation of the issue
        /// </summary>
        private void AddHoverNotes()
        {
            this.btnReprint.MouseHover += new EventHandler(Control_MouseHover);
            this.btnSetPrinters.MouseHover += new EventHandler(Control_MouseHover);
            this.txtShopFloorNo.MouseHover += new EventHandler(Control_MouseHover);
            this.txtUPC.MouseHover += new EventHandler(Control_MouseHover);
        }

        /// <summary>
        ///  Attached this event to any control that is still in testing, or has issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_MouseHover(object sender, EventArgs e)
        {
            String msg = String.Empty;
            Control c = (Control)sender;

            switch (c.Name)
            {
                case "btnReprint":
                    msg = "Continued error 'Error retreiving print info for #### - not functional";
                    break;
                case "btnSetPrinters":
                    msg = "Printers can not be set while in debug mode, only when app is installed";
                    break;
                case "btnSFOBackoutScan":
                case "btnSFOScanComplete":
                    msg = "DATA_02 and DATA_309 on MMMDEVMACSQL01 are out of sync, can't test in dev";
                    break;
                case "txtShopFloorNo":
                case "txtUPC":
                    msg = "This control has not been tested yet";
                    break;
            }
            MessageBox.Show(msg, "Development Notes");
        }

        #region SCAN ITEM TABS
        private void RbScanType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked == false)
                return;

            ToggleFormControlEvents(false);
            switch (rb.Name)
            {
                case "rbPrinted":
                    lblScanTypeHeader.Text = "Scan Printed Item";
                    pnlScanTitle.BackColor = Color.FromArgb(185, 240, 244);
                    pnlScanTitle.ForeColor = Color.Black;
                    txtShopFloorNo.Enabled = true;
                    txtUPC.Enabled = false;
                    txtScanQty.Text = "1";
                    txtScanQty.Enabled = false;
                    gScanType = "PR";
                    txtShopFloorNo.Focus();
                    grpScanMode.Visible = false;
                    break;
                case "rbVinyl":
                    lblScanTypeHeader.Text = "Scan Vinyl Item";
                    pnlScanTitle.BackColor = Color.FromArgb(146, 197, 212);
                    pnlScanTitle.ForeColor = Color.Black;
                    txtShopFloorNo.Enabled = false;
                    txtUPC.Enabled = true;
                    txtScanQty.Text = "1";
                    txtScanQty.Enabled = true;
                    gScanType = "VN";
                    txtUPC.Focus();
                    grpScanMode.Visible = true;
                    break;
                case "rbBay0":
                    lblScanTypeHeader.Text = "Scan Bay0 Item";
                    pnlScanTitle.BackColor = Color.FromArgb(105, 166, 204);
                    pnlScanTitle.ForeColor = Color.White;
                    txtShopFloorNo.Enabled = false;
                    txtUPC.Enabled = true;
                    txtScanQty.Text = "1";
                    txtScanQty.Enabled = true;
                    gScanType = "B0";
                    txtUPC.Focus();
                    grpScanMode.Visible = true;
                    break;
                case "rbQuickShip":
                    lblScanTypeHeader.Text = "Scan Quick Ship Item";
                    pnlScanTitle.BackColor = Color.FromArgb(55, 114, 180);
                    pnlScanTitle.ForeColor = Color.White;
                    txtShopFloorNo.Enabled = false;
                    txtUPC.Enabled = true;
                    txtScanQty.Text = "1";
                    txtScanQty.Enabled = true;
                    gScanType = "QS";
                    txtUPC.Focus();
                    grpScanMode.Visible = true;
                    break;
                case "rbNonMM":
                    lblScanTypeHeader.Text = "Scan Non-Millennium Item";
                    pnlScanTitle.BackColor = Color.FromArgb(36, 67, 152);
                    pnlScanTitle.ForeColor = Color.White;
                    txtShopFloorNo.Enabled = false;
                    txtUPC.Enabled = true;
                    txtScanQty.Text = "1";
                    txtScanQty.Enabled = true;
                    gScanType = "NM";
                    txtUPC.Focus();
                    grpScanMode.Visible = true;
                    break;
            }
            ToggleFormControlEvents(true);
           // String fullPathToFile = ConfigurationManager.AppSettings["ImagePath"].ToString() + "! Image Not Found !.jpg";
           String fullPathToFile = comAPI.ImagePath + "! Image Not Found !.jpg";
            pbItemImage.Load(fullPathToFile);
        }

        private void ToggleFormControlEvents(Boolean bTurnOn)
        {
            if (bTurnOn)
            {
                txtShopFloorNo.TextChanged += new EventHandler(TxtShopFloorNo_Changed);
                txtUPC.TextChanged += new EventHandler(TxtUPC_Change);
                txtUPC.LostFocus += new EventHandler(TxtUPC_LostFocus);
                txtShopFloorNo.Text = String.Empty;
                txtUPC.Text = String.Empty;
            }
            else
            {
                txtShopFloorNo.TextChanged -= new EventHandler(TxtShopFloorNo_Changed);
                txtUPC.TextChanged -= new EventHandler(TxtUPC_Change);
                txtUPC.LostFocus -= new EventHandler(TxtUPC_LostFocus);
            }
        }

        //based on the window login/user name, select the default scan type
        private void SetStartScreen()
        {
            RadioButton rb = rbPrinted;//If the SQL query fails the readio button to select can't be chosed, so set this to any of the radio buttons here
            String sLogin = String.Empty;
            String sScanScreen = String.Empty;
            String sInputType = String.Empty;

            try
            {
                sLogin = Environment.UserName.ToString().ToUpper();
                NameValueCollection nvcc = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/WindowsUsers");
                sScanScreen = nvcc[Environment.UserName].ToString();

                sInputType = sScanScreen == "PRINTED" ? "SFO" : "UPC";

                switch (sScanScreen)
                {
                    case "BAY0":
                        gScanType = "B0";
                        rb = rbBay0;
                        rbBay0.Checked = true;
                        grpScanMode.Visible = false;
                        break;
                    case "PRINTED":
                        gScanType = "PR";
                        rb = rbPrinted;
                        rbPrinted.Checked = true;
                        btnScanToOrder.Visible = false;
                        grpScanMode.Visible = false;
                        break;
                    case "QS":
                        gScanType = "QS";
                        rb = rbQuickShip;
                        grpScanMode.Visible = false;
                        break;
                    case "VINYL":
                        gScanType = "VN";
                        rb = rbVinyl;
                        rbVinyl.Checked = true;
                        grpScanMode.Visible = true;
                        break;
                }

                txtShopFloorNo.Enabled = sInputType == "UPC" ? false : true;
                txtUPC.Enabled = sInputType == "SFO" ? false : true;
                if (sInputType == "UPC")
                    txtUPC.Focus();
                else
                    txtShopFloorNo.Focus();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 259, "SetStartScreen()");
                else
                    MessageBox.Show(String.Format("Error retrieving your Windows Login SLSShipping App start screen:\n\t{0}", ex.Message));
                return;
            }
            rb.Checked = true;
            RbScanType_CheckedChanged(rb, null);
        }

        private void GetFMOrderFromLabelScan()
        {
            if (comAPI == null) comAPI = new CommonAPI();

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sSQL = String.Empty;
            SqlDataAdapter adapter;

            DataTable dtOrderInfo;
            DataTable dtPrintInfo;
            DataTable dtScanInfo;
            ds = new DataSet("OpenOrders");

            String sItemType = String.Empty;
            String sScannedValue = String.Empty;
            Int32 iScannedQty = 0;
            String sUserName = String.Empty;
            bGotError = false;

            //Check to ensure operator entered a value in the Operator Name
            if (txtOperator.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter your Name/ID into Operator Name", "Missing Operator Name");
                bGotError = true;
                txtOperator.Focus();
                return;
            }

            String sScanToOrderNumber = String.Empty;
            if (txtScanToOrder.Visible == true && txtScanToOrder.Text != String.Empty)
                sScanToOrderNumber = txtScanToOrder.Text;

            sScannedValue = txtUPC.Text.Trim();
            txtScanQty.Enabled = true;
            iScannedQty = Convert.ToInt32(txtScanQty.Text.Trim());
            sUserName = txtOperator.Text.Trim();
            if (rbPrinted.Checked)
                txtScanQty.Enabled = false;

            //Run stored procedure to get information from scanned value
            sSQL = "app_spFMShippingScan";

            adapter = new SqlDataAdapter(sSQL, sqlCon);
            adapter.FillError += new FillErrorEventHandler(FillError);

            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.CommandText = sSQL;
            adapter.SelectCommand.Parameters.AddWithValue("@InputValue", sScannedValue);
            adapter.SelectCommand.Parameters.AddWithValue("@ScanQuantity", iScannedQty);
            adapter.SelectCommand.Parameters.AddWithValue("@ScanPageType", gScanType);
            adapter.SelectCommand.Parameters.AddWithValue("@OperatorName", txtOperator.Text.Trim());
            adapter.SelectCommand.Parameters.AddWithValue("@EnvironUser", Environment.UserName);
            adapter.SelectCommand.Parameters.AddWithValue("@IsTest", 0); //Use 0 (false) when testing against QA or DEV
            adapter.SelectCommand.Parameters.AddWithValue("@ScanToOrder", sScanToOrderNumber);
            adapter.SelectCommand.Parameters.AddWithValue("@ScanToCustomer", sScanToCustomer);
            adapter.SelectCommand.Parameters.AddWithValue("@Single", bSingleItemOrders);

            try
            {
                sqlCon.Open();
                adapter.Fill(ds, "OpenOrders");

                dtScanInfo = ds.Tables[0];
                dtOrderInfo = ds.Tables[1];
                dtPrintInfo = ds.Tables[2];

                if (dtOrderInfo.Rows[0]["OrderNumber"] == DBNull.Value && dtOrderInfo.Rows[0]["ItemNumber"].ToString() == "Invalid Scan")
                {
                    dgvList.DataSource = dtScanInfo;
                    txtUPC.Focus();
                    return;
                }

                if (dtOrderInfo.Rows[0]["OrderNumber"] == null)
                {
                    txtItemNumber.Text = dtOrderInfo.Rows[0]["OrderStatus"] == null ? "Unknown - Contact IT" : dtOrderInfo.Rows[0]["OrderStatus"].ToString();
                    txtItemDesc1.Text = String.Empty;
                    txtItemDesc2.Text = String.Empty;
                   // String fullPathToFile = ConfigurationManager.AppSettings["ImagePath"].ToString() + "! Image Not Found !.jpg";
                   String fullPathToFile = comAPI.ImagePath + "! Image Not Found !.jpg";
                    pbItemImage.Load(fullPathToFile);
                    dgvList.DataSource = dtScanInfo;
                }
                else
                {
                    txtItemNumber.Text = dtOrderInfo.Rows[0]["ItemNumber"].ToString();
                    txtItemDesc1.Text = dtOrderInfo.Rows[0]["ItemDesc1"].ToString();
                    txtItemDesc2.Text = dtOrderInfo.Rows[0]["ItemDesc2"].ToString();
                    LoadImage(dtOrderInfo.Rows[0]["ItemNumber"].ToString().Trim());
                }

                if (dtPrintInfo != null)
                    LoadPrintData(dtPrintInfo, false);

                if (dtScanInfo != null)
                    dgvList.DataSource = dtScanInfo;

                txtUPC.Focus();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 373, "GetFMOrderFromLabelScan");
                else
                    MessageBox.Show("Error getting Order Information: " + ex.Message);
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                ds.Dispose();
                adapter.Dispose();
            }
        }

        protected static void FillError(object sender, FillErrorEventArgs args)
        {
            //https://msdn.microsoft.com/en-us/library/6d1wk41s(v=vs.100).aspx
            //          if (args.Errors.GetType() == typeof(System.OverflowException))
            //{
            //  // Code to handle precision loss.
            //  //Add a row to table using the values from the first two columns.
            //  DataRow myRow = args.DataTable.Rows.Add(new object[]
            //     {args.Values[0], args.Values[1], DBNull.Value});
            //  //Set the RowError containing the value for the third column.
            //  args.RowError = 
            //     "OverflowException Encountered. Value from data source: " +
            //     args.Values[2];
            //  args.Continue = true;
            args.Continue = true;
        }

        private void TxtUPC_LostFocus(object sender, EventArgs e)
        {
            String sSFOrdNum = String.Empty;
            if (txtUPC.Text.Trim() == String.Empty || txtUPC.Text.Trim().Length == 12)
                return;

            if (Int32.TryParse(txtUPC.Text.Trim(), out int outVal) == false)
            {
                MessageBox.Show("Scanned value must be numeric. Please validate value scanned: " + txtUPC.Text.Trim());
                return;
            }

            if (gScanType == "PR" || gScanType == "VN")
            {
                GetFMOrderFromLabelScan();

                if (bGotError == false)
                {
                    txtUPC.Focus();
                    txtUPC.Text = String.Empty;
                }
                txtScanQty.Enabled = false;
            }
        }

        private void TxtShopFloorNo_Changed(object sender, EventArgs e)
        {
            int iLength = 0;
            String sScannedValue = txtShopFloorNo.Text.Trim();

            iLength = sScannedValue.Length;
            if (iLength == 8)
            {
                GetScheduleAndItemsFromSFO();
            }
        }

        private void TxtUPC_Change(object sender, EventArgs e)
        {
            Int32 iLength = 0;
            Int32 iDesiredLength = 0;

            iDesiredLength = gScanType == "PR" ? 8 : 12;

            iLength = txtUPC.Text.Trim().Length;

            //This is like an auto enter on receipt of 12 characters
            if (iLength == iDesiredLength)
            {
                GetFMOrderFromLabelScan();
                txtUPC.Text = String.Empty;
                txtUPC.Focus();
            }
        }

        public void LoadImage(String sItemNum)
        {
            // String sImagePath = ConfigurationManager.AppSettings["ImagePath"].ToString();
            String sImagePath = comAPI.ImagePath;
            String sItemNumber = String.Empty;
            String sFullImagePath = sImagePath;
            String sFormattedItemNum = sItemNum + ".jpg";
            sFullImagePath = sFullImagePath + sFormattedItemNum;

            if (!System.IO.File.Exists(sFullImagePath))
                sFullImagePath = sFullImagePath.Replace(sItemNum, "! Image Not Found !");

            pbItemImage.Load(sFullImagePath);
        }

        public void GetScannedHistory()
        {
            if (comAPI == null) comAPI = new CommonAPI();

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand
            {
                Connection = sqlCon
            };
            sSQL = String.Empty;
            //SqlDataReader reader;
            dt = new DataTable();

            sqlCon.Open();

            sSQL = String.Format("SELECT TOP 100 ScanID, FMStockNumber as [Item Num],ScannedDate as [Scanned Date],WindowsLogin as [Login]," +
                    "OperatorName as [Operator Name],OrderNumber as [Order Num],BayLocation as [Bay],StockFlag as [Type]" +
                    " FROM tblLogPickingScan ORDER BY CASE WHEN OperatorName = '{0}' THEN 1 ELSE 2 END, ScannedDate DESC", txtOperator.Text.Trim());
            sqlCmd.CommandText = sSQL;
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);
                dgvList.DataSource = null;
                dgvList.AutoGenerateColumns = false;
                dgvList.DataSource = dt;
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 513, "GetScannedHistory");
                else
                    MessageBox.Show("Error Loading Scanned History: " + ex.Message);
            }
            finally
            {
                sqlCon.Close();
                sqlCmd.Dispose();
                sqlCon.Dispose();
            }
        }

        public void GetItemInfo(String sItemNum)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand
            {
                Connection = sqlCon
            };
            sSQL = String.Empty;
            //SqlDataReader reader;

            sqlCon.Open();

            sSQL = String.Format("SELECT item_no As ItemNum,item_desc_1 As ItemDesc1,CASE WHEN item_desc_2 LIKE '%HOUSDIV%' THEN search_desc ELSE item_desc_2 END as ItemDesc2" +
                                " FROM FROM {0}.dbo.IMITMIDX_SQL WHERE item_no LIKE '%{1}%' ", tblItemMasterDB, sItemNum);
            sqlCmd.CommandText = sSQL;
            sqlCmd.CommandType = CommandType.Text;

            try
            {
                reader = sqlCmd.ExecuteReader();
                txtItemNumber.Text = reader["ItemNum"].ToString();
                txtItemDesc1.Text = reader["ItemDesc1"].ToString();
                txtItemDesc2.Text = reader["ItemDesc2"].ToString();
                LoadImage(sItemNum);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 551, "GetItemInfo");
                else
                    MessageBox.Show("Error getting Item Details: " + ex.Message);
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }

        private void BtnRefreshScanHistory_Click(Object sender, EventArgs e)
        {
            GetScannedHistory();
        }
        #endregion

        #region PICK TICKET BY PO NUMBER

        private void TxtPONumber_Leave(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand
            {
                Connection = sqlCon
            };
            sSQL = "app_spPickingTicketByPO";
            // SqlDataReader reader;
            DataTable dtOrders = new DataTable();

            sqlCon.Open();

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sSQL;
            sqlCmd.Parameters.AddWithValue("@PONumber", txtPONumber.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@OperatorName", Environment.UserName.ToString());

            try
            {
                reader = sqlCmd.ExecuteReader();
                dtOrders.Load(reader);

                if (dtOrders.Rows.Count > 0)
                {
                    LoadPrintData(dtOrders, false);
                }
                else
                {
                    MessageBox.Show(String.Format("No Order Data was returned for the supplied PO Number: {0}", txtPONumber.Text.Trim()), "Invalid PO");
                    this.txtPONumber.Leave -= new EventHandler(TxtPONumber_Leave);
                    txtPONumber.Text = String.Empty;
                    txtPONumber.Focus();
                    this.txtPONumber.Leave += new EventHandler(TxtPONumber_Leave);
                }
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 608, "txtPONumber_Leave");
                else
                    MessageBox.Show(String.Format("Error retrieving Pick Ticket from PO Number: {0}\n\t{1}", txtPONumber.Text.Trim(), ex.Message));
                return;
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }

        #endregion
        #region PRINTING REPORTS

        private void LoadPrintData(DataTable dtPrintInfo, Boolean isReprint = false)
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
            if (comAPI.doTimeCheck)
                timeMsg += "LoadPrintData started\r\n";

            ClearSQLExpressTables(null, null, null);

            if (comAPI.doTimeCheck)
                timeMsg += String.Format("ClearSqlExpressTables completed: {0} ms\r\n", watch.ElapsedMilliseconds);
            
            Boolean bBayLabel = false;
            Boolean bItemLabel = false;
            Boolean bPackingLabel = false;
            Boolean bPickingTicket = false;
            Boolean bPackingTicket = false;
            Boolean bRunSql = true;
            String sPackingTicket = String.Empty;
            String sCustomerNumber = String.Empty;
            Int32 iCustomerNumber = 0;
            String sTargetColumnName = String.Empty;

            sSQL = String.Empty;

            if (dtPrintInfo.Rows.Count == 0)
                return;

            String sPrintInfo = dtPrintInfo.Rows[0]["SQLString"].ToString();

            if (dtPrintInfo == null)
            {
                MessageBox.Show("No Print Information found.", "No Data");
                return;
            }

            foreach (DataRow row in dtPrintInfo.Rows)
            {
                sqlCon = new SqlConnection(comAPI.SLSShippingAppConnection);
                sqlCmd = new SqlCommand
                {
                    Connection = sqlCon,
                    CommandType = CommandType.Text
                };
                sqlCon.Open();

                String sRptCustomer = row["Customer"].ToString().Length > 0 ? row["Customer"].ToString().Trim() : null;
                sCustomerNumber = row["CustomerNumber"].ToString().Length > 0 ? row["CustomerNumber"].ToString().Trim() : null;
                String sOrderNumber = row["OrderNumber"].ToString().Trim();
                String sTargetTableName = String.Empty;
                Boolean bSingleItem = row["SingleItemOrder"].ToString().Length > 0 ? Convert.ToBoolean(row["SingleItemOrder"].ToString().Trim()): false;

                if (sRptCustomer != null && sCustomerNumber != null)
                    iCustomerNumber = Convert.ToInt32(sCustomerNumber);

                switch (row["PrintType"].ToString())
                {
                    case "BayNItemLabel":
                        bBayLabel = true;
                        bRunSql = true;
                        sTargetColumnName = "OrderNumber";
                        sTargetTableName = "tblLabel";
                        break;
                    case "ItemLabel":
                        bItemLabel = true;
                        bRunSql = true;
                        sTargetColumnName = "OrderNumber";
                        sTargetTableName = "tblLabel";
                        break;
                    case "PackingLabel":
                        bPackingLabel = true;
                        bRunSql = true;
                        sTargetColumnName = "OrderOrSONumber";
                        sTargetTableName = "tblTickets";
                        break;
                    case "PickingTicket":
                        bPickingTicket = true;
                        bRunSql = true;
                        sTargetColumnName = "OrderOrSONumber";
                        sTargetTableName = "tblPickingFile";
                        break;
                    case "RetailerPackingLabel":
                        bPackingLabel = true;
                        bRunSql = true;
                        sTargetColumnName = "OrderOrSONumber";
                        sTargetTableName = "tblRetailerPackingLabel";
                        break;
                    case "CustomerPackingTicket":
                        bRunSql = false;
                        bPackingTicket = true;
                        sPackingTicket = row["SQLString"].ToString();
                        break;
                }
                if (bRunSql && row["SQLString"].ToString().Length != 0)
                {
                    try
                    {
                        sSQL = row["SQLString"].ToString();
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandText = sSQL;
                        sqlCmd = new SqlCommand(sSQL, sqlCon);
                        sqlCmd.ExecuteNonQuery();
                        if (comAPI.doTimeCheck)
                            timeMsg += String.Format("Sqlcmd.ExecuteNonQuery completed: {0} ms\r\n", watch.ElapsedMilliseconds);
                    }
                    catch (SqlException sEx)
                    {
                        MessageBox.Show("SQL Error:" + sEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex.Message);
                    }
                }

                if (bBayLabel)
                {
                    PrintDevExpReport("Label", "rptPrintBayLabel", 1, sOrderNumber);
                    ClearSQLExpressTables("tblLabel", sOrderNumber, sTargetColumnName);
                }
                bBayLabel = false;

                if (bItemLabel)
                {
                    PrintDevExpReport("Label", "rptPrintLabel", 1, sOrderNumber);
                    ClearSQLExpressTables("tblLabel", sOrderNumber, sTargetColumnName);
                    if (comAPI.doTimeCheck)
                        timeMsg += String.Format("PrintDevExpReport and ClearSqlExpressTables(tblLabel) completed: {0} ms\r\n", watch.ElapsedMilliseconds);
                }
                bItemLabel = false;

                if (bPackingLabel)
                {
                    if (sRptCustomer != null)
                    {
                        PrintDevExpReport("tblRetailerPackingLabel", "rpt" + sRptCustomer + "PackingLabels", 1, sOrderNumber);
                        ClearSQLExpressTables("tblRetailerPackingLabel", sOrderNumber, sTargetColumnName);
                    }
                    else
                    {
                        PrintDevExpReport("Label", "rptPackingLabels", 1, sOrderNumber);
                        ClearSQLExpressTables("tblTickets", sOrderNumber, sTargetColumnName);
                    }
                }
                bPackingLabel = false;

                if (bPickingTicket)
                {
                    PrintDevExpReport("Ticket", "rptPickingTicket", 1, sOrderNumber);

                    if (comAPI.doTimeCheck)
                        timeMsg += String.Format("PrintDevExpReport and ClearSqlExpressTables(rptPickingTicket) completed: {0} ms\r\n", watch.ElapsedMilliseconds);

                    // if(!isReprint) //ADD THIS FOR PRODUCTION. IN DEV, I'M TESTING USING THE REPRINT FUNCTIONALITY
                    //BUT YOU'D NEVER WANT TO RE-SHIP EVERY TIME A USER DOES A REPRINT!!!!!!
                    if (isReprint == false && cbScanAndShip.Checked == true && bSingleItem == true)
                    {
                        ShipOrder(sOrderNumber);
                    }
                    ClearSQLExpressTables("tblTickets", sOrderNumber, sTargetColumnName);
                  
                }
                bPickingTicket = false;

                if (bPackingTicket)
                    PrintDoc(sPackingTicket, sOrderNumber);
                bPackingTicket = false;

                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();

                if (comAPI.doTimeCheck)
                    timeMsg += String.Format("Report printing completed: {0} ms \r\n", watch.ElapsedMilliseconds);
            }
            watch.Stop();
            if (comAPI.doTimeCheck)
            {
                MessageBox.Show(timeMsg);
                timeMsg = String.Empty;
            }
        }

        private void PrintDoc(String sFileName, String sOrdNo)
        {
            String c_sFolder = comAPI.PackingSlipPath;// ConfigurationManager.AppSettings["CustomerPackingSlips"].ToString();
            StreamReader streamToPrint;
            String sFullPath = c_sFolder + sFileName;

            if (System.IO.File.Exists(sFullPath) == true)
            {
                // PrintReport("Ticket", c_sFolder + sFileName, sOrdNo);                
                //ReportPrinter.ReportPrintDocument printDoc = new ReportPrinter.ReportPrintDocument(c_sFolder + sFileName);
                PrintController printController = new StandardPrintController();
                streamToPrint = new StreamReader(sFullPath);
                PrintDocument pd = new PrintDocument();

                pd.PrintController = printController;
                pd.PrinterSettings.PrinterName = comAPI.TicketPrinter;               
                pd.PrinterSettings.Copies = 1;
                //  pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                pd.Print();
                //  pd.PrintPage -= new PrintPageEventHandler(pd_PrintPage);
                //printDoc.PrintController = printController;

                using (Ghostscript.NET.Processor.GhostscriptProcessor processor = new Ghostscript.NET.Processor.GhostscriptProcessor())
                {
                    List<string> switches = new List<string>();
                    switches.Add("-empty");
                    switches.Add("-dPrinted");
                    switches.Add("-dBATCH");
                    switches.Add("-dNOPAUSE");
                    switches.Add("-dNOSAFER");
                    switches.Add("-dNumCopies=1");
                    switches.Add("-sDEVICE=mswinpr2");
                    switches.Add("-sOutputFile=%printer%" + comAPI.TicketPrinter);
                    switches.Add("-f");
                    switches.Add(sFullPath);
                    processor.StartProcessing(switches.ToArray(), null);
                }
            }
            else
                MessageBox.Show("Customer Packing Ticket expected for customer but not found. \nContact management and/or SLS Customer Service regarding missing Packing Ticket.", "Missing Customer Packing Slip");
        }

        // The PrintPage event is raised for each page to be printed.
        //private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        //{
        //    float linesPerPage = 0;
        //    float yPos = 0;
        //    int count = 0;
        //    float leftMargin = ev.MarginBounds.Left;
        //    float topMargin = ev.MarginBounds.Top;
        //    String line = null;

        //    // Calculate the number of lines per page.
        //    linesPerPage = ev.MarginBounds.Height /
        //       printFont.GetHeight(ev.Graphics);

        //    // Iterate over the file, printing each line.
        //    while (count < linesPerPage &&
        //       ((line = streamToPrint.ReadLine()) != null))
        //    {
        //        yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
        //        ev.Graphics.DrawString(line, printFont, Brushes.Black,
        //           leftMargin, yPos, new StringFormat());
        //        count++;
        //    }

        //    // If more lines exist, print another page.
        //    if (line != null)
        //        ev.HasMorePages = true;
        //    else
        //        ev.HasMorePages = false;
        //}

        private void PrintDevExpReport(String sRptType, String sReport, short iCopies = 1, String sOrderNo = null)
        {
            XtraReport report = new XtraReport();
            switch (sReport)
            {
                case "rptPrintLabel":
                    report = new rptPrintLabel();
                    report.FilterString = String.Format("[OrderNumber] = '{0}'", sOrderNo);
                    break;
                case "rptBayLabel":
                    report = new rptBayLabel();
                    report.FilterString = String.Format("[OrderNumber] = '{0}'", sOrderNo);
                    break;
                case "rptPackingLabels":
                    report = new rptPackingLabels();
                    report.FilterString = String.Format("[OrderOrSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptPickingTicket":
                    report = new rptPickingTicket();
                    report.FilterString = String.Format("[OrderorSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptFanBrandsLabel":
                    report = new rptFanBrandsLabel();
                    break;
                case "rptQuickShipLabel":
                    report = new rptQuickShipLabel();
                    break;
                case "rptHSNPackingLabels":
                    report = new rptHSNPackingLabels();
                    report.FilterString = String.Format("[OrderorSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptWalmartPackingLabels":
                    report = new rptWalmartPackingLabels();
                    report.FilterString = String.Format("[OrderorSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptTargetPackingLabels":
                    report = new rptTargetPackingLabels();
                    report.FilterString = String.Format("[OrderorSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptAdvanceAutoPartsPackingLabels":
                    report = new rptAdvanceAutoPartsPackingLabels();
                    report.FilterString = String.Format("[OrderorSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptOfficeDepotPackingLabels":
                    report = new rptOfficeDepotPackingLabels();
                    report.FilterString = String.Format("[OrderorSONumber] = '{0}'", sOrderNo);
                    break;
                case "rptFanaticsPackingLabels":
                    report = new rptFanaticsPackingLabels();
                    break;
            }
            PublishReport(report, sRptType);
        }

        private void BindToData(String sRptType, String sReport, XtraReport report)
        {
            DevExpress.DataAccess.Sql.SqlDataSource ds = new SqlDataSource("SLSShippingAppConnection");
            DevExpress.DataAccess.Sql.CustomSqlQuery customQuery = new CustomSqlQuery
            {
                Name = "ReportDataQuery"
            };
            String sqlQuery = "SELECT * FROM ";
            String sqlTable = String.Empty;

            if (sReport.Contains("Retailer"))
            {
                sqlTable = "tblRetailerPackingLabel";
            }
            else if (sReport.Contains("Bay"))
            {
                sqlTable = "tblLabel";
            }
            else if (sReport.Contains("PrintLabel"))
            {
                sqlTable = "tblLabel";
            }
            else if (sReport.Contains("FanBrands"))
            {
                sqlTable = "tblLabelFanBrands";
            }
            else if (sReport.Contains("Packing"))
            {
                sqlTable = "tblTickets";
            }
            else if (sReport.Contains("Picking"))
            {
                sqlTable = "tblPickingFile";
            }
            else if (sReport.Contains("QuickShipLabel"))
            {
                sqlTable = "tblLabelQuickShip";
            }
            customQuery.Sql = String.Format("{0}{1}", sqlQuery, sqlTable);
            ds.Queries.Add(customQuery);
            report.DataSource = ds;
            report.DataMember = "ReportDataQuery";
        }

        private void CreateDetailReport(XtraReport report, String dataMember)
        {
            //create a detail report band and bind it to data
            DetailReportBand detailReportBand = new DetailReportBand();
            report.Bands.Add(detailReportBand);

            //add a header to the detail report
            ReportHeaderBand detailReportHeader = new ReportHeaderBand
            {
                HeightF = 0
            };
            detailReportBand.Bands.Add(detailReportHeader);

            DetailBand detailBand = new DetailBand();
            detailReportBand.Bands.Add(detailBand);

            detailReportBand.DataSource = report.DataSource;
            detailReportBand.DataMember = dataMember;
        }

        private void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrintPage += PrintDocument_PrintPage;
            e.PrintDocument.QueryPageSettings += PrintDocument_QueryPageSettings;
        }

        static void PrintDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            var s = e.PageSettings.PaperSize;
            e.PageSettings.PaperSize = new PaperSize("Custom", s.Height, s.Width);
        }

        static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle pageBounds = e.PageBounds;
            pageBounds = GraphicsUnitConverter.Convert(pageBounds, GraphicsDpi.HundredthsOfAnInch, GraphicsDpi.Document);
            g.PageUnit = GraphicsUnit.Document;

            if (clockwise)
            {
                g.TranslateTransform(pageBounds.Width, 0);
                g.RotateTransform(90);
            }
            else
            {
                g.TranslateTransform(0, pageBounds.Height);
                g.RotateTransform(-90);
            }
        }

        private void PublishReport(XtraReport report, String sRptType)
        {
            if (comAPI.doTimeCheck)
                timeMsg += String.Format("Entered PublishReport {0}: {1}\r\n", report.Name, watch.ElapsedMilliseconds);
            String sPrinterName = sRptType == "Label" ? comAPI.LabelPrinter : comAPI.TicketPrinter;

            if (comAPI.doTimeCheck)
                timeMsg += String.Format("All Printer info set: {0}\r\n", watch.ElapsedMilliseconds);

            try
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    if (sRptType == "Label")
                        printTool.PrintingSystem.StartPrint += PrintingSystem_StartPrint;

                    //This is for testing purposes only
                    // printTool.ShowPreviewDialog();
                    printTool.PrinterSettings.PrinterName = sPrinterName;
                    printTool.Print();

                    if (sRptType == "Label")
                        printTool.PrintingSystem.StartPrint -= PrintingSystem_StartPrint;

                    if (comAPI.doTimeCheck)
                        timeMsg += String.Format("Printing Completed: {0}\r\n", watch.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error printing to {0}: {1}", sPrinterName, ex.Message));
            }
            finally
            {
                report.DataSource = null;
            }
        }
        #endregion

        #region PRINTER ASSIGNMENT

        private void LoadAvailablePrinters()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                this.cboLabelPrinter.Items.Add(printer);
                this.cboTicketPrinter.Items.Add(printer);
                this.cboShippingLabelPrinter.Items.Add(printer);
            }
        }

        public void SetPrinter(String sRptType, String sReport)
        {
            String sPrinter = String.Empty;
            Boolean bPrinterOK = false;
            PrinterSettings settings = new PrinterSettings();
            sPrinter = settings.PrinterName;
            bPrinterOK = false;

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer == sPrinter)
                    bPrinterOK = true;
            }

            if (!bPrinterOK)
            {
                switch (sRptType)
                {
                    case "Label":
                        sPrinter = comAPI.LabelPrinter;// sLabelPrinter;
                        break;
                    case "Ticket":
                        sPrinter = comAPI.TicketPrinter;// sTicketPrinter;
                        break;
                }
            }
        }

        public void BtnAssignPrinter_Click(object sender, EventArgs e)
        {
            String sReport = String.Empty;
            String sLabelPrinter = String.Empty;
            String sTicketPrinter = String.Empty;
            String sShippingLabelPrinter = String.Empty;

            cboLabelPrinter.Focus();
            sLabelPrinter = cboLabelPrinter.Text.Trim();
            cboTicketPrinter.Focus();
            sTicketPrinter = cboTicketPrinter.Text.Trim();
            cboShippingLabelPrinter.Focus();
            sShippingLabelPrinter = cboShippingLabelPrinter.Text.Trim();

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = config.AppSettings.Settings;

                // update LabelPrinter
                settings["LabelPrinter"].Value = sLabelPrinter;
                // update PickTicketPrinter
                settings["PickTicketPrinter"].Value = sTicketPrinter;
                //update ShippingLabelPrinter
                settings["ShipLabelPrinter"].Value = sShippingLabelPrinter;

                //save the file
                config.Save(ConfigurationSaveMode.Modified);
                //relaod the section you modified
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 974, "btnAssignPrinter_Click");
                else
                    MessageBox.Show(String.Format("Error Assigning Printers:\n\t{0}", ex.Message));
                return;
            }
            finally
            {
                comAPI = new CommonAPI();
            }
            MessageBox.Show(String.Format("Printer Assingment Complete.\n\tLabel Printer:\t{0}\n\tPickTicket Printer:\t{1}", sLabelPrinter, sTicketPrinter), "Printer Assignment");
        }

        #endregion

        #region CLOSE HOLDING BAY

        public void BtnCloseBay_Click(Object sender, EventArgs e)
        {
            if (Int32.TryParse(txtOrderNumber.Text.Trim(), out int outVal) == false)
            {
                MessageBox.Show("Order Number must be a number.", "Invalid Order Number");
                return;
            }

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();

            dt = new DataTable();
            String sStatusMsg = String.Empty;

            sSQL = String.Format("SELECT * FROM tblBay WHERE OrderNumber = '{0}'", txtOrderNumber.Text.Trim().PadLeft(8, '0'));
            sqlCmd = new SqlCommand
            {
                Connection = sqlCon,
                CommandType = CommandType.Text,
                CommandText = sSQL
            };

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1015, "btnCloseBay_Cick");
                else
                    MessageBox.Show(String.Format("Error retrieving bay information:\n\t{0}", ex.Message), "Close Holding Bay");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                sStatusMsg = String.Format("Holding Bay for Order: {0} was not found", txtOrderNumber.Text.Trim());
                lblStatus.Text = sStatusMsg;
                return;
            }
            else if (dt.Rows[0]["BayShippedDate"] != null)
            {
                sStatusMsg = String.Format("This bay was shipped on {0}", dt.Rows[0]["BayShippedDate"].ToString());
                lblStatus.Text = sStatusMsg;
            }
            else
            {
                sStatusMsg = String.Format("Status of Bay {0} for order {1} has been set to 'shipped'.", dt.Rows[0]["BayLocation"].ToString(), dt.Rows[0]["OrderNumber"].ToString());
                lblStatus.Text = sStatusMsg;
                sSQL = String.Format("UPDATE tblBay SET BayShippedDate = GETDATE() WHERE BayID={0}", dt.Rows[0]["BayID"].ToString());
                comAPI.RunUpdateSQL(sSQL);
            }

            this.txtOrderNumber.Text = String.Empty;
            this.txtOrderNumber.Focus();

            dt.Dispose();
        }

        #endregion

        #region ADMIN SECTION

        private void TxtLeadTimePwd_TextChanged(object sender, EventArgs e)
        {
            String sPwd = comAPI.LeadTimePwd;// ConfigurationManager.AppSettings["leadTimePwd"];
            Int16 iPwdLength = comAPI.LeadTimePwdLength;// Convert.ToInt16(ConfigurationManager.AppSettings["leadTimePwdLength"]);

            if (txtLeadTimePwd.Text.Trim().Length == iPwdLength && txtLeadTimePwd.Text.Trim() == sPwd)
                btnUpdateLeadTime.Enabled = true;
        }

        private void TxtBackoutPwd_TextChanged(object sender, EventArgs e)
        {
            String sPwd = comAPI.BackoutPwd;// ConfigurationManager.AppSettings["backoutPwd"];
            Int16 iPwdLength = comAPI.BackoutPwdLength;// Convert.ToInt16(ConfigurationManager.AppSettings["backoutPwdLength"]);

            if (txtBackoutPwd.Text.Trim().Length == iPwdLength && txtBackoutPwd.Text.Trim() == sPwd)
                btnSearchOrderScan.Enabled = true;
        }

        private void TxtWindowsUsersPwd_TextChanged(object sender, EventArgs e)
        {
            String sPwd = comAPI.WinUserPwd;// ConfigurationManager.AppSettings["windowsUsersPwd"];
            Int16 iPwdLenght = comAPI.WinUserPwdLength;// Convert.ToInt16(ConfigurationManager.AppSettings["windowsUserPwdLength"]);

            if (txtWindowsUsersPwd.Text.Trim().Length == iPwdLenght && txtWindowsUsersPwd.Text.Trim() == sPwd)
                btnAddEditWindowsUsers.Enabled = true;
        }

        private void BtnAddEditWindowsUsers_Click(object sender, EventArgs e)
        {
            WindowsUsers w = new WindowsUsers();
            //show dialog as modal dialog 
            w.ShowDialog(this);
            txtWindowsUsersPwd.Text = String.Empty;
            btnAddEditWindowsUsers.Enabled = false;
        }

        private void TcUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcUI.SelectedTab.Name.ToUpper())
            {
                case "TABADMIN":
                case "TABMAINTENANCE":
                case "TABQSMAINTENANCE":
                case "TABSFO":
                    scShipping.Panel2Collapsed = true;
                    break;
                default:
                    scShipping.Panel2Collapsed = false;
                    break;
            }

            switch (tcUI.SelectedTab.Name.ToUpper())
            {
                case "TABSFO":
                    FrmSFOScanning_Load();
                    break;
                case "TABMAINTENANCE":
                    LoadMaintentanceTab();
                    break;
                case "TABQSMAINTENANCE":
                    LoadQSMaintenanceTab();
                    break;
                case "TABPRINTERS":
                    foreach (object item in cboLabelPrinter.Items)
                    {
                        if (item.ToString() == comAPI.LabelPrinter)// ConfigurationManager.AppSettings["LabelPrinter"].ToString())
                        {
                            cboLabelPrinter.SelectedItem = item;
                            break;
                        }
                    }
                    foreach (object item in cboTicketPrinter.Items)
                    {
                        if (item.ToString() == comAPI.TicketPrinter)// ConfigurationManager.AppSettings["PickTicketPrinter"].ToString())
                        {
                            cboTicketPrinter.SelectedItem = item;
                            break;
                        }
                    }
                    foreach(object item in cboShippingLabelPrinter.Items)
                    {
                        if(item.ToString() == comAPI.ShipLabelPrinter)// ConfigurationManager.AppSettings["ShipLabelPrinter"].ToString())
                        {
                            cboShippingLabelPrinter.SelectedItem = item;
                            break;
                        }
                    }
                    break;
            }
        }

        private void GetLeadTime()
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();
            dt = new DataTable();

            sSQL = "SELECT SysSettingValueInt FROM ShippingSysSettings WHERE SysSettingCode = 'LeadTime' ";

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = sSQL;

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);
                if (dt != null && dt.Rows.Count == 1)
                    txtLeadTime.Text = dt.Rows[0]["SysSettingValueInt"].ToString();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1163, "GetLeadTime");
                else
                    MessageBox.Show(String.Format("Error getting Lead Time value:\n\t{0}", ex.Message), "Lead Time");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dt.Dispose();
            }
        }

        private void UpdateLeadTime(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            Int16 iLeadTime = 0;
            String sPwd = String.Empty;
            sSQL = String.Empty;
            String sItemNumber = String.Empty;

            bGotError = false;

            if (txtLeadTime.Text == String.Empty)
            {
                MessageBox.Show("Please enter the number of days to set as the lead time", "Missing Lead Time");
                bGotError = true;
                txtLeadTime.Focus();
                return;
            }

            iLeadTime = Convert.ToInt16(txtLeadTime.Text.Trim());

            sSQL = "spSetShippingLeadTime";
            sqlCmd.CommandText = sSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@LeadTime", iLeadTime);
            sqlCmd.Parameters.AddWithValue("@Password", txtLeadTimePwd.Text.Trim());
            sqlCon.Open();

            try
            {
                Object o = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1212, "UpdateLeadTime");
                else
                    MessageBox.Show(String.Format("Error setting Lead Time value:\n\t{0}", ex.Message), "Lead Time Update");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                txtLeadTimePwd.Text = String.Empty;
            }

            GetLeadTime();
        }

        private void CmdSearchForOrder_Click(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;
            dt = new DataTable();

            if (this.txtSearchOrderNo.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter the order number to search for", "Missing Order Number");
                bGotError = true;
                this.txtSearchOrderNo.Focus();
                return;
            }

            sSQL = String.Format("SELECT ScanID, FMStockNumber as [Item Num      ], ScannedDate as [Scanned Date  ], WindowsLogin as [Login         ]," +
                " OperatorName as [Operator Name ],OrderNumber as [Order Num     ],BayLocation as [Bay           ],StockFlag as [Type          ] " +
                "FROM tblLogPickingScan WHERE OrderNumber = '{0}' ORDER BY FMStockNumber ", txtSearchOrderNo.Text.Trim());

            sqlCmd.CommandText = sSQL;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.dgvFoundOrder.SelectionChanged -= new EventHandler(DgvFoundOrder_SelectionChanged);
                    dgvFoundOrder.DataSource = dt;
                    this.dgvFoundOrder.SelectionChanged += new EventHandler(DgvFoundOrder_SelectionChanged);
                }
                else
                {
                    this.txtItemNumber.Text = String.Empty;
                    this.txtItemDesc1.Text = String.Empty;
                    this.txtItemDesc2.Text = String.Empty;
                  //  String sFullImagePath = ConfigurationManager.AppSettings["ImagePath"].ToString() + "! Image Not Found !.jpg";
                    String sFullImagePath = comAPI.ImagePath + "! Image Not Found !.jpg";
                    pbItemImage.Load(sFullImagePath);
                    MessageBox.Show("Order Not Found", "No Order");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1279, "cmdSearchForOrder_Click");
                else
                    MessageBox.Show(String.Format("Error retrieving Scanned item infomration:\n\t{0}", ex.Message));
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                reader.Dispose();
            }
        }

        private void DgvFoundOrder_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DgvFoundOrder_SelectionChanged(sender, e);
        }

        private void DgvFoundOrder_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFoundOrder.Rows.Count > 0)
            {
                Int16 iRowIndex = Convert.ToInt16(dgvFoundOrder.SelectedRows[0].Index);
                DataGridViewRow dgvRow = dgvFoundOrder.Rows[iRowIndex];
                sqlCon = new SqlConnection(comAPI.ShippingConnection);
                sqlCmd = new SqlCommand();
                sSQL = String.Format("SELECT item_no,item_desc_1,item_desc_2 FROM {0}.dbo.IMITMIDX_SQL WHERE item_no = '{1}'", tblItemMasterDB, dgvRow.Cells[1].Value.ToString().Trim());
                dt = new DataTable();
                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = sSQL;

                try
                {
                    reader = sqlCmd.ExecuteReader();
                    dt.Load(reader);

                    txtFoundItemNo.Text = dt.Rows[0]["item_no"].ToString();
                    txtFoundDesc1.Text = dt.Rows[0]["item_desc_1"].ToString();
                    txtFoundDesc2.Text = dt.Rows[0]["item_desc_2"].ToString();
                }
                catch (Exception ex)
                {
                    txtFoundItemNo.Text = String.Empty;
                    txtFoundDesc1.Text = String.Empty;
                    txtFoundDesc2.Text = String.Empty;
                    if (comAPI.ShowVerbose)//bVerbose)
                        ShowErrorDetails(ex, 1328, "dgvFoundOrder_SelectionChanged");
                    else
                        MessageBox.Show(String.Format("Error retrieving Item information:\n\t{0}", ex.Message), "Data Error");
                }
                finally
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                    sqlCmd.Dispose();
                    dt = null;
                }
            }
        }

        private void BtnBackoutScan_Click(Object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;
            Int32 iScanID = 0;
            String sItemNumber = String.Empty;
            String sMsg = String.Empty;
            String sBackoutBayAction = String.Empty;
            bGotError = false;
            dt = new DataTable();

            Int16 iRowIndex = Convert.ToInt16(dgvFoundOrder.SelectedRows[0].Index);

            if (txtAdminOperator.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter your Name/ID into Operator Name", "Missing Operator Name");
                bGotError = true;
                txtAdminOperator.Focus();
                return;
            }

            iScanID = Convert.ToInt32(dgvFoundOrder.Rows[iRowIndex].Cells[0].Value.ToString());
            sItemNumber = dgvFoundOrder.Rows[iRowIndex].Cells[1].Value.ToString();
            sSQL = "app_spBackoutScan";
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sSQL;
            sqlCmd.Parameters.AddWithValue("@ScanID", iScanID);
            sqlCmd.Parameters.AddWithValue("@ScannedQty", 1);
            sqlCmd.Parameters.AddWithValue("@OperatorName", this.txtAdminOperator.Text.Trim());

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);

                sMsg = String.Format("Backout Complete:\n\n\tOrderNumber:\t{0}\n\tItem Number:\t{1}\n\tBay Location:\t{2}\n", dt.Rows[0]["OrderNumber"].ToString(), dt.Rows[0]["ItemNumber"].ToString(), dt.Rows[0]["BayLocation"].ToString());

                switch (dt.Rows[0]["BackoutBay"].ToString())
                {
                    case "SUBTRACT 1":
                        sBackoutBayAction = "Bay has been reduced by 1 for Item scanned";
                        break;
                    case "DELETE Bay Detail":
                        sBackoutBayAction = "With backout of this item, no more of this\n\t\titem is left in bay, so item removed from bay";
                        break;
                    case "DELETE Bay":
                        sBackoutBayAction = "With backout of this item, no items yet picked\n\t\t\tfor order therefor bay has been removed";
                        break;
                    case "DELETE Bay - Shipment Voided":
                        sBackoutBayAction = "With backout of this item, no items yet picked\n\t\t\tfor order therefor bay has been removed\n\t\t\tThe Shipment Record has been VOIDED";
                        break;
                }
                sMsg = sMsg + "\tBayAction:\t" + sBackoutBayAction;
                dt.Dispose();

                CmdSearchForOrder_Click(sender, e);
                MessageBox.Show(sMsg);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1405, "btnBackoutScan_Click");
                else
                    MessageBox.Show(String.Format("Error backing out scan:\n\t{0}", ex.Message), "Backout Scan");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                reader.Dispose();
            }
        }

        private void BtnReprint_Click(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;
            Int32 iScanID = 0;
            String sItemNumber = String.Empty;
            Int16 iRowIndex = Convert.ToInt16(dgvFoundOrder.SelectedRows[0].Index);
            dt = new DataTable();

            bGotError = false;

            if (this.txtAdminOperator.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter your Name/ID into Operator Name", "Missing Operator Name");
                this.txtAdminOperator.Focus();
                bGotError = true;
                return;
            }

            iScanID = Convert.ToInt32(dgvFoundOrder.Rows[iRowIndex].Cells[0].Value.ToString());
            sItemNumber = dgvFoundOrder.Rows[iRowIndex].Cells[1].Value.ToString();
            sSQL = "app_spReprint";
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();
            sqlCmd.CommandText = sSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ScanID", iScanID);
            sqlCmd.Parameters.AddWithValue("@OperatorName", this.txtAdminOperator.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@EnvironUser", Environment.UserName.ToString());

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Error retrieving Print Info for Order/Item", "Reprint Info");
                    sqlCon.Close();
                    sqlCon.Dispose();
                    sqlCmd.Dispose();
                    reader.Dispose();
                    return;
                }
                LoadPrintData(dt, true);
                DgvFoundOrder_SelectionChanged(sender, e);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1472, "btnReprint_Click");
                else
                    MessageBox.Show(String.Format("Error retrieving Print Info for Order/Item:\n\t{0}", ex.Message), "Reprint Info");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                reader.Dispose();
                if (dt != null)
                    dt.Dispose();
            }
        }

        #endregion

        #region REPORT POPULATING METHODS

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public Int32 FunReturnOrderQuantity(Int32 iOrderNum, String StockNumber)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = String.Format("SELECT SUM(Qty) as TotQty FROM KitItemsAndQtys WHERE ord_no = {0} AND ITEM = '{1}' AND ord_type = 'O'", iOrderNum.ToString(), String.Format(StockNumber.Trim(), "#########"));
            sqlCmd = new SqlCommand(sSQL, sqlCon);
            Int32 iRetVal = 0;
            try
            {
                object o = sqlCmd.ExecuteScalar();
                if (o != null)
                    iRetVal = Convert.ToInt32(o);

                o = null;

                return iRetVal;
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1547, "funReturnOrderQuantity");
                else
                    MessageBox.Show(String.Format("Error retrieving Order Quantity:\n\t{0}", ex.Message), "Data Error");
                return 0;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
            }
        }

        #endregion

        #region MAINTENANCE

        private void DgvBayDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dgvBayDetails.Rows[e.RowIndex];
            Int32 intBayID = 0;
            Int32 intBayDetailID = 0;
            Int32 intNewQty = 0;
            Int32 intOldQty = 0;
            String sOrderNumber = String.Empty;
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;

            QtyUpdate q = new QtyUpdate();
            //show dialog as modal dialog and determine if dialog result is "OK"/Accept
            if (q.ShowDialog(this) == DialogResult.OK)
            {
                //read the contents of the qtyUpdate.txtNewQty textbox
                intNewQty = Convert.ToInt32(q.txtNewQty.Text.Trim());
                q.Dispose();
                //Get BayDetailID of Bay Detail record for the update and insert to log
                intBayDetailID = Convert.ToInt32(dgvRow.Cells["colBayDetailID"].Value.ToString());

                sSQL = String.Format("SELECT BD.QtyFromStock, B.OrderNumber, B.BayID FROM tblBayDetail BD  INNER JOIN tblBay B  ON BD.BayID = B.BayID WHERE BD.BayDetailID = {0}", intBayDetailID);

                sqlCmd.CommandText = sSQL;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                DataTable dtOld = new DataTable();

                try
                {
                    reader = sqlCmd.ExecuteReader();
                    dtOld.Load(reader);
                    intOldQty = Convert.ToInt32(dtOld.Rows[0]["QtyFromStock"].ToString());
                    sOrderNumber = dtOld.Rows[0]["OrderNumber"].ToString();
                    intBayID = Convert.ToInt32(dtOld.Rows[0]["BayID"].ToString());
                    // Update Bay Detail record
                    sSQL = String.Format("UPDATE tblBayDetail SET QtyFromStock = {0} WHERE BayDetailID = {1} ", intNewQty, intBayDetailID);
                    sqlCmd = new SqlCommand(sSQL, sqlCon);
                    sqlCmd.ExecuteNonQuery();

                    sSQL = String.Format("INSERT INTO tblBayUpdateLog VALUES({0},GETDATE(),'{1}','Update Quantity on BayDetailID record {0} from {2} to {3}')", intBayDetailID, Environment.MachineName + "/" + Environment.UserName, intOldQty.ToString(), intNewQty.ToString());
                    sqlCmd = new SqlCommand(sSQL, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    sqlCmd.ExecuteNonQuery();

                    sSQL = String.Format("SELECT BayDetailID,StockNumber,OrderedQty,ReceivedQty,[Qty Shipped Todate],QtyFromStock,item_desc_1,item_desc_2 FROM tblBayDetail bd INNER JOIN {0}.dbo.IMITMIDX_SQL i ON bd.StockNumber = i.item_no WHERE BayID = {1} ORDER BY StockNumber", tblItemMasterDB, intBayID);
                    sqlCmd = new SqlCommand(sSQL, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    reader = sqlCmd.ExecuteReader();
                    DataTable dtNew = new DataTable();
                    dtNew.Load(reader);
                    dgvBayDetails.DataSource = dtNew;
                    SetColumnOrder(ref dgvBayDetails);
                }
                catch (Exception ex)
                {
                    if (comAPI.ShowVerbose)//bVerbose)
                        ShowErrorDetails(ex, 1622, "dgvBayDetails_CellContentClick");
                    else
                        MessageBox.Show(String.Format("Error Updating Bay Item Quantity:\n\t{0} ", ex.Message), "Update Error");
                }
                finally
                {
                    sqlCon.Close();
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                    reader.Dispose();
                    dtOld.Dispose();
                    dgvBayDetails.FirstDisplayedScrollingRowIndex = e.RowIndex;
                }
            }
        }

        private void LoadMaintentanceTab()
        {
            DataTable dtBayDetail = new DataTable();
            sSQL = String.Format("SELECT BayDetailID,StockNumber,OrderedQty,ReceivedQty,[Qty Shipped Todate],QtyFromStock,item_desc_1,item_desc_2 FROM tblBayDetail bd INNER JOIN {0}.dbo.IMITMIDX_SQL i ON bd.StockNumber = i.item_no WHERE BayID = 0 ORDER BY StockNumber", tblItemMasterDB);
            lblOrderNotFound.Visible = false;
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            try
            {
                reader = sqlCmd.ExecuteReader();
                dtBayDetail.Load(reader);
                dgvBayDetails.DataSource = dtBayDetail;
                SetColumnOrder(ref dgvBayDetails);
                txtOrderNumber.Focus();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1657, "LoadMaintenanceTab");
                else
                    MessageBox.Show(String.Format("Error retrieving Bay Detail information:\n\t{0}", ex.Message), "Bay Detail");
            }
            finally
            {
                sqlCon.Close();
                sqlCmd.Dispose();
                reader.Dispose();
            }
        }

        private void LoadQSMaintenanceTab()
        {
            sSQL = String.Format("SELECT r.Item, i.item_desc_1, i.item_desc_2, r.QtyInStock, r.Min, r.Max, r.OnReplenishment, r.ReplenishmentDt FROM FMShipping.dbo.tblReplenishment r INNER JOIN {0}.dbo.IMITMIDX_SQL i ON r.Item = i.item_no", tblItemMasterDB);
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            DataTable dtQS = new DataTable();
            try
            {
                reader = sqlCmd.ExecuteReader();
                dtQS.Load(reader);
                bsQSMaintenance.DataSource = dtQS;
                dgvQSMaintenance.DataSource = bsQSMaintenance;
                SetColumnOrder(ref dgvQSMaintenance);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1688, "LoadQSMaintenanceTab");
                else
                    MessageBox.Show(String.Format("Error retreiving Quick Ship items:\n\t{0} ", ex.Message), "Data retrieval error");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dtQS.Dispose();
            }
        }

        private void SetColumnOrder(ref DataGridView dgv)
        {
            switch (dgv.Name)
            {
                case "dgvQSMaintenance":
                    dgv.Columns["colQSItemNo"].DisplayIndex = 0;
                    dgv.Columns["colQSItemDesc1"].DisplayIndex = 1;
                    dgv.Columns["colQSItemDesc"].DisplayIndex = 2;
                    dgv.Columns["colQSQtyInStock"].DisplayIndex = 3;
                    dgv.Columns["colQSMinQty"].DisplayIndex = 4;
                    dgv.Columns["colQSMaxQty"].DisplayIndex = 5;
                    dgv.Columns["colQSQtyRequested"].DisplayIndex = 6;
                    dgv.Columns["colQSDateRequested"].DisplayIndex = 7;
                    dgv.Columns["colQSUpdateQtyInStock"].DisplayIndex = 8;
                    dgv.Columns["colQSMin"].DisplayIndex = 9;
                    dgv.Columns["colMax"].DisplayIndex = 10;
                    dgv.Columns["colQSClear"].DisplayIndex = 11;
                    break;
                case "dgvBayDetails":
                    dgv.Columns["colBayDetailID"].DisplayIndex = 0;
                    dgv.Columns["colOrderItem"].DisplayIndex = 1;
                    dgv.Columns["colItemDesc1"].DisplayIndex = 2;
                    dgv.Columns["colItemDesc2"].DisplayIndex = 3;
                    dgv.Columns["colBayItemNumber"].DisplayIndex = 4;
                    dgv.Columns["colBayItemQtyOrdered"].DisplayIndex = 5;
                    dgv.Columns["colReceivedQty"].DisplayIndex = 6;
                    dgv.Columns["colQtyShippedTo"].DisplayIndex = 7;
                    dgv.Columns["colQtyInBay"].DisplayIndex = 8;
                    dgv.Columns["colUpdateQty"].DisplayIndex = 9;
                    break;
            }
        }

        private void BtnFindOrder_Click(object sender, EventArgs e)
        {
            if (this.txtFindOrderNumber.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter an Order Number!", "Missing Data");
                return;
            }

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = String.Format("SELECT * FROM tblBay WHERE OrderNumber = '{0}'", this.txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
            sqlCmd = new SqlCommand(sSQL, sqlCon);
            DataTable dtBay = new DataTable();
            DataTable dtBayDetail = new DataTable();
            DataTable dtSingle = new DataTable();
            // First look for the order in a Bay
            try
            {
                reader = sqlCmd.ExecuteReader();
                dtBay.Load(reader);
                if (dtBay.Rows.Count > 0)
                {
                    // Update Bay info on Holding Bay Maintenance form
                    txtBayLocation.Text = dtBay.Rows[0]["BayLocation"].ToString();
                    txtShippingDate.Text = dtBay.Rows[0]["Shipping Date"].ToString();
                    txtBayStartedAt.Text = dtBay.Rows[0]["StartedDateTime"].ToString();
                    txtPickingTicketPrinted.Text = dtBay.Rows[0]["PickedDateTime"].ToString();
                    txtBayShipped.Text = dtBay.Rows[0]["BayShippedDate"].ToString();

                    lblOrderNotFound.Visible = false;

                    //  Get Bay Detail info and update Bay Detail info on Holding Bay Maintenance subform
                    sSQL = String.Format("SELECT BayDetailID,StockNumber,OrderedQty,ReceivedQty,[Qty Shipped Todate],QtyFromStock,item_desc_1,item_desc_2 FROM tblBayDetail bd INNER JOIN {0}.dbo.IMITMIDX_SQL i ON bd.StockNumber = i.item_no WHERE BayID = {1} ORDER BY StockNumber", tblItemMasterDB, dtBay.Rows[0]["BayID"].ToString());
                    sqlCmd = new SqlCommand(sSQL, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    reader = sqlCmd.ExecuteReader();
                    dtBayDetail.Load(reader);
                    dgvBayDetails.DataSource = dtBayDetail;
                    SetColumnOrder(ref dgvBayDetails);

                    if (txtBayShipped.Text.Trim() != String.Empty)
                    {
                        lblOrderNotFound.Text = String.Format("Order: {0} has been shipped and can't be altered", txtFindOrderNumber.Text.Trim());
                        lblOrderNotFound.Visible = true;
                        dgvBayDetails.Enabled = false;
                    }
                }
                else
                {
                    // No bay - is there a Single Item Order that was picked?
                    sSQL = String.Format("SELECT * FROM tblSIngleItemPicking WHERE OrderNumber = '{0}'", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
                    sqlCmd = new SqlCommand(sSQL, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    reader = sqlCmd.ExecuteReader();
                    dtSingle.Load(reader);
                    if (dtSingle.Rows.Count > 0)
                    {
                        lblOrderNotFound.Text = String.Format("Order: {0} is a Single Item Order", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
                        lblOrderNotFound.Visible = true;
                        txtBayLocation.Text = String.Empty;
                        txtShippingDate.Text = String.Empty;
                        txtPickingTicketPrinted.Text = String.Empty;
                        txtBayShipped.Text = String.Empty;
                        txtBayStartedAt.Text = String.Empty;
                    }
                    else
                    {
                        //If no single item order, order is not found so update form accordingly
                        lblOrderNotFound.Text = String.Format("Order: {0} wasn't found", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
                        lblOrderNotFound.Visible = true;
                        txtBayLocation.Text = String.Empty;
                        txtShippingDate.Text = String.Empty;
                        txtPickingTicketPrinted.Text = String.Empty;
                        txtBayShipped.Text = String.Empty;
                        txtBayStartedAt.Text = String.Empty;
                    }

                    //If this is a Single Item order or an order not found, there are no Bay Details so set subform recordset to an empty recordset.
                    sSQL = String.Format("SELECT BayDetailID,StockNumber,OrderedQty,ReceivedQty,[Qty Shipped Todate],QtyFromStock,item_desc_1,item_desc_2 FROM tblBayDetail bd INNER JOIN {0}.dbo.IMITMIDX_SQL i ON bd.StockNumber = i.item_no WHERE BayID = 0 ORDER BY StockNumber", tblItemMasterDB);
                    sqlCmd = new SqlCommand(sSQL, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    reader = sqlCmd.ExecuteReader();
                    dtSingle.Load(reader);
                    dgvBayDetails.DataSource = dtSingle;
                    SetColumnOrder(ref dgvBayDetails);
                }
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1824, "btnFindOrder_Click");
                else
                    MessageBox.Show(String.Format("Error retrieving BayDetail information:\n\t{0}", ex.Message), "No Matching Order");
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
                dtBay.Dispose();
                dtBayDetail.Dispose();
                dtSingle.Dispose();
                reader.Dispose();
            }
        }

        private void BtnRemovePickingDate_Click(object sender, EventArgs e)
        {
            if (this.txtFindOrderNumber.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter and order number", "Missing Data");
                return;
            }

            DataTable dtBay = new DataTable();
            DataTable dtBayDetail = new DataTable();

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = String.Format("SELECT * FROM tblBay WHERE OrderNumber = '{0}'", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };

            try
            {
                reader = sqlCmd.ExecuteReader();
                dtBay.Load(reader);
                if (dtBay.Rows.Count > 0)
                {
                    if (dtBay.Rows[0]["BayShippedDate"].ToString() == String.Empty)
                    {
                        sSQL = String.Format("UPDATE tblBay SET PickedDateTime = NULL WHERE OrderNumber = '{0}'", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
                        sqlCmd = new SqlCommand(sSQL, sqlCon)
                        {
                            CommandType = CommandType.Text
                        };
                        sqlCmd.ExecuteNonQuery();

                        sSQL = String.Format("INSERT INTO tblBayUpdateLog VALUES({0},GETDATE(),'{1}','Removal of Picking Date: {2} for order {3}')", dtBay.Rows[0]["BayID"].ToString(), Environment.MachineName + "/" + Environment.UserName, dtBay.Rows[0]["PickedDateTime"].ToString(), dtBay.Rows[0]["OrderNumber"].ToString());
                        sqlCmd = new SqlCommand(sSQL, sqlCon)
                        {
                            CommandType = CommandType.Text
                        };
                        sqlCmd.ExecuteNonQuery();

                        this.txtPickingTicketPrinted.Text = String.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Order was shipped and can't be updated", "Order Shipped");
                    }
                }
                else
                {
                    MessageBox.Show("Order can't be found", "Order not Found");
                }
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1890, "btnRemovePickingDate_Click");
                else
                    MessageBox.Show(String.Format("Error Updating Order:\n\t{0}", ex.Message), "Order Update");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dtBay.Dispose();
                dtBayDetail.Dispose();
                reader.Dispose();
            }
        }

        private void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sSQL = String.Format("DELETE tblSingleItemPicking WHERE OrderNumber = '{0}'", this.txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
            sqlCon.Open();
            sqlCmd = new SqlCommand(sSQL, sqlCon);

            try
            {
                sqlCmd.ExecuteNonQuery();
                sSQL = String.Format("INSERT INTO tblBayUpdateLog VALUES({0},GETDATE(),'{1}','Delete Single Item Order Pick: {0}')", this.txtFindOrderNumber.Text.Trim(), Environment.MachineName + "/" + Environment.UserName);
                sqlCmd = new SqlCommand(sSQL, sqlCon)
                {
                    CommandType = CommandType.Text
                };

                sqlCmd.ExecuteNonQuery();

                MessageBox.Show(String.Format("Order number {0} has been deleted from the Single Piece Picking Table!", txtFindOrderNumber.Text.Trim()), "Order Removed");
                txtPickingTicketPrinted.Text = String.Empty;
                lblOrderNotFound.Visible = false;
                txtFindOrderNumber.Text = String.Empty;
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1929, "btnDeleteItem_Click");
                else
                    MessageBox.Show(String.Format("Error Deleting Order Information: {0} ", ex.Message), "Order Deletion");
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }

        private void BtnFreezeBay_Click(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sSQL = String.Format("UPDATE tblBay SET PickedDateTime = GETDATE() WHERE OrderNumber = {0}", this.txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
            DataTable dtBay = new DataTable();
            DataTable dtBayDetail = new DataTable();
            Int32 intBayID = 0;

            //Freeze bay by setting a Bay Ship Date
            sqlCon.Open();
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            try
            {
                sqlCmd.ExecuteNonQuery();
                // Log freeze of Bay
                sSQL = String.Format("INSERT INTO tblBayUpdateLog VALUES('{0}',GETDATE(),'{1}','Freeze Bay for Order: {0}')", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'), Environment.MachineName + "/" + Environment.UserName);
                sqlCmd = new SqlCommand(sSQL, sqlCon)
                {
                    CommandType = CommandType.Text
                };
                sqlCmd.ExecuteNonQuery();

                sSQL = String.Format("SELECT * FROM tblBay WHERE OrderNumber = {0}", txtFindOrderNumber.Text.Trim().PadLeft(8, '0'));
                sqlCmd = new SqlCommand(sSQL, sqlCon)
                {
                    CommandType = CommandType.Text
                };

                reader = sqlCmd.ExecuteReader();
                dtBay.Load(reader);

                if (dtBay.Rows.Count > 0)
                {
                    intBayID = Convert.ToInt32(dtBay.Rows[0]["BayID"].ToString());
                    txtPickingTicketPrinted.Text = dtBay.Rows[0]["PickedDateTime"].ToString();
                }
                //  Refresh screen
                sSQL = String.Format("SELECT BayDetailID,StockNumber,OrderedQty,ReceivedQty,[Qty Shipped Todate],QtyFromStock,item_desc_1,item_desc_2 FROM tblBayDetail bd INNER JOIN {0}.dbo.IMITMIDX_SQL i ON bd.StockNumber = i.item_no WHERE BayID = {1} ORDER BY StockNumber", tblItemMasterDB, intBayID.ToString());
                sqlCmd = new SqlCommand(sSQL, sqlCon)
                {
                    CommandType = CommandType.Text
                };

                reader = sqlCmd.ExecuteReader();
                dtBayDetail.Load(reader);

                dgvBayDetails.DataSource = dtBayDetail;
                SetColumnOrder(ref dgvBayDetails);

                txtFindOrderNumber.Focus();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 1990, "btnFreezeBay_Click");
                else
                    MessageBox.Show(String.Format("Error Freezing Bay:\n\t{0}", ex.Message), "Freeze Bay");
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
                dtBay.Dispose();
                dtBayDetail.Dispose();
            }
        }

        private void ClearBayLocation(object sender, EventArgs e)
        {
            Int32.TryParse(txtOrderNumber.Text.Trim(), out int iOut);
            if (iOut == 0)
            {
                MessageBox.Show("Order number must be a number!", "Invalid Order Number");
                txtOrderNumber.Focus();
                return;
            }
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = String.Format("SELECT * FROM tblBay WHERE OrderNumber = '{0}'", txtOrderNumber.Text.Trim().PadLeft(8, '0'));
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };
            DataTable dt = new DataTable();
            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);

                if (dt == null || dt.Rows.Count == 0)
                    MessageBox.Show(String.Format("Holding Bay for order: {0} was not found.", iOut.ToString()), "Order Not Found");
                else if (dt.Rows[0]["BayShippedDate"].ToString().Trim().Length > 0)
                    MessageBox.Show(String.Format("This Bay was shipped on {0}", dt.Rows[0]["BayShippedDate"].ToString()));
                else
                {
                    sSQL = String.Format("UPDATE tblBay SET BayShippedDate = GETDATE() WHERE BayID = {0}", dt.Rows[0]["BayID"].ToString());
                    sqlCmd = new SqlCommand(sSQL, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show(String.Format("Status of Bay {0} for order {1} has been set to shipped.", dt.Rows[0]["BayLocation"].ToString(), dt.Rows[0]["OrderNumber"].ToString()), "Bay Location Cleared");
                }
                txtOrderNumber.Text = String.Empty;
                txtOrderNumber.Focus();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2043, "ClearBayLocation");
                else
                    MessageBox.Show(String.Format("Error Clearing Bay Location:\n\t{0} ", ex.Message), "Bay Location Error");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dt.Dispose();
            }
        }

        #endregion

        #region QS MAINTENANCE SCREEN

        private void TxtQSFilter_TextChanged(object sender, EventArgs e)
        {
            bsQSMaintenance.Filter = String.Format("Item_desc_2 LIKE '%{0}%'", txtQSFilter.Text);
        }

        private void BtnMax_Click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dgvQSMaintenance.Rows[e.RowIndex];
            String sPassword = String.Empty;
            Int32 iMaxQty = 0;
            String sItemNo = String.Empty;
            String sResult = String.Empty;

            if (txtQSPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("A password is required to update the Min/Max fields", "Missing Password");
                txtQSPwd.Focus();
                return;
            }
            if (txtQSPwd.Text.Trim() != comAPI.QSRepPwd)// ConfigurationManager.AppSettings["qsReplenishmentPwd"])
            {
                MessageBox.Show("The Password entered is incorrect.", "Incorrect Password");
                txtQSPwd.Text = String.Empty;
                txtQSPwd.Focus();
                return;
            }

            sItemNo = dgvRow.Cells["colQSItemNo"].Value.ToString();
            sPassword = txtQSPwd.Text.Trim();
            iMaxQty = Convert.ToInt32(dgvRow.Cells["colQSMaxQty"].Value.ToString());

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = "spUpdateQSMaxValue";
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCmd.Parameters.AddWithValue("@Item", sItemNo);
            sqlCmd.Parameters.AddWithValue("@MaxValue", iMaxQty);
            sqlCmd.Parameters.AddWithValue("@Password", sPassword);
            SqlParameter outputParam = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };
            sqlCmd.Parameters.Add(outputParam);
            try
            {
                Object oOut = sqlCmd.ExecuteNonQuery();
                sResult = outputParam.Value.ToString();
                MessageBox.Show(sResult, "Max Update");

                dgvQSMaintenance.Refresh();
                LoadQSMaintenanceTab();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2115, "btnMax_Click");
                else
                    MessageBox.Show(String.Format("Error updating Max value:\n\t{0}", ex.Message), "Update Error");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dgvQSMaintenance.FirstDisplayedScrollingRowIndex = e.RowIndex;
            }
        }

        private void BtnMin_Click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dgvQSMaintenance.Rows[e.RowIndex];
            String sPassword = String.Empty;
            Int32 iMinQty = 0;
            String sItemNo = String.Empty;
            String sResult = String.Empty;

            if (txtQSPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("A password is required to update the Min/Max fields", "Missing Password");
                txtQSPwd.Focus();
                return;
            }
            if (txtQSPwd.Text.Trim() != comAPI.QSRepPwd)// ConfigurationManager.AppSettings["qsReplenishmentPwd"])
            {
                MessageBox.Show("The Password entered is incorrect.", "Incorrect Password");
                txtQSPwd.Text = String.Empty;
                txtQSPwd.Focus();
                return;
            }

            sItemNo = dgvRow.Cells["colQSItemNo"].Value.ToString();
            sPassword = txtQSPwd.Text.Trim();
            iMinQty = Convert.ToInt32(dgvRow.Cells["colQSMinQty"].Value.ToString());

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = "spUpdateQSMinValue";
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCmd.Parameters.AddWithValue("@Item", sItemNo);
            sqlCmd.Parameters.AddWithValue("@MinValue", iMinQty);
            sqlCmd.Parameters.AddWithValue("@Password", sPassword);
            SqlParameter outputParam = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };
            sqlCmd.Parameters.Add(outputParam);
            try
            {
                Object oOut = sqlCmd.ExecuteNonQuery();
                sResult = outputParam.Value.ToString();
                MessageBox.Show(sResult, "Min Update");
                dgvQSMaintenance.Refresh();
                LoadQSMaintenanceTab();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2177, "btnMin_Click");
                else
                    MessageBox.Show(String.Format("Error updating Min value:\n\t{0} ", ex.Message), "Update Error");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dgvQSMaintenance.FirstDisplayedScrollingRowIndex = e.RowIndex;
            }
        }

        private void BtnUpdateQty_Click(object sender, DataGridViewCellEventArgs e)
        {
            String sSQL2 = String.Empty;
            DataGridViewRow dgvRow = dgvQSMaintenance.Rows[e.RowIndex];
            String sPassword = String.Empty;
            Int32 iNewQty = 0;
            String sItemNo = String.Empty;

            if (txtQSPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("A password is required to update the Qty In Stock", "Missing Password");
                txtQSPwd.Focus();
                return;
            }
            if (txtQSPwd.Text.Trim() != comAPI.QSRepPwd)// ConfigurationManager.AppSettings["qsReplenishmentPwd"])
            {
                MessageBox.Show("The Password entered is incorrect.", "Incorrect Password");
                txtQSPwd.Text = String.Empty;
                txtQSPwd.Focus();
                return;
            }

            sItemNo = dgvRow.Cells["colQSItemNo"].Value.ToString();
            sPassword = txtQSPwd.Text.Trim();
            iNewQty = Convert.ToInt32(dgvRow.Cells["colQSQtyInStock"].Value.ToString());

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = String.Format("UPDATE tblReplenishment SET QtyInStock = {0} WHERE Item = '{1}'", iNewQty.ToString(), sItemNo);
            sSQL2 = String.Format("UPDATE tblFMQuickShipItems SET InventoryCount = {0} WHERE StockNumber = {1}", iNewQty.ToString(), sItemNo);
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.Text
            };

            try
            {
                sqlCmd.ExecuteNonQuery();
                sqlCmd = new SqlCommand(sSQL2, sqlCon)
                {
                    CommandType = CommandType.Text
                };
                sqlCmd.ExecuteNonQuery();

                MessageBox.Show(String.Format("Quantity in Stock for Item {0} updated to {1}", sItemNo.Trim(), iNewQty.ToString()), "Qty In Stock Update");
                dgvQSMaintenance.Refresh();
                LoadQSMaintenanceTab();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2236, "btnUpdateQty_Click");
                else
                    MessageBox.Show(String.Format("Error updating Qty In Stock:\n\t{0}", ex.Message), "Update Error");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dgvQSMaintenance.FirstDisplayedScrollingRowIndex = e.RowIndex;
            }
        }

        private void BtnClear_Click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dgvQSMaintenance.Rows[e.RowIndex];
            String sPassword = String.Empty;
            String sItemNo = String.Empty;
            String sResult = String.Empty;

            if (txtQSPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("A password is required to update the Min/Max fields", "Missing Password");
                txtQSPwd.Focus();
                return;
            }
            if (txtQSPwd.Text.Trim() != comAPI.QSRepPwd)// ConfigurationManager.AppSettings["qsReplenishmentPwd"])
            {
                MessageBox.Show("The Password entered is incorrect.", "Incorrect Password");
                txtQSPwd.Text = String.Empty;
                txtQSPwd.Focus();
                return;
            }

            sItemNo = dgvRow.Cells["colQSItemNo"].Value.ToString();
            sPassword = txtQSPwd.Text.Trim();

            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCon.Open();
            sSQL = "spClearQSRequestForReceipt";
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCmd.Parameters.AddWithValue("@Item", sItemNo);
            sqlCmd.Parameters.AddWithValue("@Password", sPassword);
            SqlParameter outputParam = new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };
            sqlCmd.Parameters.Add(outputParam);
            try
            {
                Object oOut = sqlCmd.ExecuteNonQuery();
                sResult = outputParam.Value.ToString();
                MessageBox.Show(sResult, "Clear Replenishment Request");
                dgvQSMaintenance.Refresh();
                LoadQSMaintenanceTab();
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2295, "btnClear_Click");
                else
                    MessageBox.Show(String.Format("Error Clearing Replenishment Request:\n\t{0} ", ex.Message), "Update Error");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dgvQSMaintenance.FirstDisplayedScrollingRowIndex = e.RowIndex;
            }
        }

        private void DgvQSMaintenance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn dgvCol = dgvQSMaintenance.Columns[e.ColumnIndex];
            switch (dgvCol.Name.ToUpper())
            {
                case "COLQSUPDATEQTYINSTOCK":
                    BtnUpdateQty_Click(sender, e);
                    break;
                case "COLQSMIN":
                    BtnMin_Click(sender, e);
                    break;
                case "COLMAX":
                    BtnMax_Click(sender, e);
                    break;
                case "COLQSCLEAR":
                    BtnClear_Click(sender, e);
                    break;
            }
        }

        #endregion

        #region SFO SCANNING

        private void FrmSFOScanning_Load()
        {
            //frmSFOScanning_Reset();
            //this.txtSFOScanQty.TextChanged -= new EventHandler(txtSFOScanQty_Change);
            //txtSFOScanQty.Text = "0";
            //txtSFOScanQty.Focus();
            //txtSFOScanQty.Enabled = true;
            //this.txtSFOScanQty.TextChanged += new EventHandler(txtSFOScanQty_Change);
            //txtSFOOperator.Text = Environment.UserName.ToString();
            //txtSFONumber.Focus();
        }

        private void FrmSFOScanning_Reset()
        {
            //txtSFOUPC.Text = String.Empty;
            //sSFOUPC = String.Empty;
            //txtSFOItemNo.Text = String.Empty;
            //sSFOItemNo = String.Empty;
            //this.txtSFOScanQty.TextChanged -= new EventHandler(txtSFOScanQty_Change);
            //txtSFOScanQty.Text = String.Empty;
            //iSFOScanQty = 0;
            //txtSFOScanQty.ForeColor = Color.Black;
            //this.txtSFOScanQty.TextChanged += new EventHandler(txtSFOScanQty_Change);
            //txtSFOQtyOrdered.Text = String.Empty;
            //txtSFOItemDesc1.Text = String.Empty;
            //sSFOItemDesc1 = String.Empty;
            //txtSFOItemDesc2.Text = String.Empty;
            //sSFOItemDesc2 = String.Empty;
            //txtSFOOperator.Text = Environment.UserName.ToString();
            //txtSFONumber.Focus();
        }

        private void BtnSFOScanComplete_Click(object sender, EventArgs e)
        {
            //if (txtSFOQtyOrdered.Text.Trim() != txtSFOScanQty.Text.Trim())
            //{
            //    this.txtSFOScanQty.TextChanged -= new EventHandler(txtSFOScanQty_Change);
            //    txtSFOScanQty.ForeColor = Color.Red;
            //    this.txtSFOScanQty.TextChanged += new EventHandler(txtSFOScanQty_Change);
            //    PrintReport("SFO", "rptPrintScanQty");
            //    frmSFOScanning_Reset();
            //}
        }

        private void SetItemInfoReadOnly(Boolean bFlag)
        {
            //txtSFOItemNo.Enabled = bFlag;
            //txtSFOItemDesc1.Enabled = bFlag;
            //txtSFOItemDesc2.Enabled = bFlag;
            //txtSFOQtyOrdered.Enabled = bFlag;
            //txtSFOScheduleNo.Enabled = bFlag;
        }

        private void TxtSFOScanQty_Change(object sender, EventArgs e)
        {
            //if (iSFOScanQty == 0)
            //    iSFOScanQty = 1;
            //else
            //    iSFOScanQty++;
            //this.txtSFOScanQty.TextChanged -= new EventHandler(txtSFOScanQty_Change);
            //txtSFOScanQty.Text = iSFOScanQty.ToString();
            //this.txtSFOScanQty.TextChanged += new EventHandler(txtSFOScanQty_Change);
            //CheckSFOScanQty();
            //txtSFOScanQty.Focus();
        }

        private void CheckSFOScanQty()
        {
            //Int32 iScanned = 0;
            //Int32 iOrdered = 0;
            //Int32 iOut = 0;
            //Int32.TryParse(txtSFOScanQty.Text.Trim().ToString(), out iOut);
            //if (iOut != 0)
            //    iScanned = iOut;
            //Int32.TryParse(txtSFOQtyOrdered.Text.Trim().ToString(), out iOut);
            //if (iOut != 0)
            //    iOrdered = iOut;

            //if (iScanned == 0 || iScanned < iOrdered)
            //{
            //    this.txtSFOScanQty.TextChanged -= new EventHandler(txtSFOScanQty_Change);
            //    txtSFOScanQty.ForeColor = Color.Red;
            //    this.txtSFOScanQty.TextChanged += new EventHandler(txtSFOScanQty_Change);
            //}
            //else if (iScanned == iOrdered)
            //{
            //    this.txtSFOScanQty.TextChanged -= new EventHandler(txtSFOScanQty_Change);
            //    txtSFOScanQty.ForeColor = Color.Black;
            //    this.txtSFOScanQty.TextChanged += new EventHandler(txtSFOScanQty_Change);
            //    MessageBox.Show(String.Format("{0}-{1} {2} Order Quantity ({3}) has been reached.", txtSFOItemNo.Text.Trim(), txtSFOItemDesc1.Text.Trim(), txtSFOItemDesc2.Text.Trim(), iOrdered.ToString()), "Order Quantity Reached");
            //    btnSFOScanComplete.Focus();
            //}
        }

        private void TxtSFONumber_Changed(object sender, EventArgs e)
        {
            //int iLength = 0;
            //String sScannedValue = txtSFONumber.Text.Trim();

            //iLength = sScannedValue.Length;
            //if (iLength == 8)
            //{
            //    GetScheduleAndItemsFromSFO();
            //}
        }

        private void GetScheduleAndItemsFromSFO()
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            DataTable dtItemsFromScan = new DataTable();
            String sItemType = String.Empty;
            String sScannedValue = String.Empty;
            String sSFONo = String.Empty;
            String sUserName = String.Empty;

            sUserName = txtOperator.Text.Trim();
            sScannedValue = txtShopFloorNo.Text.Trim();
            sSFONo = txtShopFloorNo.Text.Trim();

            if (sUserName.Length == 0)
            {
                MessageBox.Show("Please enter your Name/ID in Operator Name", "Missing Operator Name");
                return;
            }

            sSQL = "app_spGetScheduleAndItemsFromSFO";
            sqlCon.Open();
            sqlCmd = new SqlCommand(sSQL, sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCmd.Parameters.AddWithValue("@sfoNum", sSFONo);

            try
            {
                reader = sqlCmd.ExecuteReader();
                dtItemsFromScan.Load(reader);

                if (dtItemsFromScan.Rows.Count > 0)
                {
                    SetItemInfoReadOnly(true);
                    DataRow dr = dtItemsFromScan.Rows[0];
                    //txtSFOScanQty.Text = "0";
                    //iSFOScanQty = 0;
                    //txtSFOItemNo.Text = dr["item_no"].ToString();
                    //sSFOItemNo = dr["item_no"].ToString();
                    //txtSFOItemDesc1.Text = dr["item_desc_1"].ToString();
                    //sSFOItemDesc1 = dr["item_desc_1"].ToString();
                    //txtSFOItemDesc2.Text = dr["item_desc_2"].ToString();
                    //sSFOItemDesc2 = dr["item_desc_2"].ToString();
                    //txtSFOUPC.Text = dr["UPCNumber"].ToString();
                    //sSFOUPC = dr["UPCNumber"].ToString();
                    //txtSFOScheduleNo.Text = dr["ScheduleNo"].ToString();
                    //sSFOScheduleNo = dr["ScheduleNo"].ToString();

                    SetItemInfoReadOnly(false);
                    //txtSFOScanQty.Focus();
                }
                else
                {
                    MessageBox.Show("No items returned for the supplied Shop Floor Order", "SFO Not Found");
                    sSFONo = String.Empty;
                    txtShopFloorNo.Text = String.Empty;
                    txtShopFloorNo.Focus();
                }
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2501, "GetScheduleAndItemFromSFO");
                else
                    MessageBox.Show(String.Format("Error Retreiving Schedule from Shop Floor Order ({0}): {1}", sSFONo, ex.Message), "Data Retrieval");
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                dtItemsFromScan.Dispose();
            }
        }

        private void BtnSFOBackoutScan_Click(object sender, EventArgs e)
        {
            //int iQtyScanned = 0;
            //int iOut = 0;
            //int.TryParse(txtSFOScanQty.Text.Trim(), out iOut);
            //if (txtSFOScanQty.Text.Trim() != "0" && iOut != 0)
            //{
            //    iQtyScanned = iOut;
            //    iQtyScanned = iQtyScanned - 1;
            //    iSFOScanQty = iQtyScanned;
            //    txtSFOScanQty.Text = iQtyScanned.ToString();
            //    CheckSFOScanQty();
            //}
            //else
            //{
            //    MessageBox.Show("The Scanned Quantity value is nothing or not a number", "Invalid Scanned Quantity");
            //    txtSFOScanQty.Focus();
            //}
        }
        #endregion

        #region HELPERS

        private void DgvList_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            this.menuLastScans.Tag = e.RowIndex;
            this.menuLastScans.Show();
        }

        private void MiReprint_Click(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;
            Int32 iScanID = 0;
            String sItemNumber = String.Empty;
            Int16 iRowIndex = Convert.ToInt16(this.menuLastScans.Tag.ToString());
            dt = new DataTable();

            bGotError = false;

            iScanID = Convert.ToInt32(dgvList.Rows[iRowIndex].Cells["colScanID"].Value);

            sItemNumber = dgvList.Rows[iRowIndex].Cells[1].Value.ToString();
            sSQL = "app_spReprint";
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();
            sqlCmd.CommandText = sSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ScanID", iScanID);
            sqlCmd.Parameters.AddWithValue("@OperatorName", this.txtOperator.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@EnvironUser", Environment.UserName);

            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Error retrieving Print Info for Order/Item", "Reprint Info");
                    return;
                }
                LoadPrintData(dt, true);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2586, "miReprint_Click");
                else
                    MessageBox.Show(String.Format("Error retrieving Print Info for Order/Item:\n\t{0}", ex.Message), "Reprint Info");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                if (!reader.IsClosed)
                    reader.Close();

                reader.Dispose();
                dt.Dispose();
            }
        }

        private void MiBackout_Click(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(comAPI.ShippingConnection);
            sqlCmd = new SqlCommand();
            sSQL = String.Empty;
            Int32 iScanID = 0;
            String sItemNumber = String.Empty;
            String sMsg = String.Empty;
            String sBackoutBayAction = String.Empty;
            bGotError = false;
            dt = new DataTable();

            Int16 iRowIndex = Convert.ToInt16(this.menuLastScans.Tag.ToString());
            iScanID = Convert.ToInt32(dgvList.Rows[iRowIndex].Cells[0].Value.ToString());
            sItemNumber = dgvList.Rows[iRowIndex].Cells[1].Value.ToString();
            sSQL = "app_spBackoutScan";
            sqlCmd.Connection = sqlCon;
            sqlCon.Open();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = sSQL;
            sqlCmd.Parameters.AddWithValue("@ScanID", iScanID);
            sqlCmd.Parameters.AddWithValue("@ScannedQty", 1);
            sqlCmd.Parameters.AddWithValue("@OperatorName", this.txtOperator.Text.Trim());
            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);
                sMsg = String.Format("Backout Complete:\n\n\tOrderNumber:\t{0}\n\tItem Number:\t{1}\n\tBay Location:\t{2}\n", dt.Rows[0]["OrderNumber"].ToString(), dt.Rows[0]["ItemNumber"].ToString(), dt.Rows[0]["BayLocation"].ToString());

                switch (dt.Rows[0]["BackoutBay"].ToString())
                {
                    case "SUBTRACT 1":
                        sBackoutBayAction = "Bay has been reduced by 1 for Item scanned";
                        break;
                    case "DELETE Bay Detail":
                        sBackoutBayAction = "With backout of this item, no more of this\n\t\titem is left in bay, so item removed from bay";
                        break;
                    case "DELETE Bay":
                        sBackoutBayAction = "With backout of this item, no items yet picked\n\t\t\tfor order therefor bay has been removed";
                        break;
                    case "DELETE Bay - Shipment Voided":
                        sBackoutBayAction = "With backout of this item, no items yet picked\n\t\t\tfor order therefor bay has been removed\n\t\t\tThe Shipment Record has been VOIDED";
                        break;
                }
                sMsg = sMsg + "\tBayAction:\t" + sBackoutBayAction;
                dt.Dispose();

                MessageBox.Show(sMsg, "Backout Scan");
                dgvList.Rows.RemoveAt(iRowIndex);
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 2651, "miBackout_Click");
                else
                    MessageBox.Show(String.Format("Error backing out scan:\n\t{0}", ex.Message), "Backout Scan");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
                sqlCmd.Dispose();
                reader.Dispose();
            }
        }

        private void ShowErrorDetails(Exception ex, int iLineNumber, String sFunction)
        {
            String sException = ex.Message;
            String sInnerException = String.Empty;
            if (ex.InnerException != null)
                sInnerException = ex.InnerException.ToString();
            String sSource = ex.Source.ToString();
            String sType = ex.GetType().ToString();

            string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            var sLineNumText = String.Empty;
            if (index != -1)
                sLineNumText = ex.StackTrace.Substring(index + lineSearch.Length);

            String sFullException = String.Format("Type:{0}\nSource:{1}\nFunction:{2}\nLine:{3}\nSourceCodeLine:{4}\nException:{5}\nInnerEx:{6}", sType, sSource, sFunction, iLineNumber.ToString(), sLineNumText, sException, sInnerException);

            MessageBox.Show(sFullException, "Error Details");
        }

        static Boolean CheckPrinterConnected(String sPrinterName)
        {
            ManagementScope mgmtScope = new ManagementScope(@"\root\cimv2");
            mgmtScope.Connect();
            ManagementObjectSearcher objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            foreach (ManagementObject printer in objSearcher.Get())
            {
                if (printer["Name"].Equals(sPrinterName))
                {
                    return true;
                }
            }
            return true;
        }

        private void BtnScanToOrder_Click(object sender, EventArgs e)
        {
            if (btnScanToOrder.Text == "Start Scan-to-Order")
            {
                btnScanToOrder.Text = "End Scan-to-Order";
                txtScanToOrder.Visible = true;
                txtScanToOrder.Focus();
            }
            else
            {
                btnScanToOrder.Text = "Start Scan-to-Order";
                txtScanToOrder.Text = String.Empty;
                txtScanToOrder.Visible = false;
            }
        }

        private void RbScanMode_CheckChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            String rbName = rb.Name;
            switch (rbName)
            {
                case "rbNormal":
                    txtScanToOrder.Text = String.Empty;
                    txtScanToCustomer.Text = String.Empty;
                    txtScanToOrder.Visible = false;
                    txtScanToCustomer.Visible = false;
                    lblModeMsg.Visible = false;
                    bSingleItemOrders = false;
                    break;
                case "rbSingle":
                    txtScanToOrder.Text = String.Empty;
                    txtScanToCustomer.Text = String.Empty;
                    txtScanToOrder.Visible = false;
                    txtScanToCustomer.Visible = false;
                    lblModeMsg.Visible = false;
                    bSingleItemOrders = true;
                    break;
                case "rbCustomer":
                    txtScanToOrder.Text = String.Empty;
                    txtScanToCustomer.Text = String.Empty;
                    txtScanToOrder.Visible = false;
                    txtScanToCustomer.Visible = true;
                    txtScanToCustomer.Focus();
                    lblModeMsg.Visible = false;
                    bSingleItemOrders = false;
                    break;
                case "rbOrder":
                    txtScanToOrder.Text = String.Empty;
                    txtScanToCustomer.Text = String.Empty;
                    txtScanToOrder.Visible = true;
                    txtScanToOrder.Focus();
                    txtScanToCustomer.Visible = false;
                    lblModeMsg.Visible = false;
                    bSingleItemOrders = false;
                    break;
            }
        }

        private void ScanModeTextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length < 8)
                return;

            String tbName = tb.Name;
            String sql;
            sqlCon = new SqlConnection
            {
                ConnectionString = comAPI.MacolaConnection
            };

            switch (tbName)
            {
                case "txtScanToOrder":
                    sql = String.Format("SELECT 1 FROM dbo.OEORDHDR_SQL WHERE ord_no = '{0}'", txtScanToOrder.Text);
                    sScanToOrder = txtScanToOrder.Text;
                    break;
                case "txtScanToCustomer":
                    sql = String.Format("SELECT 1 FROM dbo.OEORDHDR_SQL WHERE cus_no = '{0}'", txtScanToCustomer.Text);
                    sScanToCustomer = txtScanToCustomer.Text;
                    break;
                default:
                    sql = "";
                    break;
            }
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand(sql, sqlCon);

                object o = sqlCmd.ExecuteScalar();
                if (Convert.ToInt16(o) != 1)
                {
                    lblModeMsg.Visible = true;
                    tb.Text = String.Empty;
                    tb.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error validating {0} entry: {1}", tbName == "txtScanToOrder" ? "Order Number" : "Customer Number", ex.Message));
            }
            finally
            {
                sqlCon.Close();
                sqlCmd.Dispose();
            }
        }

        private void ClearSQLExpressTables(String sTable = null, String sOrdNo = null, String sColumName = null)
        {

            sqlCon = new SqlConnection
            {
                ConnectionString = comAPI.SLSShippingAppConnection
            };
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlCon;

            String[] arrTables = { "tblLabel", "tblTickets", "tblPickingFile", "tblRetailerPackingLabel", "tblLabelFanBrands", "tblLabelQuickShip", "tblTicketsArch" };//"tblOrderItemMismatchMacola", "tblOrderItemsNotInMacola", "tblPostingLog", "tblQuickShipReport",

            try
            {
                for (int t = 0; t < arrTables.Length; t++)
                {
                    if (sTable == null || sTable == arrTables[t])
                    {
                        sSQL = String.Format("DELETE FROM SLSShippingApp.dbo.{0}", arrTables[t]);
                        if (sOrdNo != null)
                            sSQL += String.Format(" WHERE {0} = '{1}'", sColumName, sOrdNo);

                        sqlCmd.CommandText = sSQL;
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (comAPI.ShowVerbose)//bVerbose)
                    ShowErrorDetails(ex, 680, "ClearSQLExpressTables");
                else
                    MessageBox.Show(String.Format("Error Deleting print data\n\t{0}", ex.Message));
                return;
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }

        private void SLSShipping_Closing(object sender, FormClosingEventArgs e)
        {
            ClearSQLExpressTables();
            if (shiprushPanel.Connected)
              shiprushPanel.Disconnect();
            this.Dispose();
        }

        #endregion

        #region ShipRush Shipping

        private void ShipOrder(String sOrdNo)
        {
            if (comAPI.doTimeCheck)
                timeMsg += String.Format("Peform Shipment Procedure entered: {0} ms\r\n", watch.ElapsedMilliseconds);
            String sTrackingNumber = String.Empty;
            Double dblFreight = 0.0D;
            Int32 iZone = 0;
            String sCarrierCode = "U";
            String sCarrierMode = "G";
            Int32 iShipWeight = 0;

            sqlCon = new SqlConnection
            {
                ConnectionString = comAPI.SLSShippingAppConnection
            };
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlCon;
            String sSql = "SELECT * FROM tblPickingFile";
            DataTable dt = new DataTable("ShippingInfo");
            sqlCmd.CommandText = sSql;
            sqlCmd.CommandType = CommandType.Text;
           
            try
            {
                reader = sqlCmd.ExecuteReader();
                dt.Load(reader);
                if (comAPI.doTimeCheck)
                    timeMsg += String.Format("Shipment/Order details loaded into DataTable: {0} ms\r\n", watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error loading Shipment Data from local table: {0}", ex.Message), "Data Retrieval Error");
                return;
            }
            finally
            {
                sqlCon.Close();
                sqlCmd.Dispose();
                sqlCon.Dispose();
                // dt.Dispose();
            }
            if (comAPI.doTimeCheck)
                timeMsg += String.Format("Check for Existing ShipRush Connection: {0} ms\r\n", watch.ElapsedMilliseconds);

            String sShipVia = comAPI.ParseShipVia(dt.Rows[0]["ShipViaCode"].ToString().Trim());

            if (!shiprushPanel.Connected)
            {
                try
                {
                    if (comAPI.LiveShipping)//(Convert.ToBoolean(ConfigurationManager.AppSettings.Get("LiveShipping").ToString()) == true)
                        shiprushPanel.SerialNumber = comAPI.ShipRushSerialNumber;// ConfigurationManager.AppSettings.Get("ShiprushSerialNumber").ToString();
                    else
                        shiprushPanel.SerialNumber = comAPI.TestShipRushSerialNumber;// ConfigurationManager.AppSettings.Get("TestShiprushSerialNumber").ToString();

                    shiprushPanel.CarrierType = comAPI.GetShipViaTranslation(sShipVia);// dt.Rows[0]["ShipViaCode"].ToString().Trim());
                    shiprushPanel.Connect();
                    if (comAPI.doTimeCheck)
                        timeMsg += String.Format("ShipRush Connection Established: {0} ms\r\n", watch.ElapsedMilliseconds);
                    if (!shiprushPanel.Connected)
                        MessageBox.Show(shiprushPanel.ErrorMessage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Unable to conenct to ShipRush: {0}", ex.Message), "ShipRush Error");
                    return;
                }
            }

            //these only appear to be necessary for APO/FPO/International shipments
            // ZFCommodity commodity;
            // Int32 i = 0;
            if (comAPI.doTimeCheck)
                timeMsg += String.Format("Shiprush StartNewShipment(): {0} ms\r\n", watch.ElapsedMilliseconds);
            shiprushPanel.StartNewShipment();
            shiprushPanel.Settings.SaveLabels = false;
            shiprushPanel.Settings.AutoAddressValidation = true;
            shiprushPanel.ShowErrors = true;
            shiprushPanel.ShowProgress = false;
            shiprushPanel.Modal = false;
            //shiprushPanel.SlaveMode = ?

            shiprushPanel.Shipment.ToAddress.Company = dt.Rows[0]["ShipToName"].ToString().Trim();
            shiprushPanel.Shipment.ToAddress.Name = dt.Rows[0]["ShipToName"].ToString().Trim();
            shiprushPanel.Shipment.ToAddress.Address1 = dt.Rows[0]["ShipToAddress1"].ToString().Trim();
            shiprushPanel.Shipment.ToAddress.Address2 = dt.Rows[0]["ShipToAddress2"].ToString().Trim();
            shiprushPanel.Shipment.ToAddress.Address3 = dt.Rows[0]["ShipToAddress3"].ToString().Trim();
            shiprushPanel.Shipment.ToAddress.City = comAPI.GetCityFromAddress(dt.Rows[0]["ShipToCity"].ToString().Trim());
            shiprushPanel.Shipment.ToAddress.ZIP = comAPI.GetZipFromAddress(dt.Rows[0]["ShipToCity"].ToString().Trim());
            shiprushPanel.Shipment.ToAddress.Phone = dt.Rows[0]["ShipToNumber"].ToString().Trim();
            shiprushPanel.Shipment.ToAddress.State = comAPI.GetStateTranslation(dt.Rows[0]["ShipToCity"].ToString().Trim());
            shiprushPanel.Shipment.ToAddress.Country = comAPI.GetCountryTranslation(dt.Rows[0]["ShipToState"].ToString().Trim());

            //The Shiprush SDK states that installing Shiprush for testing, account 123555 has to be used, with ZipCode 67840
            //I didn't install Shiprush (on my pc) using this test account, so I'm trying to manipulate the UPS account
            //in code, for the following two lines
            //  shiprushPanel.Shipment.FromAddress.Account = "123555";
            // shiprushPanel.Shipment.FromAddress.UPSAccount = "123555";

            if (comAPI.UseHouseAccount == false && dt.Rows[0]["CustShipperAcct"].ToString().Trim().Length > 0)
                shiprushPanel.Shipment.FromAddress.Account = dt.Rows[0]["CustShipperAcct"].ToString().Trim();
            else
                shiprushPanel.Shipment.FromAddress.Account = comAPI.UPSAcct;
            //UNCOMMENT THE ABOVE LINE. ALL ALTERNATE (CUSTOMER) SHIPPING ACCOUNTS WILL HAVE TO BE ADDED
            //TO SHIPRUSH
            #region APO/FPO/INTERNATIONAL
            //Theres a lot of code that has to happen if either of these is true
            //Boolean isApoFpo = false;
            //Boolean isInternational = false;
            //if (dt.Rows[0]["ShipToCity"].ToString().Trim() == "APO" || dt.Rows[0]["ShipToCity"].ToString().Trim() == "FPO")
            //    isApoFpo = true;
            //if (GetCountryInt(dt.Rows[0]["ShipToCountry"].ToString().Trim()) > 0)
            //    isInternational = true;

            ////shipment validation is required for APO/FPO packages
            //if (isApoFpo)
            //    shiprushPanel.ValidateShipment();
            ////International shipments need extra steps, make suer the list of commodities is empty
            //if (isApoFpo || isInternational)
            //{
            //    while (shiprushPanel.Shipment.International.CommoditiesCount > 0)
            //        shiprushPanel.Shipment.International.DeleteCommodity(0);

            //    shiprushPanel.Shipment.International.DescriptionOfGoods = "Put some generic description here?";

            //    shiprushPanel.Shipment.DocInd = 1; //?
            //    //add commodity (always required for USPS shipments)
            //    commodity = shiprushPanel.Shipment.International.AddCommodity();

            //    //I'm bailing out here, there is much more in the NonVisual - VB shiprush project
            //}

            //'StartNewShipment() creates a single parcel shipment.
            //'to add packages (e.g. create an mps shipment), call AddPackage for each package to add. E.g.
            //'AxZFShippingPanel1.Shipment.AddPackage()
            #endregion

            //Service
            shiprushPanel.Shipment.Service.ServiceType = comAPI.GetShippingService(sShipVia);// dt.Rows[0]["ShipViaCode"].ToString().Trim());
            sCarrierCode = comAPI.GetCarrierCode(sShipVia);// dt.Rows[0]["ShipViaCode"].ToString().Trim());
            sCarrierMode = comAPI.GetCarrierMode(sShipVia);// dt.Rows[0]["ShipViaCode"].ToString().Trim());

            //'make it return services if appro, see FAQ in the SDK docs for some info
            //'see SDK docs for : TRetService: Return service type
            //I don't think we need this because customers call in for returns.
            //shiprushPanel.Shipment.ERLEmail = dt.Rows[0]["ShipToEmail"].ToString().Trim();
            iShipWeight = Convert.ToInt32(Math.Floor(Convert.ToDouble(dt.Rows[0]["OrderWeight"].ToString().Trim())));
            shiprushPanel.Shipment.Packages(0).Weight = iShipWeight;
            shiprushPanel.Shipment.Packages(0).PackagingType = 0;

            if (comAPI.doTimeCheck)
                timeMsg += String.Format("Shipment/Order details loaded into Shiprush.Shipment(): {0} ms\r\n", watch.ElapsedMilliseconds);

            try
            {
                if (comAPI.doTimeCheck)
                    timeMsg += String.Format("Execute Shiprush.Ship(): {0} ms\r\n", watch.ElapsedMilliseconds);
                shiprushPanel.Ship();
                if (comAPI.doTimeCheck)
                    timeMsg += String.Format("Shiprush.Ship() completed: {0} ms\r\n", watch.ElapsedMilliseconds);
                sTrackingNumber = shiprushPanel.Shipment.TrackingNumber;
                dblFreight = shiprushPanel.Shipment.EffectiveCarrierCharges*.01; //?? These Starship.Shipment values are returned as integers
                dblFreight = shiprushPanel.Shipment.ShippingCharges*.01; //?? so 15.01 is returned as 1501
                iZone = Convert.ToInt32(shiprushPanel.Shipment.UPSZone);
                if (comAPI.doTimeCheck)
                    timeMsg += String.Format("TrackingNumber, Freight charge retrieved: {0} ms\r\n", watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Shiprush returned and error for this shipment: {0}\r\n{1}", shiprushPanel.ErrorMessage.ToString(), ex.Message), "Shipping Error");
                return;
            }

            if (sTrackingNumber != null &&  sTrackingNumber.Length > 0)
            {
                if (comAPI.doTimeCheck)
                    timeMsg += String.Format("Create ARSHTFIL_SQL insert SQL string: {0} ms\r\n", watch.ElapsedMilliseconds);
                sSql = String.Format("INSERT INTO ARSHTFIL_SQL(ord_no,shipment_no,carrier_cd,mode,ship_cost,tracking_no,zone,ship_weight,complete_fg,ship_dt) " +
                       " VALUES('{0}',1,'{1}','{2}',{3},'{4}',{5},{6},'P',CONVERT(INT, CONVERT(VARCHAR, GETDATE(), 112)))", sOrdNo, sCarrierCode, sCarrierMode, dblFreight, sTrackingNumber, iZone, iShipWeight);
                
                sqlCon = new SqlConnection(comAPI.MacolaConnection);
                sqlCmd = new SqlCommand(sSql, sqlCon);
                sqlCon.Open();
                sqlCmd.CommandText = sSql;
                sqlCmd.CommandType = CommandType.Text;

                try
                {
                    sqlCmd.ExecuteNonQuery();
                    if (comAPI.doTimeCheck)
                    {
                        sSQL = String.Format("DELETE FROM ARSHTFIL_SQL WHERE ord_no = '{0}'", sOrdNo);
                        sqlCmd.CommandText = sSQL;
                        sqlCmd.ExecuteNonQuery();
                        timeMsg += String.Format("ARSHTFIL_SQL record insert (and deletion) complete: {0} ms\r\n", watch.ElapsedMilliseconds);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Error creating ARSHTFIL_SQL record for Order Shipment: {0}", ex.Message), "Macola Shipment Record Error");

                }
                finally
                {
                    sqlCon.Close();
                    sqlCon.Dispose();
                    sqlCmd.Dispose();
                    if (comAPI.doTimeCheck)
                        timeMsg += String.Format("Exit Shiprush.Shippment function: {0} ms\r\n", watch.ElapsedMilliseconds);
                }
            }
        }

        #endregion  
    }
}