using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace OdbcExporter
{
	internal class CompExportConf
	{
		private XmlDocument doc = null;

		private XmlNodeList xmlnode = null;

		private XmlAttributeCollection xmlattrc = null;

		private string SimpleError;

		private string Error;

		private string configfile;

		public CompExportConf(string thefile)
		{
			this.configfile = thefile;
			this.doc = new XmlDocument();
		}

		public string GetBigError()
		{
			return this.Error;
		}

		public string getCompanyConnection(int arrayind)
		{
			this.xmlattrc = this.xmlnode[arrayind].Attributes;
			return this.xmlattrc[1].Value.ToString();
		}

		public string getCompanyConnection(string companyname)
		{
			string value = "Connection not found";
			int num = this.GetnumberCompanies();
			if (num >= 1)
			{
				for (int i = 0; i < num; i++)
				{
					XmlAttributeCollection attributes = this.xmlnode[i].Attributes;
					if (attributes[0].Value == companyname)
					{
						value = attributes[1].Value;
					}
				}
			}
			return value;
		}

		public string getCompanyName(int arrayind)
		{
			this.xmlattrc = this.xmlnode[arrayind].Attributes;
			return this.xmlattrc[0].Value.ToString();
		}

		public string[] getCompExportDetails(int arrayind)
		{
			int count = this.xmlnode[arrayind].ChildNodes.Count;
			string[] strArrays = new string[0];
			string str = "";
			for (int i = 0; i < count; i++)
			{
				XmlAttributeCollection attributes = this.xmlnode[arrayind].ChildNodes[i].Attributes;
				if (attributes[2].Value.ToString() == "true")
				{
					for (int j = 0; j < attributes.Count; j++)
					{
						str = string.Concat(str, attributes[j].Value.ToString());
						if ((j >= attributes.Count - 1 ? false : j >= 0))
						{
							str = string.Concat(str, "|");
						}
					}
					strArrays[i] = str;
					str = "";
				}
			}
			return strArrays;
		}

		public ArrayList getCompExportDetails2(int arrayind)
		{
			int count = this.xmlnode[arrayind].ChildNodes.Count;
			ArrayList arrayLists = new ArrayList();
			string str = "";
			for (int i = 0; i < count; i++)
			{
				XmlAttributeCollection attributes = this.xmlnode[arrayind].ChildNodes[i].Attributes;
				if (attributes[2].Value.ToString() == "true")
				{
					for (int j = 0; j < attributes.Count; j++)
					{
						str = string.Concat(str, attributes[j].Value.ToString());
						if ((j >= attributes.Count - 1 ? false : j >= 0))
						{
							str = string.Concat(str, "|");
						}
					}
					arrayLists.Add(str);
					str = "";
				}
			}
			return arrayLists;
		}

		public int GetnumberCompanies()
		{
			return this.xmlnode.Count;
		}

		public string GetSmallError()
		{
			return this.SimpleError;
		}

		public bool load()
		{
			bool flag;
			try
			{
				this.doc.Load(this.configfile);
				this.xmlnode = this.doc.GetElementsByTagName("company");
				flag = true;
			}
			catch (DirectoryNotFoundException directoryNotFoundException)
			{
				this.Error = directoryNotFoundException.Message;
				this.SimpleError = "Directory path to config file not found";
				flag = false;
			}
			catch (FileNotFoundException fileNotFoundException)
			{
				this.Error = fileNotFoundException.Message;
				this.SimpleError = "Config File Not found";
				flag = false;
			}
			catch (FileLoadException fileLoadException)
			{
				this.Error = fileLoadException.Message;
				this.SimpleError = "Problem loading config file";
				flag = false;
			}
			catch (XmlException xmlException)
			{
				this.Error = xmlException.Message;
				this.SimpleError = "XML not formatted correctly in config file";
				flag = false;
			}
			return flag;
		}
	}
}