﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.17929 版自动生成。
// 
#pragma warning disable 1591

namespace Wxjzgcjczy.Web.ShiGongXuKeService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ShiGongXuKeSoap", Namespace="http://tempuri.org/")]
    public partial class ShiGongXuKe : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetXML_ForJAVAOperationCompleted;
        
        private System.Threading.SendOrPostCallback ShowSGXKInfoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ShiGongXuKe() {
            this.Url = global::Wxjzgcjczy.Web.Properties.Settings.Default.Wxjzgcjczy_Web_ShiGongXuKeService_ShiGongXuKe;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetXMLCompletedEventHandler GetXMLCompleted;
        
        /// <remarks/>
        public event GetXML_ForJAVACompletedEventHandler GetXML_ForJAVACompleted;
        
        /// <remarks/>
        public event ShowSGXKInfoCompletedEventHandler ShowSGXKInfoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetXML", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public DataExchangeResult GetXML(string userid, string password, string XMLStr) {
            object[] results = this.Invoke("GetXML", new object[] {
                        userid,
                        password,
                        XMLStr});
            return ((DataExchangeResult)(results[0]));
        }
        
        /// <remarks/>
        public void GetXMLAsync(string userid, string password, string XMLStr) {
            this.GetXMLAsync(userid, password, XMLStr, null);
        }
        
        /// <remarks/>
        public void GetXMLAsync(string userid, string password, string XMLStr, object userState) {
            if ((this.GetXMLOperationCompleted == null)) {
                this.GetXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetXMLOperationCompleted);
            }
            this.InvokeAsync("GetXML", new object[] {
                        userid,
                        password,
                        XMLStr}, this.GetXMLOperationCompleted, userState);
        }
        
        private void OnGetXMLOperationCompleted(object arg) {
            if ((this.GetXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetXMLCompleted(this, new GetXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetXML_ForJAVA", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetXML_ForJAVA(string userid, string password, string XMLStr) {
            object[] results = this.Invoke("GetXML_ForJAVA", new object[] {
                        userid,
                        password,
                        XMLStr});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetXML_ForJAVAAsync(string userid, string password, string XMLStr) {
            this.GetXML_ForJAVAAsync(userid, password, XMLStr, null);
        }
        
        /// <remarks/>
        public void GetXML_ForJAVAAsync(string userid, string password, string XMLStr, object userState) {
            if ((this.GetXML_ForJAVAOperationCompleted == null)) {
                this.GetXML_ForJAVAOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetXML_ForJAVAOperationCompleted);
            }
            this.InvokeAsync("GetXML_ForJAVA", new object[] {
                        userid,
                        password,
                        XMLStr}, this.GetXML_ForJAVAOperationCompleted, userState);
        }
        
        private void OnGetXML_ForJAVAOperationCompleted(object arg) {
            if ((this.GetXML_ForJAVACompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetXML_ForJAVACompleted(this, new GetXML_ForJAVACompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ShowSGXKInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ShowSGXKInfo(string userid, string password, string type) {
            object[] results = this.Invoke("ShowSGXKInfo", new object[] {
                        userid,
                        password,
                        type});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ShowSGXKInfoAsync(string userid, string password, string type) {
            this.ShowSGXKInfoAsync(userid, password, type, null);
        }
        
        /// <remarks/>
        public void ShowSGXKInfoAsync(string userid, string password, string type, object userState) {
            if ((this.ShowSGXKInfoOperationCompleted == null)) {
                this.ShowSGXKInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnShowSGXKInfoOperationCompleted);
            }
            this.InvokeAsync("ShowSGXKInfo", new object[] {
                        userid,
                        password,
                        type}, this.ShowSGXKInfoOperationCompleted, userState);
        }
        
        private void OnShowSGXKInfoOperationCompleted(object arg) {
            if ((this.ShowSGXKInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ShowSGXKInfoCompleted(this, new ShowSGXKInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class DataExchangeResult {
        
        private bool successField;
        
        private string messageField;
        
        /// <remarks/>
        public bool Success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetXMLCompletedEventHandler(object sender, GetXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DataExchangeResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DataExchangeResult)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetXML_ForJAVACompletedEventHandler(object sender, GetXML_ForJAVACompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetXML_ForJAVACompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetXML_ForJAVACompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void ShowSGXKInfoCompletedEventHandler(object sender, ShowSGXKInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ShowSGXKInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ShowSGXKInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591