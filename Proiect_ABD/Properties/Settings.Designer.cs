﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proiect_ABD.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=HEKARMEN\\SQLEXPRESS;Initial Catalog=MonitorizareEchipamente;Integrate" +
            "d Security=True;TrustServerCertificate=True")]
        public string MonitorizareEchipamenteConnectionString {
            get {
                return ((string)(this["MonitorizareEchipamenteConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=HEKARMEN\\SQLEXPRESS;Initial Catalog=Proiect_ABD;Integrated Security=T" +
            "rue;TrustServerCertificate=True")]
        public string Proiect_ABDConnectionString {
            get {
                return ((string)(this["Proiect_ABDConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=HEKARMEN\\SQLEXPRESS;Initial Catalog=Proiect_ABD;Integrated Security=T" +
            "rue;Encrypt=True;TrustServerCertificate=True")]
        public string Proiect_ABDConnectionString1 {
            get {
                return ((string)(this["Proiect_ABDConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.;Initial Catalog=Proiect_ABD;Integrated Security=True;Encrypt=True;T" +
            "rustServerCertificate=True")]
        public string Proiect_ABDConnectionString2 {
            get {
                return ((string)(this["Proiect_ABDConnectionString2"]));
            }
        }
    }
}
