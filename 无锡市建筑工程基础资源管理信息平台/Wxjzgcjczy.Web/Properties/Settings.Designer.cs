﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wxjzgcjczy.Web.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://221.224.147.148/ZJGSMGService/WebServiceSendMSG.asmx")]
        public string MunSupervisionProject_Web_WebReferenceSendMSG_WebServiceSendMSG {
            get {
                return ((string)(this["MunSupervisionProject_Web_WebReferenceSendMSG_WebServiceSendMSG"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://58.213.147.228/JSFront/SGXKDataExchange/ShiGongXuKe.asmx")]
        public string Wxjzgcjczy_Web_ShiGongXuKeService_ShiGongXuKe {
            get {
                return ((string)(this["Wxjzgcjczy_Web_ShiGongXuKeService_ShiGongXuKe"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://58.213.147.228/JSFront/SGXKDataExchange/DataShareService.asmx")]
        public string Wxjzgcjczy_Web_DataShareService_DataShareService {
            get {
                return ((string)(this["Wxjzgcjczy_Web_DataShareService_DataShareService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://58.213.147.230:8000/tDataService/ReceiveDataService.ws")]
        public string Wxjzgcjczy_Web_ReceiveDataService_ReceiveDataService {
            get {
                return ((string)(this["Wxjzgcjczy_Web_ReceiveDataService_ReceiveDataService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://58.213.147.243:8080/jscedc/services/dataDownService.dataDownServiceHttpSoa" +
            "p11Endpoint/")]
        public string Wxjzgcjczy_Web_DataDownService_dataDownService {
            get {
                return ((string)(this["Wxjzgcjczy_Web_DataDownService_dataDownService"]));
            }
        }
    }
}
