namespace SLSShippingApp.Reports
{
    partial class rptPackingLabels
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.SelectQuery selectQuery1 = new DevExpress.DataAccess.Sql.SelectQuery();
            DevExpress.DataAccess.Sql.Column column1 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression1 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Table table1 = new DevExpress.DataAccess.Sql.Table();
            DevExpress.DataAccess.Sql.Column column2 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression2 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column3 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression3 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column4 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression4 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column5 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression5 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column6 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression6 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column7 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression7 = new DevExpress.DataAccess.Sql.ColumnExpression();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptPackingLabels));
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle1 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblPTBarCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.lblCusNo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPO = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCustomerNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCustomer = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPONumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNumBoxes = new DevExpress.XtraReports.UI.XRLabel();
            this.lblShipVia = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "SLSShippingAppConnection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            columnExpression1.ColumnName = "PT";
            table1.Name = "tblTickets";
            columnExpression1.Table = table1;
            column1.Expression = columnExpression1;
            columnExpression2.ColumnName = "CustomerNumber";
            columnExpression2.Table = table1;
            column2.Expression = columnExpression2;
            columnExpression3.ColumnName = "CustomerName";
            columnExpression3.Table = table1;
            column3.Expression = columnExpression3;
            columnExpression4.ColumnName = "PONumber";
            columnExpression4.Table = table1;
            column4.Expression = columnExpression4;
            columnExpression5.ColumnName = "ShipViaDescription";
            columnExpression5.Table = table1;
            column5.Expression = columnExpression5;
            columnExpression6.ColumnName = "BoxCount";
            columnExpression6.Table = table1;
            column6.Expression = columnExpression6;
            columnExpression7.ColumnName = "NumberOfBoxes";
            columnExpression7.Table = table1;
            column7.Expression = columnExpression7;
            selectQuery1.Columns.Add(column1);
            selectQuery1.Columns.Add(column2);
            selectQuery1.Columns.Add(column3);
            selectQuery1.Columns.Add(column4);
            selectQuery1.Columns.Add(column5);
            selectQuery1.Columns.Add(column6);
            selectQuery1.Columns.Add(column7);
            selectQuery1.Name = "tblTickets";
            selectQuery1.Tables.Add(table1);
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Arial", 14.25F);
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Title.Name = "Title";
            // 
            // DetailCaption1
            // 
            this.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DetailCaption1.BorderColor = System.Drawing.Color.White;
            this.DetailCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.DetailCaption1.BorderWidth = 2F;
            this.DetailCaption1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.DetailCaption1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.DetailCaption1.Name = "DetailCaption1";
            this.DetailCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailData1
            // 
            this.DetailData1.BorderColor = System.Drawing.Color.Transparent;
            this.DetailData1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.DetailData1.BorderWidth = 2F;
            this.DetailData1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.DetailData1.ForeColor = System.Drawing.Color.Black;
            this.DetailData1.Name = "DetailData1";
            this.DetailData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailData3_Odd
            // 
            this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent;
            this.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DetailData3_Odd.BorderWidth = 1F;
            this.DetailData3_Odd.Font = new System.Drawing.Font("Arial", 8.25F);
            this.DetailData3_Odd.ForeColor = System.Drawing.Color.Black;
            this.DetailData3_Odd.Name = "DetailData3_Odd";
            this.DetailData3_Odd.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageInfo
            // 
            this.PageInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.PageInfo.Name = "PageInfo";
            this.PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 5F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 6.452798F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblShipVia,
            this.lblNumBoxes,
            this.lblPONumber,
            this.lblCustomer,
            this.lblCustomerNumber,
            this.lblPO,
            this.lblName,
            this.lblCusNo,
            this.xrShape1,
            this.lblPTBarCode});
            this.Detail.HeightF = 194.2453F;
            this.Detail.Name = "Detail";
            // 
            // lblPTBarCode
            // 
            this.lblPTBarCode.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PT]")});
            this.lblPTBarCode.Font = new System.Drawing.Font("3 of 9 Barcode", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPTBarCode.LocationFloat = new DevExpress.Utils.PointFloat(24.24529F, 0F);
            this.lblPTBarCode.Name = "lblPTBarCode";
            this.lblPTBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblPTBarCode.SizeF = new System.Drawing.SizeF(362.2642F, 53.18868F);
            this.lblPTBarCode.StylePriority.UseFont = false;
            this.lblPTBarCode.StylePriority.UseTextAlignment = false;
            this.lblPTBarCode.Text = "lblPTBarCode";
            this.lblPTBarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrShape1
            // 
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(22.3585F, 55.19811F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeRectangle1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(365.0944F, 101.3019F);
            // 
            // lblCusNo
            // 
            this.lblCusNo.Font = new System.Drawing.Font("Arial", 12F);
            this.lblCusNo.LocationFloat = new DevExpress.Utils.PointFloat(43.4906F, 60.85849F);
            this.lblCusNo.Multiline = true;
            this.lblCusNo.Name = "lblCusNo";
            this.lblCusNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblCusNo.SizeF = new System.Drawing.SizeF(105.6604F, 23F);
            this.lblCusNo.StylePriority.UseFont = false;
            this.lblCusNo.StylePriority.UseTextAlignment = false;
            this.lblCusNo.Text = "Customer No:";
            this.lblCusNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial", 12F);
            this.lblName.LocationFloat = new DevExpress.Utils.PointFloat(43.4906F, 93.44337F);
            this.lblName.Multiline = true;
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblName.SizeF = new System.Drawing.SizeF(106.6038F, 23F);
            this.lblName.StylePriority.UseFont = false;
            this.lblName.StylePriority.UseTextAlignment = false;
            this.lblName.Text = "Customer:";
            this.lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblPO
            // 
            this.lblPO.Font = new System.Drawing.Font("Arial", 12F);
            this.lblPO.LocationFloat = new DevExpress.Utils.PointFloat(43.4906F, 124.0849F);
            this.lblPO.Multiline = true;
            this.lblPO.Name = "lblPO";
            this.lblPO.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblPO.SizeF = new System.Drawing.SizeF(106.6038F, 23F);
            this.lblPO.StylePriority.UseFont = false;
            this.lblPO.StylePriority.UseTextAlignment = false;
            this.lblPO.Text = "PO:";
            this.lblPO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblCustomerNumber
            // 
            this.lblCustomerNumber.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustomerNumber]")});
            this.lblCustomerNumber.Font = new System.Drawing.Font("Arial", 12F);
            this.lblCustomerNumber.LocationFloat = new DevExpress.Utils.PointFloat(166.717F, 60.85849F);
            this.lblCustomerNumber.Multiline = true;
            this.lblCustomerNumber.Name = "lblCustomerNumber";
            this.lblCustomerNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblCustomerNumber.SizeF = new System.Drawing.SizeF(206.6038F, 23F);
            this.lblCustomerNumber.StylePriority.UseFont = false;
            this.lblCustomerNumber.StylePriority.UseTextAlignment = false;
            this.lblCustomerNumber.Text = "lblCustomerNumber";
            this.lblCustomerNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCustomer
            // 
            this.lblCustomer.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustomerName]")});
            this.lblCustomer.Font = new System.Drawing.Font("Arial", 12F);
            this.lblCustomer.LocationFloat = new DevExpress.Utils.PointFloat(164.8302F, 93.38677F);
            this.lblCustomer.Multiline = true;
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblCustomer.SizeF = new System.Drawing.SizeF(210.3774F, 22.99999F);
            this.lblCustomer.StylePriority.UseFont = false;
            this.lblCustomer.StylePriority.UseTextAlignment = false;
            this.lblCustomer.Text = "lblCustomer";
            this.lblCustomer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblPONumber
            // 
            this.lblPONumber.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PONumber]")});
            this.lblPONumber.Font = new System.Drawing.Font("Arial", 12F);
            this.lblPONumber.LocationFloat = new DevExpress.Utils.PointFloat(164.8302F, 124.9717F);
            this.lblPONumber.Multiline = true;
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblPONumber.SizeF = new System.Drawing.SizeF(210.3773F, 23F);
            this.lblPONumber.StylePriority.UseFont = false;
            this.lblPONumber.StylePriority.UseTextAlignment = false;
            this.lblPONumber.Text = "lblPONumber";
            this.lblPONumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblNumBoxes
            // 
            this.lblNumBoxes.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NumberOfBoxes]")});
            this.lblNumBoxes.Font = new System.Drawing.Font("Arial", 12F);
            this.lblNumBoxes.LocationFloat = new DevExpress.Utils.PointFloat(18.58492F, 161.8019F);
            this.lblNumBoxes.Multiline = true;
            this.lblNumBoxes.Name = "lblNumBoxes";
            this.lblNumBoxes.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblNumBoxes.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblNumBoxes.StylePriority.UseFont = false;
            this.lblNumBoxes.StylePriority.UseTextAlignment = false;
            this.lblNumBoxes.Text = "lblNumBoxes";
            this.lblNumBoxes.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblShipVia
            // 
            this.lblShipVia.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ShipViaDescription]")});
            this.lblShipVia.Font = new System.Drawing.Font("Arial", 12F);
            this.lblShipVia.LocationFloat = new DevExpress.Utils.PointFloat(200.6604F, 161.8019F);
            this.lblShipVia.Multiline = true;
            this.lblShipVia.Name = "lblShipVia";
            this.lblShipVia.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblShipVia.SizeF = new System.Drawing.SizeF(187.7359F, 23F);
            this.lblShipVia.StylePriority.UseFont = false;
            this.lblShipVia.StylePriority.UseTextAlignment = false;
            this.lblShipVia.Text = "lblShipVia";
            this.lblShipVia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // rptPackingLabels
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "tblTickets";
            this.DataSource = this.sqlDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 6);
            this.PageHeight = 400;
            this.PageWidth = 408;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.None;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.DetailCaption1,
            this.DetailData1,
            this.DetailData3_Odd,
            this.PageInfo});
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRControlStyle Title;
        private DevExpress.XtraReports.UI.XRControlStyle DetailCaption1;
        private DevExpress.XtraReports.UI.XRControlStyle DetailData1;
        private DevExpress.XtraReports.UI.XRControlStyle DetailData3_Odd;
        private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lblPTBarCode;
        private DevExpress.XtraReports.UI.XRLabel lblCusNo;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRLabel lblPO;
        private DevExpress.XtraReports.UI.XRLabel lblName;
        private DevExpress.XtraReports.UI.XRLabel lblPONumber;
        private DevExpress.XtraReports.UI.XRLabel lblCustomer;
        private DevExpress.XtraReports.UI.XRLabel lblCustomerNumber;
        private DevExpress.XtraReports.UI.XRLabel lblShipVia;
        private DevExpress.XtraReports.UI.XRLabel lblNumBoxes;
    }
}
