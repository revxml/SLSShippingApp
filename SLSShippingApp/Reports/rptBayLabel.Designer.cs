namespace SLSShippingApp.Reports
{
    partial class rptBayLabel
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
            DevExpress.DataAccess.Sql.Column column8 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression8 = new DevExpress.DataAccess.Sql.ColumnExpression();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptBayLabel));
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.lblDateTime = new DevExpress.XtraReports.UI.XRLabel();
            this.label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblShippigDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblWeight = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSize = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblDueDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOrdWeight = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOrdSize = new DevExpress.XtraReports.UI.XRLabel();
            this.lblOrdNo = new DevExpress.XtraReports.UI.XRLabel();
            this.topLine = new DevExpress.XtraReports.UI.XRLine();
            this.lblCustomer = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCusNo = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "SLSShippingAppConnection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            columnExpression1.ColumnName = "OrderNumber";
            table1.Name = "tblLabel";
            columnExpression1.Table = table1;
            column1.Expression = columnExpression1;
            columnExpression2.ColumnName = "ShippingDate";
            columnExpression2.Table = table1;
            column2.Expression = columnExpression2;
            columnExpression3.ColumnName = "OrderSize";
            columnExpression3.Table = table1;
            column3.Expression = columnExpression3;
            columnExpression4.ColumnName = "CustomerNum";
            columnExpression4.Table = table1;
            column4.Expression = columnExpression4;
            columnExpression5.ColumnName = "CustomerName";
            columnExpression5.Table = table1;
            column5.Expression = columnExpression5;
            columnExpression6.ColumnName = "IsKit";
            columnExpression6.Table = table1;
            column6.Expression = columnExpression6;
            columnExpression7.ColumnName = "OrderWeight";
            columnExpression7.Table = table1;
            column7.Expression = columnExpression7;
            columnExpression8.ColumnName = "BayLocation";
            columnExpression8.Table = table1;
            column8.Expression = columnExpression8;
            selectQuery1.Columns.Add(column1);
            selectQuery1.Columns.Add(column2);
            selectQuery1.Columns.Add(column3);
            selectQuery1.Columns.Add(column4);
            selectQuery1.Columns.Add(column5);
            selectQuery1.Columns.Add(column6);
            selectQuery1.Columns.Add(column7);
            selectQuery1.Columns.Add(column8);
            selectQuery1.Name = "tblLabel";
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
            this.BottomMargin.HeightF = 5F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // lblDateTime
            // 
            this.lblDateTime.CanGrow = false;
            this.lblDateTime.LocationFloat = new DevExpress.Utils.PointFloat(115.7547F, 4.254726F);
            this.lblDateTime.Multiline = true;
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDateTime.SizeF = new System.Drawing.SizeF(259.434F, 23F);
            this.lblDateTime.Text = "Now()";
            // 
            // label1
            // 
            this.label1.CanGrow = false;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.LocationFloat = new DevExpress.Utils.PointFloat(7.886807F, 2.22643F);
            this.label1.Name = "label1";
            this.label1.SizeF = new System.Drawing.SizeF(98.75471F, 24.19433F);
            this.label1.StyleName = "Title";
            this.label1.StylePriority.UseFont = false;
            this.label1.StylePriority.UseForeColor = false;
            this.label1.Text = "Bay Label";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblShippigDate,
            this.lblWeight,
            this.lblSize,
            this.lblNumber,
            this.lblName,
            this.lblCNumber,
            this.xrLabel1,
            this.lblDueDate,
            this.lblOrdWeight,
            this.lblOrdSize,
            this.lblOrdNo,
            this.topLine,
            this.lblDateTime,
            this.label1,
            this.lblCustomer,
            this.lblCusNo});
            this.Detail.HeightF = 212.8302F;
            this.Detail.Name = "Detail";
            // 
            // lblShippigDate
            // 
            this.lblShippigDate.CanGrow = false;
            this.lblShippigDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ShippingDate]")});
            this.lblShippigDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblShippigDate.LocationFloat = new DevExpress.Utils.PointFloat(115.7547F, 179.7264F);
            this.lblShippigDate.Multiline = true;
            this.lblShippigDate.Name = "lblShippigDate";
            this.lblShippigDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblShippigDate.SizeF = new System.Drawing.SizeF(153.7736F, 23F);
            this.lblShippigDate.StylePriority.UseFont = false;
            this.lblShippigDate.StylePriority.UseTextAlignment = false;
            this.lblShippigDate.Text = "lblShippigDate";
            this.lblShippigDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblShippigDate.TextFormatString = "{0:d}";
            // 
            // lblWeight
            // 
            this.lblWeight.CanGrow = false;
            this.lblWeight.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OrderWeight]")});
            this.lblWeight.LocationFloat = new DevExpress.Utils.PointFloat(115.7547F, 150.4811F);
            this.lblWeight.Multiline = true;
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblWeight.SizeF = new System.Drawing.SizeF(152.8302F, 23F);
            this.lblWeight.StylePriority.UseTextAlignment = false;
            this.lblWeight.Text = "lblWeight";
            this.lblWeight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblSize
            // 
            this.lblSize.CanGrow = false;
            this.lblSize.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OrderSize]")});
            this.lblSize.LocationFloat = new DevExpress.Utils.PointFloat(116.6981F, 120.2924F);
            this.lblSize.Multiline = true;
            this.lblSize.Name = "lblSize";
            this.lblSize.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSize.SizeF = new System.Drawing.SizeF(150.9434F, 23F);
            this.lblSize.StylePriority.UseTextAlignment = false;
            this.lblSize.Text = "lblSize";
            this.lblSize.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblNumber
            // 
            this.lblNumber.CanGrow = false;
            this.lblNumber.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OrderNumber]")});
            this.lblNumber.LocationFloat = new DevExpress.Utils.PointFloat(115.7547F, 93.87735F);
            this.lblNumber.Multiline = true;
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNumber.SizeF = new System.Drawing.SizeF(150.9434F, 23F);
            this.lblNumber.StylePriority.UseTextAlignment = false;
            this.lblNumber.Text = "lblNumber";
            this.lblNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.CanGrow = false;
            this.lblName.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustomerName]")});
            this.lblName.LocationFloat = new DevExpress.Utils.PointFloat(115.7547F, 65.57547F);
            this.lblName.Multiline = true;
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblName.SizeF = new System.Drawing.SizeF(273.5849F, 23F);
            this.lblName.StylePriority.UseTextAlignment = false;
            this.lblName.Text = "lblName";
            this.lblName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCNumber
            // 
            this.lblCNumber.CanGrow = false;
            this.lblCNumber.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustomerNum]")});
            this.lblCNumber.LocationFloat = new DevExpress.Utils.PointFloat(114.8113F, 38.21698F);
            this.lblCNumber.Multiline = true;
            this.lblCNumber.Name = "lblCNumber";
            this.lblCNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCNumber.SizeF = new System.Drawing.SizeF(272.6415F, 23F);
            this.lblCNumber.StylePriority.UseTextAlignment = false;
            this.lblCNumber.Text = "lblCNumber";
            this.lblCNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Black;
            this.xrLabel1.CanGrow = false;
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BayLocation]")});
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 20F);
            this.xrLabel1.ForeColor = System.Drawing.Color.White;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(276.1321F, 94.82075F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(116.0378F, 107.9056F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.Text = "xrLabel1";
            // 
            // lblDueDate
            // 
            this.lblDueDate.CanGrow = false;
            this.lblDueDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDueDate.LocationFloat = new DevExpress.Utils.PointFloat(7.264163F, 179.7264F);
            this.lblDueDate.Multiline = true;
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblDueDate.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblDueDate.StylePriority.UseFont = false;
            this.lblDueDate.StylePriority.UseTextAlignment = false;
            this.lblDueDate.Text = "Due Date:";
            this.lblDueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblOrdWeight
            // 
            this.lblOrdWeight.CanGrow = false;
            this.lblOrdWeight.LocationFloat = new DevExpress.Utils.PointFloat(6.320766F, 151.4245F);
            this.lblOrdWeight.Multiline = true;
            this.lblOrdWeight.Name = "lblOrdWeight";
            this.lblOrdWeight.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOrdWeight.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblOrdWeight.StylePriority.UseTextAlignment = false;
            this.lblOrdWeight.Text = "Order Weight:";
            this.lblOrdWeight.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblOrdSize
            // 
            this.lblOrdSize.CanGrow = false;
            this.lblOrdSize.LocationFloat = new DevExpress.Utils.PointFloat(5.37737F, 121.2358F);
            this.lblOrdSize.Multiline = true;
            this.lblOrdSize.Name = "lblOrdSize";
            this.lblOrdSize.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOrdSize.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblOrdSize.StylePriority.UseTextAlignment = false;
            this.lblOrdSize.Text = "Order Size:";
            this.lblOrdSize.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblOrdNo
            // 
            this.lblOrdNo.CanGrow = false;
            this.lblOrdNo.LocationFloat = new DevExpress.Utils.PointFloat(9.150955F, 93.87735F);
            this.lblOrdNo.Multiline = true;
            this.lblOrdNo.Name = "lblOrdNo";
            this.lblOrdNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblOrdNo.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblOrdNo.StylePriority.UseTextAlignment = false;
            this.lblOrdNo.Text = "Order Number:";
            this.lblOrdNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // topLine
            // 
            this.topLine.LineWidth = 3F;
            this.topLine.LocationFloat = new DevExpress.Utils.PointFloat(9.150955F, 27.89622F);
            this.topLine.Name = "topLine";
            this.topLine.SizeF = new System.Drawing.SizeF(365.0943F, 6.962269F);
            // 
            // lblCustomer
            // 
            this.lblCustomer.CanGrow = false;
            this.lblCustomer.LocationFloat = new DevExpress.Utils.PointFloat(8.490566F, 67.17924F);
            this.lblCustomer.Multiline = true;
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCustomer.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblCustomer.StylePriority.UseTextAlignment = false;
            this.lblCustomer.Text = "Customer:";
            this.lblCustomer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblCusNo
            // 
            this.lblCusNo.CanGrow = false;
            this.lblCusNo.LocationFloat = new DevExpress.Utils.PointFloat(9.433962F, 37.93395F);
            this.lblCusNo.Multiline = true;
            this.lblCusNo.Name = "lblCusNo";
            this.lblCusNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCusNo.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.lblCusNo.StylePriority.UseTextAlignment = false;
            this.lblCusNo.Text = "Customer No:";
            this.lblCusNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // rptBayLabel
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "tblLabel";
            this.DataSource = this.sqlDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(3, 3, 5, 5);
            this.PageHeight = 200;
            this.PageWidth = 400;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.RollPaper = true;
            this.ShowPrintMarginsWarning = false;
            this.ShowPrintStatusDialog = false;
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
        private DevExpress.XtraReports.UI.XRLabel lblDateTime;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lblCustomer;
        private DevExpress.XtraReports.UI.XRLabel lblCusNo;
        private DevExpress.XtraReports.UI.XRLine topLine;
        private DevExpress.XtraReports.UI.XRLabel lblOrdWeight;
        private DevExpress.XtraReports.UI.XRLabel lblOrdSize;
        private DevExpress.XtraReports.UI.XRLabel lblOrdNo;
        private DevExpress.XtraReports.UI.XRLabel lblDueDate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lblShippigDate;
        private DevExpress.XtraReports.UI.XRLabel lblWeight;
        private DevExpress.XtraReports.UI.XRLabel lblSize;
        private DevExpress.XtraReports.UI.XRLabel lblNumber;
        private DevExpress.XtraReports.UI.XRLabel lblName;
        private DevExpress.XtraReports.UI.XRLabel lblCNumber;
    }
}
