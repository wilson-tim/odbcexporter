using System;
using System.Data.SqlClient;

namespace TravelinkExporter
{
	internal class sqlconnect
	{
		private string Connection;

		private string SqlQuery;

		private SqlConnection SqlCon;

		public SqlDataReader recordset;

		public sqlconnect(string connectionstring, string thequery)
		{
			this.Connection = connectionstring;
			this.SqlQuery = thequery;
		}

		public bool Closedatareader()
		{
			bool flag;
			if (this.recordset == null)
			{
				flag = false;
			}
			else
			{
				this.recordset.Close();
				flag = true;
			}
			return flag;
		}

		public bool CloseSqlConnection()
		{
			bool flag;
			if (this.SqlCon == null)
			{
				flag = false;
			}
			else
			{
				this.SqlCon.Close();
				flag = true;
			}
			return flag;
		}

		public SqlDataReader GetRecordset()
		{
			SqlDataReader sqlDataReader;
			this.SqlCon = new SqlConnection(this.Connection);
			try
			{
				this.SqlCon.Open();
				SqlCommand sqlCommand = new SqlCommand(this.SqlQuery, this.SqlCon);
				this.recordset = sqlCommand.ExecuteReader();
				sqlDataReader = this.recordset;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return sqlDataReader;
		}
	}
}