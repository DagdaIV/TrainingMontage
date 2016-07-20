namespace Shoyeido_Work_Calculator
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
            this.SetBut = new System.Windows.Forms.Button();
            this.ClearBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 31);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2016, 7, 13, 21, 35, 15, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(127, 31);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.Value = new System.DateTime(2016, 7, 13, 21, 36, 21, 0);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(12, 57);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker3.TabIndex = 2;
            this.dateTimePicker3.Value = new System.DateTime(2016, 7, 13, 21, 36, 31, 0);
            this.dateTimePicker3.Visible = false;
            this.dateTimePicker3.ValueChanged += new System.EventHandler(this.dateTimePicker3_ValueChanged);
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Location = new System.Drawing.Point(127, 57);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker4.TabIndex = 3;
            this.dateTimePicker4.Value = new System.DateTime(2016, 7, 13, 21, 36, 38, 0);
            this.dateTimePicker4.Visible = false;
            this.dateTimePicker4.ValueChanged += new System.EventHandler(this.dateTimePicker4_ValueChanged);
            // 
            // dateTimePicker5
            // 
            this.dateTimePicker5.Location = new System.Drawing.Point(12, 83);
            this.dateTimePicker5.Name = "dateTimePicker5";
            this.dateTimePicker5.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker5.TabIndex = 4;
            this.dateTimePicker5.Value = new System.DateTime(2016, 7, 13, 21, 36, 43, 0);
            this.dateTimePicker5.Visible = false;
            this.dateTimePicker5.ValueChanged += new System.EventHandler(this.dateTimePicker5_ValueChanged);
            // 
            // dateTimePicker6
            // 
            this.dateTimePicker6.Location = new System.Drawing.Point(127, 83);
            this.dateTimePicker6.Name = "dateTimePicker6";
            this.dateTimePicker6.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker6.TabIndex = 5;
            this.dateTimePicker6.Value = new System.DateTime(2016, 7, 13, 21, 36, 49, 0);
            this.dateTimePicker6.Visible = false;
            this.dateTimePicker6.ValueChanged += new System.EventHandler(this.dateTimePicker6_ValueChanged);
            // 
            // SetBut
            // 
            this.SetBut.Location = new System.Drawing.Point(36, 109);
            this.SetBut.Name = "SetBut";
            this.SetBut.Size = new System.Drawing.Size(75, 23);
            this.SetBut.TabIndex = 6;
            this.SetBut.Text = "Set";
            this.SetBut.UseVisualStyleBackColor = true;
            this.SetBut.Click += new System.EventHandler(this.SetBut_Click);
            // 
            // ClearBut
            // 
            this.ClearBut.Location = new System.Drawing.Point(127, 108);
            this.ClearBut.Name = "ClearBut";
            this.ClearBut.Size = new System.Drawing.Size(75, 23);
            this.ClearBut.TabIndex = 7;
            this.ClearBut.Text = "Clear";
            this.ClearBut.UseVisualStyleBackColor = true;
            this.ClearBut.Click += new System.EventHandler(this.ClearBut_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 169);
            this.Controls.Add(this.ClearBut);
            this.Controls.Add(this.SetBut);
            this.Controls.Add(this.dateTimePicker6);
            this.Controls.Add(this.dateTimePicker5);
            this.Controls.Add(this.dateTimePicker4);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "Form2";
            this.Text = "PTO Dates";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.DateTimePicker dateTimePicker5;
        private System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.Button SetBut;
        private System.Windows.Forms.Button ClearBut;
    }
}