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
        string docloc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public Form2()
        {
            InitializeComponent();
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
            if (File.Exists(@Path.Combine(docloc, "pto.txt")))
            {
                ptodates = File.ReadAllLines(@Path.Combine(docloc, "pto.txt"));
            }
            else
            {
                File.Create(@Path.Combine(docloc, "pto.txt"));
                ptodates[0] = "";
                ptodates[1] = "";
                ptodates[2] = "";
            }
            for(byte count = 0; count<2; count++)
            {
                if (!ptodates[count].Equals(""))
                {
                    if (count == 0)
                    {
                        dateTimePicker1.Value = ConvDate(ptodates[count].Substring(0, 8));
                        dateTimePicker2.Value = ConvDate(ptodates[count].Substring(8, 8));
                    }
                    if (count == 1)
                    {
                        dateTimePicker3.Value = ConvDate(ptodates[count].Substring(0, 8));
                        dateTimePicker4.Value = ConvDate(ptodates[count].Substring(8, 8));
                    }
                    if (count == 2)
                    {
                        dateTimePicker5.Value = ConvDate(ptodates[count].Substring(0, 8));
                        dateTimePicker6.Value = ConvDate(ptodates[count].Substring(8, 8));
                    }
                }
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            datevals[1] = dateTimePicker3.Value.ToString("yyyyMMdd") + dateTimePicker4.Value.ToString("yyyyMMdd");
            dateTimePicker5.Visible = true;
            dateTimePicker6.Visible = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            datevals[0] = dateTimePicker1.Value.ToString("yyyyMMdd") + dateTimePicker2.Value.ToString("yyyyMMdd");
            dateTimePicker3.Visible = true;
            dateTimePicker4.Visible = true;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            datevals[2] = dateTimePicker5.Value.ToString("yyyyMMdd") + dateTimePicker6.Value.ToString("yyyyMMdd");
        }

        private void SetBut_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(@Path.Combine(docloc, "pto.txt"), datevals);
        }
    }
}
