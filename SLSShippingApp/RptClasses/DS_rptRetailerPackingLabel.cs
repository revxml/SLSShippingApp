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


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/DS_rptRetailerPackingLabel.xsd")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/DS_rptRetailerPackingLabel.xsd", IsNullable=false)]
public partial class DS_rptRetailerPackingLabel {
    
    private DS_rptRetailerPackingLabelTblRetailerPackingLabel[] itemsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("tblRetailerPackingLabel")]
    public DS_rptRetailerPackingLabelTblRetailerPackingLabel[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/DS_rptRetailerPackingLabel.xsd")]
public partial class DS_rptRetailerPackingLabelTblRetailerPackingLabel {
    
    private System.DateTime pickedDateField;
    
    private bool pickedDateFieldSpecified;
    
    private string orderOrSONumberField;
    
    private string pKTKField;
    
    private int orderLineNumberField;
    
    private bool orderLineNumberFieldSpecified;
    
    private int stockNumberField;
    
    private bool stockNumberFieldSpecified;
    
    private string stockDescription1Field;
    
    private string stockDescription2Field;
    
    private string stockDescription3Field;
    
    private int quantityToShipField;
    
    private bool quantityToShipFieldSpecified;
    
    private int recTodayField;
    
    private bool recTodayFieldSpecified;
    
    private string txtImageNameField;
    
    private string customerNumberField;
    
    private string customerNameField;
    
    private string pONumberField;
    
    private string shipToNumberField;
    
    private string shipToNameField;
    
    private string shipToAddress1Field;
    
    private string shipToAddress2Field;
    
    private string shipToAddress3Field;
    
    private string shipToCityField;
    
    private string shipToStateField;
    
    private string shipToZipCodeField;
    
    private string shipToCountryField;
    
    private string shipViaCodeField;
    
    private string customerProfileField;
    
    private string bayLocationField;
    
    private int qtyFromStockField;
    
    private bool qtyFromStockFieldSpecified;
    
    private int qtyShippedTodateField;
    
    private bool qtyShippedTodateFieldSpecified;
    
    private string billToNameField;
    
    private string billToAddress1Field;
    
    private string billToAddress2Field;
    
    private string bIllToAddress3Field;
    
    private string bIllToCityField;
    
    private string billToStateField;
    
    private string billToZipCodeField;
    
    private string billToCountryField;
    
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
    
    private string searchDescField;
    
    private decimal unitPriceField;
    
    private bool unitPriceFieldSpecified;
    
    private decimal shipCostField;
    
    private bool shipCostFieldSpecified;
    
    private decimal unitTaxAmtField;
    
    private bool unitTaxAmtFieldSpecified;
    
    private decimal totalOrderCostField;
    
    private bool totalOrderCostFieldSpecified;
    
    private string customerOrderNoField;
    
    private string shipToPhoneField;
    
    private string orderedByNameField;
    
    private string orderedByAddr1Field;
    
    private string orderedByAddr2Field;
    
    private string orderedByAddr3Field;
    
    private string orderedByCityField;
    
    private string orderedByStateField;
    
    private string orderedByZipCodeField;
    
    private string orderedByCountryField;
    
    private string retailerItemDescField;
    
    private string tCNumberField;
    
    private string uPCField;
    
    private string operatorNameField;
    
    private string environUserField;
    
    private string aSNNumberField;
    
    private string dPCIField;
    
    private string giftWrapField;
    
    private string mFGIDField;
    
    private string returnMethodField;
    
    private string returnPolicyField;
    
    private string giftMessageField;
    
    /// <remarks/>
    public System.DateTime PickedDate {
        get {
            return this.pickedDateField;
        }
        set {
            this.pickedDateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PickedDateSpecified {
        get {
            return this.pickedDateFieldSpecified;
        }
        set {
            this.pickedDateFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string OrderOrSONumber {
        get {
            return this.orderOrSONumberField;
        }
        set {
            this.orderOrSONumberField = value;
        }
    }
    
    /// <remarks/>
    public string PKTK {
        get {
            return this.pKTKField;
        }
        set {
            this.pKTKField = value;
        }
    }
    
    /// <remarks/>
    public int OrderLineNumber {
        get {
            return this.orderLineNumberField;
        }
        set {
            this.orderLineNumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool OrderLineNumberSpecified {
        get {
            return this.orderLineNumberFieldSpecified;
        }
        set {
            this.orderLineNumberFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public int StockNumber {
        get {
            return this.stockNumberField;
        }
        set {
            this.stockNumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool StockNumberSpecified {
        get {
            return this.stockNumberFieldSpecified;
        }
        set {
            this.stockNumberFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string StockDescription1 {
        get {
            return this.stockDescription1Field;
        }
        set {
            this.stockDescription1Field = value;
        }
    }
    
    /// <remarks/>
    public string StockDescription2 {
        get {
            return this.stockDescription2Field;
        }
        set {
            this.stockDescription2Field = value;
        }
    }
    
    /// <remarks/>
    public string StockDescription3 {
        get {
            return this.stockDescription3Field;
        }
        set {
            this.stockDescription3Field = value;
        }
    }
    
    /// <remarks/>
    public int QuantityToShip {
        get {
            return this.quantityToShipField;
        }
        set {
            this.quantityToShipField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool QuantityToShipSpecified {
        get {
            return this.quantityToShipFieldSpecified;
        }
        set {
            this.quantityToShipFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public int RecToday {
        get {
            return this.recTodayField;
        }
        set {
            this.recTodayField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool RecTodaySpecified {
        get {
            return this.recTodayFieldSpecified;
        }
        set {
            this.recTodayFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string txtImageName {
        get {
            return this.txtImageNameField;
        }
        set {
            this.txtImageNameField = value;
        }
    }
    
    /// <remarks/>
    public string CustomerNumber {
        get {
            return this.customerNumberField;
        }
        set {
            this.customerNumberField = value;
        }
    }
    
    /// <remarks/>
    public string CustomerName {
        get {
            return this.customerNameField;
        }
        set {
            this.customerNameField = value;
        }
    }
    
    /// <remarks/>
    public string PONumber {
        get {
            return this.pONumberField;
        }
        set {
            this.pONumberField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToNumber {
        get {
            return this.shipToNumberField;
        }
        set {
            this.shipToNumberField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToName {
        get {
            return this.shipToNameField;
        }
        set {
            this.shipToNameField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToAddress1 {
        get {
            return this.shipToAddress1Field;
        }
        set {
            this.shipToAddress1Field = value;
        }
    }
    
    /// <remarks/>
    public string ShipToAddress2 {
        get {
            return this.shipToAddress2Field;
        }
        set {
            this.shipToAddress2Field = value;
        }
    }
    
    /// <remarks/>
    public string ShipToAddress3 {
        get {
            return this.shipToAddress3Field;
        }
        set {
            this.shipToAddress3Field = value;
        }
    }
    
    /// <remarks/>
    public string ShipToCity {
        get {
            return this.shipToCityField;
        }
        set {
            this.shipToCityField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToState {
        get {
            return this.shipToStateField;
        }
        set {
            this.shipToStateField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToZipCode {
        get {
            return this.shipToZipCodeField;
        }
        set {
            this.shipToZipCodeField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToCountry {
        get {
            return this.shipToCountryField;
        }
        set {
            this.shipToCountryField = value;
        }
    }
    
    /// <remarks/>
    public string ShipViaCode {
        get {
            return this.shipViaCodeField;
        }
        set {
            this.shipViaCodeField = value;
        }
    }
    
    /// <remarks/>
    public string CustomerProfile {
        get {
            return this.customerProfileField;
        }
        set {
            this.customerProfileField = value;
        }
    }
    
    /// <remarks/>
    public string BayLocation {
        get {
            return this.bayLocationField;
        }
        set {
            this.bayLocationField = value;
        }
    }
    
    /// <remarks/>
    public int QtyFromStock {
        get {
            return this.qtyFromStockField;
        }
        set {
            this.qtyFromStockField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool QtyFromStockSpecified {
        get {
            return this.qtyFromStockFieldSpecified;
        }
        set {
            this.qtyFromStockFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public int QtyShippedTodate {
        get {
            return this.qtyShippedTodateField;
        }
        set {
            this.qtyShippedTodateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool QtyShippedTodateSpecified {
        get {
            return this.qtyShippedTodateFieldSpecified;
        }
        set {
            this.qtyShippedTodateFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string BillToName {
        get {
            return this.billToNameField;
        }
        set {
            this.billToNameField = value;
        }
    }
    
    /// <remarks/>
    public string BillToAddress1 {
        get {
            return this.billToAddress1Field;
        }
        set {
            this.billToAddress1Field = value;
        }
    }
    
    /// <remarks/>
    public string BillToAddress2 {
        get {
            return this.billToAddress2Field;
        }
        set {
            this.billToAddress2Field = value;
        }
    }
    
    /// <remarks/>
    public string BIllToAddress3 {
        get {
            return this.bIllToAddress3Field;
        }
        set {
            this.bIllToAddress3Field = value;
        }
    }
    
    /// <remarks/>
    public string BIllToCity {
        get {
            return this.bIllToCityField;
        }
        set {
            this.bIllToCityField = value;
        }
    }
    
    /// <remarks/>
    public string BillToState {
        get {
            return this.billToStateField;
        }
        set {
            this.billToStateField = value;
        }
    }
    
    /// <remarks/>
    public string BillToZipCode {
        get {
            return this.billToZipCodeField;
        }
        set {
            this.billToZipCodeField = value;
        }
    }
    
    /// <remarks/>
    public string BillToCountry {
        get {
            return this.billToCountryField;
        }
        set {
            this.billToCountryField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime OrderDate {
        get {
            return this.orderDateField;
        }
        set {
            this.orderDateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool OrderDateSpecified {
        get {
            return this.orderDateFieldSpecified;
        }
        set {
            this.orderDateFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime ShipDate {
        get {
            return this.shipDateField;
        }
        set {
            this.shipDateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShipDateSpecified {
        get {
            return this.shipDateFieldSpecified;
        }
        set {
            this.shipDateFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string ShipInstructions1 {
        get {
            return this.shipInstructions1Field;
        }
        set {
            this.shipInstructions1Field = value;
        }
    }
    
    /// <remarks/>
    public string ShipInstructions2 {
        get {
            return this.shipInstructions2Field;
        }
        set {
            this.shipInstructions2Field = value;
        }
    }
    
    /// <remarks/>
    public string CustShipperAcct {
        get {
            return this.custShipperAcctField;
        }
        set {
            this.custShipperAcctField = value;
        }
    }
    
    /// <remarks/>
    public string CustNote1 {
        get {
            return this.custNote1Field;
        }
        set {
            this.custNote1Field = value;
        }
    }
    
    /// <remarks/>
    public string CustNote2 {
        get {
            return this.custNote2Field;
        }
        set {
            this.custNote2Field = value;
        }
    }
    
    /// <remarks/>
    public string CustNote3 {
        get {
            return this.custNote3Field;
        }
        set {
            this.custNote3Field = value;
        }
    }
    
    /// <remarks/>
    public string CustNote4 {
        get {
            return this.custNote4Field;
        }
        set {
            this.custNote4Field = value;
        }
    }
    
    /// <remarks/>
    public string CustNote5 {
        get {
            return this.custNote5Field;
        }
        set {
            this.custNote5Field = value;
        }
    }
    
    /// <remarks/>
    public string RetailerName {
        get {
            return this.retailerNameField;
        }
        set {
            this.retailerNameField = value;
        }
    }
    
    /// <remarks/>
    public string RetailerFld1 {
        get {
            return this.retailerFld1Field;
        }
        set {
            this.retailerFld1Field = value;
        }
    }
    
    /// <remarks/>
    public string RetailerFld2 {
        get {
            return this.retailerFld2Field;
        }
        set {
            this.retailerFld2Field = value;
        }
    }
    
    /// <remarks/>
    public string RetailerFld3 {
        get {
            return this.retailerFld3Field;
        }
        set {
            this.retailerFld3Field = value;
        }
    }
    
    /// <remarks/>
    public string RetailerFld4 {
        get {
            return this.retailerFld4Field;
        }
        set {
            this.retailerFld4Field = value;
        }
    }
    
    /// <remarks/>
    public string RetailerFld5 {
        get {
            return this.retailerFld5Field;
        }
        set {
            this.retailerFld5Field = value;
        }
    }
    
    /// <remarks/>
    public decimal OrderWeight {
        get {
            return this.orderWeightField;
        }
        set {
            this.orderWeightField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool OrderWeightSpecified {
        get {
            return this.orderWeightFieldSpecified;
        }
        set {
            this.orderWeightFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string SearchDesc {
        get {
            return this.searchDescField;
        }
        set {
            this.searchDescField = value;
        }
    }
    
    /// <remarks/>
    public decimal UnitPrice {
        get {
            return this.unitPriceField;
        }
        set {
            this.unitPriceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool UnitPriceSpecified {
        get {
            return this.unitPriceFieldSpecified;
        }
        set {
            this.unitPriceFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public decimal ShipCost {
        get {
            return this.shipCostField;
        }
        set {
            this.shipCostField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShipCostSpecified {
        get {
            return this.shipCostFieldSpecified;
        }
        set {
            this.shipCostFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public decimal UnitTaxAmt {
        get {
            return this.unitTaxAmtField;
        }
        set {
            this.unitTaxAmtField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool UnitTaxAmtSpecified {
        get {
            return this.unitTaxAmtFieldSpecified;
        }
        set {
            this.unitTaxAmtFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public decimal TotalOrderCost {
        get {
            return this.totalOrderCostField;
        }
        set {
            this.totalOrderCostField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TotalOrderCostSpecified {
        get {
            return this.totalOrderCostFieldSpecified;
        }
        set {
            this.totalOrderCostFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string CustomerOrderNo {
        get {
            return this.customerOrderNoField;
        }
        set {
            this.customerOrderNoField = value;
        }
    }
    
    /// <remarks/>
    public string ShipToPhone {
        get {
            return this.shipToPhoneField;
        }
        set {
            this.shipToPhoneField = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByName {
        get {
            return this.orderedByNameField;
        }
        set {
            this.orderedByNameField = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByAddr1 {
        get {
            return this.orderedByAddr1Field;
        }
        set {
            this.orderedByAddr1Field = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByAddr2 {
        get {
            return this.orderedByAddr2Field;
        }
        set {
            this.orderedByAddr2Field = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByAddr3 {
        get {
            return this.orderedByAddr3Field;
        }
        set {
            this.orderedByAddr3Field = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByCity {
        get {
            return this.orderedByCityField;
        }
        set {
            this.orderedByCityField = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByState {
        get {
            return this.orderedByStateField;
        }
        set {
            this.orderedByStateField = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByZipCode {
        get {
            return this.orderedByZipCodeField;
        }
        set {
            this.orderedByZipCodeField = value;
        }
    }
    
    /// <remarks/>
    public string OrderedByCountry {
        get {
            return this.orderedByCountryField;
        }
        set {
            this.orderedByCountryField = value;
        }
    }
    
    /// <remarks/>
    public string RetailerItemDesc {
        get {
            return this.retailerItemDescField;
        }
        set {
            this.retailerItemDescField = value;
        }
    }
    
    /// <remarks/>
    public string TCNumber {
        get {
            return this.tCNumberField;
        }
        set {
            this.tCNumberField = value;
        }
    }
    
    /// <remarks/>
    public string UPC {
        get {
            return this.uPCField;
        }
        set {
            this.uPCField = value;
        }
    }
    
    /// <remarks/>
    public string OperatorName {
        get {
            return this.operatorNameField;
        }
        set {
            this.operatorNameField = value;
        }
    }
    
    /// <remarks/>
    public string EnvironUser {
        get {
            return this.environUserField;
        }
        set {
            this.environUserField = value;
        }
    }
    
    /// <remarks/>
    public string ASNNumber {
        get {
            return this.aSNNumberField;
        }
        set {
            this.aSNNumberField = value;
        }
    }
    
    /// <remarks/>
    public string DPCI {
        get {
            return this.dPCIField;
        }
        set {
            this.dPCIField = value;
        }
    }
    
    /// <remarks/>
    public string GiftWrap {
        get {
            return this.giftWrapField;
        }
        set {
            this.giftWrapField = value;
        }
    }
    
    /// <remarks/>
    public string MFGID {
        get {
            return this.mFGIDField;
        }
        set {
            this.mFGIDField = value;
        }
    }
    
    /// <remarks/>
    public string ReturnMethod {
        get {
            return this.returnMethodField;
        }
        set {
            this.returnMethodField = value;
        }
    }
    
    /// <remarks/>
    public string ReturnPolicy {
        get {
            return this.returnPolicyField;
        }
        set {
            this.returnPolicyField = value;
        }
    }
    
    /// <remarks/>
    public string GiftMessage {
        get {
            return this.giftMessageField;
        }
        set {
            this.giftMessageField = value;
        }
    }
}
