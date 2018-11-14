using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace TravelinkExporter
{
	internal class sqlcachewriter
	{
		private string connectstring;

		private string SqlQuery;

		private string filename;

		private int showstatus;

		private Form1 theparent;

		public sqlcachewriter(string theconnection, string thequery, string fname, Form1 parent)
		{
			this.connectstring = theconnection;
			this.SqlQuery = thequery;
			this.filename = fname;
			this.theparent = parent;
			this.doit();
		}

		public void doit()
		{
			SqlConnection sqlConnection = new SqlConnection(this.connectstring);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(this.SqlQuery, sqlConnection);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet, "testing");
			sqlConnection.Close();
			DataTable item = dataSet.Tables["testing"];
			StreamWriter streamWriter = new StreamWriter(this.filename);
			StringBuilder stringBuilder = new StringBuilder();
			this.showstatus = item.Rows.Count;
			if (item.Rows.Count <= 0)
			{
				streamWriter.Write("");
			}
			else
			{
				int length = (int)item.Rows[0].ItemArray.Length;
				foreach (DataRow row in item.Rows)
				{
					string str = null;
					for (int i = 0; i <= length - 1; i++)
					{
						str = row[i].ToString();
						if ((i >= length - 1 ? false : i >= 0))
						{
							str = string.Concat(str, "|");
						}
						if (str != null)
						{
							string str1 = str.Replace(Environment.NewLine, string.Empty);
							str1 = str1.Replace("System.Byte[]", string.Empty);
							stringBuilder.Append(str1);
						}
					}
					stringBuilder.Append(Environment.NewLine);
				}
				streamWriter.Write(stringBuilder.ToString());
				streamWriter.Flush();
			}
			stringBuilder = null;
			streamWriter = null;
			item.Dispose();
			dataSet.Dispose();
			sqlDataAdapter.Dispose();
		}

		public int getprogbarmax()
		{
			return this.showstatus;
		}
	}
}