using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TravelinkExporter
{
	public class Form2 : Form
	{
		private IContainer components = null;

		private Button button1;

		private Label label1;

		private Label label2;

		public Form2()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Hide();
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
			this.button1 = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			base.SuspendLayout();
			this.button1.Location = new Point(75, 95);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(33, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(153, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "TUI Data Exporter";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(33, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(129, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Version 2.0.1  08.03.2016";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(225, 130);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			this.MaximumSize = new System.Drawing.Size(233, 157);
			this.MinimumSize = new System.Drawing.Size(233, 157);
			base.Name = "Form2";
			this.Text = "About";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}