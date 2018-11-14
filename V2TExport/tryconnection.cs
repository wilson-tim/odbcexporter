using System;
using System.Data.SqlClient;

namespace TravelinkExporter
{
	internal class tryconnection
	{
		private string Connectstring;

		public tryconnection(string connection)
		{
			this.Connectstring = connection;
		}

		public string trytoconnect()
		{
			string str;
			SqlConnection sqlConnection = new SqlConnection(this.Connectstring);
			try
			{
				try
				{
					sqlConnection.Open();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					str = string.Concat("There was a problem connecting to the server \n", exception.Message.ToString());
					return str;
				}
			}
			finally
			{
				if (sqlConnection != null)
				{
					((IDisposable)sqlConnection).Dispose();
				}
			}
			str = "Connection Successful";
			return str;
		}
	}
}