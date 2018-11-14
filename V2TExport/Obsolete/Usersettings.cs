using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace TravelinkExporter
{
	internal class Usersettings
	{
		public Usersettings()
		{
		}

		public string GetSettingValue(string settingname)
		{
			string str;
			ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			NameValueCollection appSettings = ConfigurationManager.AppSettings;
			string[] allKeys = appSettings.AllKeys;
			for (int i = 0; i < (int)allKeys.Length; i++)
			{
				string str1 = allKeys[i];
				MessageBox.Show("has key");
			}
			int num = 0;
			while (true)
			{
				if (num < appSettings.Count)
				{
					MessageBox.Show(num.ToString());
					if (!(appSettings.GetKey(num).ToString() == settingname))
					{
						num++;
					}
					else
					{
						str = appSettings[num].ToString();
						break;
					}
				}
				else
				{
					str = "";
					break;
				}
			}
			return str;
		}
	}
}