using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace TravelinkExporter
{
	internal class DataWriter1
	{
		private string connectstring;

		private string SqlQuery;

		private string filename;

		private Form1 theparent;

		public DataWriter1(string theconnection, string thequery, string fname, Form1 parent)
		{
			this.connectstring = theconnection;
			this.SqlQuery = thequery;
			this.filename = fname;
			this.theparent = parent;
		}

		public static string BytesToHex(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder((int)bytes.Length);
			for (int i = 0; i < (int)bytes.Length; i++)
			{
				stringBuilder.Append(bytes[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		public string ExportToCsv()
		{
			string str;
			try
			{
				SqlConnection sqlConnection = new SqlConnection(this.connectstring);
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand(string.Concat("SELECT COUNT(*) from ", this.SqlQuery), sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				int num = Convert.ToInt32(sqlDataReader[0].ToString());
				string str1 = sqlDataReader[0].ToString();
				sqlCommand.Dispose();
				sqlDataReader.Dispose();
				SqlCommand sqlCommand1 = new SqlCommand(string.Concat("SELECT * from ", this.SqlQuery), sqlConnection);
				SqlDataReader sqlDataReader1 = sqlCommand1.ExecuteReader();
				int fieldCount = sqlDataReader1.FieldCount;
				string str2 = "";
				int num1 = 1;
				decimal num2 = new decimal(0);
				StreamWriter streamWriter = new StreamWriter(this.filename);
				StringBuilder stringBuilder = new StringBuilder();
				while (sqlDataReader1.Read())
				{
					num2 = num2++;
					decimal num3 = num2 / num;
					int num4 = (int)(num3 * new decimal(100));
					string[] strArrays = new string[] { this.SqlQuery.ToString(), "  -  [", num2.ToString(), " / ", str1, "]" };
					string str3 = string.Concat(strArrays);
					this.theparent.SetSomeText(num4, str3);
					if (num1 == 1)
					{
						for (int i = 0; i < fieldCount; i++)
						{
							stringBuilder.Append(sqlDataReader1.GetName(i).ToString());
							if ((i >= fieldCount - 1 ? false : i >= 0))
							{
								stringBuilder.Append("|");
							}
						}
						stringBuilder.AppendLine();
					}
					num1 = 0;
					for (int j = 0; j < fieldCount; j++)
					{
						if (!(sqlDataReader1[j].GetType().ToString() == "System.Byte[]"))
						{
							str2 = sqlDataReader1[j].ToString();
							str2 = str2.Replace(Environment.NewLine, string.Empty);
							stringBuilder.Append(str2);
						}
						else
						{
							byte[] item = (byte[])sqlDataReader1[j];
							stringBuilder.Append(DataWriter1.BytesToHex(item));
						}
						if ((j >= fieldCount - 1 ? false : j >= 0))
						{
							stringBuilder.Append("|");
						}
					}
					stringBuilder.AppendLine();
				}
				streamWriter.Write(stringBuilder.ToString());
				streamWriter.Close();
				streamWriter.Dispose();
				stringBuilder = null;
				sqlCommand1.Dispose();
				if (sqlDataReader1 != null)
				{
					sqlDataReader1.Dispose();
				}
				if (sqlConnection != null)
				{
					sqlConnection.Close();
				}
				str = "done";
			}
			catch (Exception exception)
			{
				str = string.Concat("A problem occured ", exception.ToString());
			}
			return str;
		}
	}
}