using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Shoyeido_Work_Calculator
{
    public partial class Form2 : Form
    {
        public string[] datevals = new string[3];
        String dateTimePickerFormat = "MM/dd/yyyy";
        string docloc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public Form2()
        {
            InitializeComponent();
            this.dateTimePicker1.CustomFormat = " ";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = " ";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker3.CustomFormat = " ";
            this.dateTimePicker3.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker4.CustomFormat = " ";
            this.dateTimePicker4.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker5.CustomFormat = " ";
            this.dateTimePicker5.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker6.CustomFormat = " ";
            this.dateTimePicker6.Format = DateTimePickerFormat.Custom;
        }

        public DateTime ConvDate(string date)
        {
            DateTime dayquest = new DateTime(int.Parse(date.Substring(0,4)),int.Parse(date.Substring(4,2)),int.Parse(date.Substring(6,2)));
            return dayquest;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] ptodates = new string[3];
            datevals[0] = "";
            datevals[1] = "";
            datevals[2] = "";
            ptodates = File.ReadAllLines(@Path.Combine(docloc, "pto.txt"));
            for(byte count = 0; count<=2; count++)
            {
                if (!ptodates[count].Equals(""))
                {
                    if (count == 0)
                    {
                        this.dateTimePicker1.CustomFormat = dateTimePickerFormat;
                        this.dateTimePicker2.CustomFormat = dateTimePickerFormat;
                        dateTimePicker1.Value = ConvDate(ptodates[count].Substring(0, 8));
                        dateTimePicker2.Value = ConvDate(ptodates[count].Substring(8, 8));
                    }
                    if (count == 1)
                    {
                        this.dateTimePicker3.CustomFormat = dateTimePickerFormat;
                        this.dateTimePicker4.CustomFormat = dateTimePickerFormat;
                        dateTimePicker3.Value = ConvDate(ptodates[count].Substring(0, 8));
                        dateTimePicker4.Value = ConvDate(ptodates[count].Substring(8, 8));
                    }
                    if (count == 2)
                    {
                        this.dateTimePicker5.CustomFormat = dateTimePickerFormat;
                        this.dateTimePicker6.CustomFormat = dateTimePickerFormat;
                        dateTimePicker5.Value = ConvDate(ptodates[count].Substring(0, 8));
                        dateTimePicker6.Value = ConvDate(ptodates[count].Substring(8, 8));
                    }
                }
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker4.CustomFormat = dateTimePickerFormat;
            datevals[1] = dateTimePicker3.Value.ToString("yyyyMMdd") + dateTimePicker4.Value.ToString("yyyyMMdd");
            dateTimePicker5.Visible = true;
            dateTimePicker6.Visible = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker2.CustomFormat = dateTimePickerFormat;
            datevals[0] = dateTimePicker1.Value.ToString("yyyyMMdd") + dateTimePicker2.Value.ToString("yyyyMMdd");
            dateTimePicker3.Visible = true;
            dateTimePicker4.Visible = true;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker6.CustomFormat = dateTimePickerFormat;
            datevals[2] = dateTimePicker5.Value.ToString("yyyyMMdd") + dateTimePicker6.Value.ToString("yyyyMMdd");
        }

        private void SetBut_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(@Path.Combine(docloc, "pto.txt"), datevals);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.CustomFormat = dateTimePickerFormat;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker3.CustomFormat = dateTimePickerFormat;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker5.CustomFormat = dateTimePickerFormat;
        }

        private void ClearBut_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(@Path.Combine(docloc, "pto.txt"), new string[] { "", "", "" });
            this.dateTimePicker1.CustomFormat = " ";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = " ";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker3.CustomFormat = " ";
            this.dateTimePicker3.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker4.CustomFormat = " ";
            this.dateTimePicker4.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker5.CustomFormat = " ";
            this.dateTimePicker5.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker6.CustomFormat = " ";
            this.dateTimePicker6.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Visible = false;
            dateTimePicker4.Visible = false;
            dateTimePicker5.Visible = false;
            dateTimePicker6.Visible = false;
        }
    }
}
