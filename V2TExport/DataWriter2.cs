using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Odbc;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OdbcExporter
{
	internal class DataWriter2
	{
		private string connectstring;

		private string TableName;

		private string SqlCommand;

		private string filename;

		private string CompName;

		private Form1 theparent;

		private OdbcConnection conn;

		private string Error;

		private string SimpleError;

		public DataWriter2(string theconnection, string thecompany, Form1 parent)
		{
			this.connectstring = theconnection;
			this.CompName = thecompany;
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

		public void Close()
		{
			if (this.conn != null)
			{
				this.conn.Dispose();
			}
		}

		public bool Connect()
		{
			bool flag;
			try
			{
                this.conn = new OdbcConnection(this.connectstring);
                this.conn.Open();
                flag = true;
			}
            catch (OdbcException exception2)
            {
                Exception exception = exception2;
                this.SimpleError = "OdbcError";
                this.Error = exception.Message;
                MessageBox.Show(exception.Message);
                flag = false;
            }
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.SimpleError = "ConnectionError";
				this.Error = exception.Message;
				flag = false;
			}
			return flag;
		}

		public bool ExportToTxt()
		{
			bool flag;
			try
			{
                string str = string.Concat("SELECT COUNT(*) from ", this.TableName);
				System.Data.Odbc.OdbcCommand sqlCommand = new System.Data.Odbc.OdbcCommand(str, this.conn);
				OdbcDataReader sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				int num = Convert.ToInt32(sqlDataReader[0].ToString());
				string str1 = sqlDataReader[0].ToString();
				sqlDataReader.Close();

                if (!(this.SqlCommand != ""))
				{
					sqlCommand.CommandText = string.Concat("SELECT * from ", this.TableName);
				}
				else
				{
					sqlCommand.CommandText = this.SqlCommand;
				}
				sqlDataReader = sqlCommand.ExecuteReader();
				int fieldCount = sqlDataReader.FieldCount;
				decimal num1 = new decimal(0);
				StreamWriter streamWriter = new StreamWriter(string.Concat(this.filename, ".Txt"));
				StringBuilder stringBuilder = new StringBuilder();
				while (sqlDataReader.Read())
				{
					decimal num2 = num1;
					num1 = num2++;
					this.updateDisplay(num2, num, str1);
					for (int i = 0; i < fieldCount; i++)
					{
						if (!(sqlDataReader[i].GetType().ToString() == "System.Byte[]"))
						{
							stringBuilder.Append(sqlDataReader[i].ToString());
						}
						else
						{
							byte[] item = (byte[])sqlDataReader[i];
							stringBuilder.Append(DataWriter2.BytesToHex(item));
						}
						if ((i >= fieldCount - 1 ? false : i >= 0))
						{
							stringBuilder.Append("|");
						}
					}
					stringBuilder.Replace(Environment.NewLine, string.Empty);
					streamWriter.WriteLine(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
				streamWriter.Close();
				streamWriter.Dispose();
				stringBuilder = null;
				sqlCommand.Dispose();
				sqlDataReader.Dispose();
				flag = true;
			}
			catch (OdbcException odbcException1)
			{
				OdbcException odbcException = odbcException1;
				this.SimpleError = "OdbcError";
				this.Error = odbcException.Message;
				flag = false;
			}
			catch (IOException oException1)
			{
				IOException oException = oException1;
				this.SimpleError = "FileError";
				this.Error = oException.Message;
				flag = false;
			}
			catch (UnauthorizedAccessException unauthorizedAccessException1)
			{
				UnauthorizedAccessException unauthorizedAccessException = unauthorizedAccessException1;
				this.SimpleError = "PermissionsError";
				this.Error = unauthorizedAccessException.Message;
				flag = false;
			}
			return flag;
		}

		public string GetError()
		{
			return this.Error;
		}

		public string GetSError()
		{
			return this.SimpleError;
		}

		public bool IsDetailsSet()
		{
			return ((this.TableName == null || this.SqlCommand == null ? true : this.filename == null) ? false : true);
		}

		public void setExportDetails(string tableName, string SqlCommand, string filename)
		{
			this.TableName = tableName;
			this.SqlCommand = SqlCommand;
			this.filename = filename;
		}

		public void updateDisplay(decimal counter, int max, string rownum)
		{
			decimal num = counter / max;
			int num1 = (int)(num * new decimal(100));
			string[] compName = new string[] { this.CompName, "  -  ", this.TableName, "  -  [", counter.ToString(), " / ", rownum, "]" };
			string str = string.Concat(compName);
			this.theparent.SetSomeText(num1, str);
		}
	}
}