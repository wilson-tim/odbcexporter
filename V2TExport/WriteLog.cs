using System;
using System.IO;
using System.Windows.Forms;

namespace TravelinkExporter
{
	internal class WriteLog
	{
		private StreamWriter filehandle = null;

		private string Logfilename;

		public WriteLog(string fname)
		{
			this.Logfilename = fname;
			if (this.Logfilename != "")
			{
				try
				{
					this.filehandle = new StreamWriter(this.Logfilename, true);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(string.Concat("Problem with log file\\n", exception.ToString()));
				}
			}
		}

		public void CloseLog()
		{
			if (this.filehandle != null)
			{
				this.filehandle.Close();
				this.filehandle.Dispose();
			}
		}

		public void WriteToLog(string entry)
		{
			try
			{
				this.filehandle.WriteLine(string.Concat(DateTime.Now, " ", entry));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Problem with log file\\n", exception.ToString()));
			}
		}
	}
}