﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace SLSShippingApp.RptClasses
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/clsPickingTicket.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/clsPickingTicket.xsd", IsNullable = false)]
    public partial class clsPickingTicket
    {

        private clsPickingTicketTblPickingFile[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tblPickingFile")]
        public clsPickingTicketTblPickingFile[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/clsPickingTicket.xsd")]
    public partial class clsPickingTicketTblPickingFile
    {

        private string pKTKField;

        private int recTodayField;

        private bool recTodayFieldSpecified;

        private string txtImageNameField;

        private string pONumberField;

        private string shipToNumberField;

        private string shipToNameField;

        private string shipToAddress1Field;

        private string shipToAddress2Field;

        private string shipToAddress3Field;

        private string shipToCityField;

        private string shipToStateField;

        private string shipToZipCodeField;

        private string shipViaCodeField;

        private string customerProfileField;

        private string bayLocationField;

        private int qtyFromStockField;

        private bool qtyFromStockFieldSpecified;

        private string billToNameField;

        private string billToAddress1Field;

        private string billToAddress2Field;

        private string bIllToAddress3Field;

        private string bIllToCityField;

        private string billToStateField;

        private System.DateTime orderDateField;

        private bool orderDateFieldSpecified;

        private System.DateTime shipDateField;

        private bool shipDateFieldSpecified;

        private string shipInstructions1Field;

        private string shipInstructions2Field;

        private string custShipperAcctField;

        private string custNote1Field;

        private string custNote2Field;

        private string custNote3Field;

        private string custNote4Field;

        private string custNote5Field;

        private string retailerNameField;

        private string retailerFld1Field;

        private string retailerFld2Field;

        private string retailerFld3Field;

        private string retailerFld4Field;

        private string retailerFld5Field;

        private decimal orderWeightField;

        private bool orderWeightFieldSpecified;

        private string operatorNameField;

        private string environUserField;

        private string billToZipCodeField;

        private System.DateTime pickedDateField;

        private bool pickedDateFieldSpecified;

        private string orderOrSoNumberField;

        private int orderLineNumberField;

        private bool orderLineNumberFieldSpecified;

        private int stockNumberField;

        private bool stockNumberFieldSpecified;

        private string stockDescription1Field;

        private string stockDescription2Field;

        private string stockDescription3Field;

        private int quantityToShipField;

        private bool quantityToShipFieldSpecified;

        private string customerNumberField;

        private string customerNameField;

        private int qtyShippedTodateField;

        private bool qtyShippedTodateFieldSpecified;

        private bool holdField;

        private string noteProp65Field;

        public clsPickingTicketTblPickingFile()
        {
            this.holdField = false;
        }

        /// <remarks/>
        public string PKTK
        {
            get
            {
                return this.pKTKField;
            }
            set
            {
                this.pKTKField = value;
            }
        }

        /// <remarks/>
        public int RecToday
        {
            get
            {
                return this.recTodayField;
            }
            set
            {
                this.recTodayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RecTodaySpecified
        {
            get
            {
                return this.recTodayFieldSpecified;
            }
            set
            {
                this.recTodayFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string txtImageName
        {
            get
            {
                return this.txtImageNameField;
            }
            set
            {
                this.txtImageNameField = value;
            }
        }

        /// <remarks/>
        public string PONumber
        {
            get
            {
                return this.pONumberField;
            }
            set
            {
                this.pONumberField = value;
            }
        }

        /// <remarks/>
        public string ShipToNumber
        {
            get
            {
                return this.shipToNumberField;
            }
            set
            {
                this.shipToNumberField = value;
            }
        }

        /// <remarks/>
        public string ShipToName
        {
            get
            {
                return this.shipToNameField;
            }
            set
            {
                this.shipToNameField = value;
            }
        }

        /// <remarks/>
        public string ShipToAddress1
        {
            get
            {
                return this.shipToAddress1Field;
            }
            set
            {
                this.shipToAddress1Field = value;
            }
        }

        /// <remarks/>
        public string ShipToAddress2
        {
            get
            {
                return this.shipToAddress2Field;
            }
            set
            {
                this.shipToAddress2Field = value;
            }
        }

        /// <remarks/>
        public string ShipToAddress3
        {
            get
            {
                return this.shipToAddress3Field;
            }
            set
            {
                this.shipToAddress3Field = value;
            }
        }

        /// <remarks/>
        public string ShipToCity
        {
            get
            {
                return this.shipToCityField;
            }
            set
            {
                this.shipToCityField = value;
            }
        }

        /// <remarks/>
        public string ShipToState
        {
            get
            {
                return this.shipToStateField;
            }
            set
            {
                this.shipToStateField = value;
            }
        }

        /// <remarks/>
        public string ShipToZipCode
        {
            get
            {
                return this.shipToZipCodeField;
            }
            set
            {
                this.shipToZipCodeField = value;
            }
        }

        /// <remarks/>
        public string ShipViaCode
        {
            get
            {
                return this.shipViaCodeField;
            }
            set
            {
                this.shipViaCodeField = value;
            }
        }

        /// <remarks/>
        public string CustomerProfile
        {
            get
            {
                return this.customerProfileField;
            }
            set
            {
                this.customerProfileField = value;
            }
        }

        /// <remarks/>
        public string BayLocation
        {
            get
            {
                return this.bayLocationField;
            }
            set
            {
                this.bayLocationField = value;
            }
        }

        /// <remarks/>
        public int QtyFromStock
        {
            get
            {
                return this.qtyFromStockField;
            }
            set
            {
                this.qtyFromStockField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool QtyFromStockSpecified
        {
            get
            {
                return this.qtyFromStockFieldSpecified;
            }
            set
            {
                this.qtyFromStockFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string BillToName
        {
            get
            {
                return this.billToNameField;
            }
            set
            {
                this.billToNameField = value;
            }
        }

        /// <remarks/>
        public string BillToAddress1
        {
            get
            {
                return this.billToAddress1Field;
            }
            set
            {
                this.billToAddress1Field = value;
            }
        }

        /// <remarks/>
        public string BillToAddress2
        {
            get
            {
                return this.billToAddress2Field;
            }
            set
            {
                this.billToAddress2Field = value;
            }
        }

        /// <remarks/>
        public string BIllToAddress3
        {
            get
            {
                return this.bIllToAddress3Field;
            }
            set
            {
                this.bIllToAddress3Field = value;
            }
        }

        /// <remarks/>
        public string BIllToCity
        {
            get
            {
                return this.bIllToCityField;
            }
            set
            {
                this.bIllToCityField = value;
            }
        }

        /// <remarks/>
        public string BillToState
        {
            get
            {
                return this.billToStateField;
            }
            set
            {
                this.billToStateField = value;
            }
        }

        /// <remarks/>
        public System.DateTime OrderDate
        {
            get
            {
                return this.orderDateField;
            }
            set
            {
                this.orderDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrderDateSpecified
        {
            get
            {
                return this.orderDateFieldSpecified;
            }
            set
            {
                this.orderDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime ShipDate
        {
            get
            {
                return this.shipDateField;
            }
            set
            {
                this.shipDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ShipDateSpecified
        {
            get
            {
                return this.shipDateFieldSpecified;
            }
            set
            {
                this.shipDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string ShipInstructions1
        {
            get
            {
                return this.shipInstructions1Field;
            }
            set
            {
                this.shipInstructions1Field = value;
            }
        }

        /// <remarks/>
        public string ShipInstructions2
        {
            get
            {
                return this.shipInstructions2Field;
            }
            set
            {
                this.shipInstructions2Field = value;
            }
        }

        /// <remarks/>
        public string CustShipperAcct
        {
            get
            {
                return this.custShipperAcctField;
            }
            set
            {
                this.custShipperAcctField = value;
            }
        }

        /// <remarks/>
        public string CustNote1
        {
            get
            {
                return this.custNote1Field;
            }
            set
            {
                this.custNote1Field = value;
            }
        }

        /// <remarks/>
        public string CustNote2
        {
            get
            {
                return this.custNote2Field;
            }
            set
            {
                this.custNote2Field = value;
            }
        }

        /// <remarks/>
        public string CustNote3
        {
            get
            {
                return this.custNote3Field;
            }
            set
            {
                this.custNote3Field = value;
            }
        }

        /// <remarks/>
        public string CustNote4
        {
            get
            {
                return this.custNote4Field;
            }
            set
            {
                this.custNote4Field = value;
            }
        }

        /// <remarks/>
        public string CustNote5
        {
            get
            {
                return this.custNote5Field;
            }
            set
            {
                this.custNote5Field = value;
            }
        }

        /// <remarks/>
        public string RetailerName
        {
            get
            {
                return this.retailerNameField;
            }
            set
            {
                this.retailerNameField = value;
            }
        }

        /// <remarks/>
        public string RetailerFld1
        {
            get
            {
                return this.retailerFld1Field;
            }
            set
            {
                this.retailerFld1Field = value;
            }
        }

        /// <remarks/>
        public string RetailerFld2
        {
            get
            {
                return this.retailerFld2Field;
            }
            set
            {
                this.retailerFld2Field = value;
            }
        }

        /// <remarks/>
        public string RetailerFld3
        {
            get
            {
                return this.retailerFld3Field;
            }
            set
            {
                this.retailerFld3Field = value;
            }
        }

        /// <remarks/>
        public string RetailerFld4
        {
            get
            {
                return this.retailerFld4Field;
            }
            set
            {
                this.retailerFld4Field = value;
            }
        }

        /// <remarks/>
        public string RetailerFld5
        {
            get
            {
                return this.retailerFld5Field;
            }
            set
            {
                this.retailerFld5Field = value;
            }
        }

        /// <remarks/>
        public decimal OrderWeight
        {
            get
            {
                return this.orderWeightField;
            }
            set
            {
                this.orderWeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrderWeightSpecified
        {
            get
            {
                return this.orderWeightFieldSpecified;
            }
            set
            {
                this.orderWeightFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string OperatorName
        {
            get
            {
                return this.operatorNameField;
            }
            set
            {
                this.operatorNameField = value;
            }
        }

        /// <remarks/>
        public string EnvironUser
        {
            get
            {
                return this.environUserField;
            }
            set
            {
                this.environUserField = value;
            }
        }

        /// <remarks/>
        public string BillToZipCode
        {
            get
            {
                return this.billToZipCodeField;
            }
            set
            {
                this.billToZipCodeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime PickedDate
        {
            get
            {
                return this.pickedDateField;
            }
            set
            {
                this.pickedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PickedDateSpecified
        {
            get
            {
                return this.pickedDateFieldSpecified;
            }
            set
            {
                this.pickedDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string OrderOrSoNumber
        {
            get
            {
                return this.orderOrSoNumberField;
            }
            set
            {
                this.orderOrSoNumberField = value;
            }
        }

        /// <remarks/>
        public int OrderLineNumber
        {
            get
            {
                return this.orderLineNumberField;
            }
            set
            {
                this.orderLineNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrderLineNumberSpecified
        {
            get
            {
                return this.orderLineNumberFieldSpecified;
            }
            set
            {
                this.orderLineNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int StockNumber
        {
            get
            {
                return this.stockNumberField;
            }
            set
            {
                this.stockNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StockNumberSpecified
        {
            get
            {
                return this.stockNumberFieldSpecified;
            }
            set
            {
                this.stockNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string StockDescription1
        {
            get
            {
                return this.stockDescription1Field;
            }
            set
            {
                this.stockDescription1Field = value;
            }
        }

        /// <remarks/>
        public string StockDescription2
        {
            get
            {
                return this.stockDescription2Field;
            }
            set
            {
                this.stockDescription2Field = value;
            }
        }

        /// <remarks/>
        public string StockDescription3
        {
            get
            {
                return this.stockDescription3Field;
            }
            set
            {
                this.stockDescription3Field = value;
            }
        }

        /// <remarks/>
        public int QuantityToShip
        {
            get
            {
                return this.quantityToShipField;
            }
            set
            {
                this.quantityToShipField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool QuantityToShipSpecified
        {
            get
            {
                return this.quantityToShipFieldSpecified;
            }
            set
            {
                this.quantityToShipFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string CustomerNumber
        {
            get
            {
                return this.customerNumberField;
            }
            set
            {
                this.customerNumberField = value;
            }
        }

        /// <remarks/>
        public string CustomerName
        {
            get
            {
                return this.customerNameField;
            }
            set
            {
                this.customerNameField = value;
            }
        }

        /// <remarks/>
        public int QtyShippedTodate
        {
            get
            {
                return this.qtyShippedTodateField;
            }
            set
            {
                this.qtyShippedTodateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool QtyShippedTodateSpecified
        {
            get
            {
                return this.qtyShippedTodateFieldSpecified;
            }
            set
            {
                this.qtyShippedTodateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool Hold
        {
            get
            {
                return this.holdField;
            }
            set
            {
                this.holdField = value;
            }
        }

        /// <remarks/>
        public string NoteProp65
        {
            get
            {
                return this.noteProp65Field;
            }
            set
            {
                this.noteProp65Field = value;
            }
        }
    }
}