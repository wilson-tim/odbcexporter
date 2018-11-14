using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace TravelinkExporter
{
	internal class CsvParser
	{
		private string CsvFile;

		public CsvParser(string filegiven)
		{
			this.CsvFile = filegiven;
		}

		public Hashtable CsvtoDic()
		{
			Hashtable hashtables = new Hashtable();
			try
			{
				StreamReader streamReader = new StreamReader(this.CsvFile);
				try
				{
					streamReader.ReadLine();
					while (true)
					{
						string str = streamReader.ReadLine();
						string str1 = str;
						if (str == null)
						{
							break;
						}
						string[] strArrays = str1.Split(new char[] { ',' });
						if (strArrays[(int)strArrays.Length - 1].Contains("Y"))
						{
							hashtables.Add(strArrays[0], strArrays[1]);
						}
					}
				}
				finally
				{
					if (streamReader != null)
					{
						((IDisposable)streamReader).Dispose();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("There has been a problem\n\n", exception.ToString()));
			}
			return hashtables;
		}
	}
}