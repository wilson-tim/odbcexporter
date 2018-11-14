using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TravelinkExporter.Properties;

namespace TravelinkExporter
{
	public class Form1 : Form
	{
		private IContainer components = null;

		private ProgressBar progressBar1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem helpToolStripMenuItem;

		public ProgressBar progressBar2;

		private ToolStripMenuItem startToolStripMenuItem;

		private Label label1;

		private Label label2;

		private TextBox textBox1;

		private Label label3;

		private ToolStripMenuItem optionsToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private ToolStripMenuItem aboutToolStripMenuItem;

		private BackgroundWorker backgroundWorker1;

		public string startedby = "";

		public string XmlConfig;

		public string DestinationFolder;

		public System.Windows.Forms.Timer myTimer;

		private Options setoptions;

		private WriteLog logger;

		public Form1()
		{
			this.InitializeComponent();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new Form2()).ShowDialog();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			DataWriter2 dataWriter2;
			string str;
			CompExportConf compExportConf = new CompExportConf(this.XmlConfig);
			if (compExportConf.load())
			{
				int year = DateTime.Now.Year;
				string str1 = year.ToString();
				year = DateTime.Now.Month;
				string str2 = year.ToString();
				year = DateTime.Now.Day;
				string str3 = string.Concat(str1, str2, year.ToString());
				int num = compExportConf.GetnumberCompanies();
				decimal num1 = new decimal(0);
				for (int i = 0; i < num; i++)
				{
					string companyName = compExportConf.getCompanyName(i);
					string companyConnection = compExportConf.getCompanyConnection(i);
					ArrayList compExportDetails2 = compExportConf.getCompExportDetails2(i);
					int count = compExportDetails2.Count;
					dataWriter2 = new DataWriter2(companyConnection, companyName, this);
					bool flag = dataWriter2.Connect();
					if (!flag)
					{
						this.logger.WriteToLog(string.Concat("ERROR Couldn't Connect to ", companyName));
						this.logger.WriteToLog("-----------------------------------");
					}
					else
					{
						this.logger.WriteToLog(string.Concat("Connected to ", companyName));
						this.logger.WriteToLog("-----------------------------------");
					}
					if ((compExportDetails2.Count <= 0 ? false : flag))
					{
						this.backgroundWorker1.ReportProgress(0, "");
						for (int j = 0; j < compExportDetails2.Count; j++)
						{
							string[] strArrays = compExportDetails2[j].ToString().Split(new char[] { '|' });
							string str4 = strArrays[0];
							str = strArrays[1];
							string str5 = strArrays[2];
							string str6 = strArrays[3];
							string upper = companyName.Substring(0, 3).ToUpper();
							string str7 = string.Concat(upper, "_", str, str3);
							string str8 = string.Concat(this.DestinationFolder, "\\", str7);
							dataWriter2.setExportDetails(str4, str6, str8);
							bool txt = dataWriter2.ExportToTxt();
							num1 = num1++;
							decimal num2 = num1 / count;
							int num3 = (int)(num2 * new decimal(100));
							this.backgroundWorker1.ReportProgress(num3, str4);
							if (txt)
							{
								this.logger.WriteToLog(string.Concat(str, " Complete"));
							}
							else
							{
								if (dataWriter2.GetSError() == "SqlError")
								{
									goto Label1;
								}
								if ((dataWriter2.GetSError() == "FileError" ? false : !(dataWriter2.GetSError() == "PermissionsError")))
								{
									this.backgroundWorker1.CancelAsync();
									Application.Exit();
								}
								else
								{
									this.logger.WriteToLog(string.Concat(str, dataWriter2.GetSError()));
									if (Settings.Default.IncludeErrors)
									{
										this.logger.WriteToLog(string.Concat(str, " ", dataWriter2.GetError()));
									}
									break;
								}
							}
						}
                    }
					dataWriter2.Close();
					dataWriter2 = null;
					count = 0;
					num1 = new decimal(0);
				}
				if (this.logger != null)
				{
					this.logger.CloseLog();
				}
			}
			else
			{
				this.logger.WriteToLog(string.Concat("Problem with XML config file ", compExportConf.GetSmallError()));
				this.logger.WriteToLog("-----------------------------------");
				if (Settings.Default.IncludeErrors)
				{
					this.logger.WriteToLog(compExportConf.GetBigError());
				}
				MessageBox.Show("Problem with XML Config file\nPlease correct.");
			}
			return;
		Label1:
			this.logger.WriteToLog(string.Concat(str, " ", dataWriter2.GetSError()));
			if (Settings.Default.IncludeErrors)
			{
				this.logger.WriteToLog(dataWriter2.GetError());
			}
		//	goto Label0;
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBar1.Value = e.ProgressPercentage;
			e.ProgressPercentage.ToString();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.textBox1.Text = "";
			this.progressBar1.Value = 0;
			this.progressBar2.Value = 0;
			if (this.startedby == "CmdLine")
			{
				this.logger.CloseLog();
				Thread.Sleep(1000);
				Application.Exit();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.backgroundWorker1.CancelAsync();
			Application.Exit();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if ((int)Environment.GetCommandLineArgs().Length >= 2)
			{
				this.startedby = "CmdLine";
				this.myTimer = new System.Windows.Forms.Timer()
				{
					Interval = 1000
				};
				this.myTimer.Tick += new EventHandler(this.myTimer_Tick);
				this.myTimer.Start();
			}
		}

		public void initialiseProgBars()
		{
			this.progressBar1.Minimum = 0;
			this.progressBar1.Value = 0;
			this.progressBar1.Step = 1;
			this.progressBar1.Style = ProgressBarStyle.Blocks;
			this.progressBar2.Minimum = 0;
			this.progressBar2.Value = 0;
			this.progressBar2.Step = 1;
			this.progressBar2.Style = ProgressBarStyle.Blocks;
		}

		private void InitializeComponent()
		{
			this.progressBar1 = new ProgressBar();
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.startToolStripMenuItem = new ToolStripMenuItem();
			this.optionsToolStripMenuItem = new ToolStripMenuItem();
			this.exitToolStripMenuItem = new ToolStripMenuItem();
			this.helpToolStripMenuItem = new ToolStripMenuItem();
			this.aboutToolStripMenuItem = new ToolStripMenuItem();
			this.progressBar2 = new ProgressBar();
			this.label1 = new Label();
			this.label2 = new Label();
			this.textBox1 = new TextBox();
			this.label3 = new Label();
			this.backgroundWorker1 = new BackgroundWorker();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.progressBar1.Location = new Point(12, 195);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(375, 23);
			this.progressBar1.TabIndex = 0;
			ToolStripItemCollection items = this.menuStrip1.Items;
 			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.fileToolStripMenuItem, this.helpToolStripMenuItem };
 			items.AddRange(toolStripItemArray);
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(399, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			ToolStripItemCollection dropDownItems = this.fileToolStripMenuItem.DropDownItems;
			toolStripItemArray = new ToolStripItem[] { this.startToolStripMenuItem, this.optionsToolStripMenuItem, this.exitToolStripMenuItem };
			dropDownItems.AddRange(toolStripItemArray);
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new EventHandler(this.startToolStripMenuItem_Click);
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new EventHandler(this.optionsToolStripMenuItem_Click);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
			ToolStripItemCollection ToolStripItemCollection = this.helpToolStripMenuItem.DropDownItems;
			toolStripItemArray = new ToolStripItem[] { this.aboutToolStripMenuItem };
			ToolStripItemCollection.AddRange(toolStripItemArray);
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
			this.progressBar2.Location = new Point(12, 126);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(374, 23);
			this.progressBar2.TabIndex = 5;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(9, 110);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Table / View progress";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 179);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Overall Progress";
			this.textBox1.Location = new Point(12, 62);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(374, 20);
			this.textBox1.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(12, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Current table / view";
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(399, 250);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar2);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			this.MaximumSize = new System.Drawing.Size(407, 277);
			this.MinimumSize = new System.Drawing.Size(407, 277);
			base.Name = "Form1";
			this.Text = "TravelinkExporter";
			base.Load += new EventHandler(this.Form1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void myTimer_Tick(object sender, EventArgs e)
		{
			this.myTimer.Stop();
			if (!this.backgroundWorker1.IsBusy)
			{
				this.XmlConfig = Settings.Default.ExportList.ToString();
				this.DestinationFolder = Settings.Default.FoldertoExport.ToString();
				this.logger = new WriteLog(Settings.Default.Logfile);
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.setoptions = new Options();
			this.setoptions.ShowDialog();
		}

		public void SetSomeText(int percentage, string spercentage)
		{
			if ((this.progressBar2.InvokeRequired ? false : !this.textBox1.InvokeRequired))
			{
				this.progressBar2.Value = percentage;
				this.textBox1.Text = spercentage;
			}
			else
			{
				try
				{
					Form1.SetTextCallback setTextCallback = new Form1.SetTextCallback(this.SetSomeText);
					object[] objArray = new object[] { percentage, spercentage };
					base.Invoke(setTextCallback, objArray);
				}
				catch (Exception exception)
				{
				}
			}
		}

		private void startToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.backgroundWorker1.IsBusy)
			{
				MessageBox.Show("Program is already running");
			}
			else
			{
				this.XmlConfig = Settings.Default.ExportList.ToString();
				this.DestinationFolder = Settings.Default.FoldertoExport.ToString();
				this.logger = new WriteLog(Settings.Default.Logfile);
				this.backgroundWorker1.RunWorkerAsync();
			}
		}

		private delegate void SetTextCallback(int percentage, string spercentage);
	}
}