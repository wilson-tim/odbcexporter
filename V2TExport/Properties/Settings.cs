using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TravelinkExporter.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance;

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		public string ConnectionString
		{
			get
			{
				return (string)this["ConnectionString"];
			}
		}

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string ExportList
		{
			get
			{
				return (string)this["ExportList"];
			}
			set
			{
				this["ExportList"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string FoldertoExport
		{
			get
			{
				return (string)this["FoldertoExport"];
			}
			set
			{
				this["FoldertoExport"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		public bool IncludeErrors
		{
			get
			{
				return (bool)this["IncludeErrors"];
			}
			set
			{
				this["IncludeErrors"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string Logfile
		{
			get
			{
				return (string)this["Logfile"];
			}
			set
			{
				this["Logfile"] = value;
			}
		}

		static Settings()
		{
			Settings.defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		}

		public Settings()
		{
		}

		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
		}

		private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
		{
		}
	}
}