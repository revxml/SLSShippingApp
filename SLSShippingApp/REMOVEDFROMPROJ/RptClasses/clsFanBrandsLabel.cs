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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/clsFanBrandsLabel.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/clsFanBrandsLabel.xsd", IsNullable = false)]
    public partial class clsFanBrandsLabel
    {

        private clsFanBrandsLabelTblLabelFanBrands[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tblLabelFanBrands")]
        public clsFanBrandsLabelTblLabelFanBrands[] Items
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/clsFanBrandsLabel.xsd")]
    public partial class clsFanBrandsLabelTblLabelFanBrands
    {

        private string stockNumberField;

        private string operatorNameField;

        private string environUserField;

        private string txtImageFileField;

        /// <remarks/>
        public string StockNumber
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
        public string txtImageFile
        {
            get
            {
                return this.txtImageFileField;
            }
            set
            {
                this.txtImageFileField = value;
            }
        }
    }

}