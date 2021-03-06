﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Maptool.soso {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="soso.syntoolSoap")]
    public interface syntoolSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Login(string name, string pwd);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPower", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetPower(string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetUserPower", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool GetUserPower(string value, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMenu", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Maptool.soso.keyValue[] GetMenu(string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMaptoolList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetMaptoolList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetTag", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SetTag(string id, int iden, string tab, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetTag_2", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SetTag_2(string id, int iden, string tab, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool AddLink(Maptool.soso.Link model, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCompleteLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetCompleteLink(string t_id, int iden, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DelCompleteLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool DelCompleteLink(string t_id, int iden, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetShopList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetShopList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetDefaultShop", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool SetDefaultShop(int iden, string t_id, string is_default, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetListAll101", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetListAll101(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, string sql, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetGlobalConfig", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Maptool.soso.GlobalConfig GetGlobalConfig();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateGlobalConfig", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void UpdateGlobalConfig(Maptool.soso.GlobalConfig model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetMaptoolDataList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetMaptoolDataList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetSellType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SetSellType(int id, int selltype, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SetData(Maptool.soso.Link_Mid mid, int col, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetImageUrl", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetImageUrl(int goodsId, int goodsPackageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCheckDataInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetCheckDataInfo(int iden, int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetLiuXiangList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetLiuXiangList(string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRegion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetRegion(int ParentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuYuLiuXiang", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetQuYuLiuXiang(string pid, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetLiuXiang", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SetLiuXiang(string pid, string[] aid, int type, int way, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetCancelLiuXiang", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool SetCancelLiuXiang(string pid, string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRegionAll", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetRegionAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetConfigPriceMeList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Maptool.soso.ConfigPriceMe[] GetConfigPriceMeList(string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPriceByID", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetPriceByID(string ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetPricePlus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void SetPricePlus(string ID, decimal Plus, string CateGory);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllConfigPriceMe", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Maptool.soso.ConfigPriceMe[] GetAllConfigPriceMe(string authKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetConfigPriceMe", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool SetConfigPriceMe(string authKey, System.Data.DataTable dt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetConfigList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Maptool.soso.Config[] GetConfigList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetConfigList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool SetConfigList(Maptool.soso.Config[] list, string authKey);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class keyValue : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Config : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        private string incNameField;
        
        private float discountRateField;
        
        private string cgyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string incName {
            get {
                return this.incNameField;
            }
            set {
                this.incNameField = value;
                this.RaisePropertyChanged("incName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public float discountRate {
            get {
                return this.discountRateField;
            }
            set {
                this.discountRateField = value;
                this.RaisePropertyChanged("discountRate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string cgy {
            get {
                return this.cgyField;
            }
            set {
                this.cgyField = value;
                this.RaisePropertyChanged("cgy");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ConfigPriceMe : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        private string nameField;
        
        private float price_PlusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public float Price_Plus {
            get {
                return this.price_PlusField;
            }
            set {
                this.price_PlusField = value;
                this.RaisePropertyChanged("Price_Plus");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Link_Mid : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private int idenField;
        
        private int sumField;
        
        private int stockTypeField;
        
        private int priceTypeField;
        
        private System.DateTime createdField;
        
        private System.DateTime updatedField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int iden {
            get {
                return this.idenField;
            }
            set {
                this.idenField = value;
                this.RaisePropertyChanged("iden");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int Sum {
            get {
                return this.sumField;
            }
            set {
                this.sumField = value;
                this.RaisePropertyChanged("Sum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int StockType {
            get {
                return this.stockTypeField;
            }
            set {
                this.stockTypeField = value;
                this.RaisePropertyChanged("StockType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public int PriceType {
            get {
                return this.priceTypeField;
            }
            set {
                this.priceTypeField = value;
                this.RaisePropertyChanged("PriceType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public System.DateTime Created {
            get {
                return this.createdField;
            }
            set {
                this.createdField = value;
                this.RaisePropertyChanged("Created");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public System.DateTime Updated {
            get {
                return this.updatedField;
            }
            set {
                this.updatedField = value;
                this.RaisePropertyChanged("Updated");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class GlobalConfig : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        private int stocklimitField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int Stocklimit {
            get {
                return this.stocklimitField;
            }
            set {
                this.stocklimitField = value;
                this.RaisePropertyChanged("Stocklimit");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Link : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private string spidField;
        
        private string t_idField;
        
        private int idenField;
        
        private System.DateTime createdField;
        
        private System.DateTime updatedField;
        
        private bool is_defaultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string spid {
            get {
                return this.spidField;
            }
            set {
                this.spidField = value;
                this.RaisePropertyChanged("spid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string t_id {
            get {
                return this.t_idField;
            }
            set {
                this.t_idField = value;
                this.RaisePropertyChanged("t_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int iden {
            get {
                return this.idenField;
            }
            set {
                this.idenField = value;
                this.RaisePropertyChanged("iden");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.DateTime created {
            get {
                return this.createdField;
            }
            set {
                this.createdField = value;
                this.RaisePropertyChanged("created");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public System.DateTime updated {
            get {
                return this.updatedField;
            }
            set {
                this.updatedField = value;
                this.RaisePropertyChanged("updated");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public bool is_default {
            get {
                return this.is_defaultField;
            }
            set {
                this.is_defaultField = value;
                this.RaisePropertyChanged("is_default");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface syntoolSoapChannel : Maptool.soso.syntoolSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class syntoolSoapClient : System.ServiceModel.ClientBase<Maptool.soso.syntoolSoap>, Maptool.soso.syntoolSoap {
        
        public syntoolSoapClient() {
        }
        
        public syntoolSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public syntoolSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public syntoolSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public syntoolSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Login(string name, string pwd) {
            return base.Channel.Login(name, pwd);
        }
        
        public string GetPower(string authKey) {
            return base.Channel.GetPower(authKey);
        }
        
        public bool GetUserPower(string value, string authKey) {
            return base.Channel.GetUserPower(value, authKey);
        }
        
        public Maptool.soso.keyValue[] GetMenu(string authKey) {
            return base.Channel.GetMenu(authKey);
        }
        
        public System.Data.DataSet GetMaptoolList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden) {
            return base.Channel.GetMaptoolList(PageSize, PageIndex, order, orderField, like, whereField, whereString, BinType, authKey, iden);
        }
        
        public string SetTag(string id, int iden, string tab, string authKey) {
            return base.Channel.SetTag(id, iden, tab, authKey);
        }
        
        public string SetTag_2(string id, int iden, string tab, string authKey) {
            return base.Channel.SetTag_2(id, iden, tab, authKey);
        }
        
        public bool AddLink(Maptool.soso.Link model, string authKey) {
            return base.Channel.AddLink(model, authKey);
        }
        
        public System.Data.DataSet GetCompleteLink(string t_id, int iden, string authKey) {
            return base.Channel.GetCompleteLink(t_id, iden, authKey);
        }
        
        public bool DelCompleteLink(string t_id, int iden, string authKey) {
            return base.Channel.DelCompleteLink(t_id, iden, authKey);
        }
        
        public System.Data.DataSet GetShopList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden) {
            return base.Channel.GetShopList(PageSize, PageIndex, order, orderField, like, whereField, whereString, BinType, authKey, iden);
        }
        
        public bool SetDefaultShop(int iden, string t_id, string is_default, string authKey) {
            return base.Channel.SetDefaultShop(iden, t_id, is_default, authKey);
        }
        
        public System.Data.DataSet GetListAll101(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, string sql, string authKey) {
            return base.Channel.GetListAll101(PageSize, PageIndex, order, orderField, like, whereField, whereString, sql, authKey);
        }
        
        public Maptool.soso.GlobalConfig GetGlobalConfig() {
            return base.Channel.GetGlobalConfig();
        }
        
        public void UpdateGlobalConfig(Maptool.soso.GlobalConfig model) {
            base.Channel.UpdateGlobalConfig(model);
        }
        
        public System.Data.DataSet GetMaptoolDataList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden) {
            return base.Channel.GetMaptoolDataList(PageSize, PageIndex, order, orderField, like, whereField, whereString, BinType, authKey, iden);
        }
        
        public string SetSellType(int id, int selltype, string authKey) {
            return base.Channel.SetSellType(id, selltype, authKey);
        }
        
        public string SetData(Maptool.soso.Link_Mid mid, int col, string authKey) {
            return base.Channel.SetData(mid, col, authKey);
        }
        
        public string GetImageUrl(int goodsId, int goodsPackageId) {
            return base.Channel.GetImageUrl(goodsId, goodsPackageId);
        }
        
        public System.Data.DataSet GetCheckDataInfo(int iden, int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, string authKey) {
            return base.Channel.GetCheckDataInfo(iden, PageSize, PageIndex, order, orderField, like, whereField, whereString, authKey);
        }
        
        public System.Data.DataTable GetLiuXiangList(string authKey) {
            return base.Channel.GetLiuXiangList(authKey);
        }
        
        public System.Data.DataTable GetRegion(int ParentId) {
            return base.Channel.GetRegion(ParentId);
        }
        
        public System.Data.DataTable GetQuYuLiuXiang(string pid, string authKey) {
            return base.Channel.GetQuYuLiuXiang(pid, authKey);
        }
        
        public string SetLiuXiang(string pid, string[] aid, int type, int way, string authKey) {
            return base.Channel.SetLiuXiang(pid, aid, type, way, authKey);
        }
        
        public bool SetCancelLiuXiang(string pid, string authKey) {
            return base.Channel.SetCancelLiuXiang(pid, authKey);
        }
        
        public System.Data.DataTable GetRegionAll() {
            return base.Channel.GetRegionAll();
        }
        
        public Maptool.soso.ConfigPriceMe[] GetConfigPriceMeList(string authKey) {
            return base.Channel.GetConfigPriceMeList(authKey);
        }
        
        public System.Data.DataTable GetPriceByID(string ID) {
            return base.Channel.GetPriceByID(ID);
        }
        
        public void SetPricePlus(string ID, decimal Plus, string CateGory) {
            base.Channel.SetPricePlus(ID, Plus, CateGory);
        }
        
        public Maptool.soso.ConfigPriceMe[] GetAllConfigPriceMe(string authKey) {
            return base.Channel.GetAllConfigPriceMe(authKey);
        }
        
        public bool SetConfigPriceMe(string authKey, System.Data.DataTable dt) {
            return base.Channel.SetConfigPriceMe(authKey, dt);
        }
        
        public Maptool.soso.Config[] GetConfigList() {
            return base.Channel.GetConfigList();
        }
        
        public bool SetConfigList(Maptool.soso.Config[] list, string authKey) {
            return base.Channel.SetConfigList(list, authKey);
        }
    }
}
