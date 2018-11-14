using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OdbcExporter.Properties;

namespace OdbcExporter
{
	public class Options : Form
	{
		public int isopen = 0;

		private IContainer components = null;

		private Button button1;

		private Button button2;

		private TextBox textBox2;

		private TextBox textBox3;

		private Label label2;

		private Label label3;

		private Button button3;

		private Button button4;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private GroupBox Logging;

		private CheckBox checkBox1;

		private Button button5;

		private TextBox textBox1;

		public Options()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Settings.Default.ExportList = this.textBox2.Text;
			Settings.Default.FoldertoExport = this.textBox3.Text;
			Settings.Default.Logfile = this.textBox1.Text;
			if (!this.checkBox1.Checked)
			{
				Settings.Default.IncludeErrors = false;
			}
			else
			{
				Settings.Default.IncludeErrors = true;
			}
			Settings.Default.Save();
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Title = "Open XML File",
				Filter = "XML config Files|*.xml"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.textBox2.Text = openFileDialog.FileName.ToString();
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.textBox3.Text = folderBrowserDialog.SelectedPath.ToString();
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Title = "Select Log file",
				Filter = "Log files|*.log"
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					Stream stream = saveFileDialog.OpenFile();
					Stream stream1 = stream;
					if (stream != null)
					{
						stream1.Close();
					}
					this.textBox1.Text = saveFileDialog.FileName.ToString();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(string.Concat("Problem with log file\\n", exception.ToString()));
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBox1.Checked)
			{
				Settings.Default.IncludeErrors = false;
			}
			else
			{
				Settings.Default.IncludeErrors = true;
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

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Logging = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Logging.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(326, 340);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(295, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(13, 46);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(294, 20);
            this.textBox3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Config file ( xml )";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current directory";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(329, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 20);
            this.button3.TabIndex = 8;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(329, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 19);
            this.button4.TabIndex = 9;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 96);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Company Tables / Views to export";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Location = new System.Drawing.Point(15, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 95);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Directory";
            // 
            // Logging
            // 
            this.Logging.Controls.Add(this.checkBox1);
            this.Logging.Controls.Add(this.button5);
            this.Logging.Controls.Add(this.textBox1);
            this.Logging.Location = new System.Drawing.Point(15, 215);
            this.Logging.Name = "Logging";
            this.Logging.Size = new System.Drawing.Size(386, 100);
            this.Logging.TabIndex = 13;
            this.Logging.TabStop = false;
            this.Logging.Text = "Logging";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(140, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Enable Full Error logging";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(329, 51);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 20);
            this.button5.TabIndex = 1;
            this.button5.Text = "---";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 20);
            this.textBox1.TabIndex = 0;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 375);
            this.Controls.Add(this.Logging);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(425, 413);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 413);
            this.Name = "Options";
            this.Text = "Set Program Options";
            this.Load += new System.EventHandler(this.On_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Logging.ResumeLayout(false);
            this.Logging.PerformLayout();
            this.ResumeLayout(false);

		}

		private void On_Load(object sender, EventArgs e)
		{
			this.textBox2.Text = Settings.Default.ExportList.ToString();
			this.textBox3.Text = Settings.Default.FoldertoExport.ToString();
			this.textBox1.Text = Settings.Default.Logfile.ToString();
			if (Settings.Default.IncludeErrors.Equals(true))
			{
				this.checkBox1.Checked = true;
			}
		}
	}
}