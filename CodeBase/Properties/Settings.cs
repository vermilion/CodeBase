namespace sliver.SnippetCompiler.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("v3.5")]
        public string CompilerVersion
        {
            get
            {
                return (string) this["CompilerVersion"];
            }
            set
            {
                this["CompilerVersion"] = value;
            }
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [UserScopedSetting, DefaultSettingValue("False"), DebuggerNonUserCode]
        public bool Language_CSharp_AllowUnsafeCodeBlocks
        {
            get
            {
                return (bool) this["Language_CSharp_AllowUnsafeCodeBlocks"];
            }
            set
            {
                this["Language_CSharp_AllowUnsafeCodeBlocks"] = value;
            }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("False")]
        public bool Language_CSharp_CreateXmlDocumentation
        {
            get
            {
                return (bool) this["Language_CSharp_CreateXmlDocumentation"];
            }
            set
            {
                this["Language_CSharp_CreateXmlDocumentation"] = value;
            }
        }

        [UserScopedSetting, DefaultSettingValue("True"), DebuggerNonUserCode]
        public bool Language_Vb_OptionCompareBinary
        {
            get
            {
                return (bool) this["Language_Vb_OptionCompareBinary"];
            }
            set
            {
                this["Language_Vb_OptionCompareBinary"] = value;
            }
        }

        [DebuggerNonUserCode, DefaultSettingValue("True"), UserScopedSetting]
        public bool Language_Vb_OptionExplicit
        {
            get
            {
                return (bool) this["Language_Vb_OptionExplicit"];
            }
            set
            {
                this["Language_Vb_OptionExplicit"] = value;
            }
        }

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool Language_Vb_OptionInfer
        {
            get
            {
                return (bool) this["Language_Vb_OptionInfer"];
            }
            set
            {
                this["Language_Vb_OptionInfer"] = value;
            }
        }

        [DefaultSettingValue("False"), DebuggerNonUserCode, UserScopedSetting]
        public bool Language_Vb_OptionStrict
        {
            get
            {
                return (bool) this["Language_Vb_OptionStrict"];
            }
            set
            {
                this["Language_Vb_OptionStrict"] = value;
            }
        }

        [DefaultSettingValue(""), DebuggerNonUserCode, UserScopedSetting]
        public string UserEmailAddress
        {
            get
            {
                return (string) this["UserEmailAddress"];
            }
            set
            {
                this["UserEmailAddress"] = value;
            }
        }

        [DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
        public string UserName
        {
            get
            {
                return (string) this["UserName"];
            }
            set
            {
                this["UserName"] = value;
            }
        }
    }
}

