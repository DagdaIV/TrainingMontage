﻿using System;
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
    public partial class Shoyeido : Form
    {
        private bool TmSrt = false;
        private bool SetGoalVal = false;
        private bool SetCTDVal = false;
        private bool Weekday = false;
        private bool Weeks = false;
        private bool Months = false;
        private bool Years = false;
        private double CPDBase;
        private double CPDAdj;
        private double CPDAdjPrec;
        private double CPHBase;
        private double CPHAdj;
        private double CPHAdjPrec;
        private double DayFinPerc;
        private int wdthismonth;
        private int wdremain;
        private double wdremainprec;
        private DateTime firstdayom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private double TPCAdj;
        private double TPCAdjPrec;
        private double TPCBase;
        private double callshouldatm;
        private double CallsTodayVal = 0;
        private double CallsToDateVal = 0;
        private double CallGoalVal = 0;
        private double totaltimeleft;
        private TimeSpan timespent;
        private TimeSpan timeshort = new TimeSpan(0, 0, 0);
        private TimeSpan halfhour = new TimeSpan(0, 30, 0);
        private DateTime Moment = new DateTime();
        string docloc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        public Shoyeido()
        {
            InitializeComponent();
            if(DateTime.Today.DayOfWeek == DayOfWeek.Monday && DateTime.Today.Day <=3)
            CallsTodayVal = 0;
            wdthismonth = workdaytot(firstdayom);
            wdremain = workdaytot(DateTime.Today);
            wdremainprec = (double)wdremain - 1 + DayFinPerc;
            string[] entrytext = new string[3];
            if (!File.Exists(@Path.Combine(docloc, "pto.txt")))
                File.WriteAllLines(@Path.Combine(docloc, "pto.txt"), new string[]{"","",""});
        }

        public DateTime ConvDate(string date)
        {
            DateTime dayquest = new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));
            return dayquest;
        }

        private bool DateBetween (DateTime inpdate, string datestring)
        {
            if (!datestring.Equals("") && (inpdate >= ConvDate(datestring.Substring(0, 8)) && inpdate <= ConvDate(datestring.Substring(8, 8))))
                return true;
            else
                return false;
        }

        private bool ptotest(DateTime UseDate)
        {
            string[] ptodates = new string[3];
            if (File.Exists(@Path.Combine(docloc, "pto.txt")))
                ptodates = File.ReadAllLines(@Path.Combine(docloc, "pto.txt"));
            else
            {
                ptodates= new string[]{"","",""};
            }
            for (int counter = 0; counter <= 2; counter++)
            {
                if (DateBetween(UseDate, ptodates[counter]))
                    return true;
            }
            return false;
        }

        private bool isholiday(DateTime UseDate)
        {
            DateTime laborhelper = new DateTime(UseDate.Year, 9, 1);
            DateTime thankshelper = new DateTime(UseDate.Year, 11, 1);
            DateTime newyear = new DateTime(UseDate.Year, 1, 1);
            DateTime memorial = new DateTime(UseDate.Year, 5, 30);
            DateTime independ = new DateTime(UseDate.Year, 7, 4);
            DateTime labor = new DateTime(UseDate.Year, 9, 9 - (int)laborhelper.DayOfWeek);
            DateTime thanks = new DateTime(UseDate.Year, 11, 26 - (int)thankshelper.DayOfWeek);
            DateTime christ = new DateTime(UseDate.Year, 12, 25);
            if ((byte)UseDate.DayOfWeek==0|| (byte)UseDate.DayOfWeek==6|| ptotest(UseDate) || UseDate.Equals(newyear) || UseDate.Equals(memorial) || UseDate.Equals(independ) || UseDate.Equals(labor) || UseDate.Equals(thanks) || UseDate.Equals(christ))
                return true;
            else
                return false;
        }

        private void TimeStream(object source, EventArgs e)
        {
              Moment = DateTime.Now;
              CurrentTime.Text = Moment.ToString("h:mm:ss");
              CPDAdjPrec = (CallGoalVal - (CallsToDateVal + CallsTodayVal)) / ((wdremain-1) + (totaltimeleft / 8));
              CPHAdjPrec = CPDAdjPrec / 8;
              TPCAdjPrec = 60 / CPHAdjPrec / 60;
              DayFinPerc = (timetodec(timespent) / (8-timetodec(timeshort)));
              TPCChange.Text = dectoHHMM(TPCAdjPrec - TPCAdj);
              TimePerCall.Text = dectoHHMM(TPCAdj);
              if (Moment.TimeOfDay.Hours < 13)
                  timespent = Moment.AddHours(-8.5).TimeOfDay;
              else
                  timespent = Moment.AddHours(-9.5).TimeOfDay;
              totaltimeleft = 8 - timetodec(timeshort.Add(timespent));
              callshouldatm = CPDAdj * DayFinPerc;
              if (CallsTodayVal > CPDAdj)
                  SpareTime.Text = "Goal Complete!";
              else if (CallsTodayVal > callshouldatm)
                  SpareTime.Text = dectoHHMM((CallsTodayVal - callshouldatm) * TPCAdj);
              else
                  SpareTime.Text = "-(" + (int)(callshouldatm - CallsTodayVal + 1) + ") calls";
        }

        private string dectoHHMM(double decinput)
        {
            int Hour;
            int Minute;
            int Second;
            string negifier = "";
            if (decinput < 0)
            {
                decinput = decinput * -1;
                negifier = "-";
            }
            Hour = (int)Math.Round(decinput - .5);
            Minute = (int)Math.Round(((decinput - Hour) * 60) - .5);
            if (Minute == 60)
            {
                Hour++;
                Minute = 0;
            }
            Second = (int)Math.Round(((((decinput - Hour) * 60) - Minute) * 60) - .5);
            if (Second == 60)
            {
                Minute++;
                Second = 0;
            }
            if (Hour > 0)
                return negifier + Hour + ":" + Minute.ToString("D2") + ":" + Second.ToString("D2");
            else
                return negifier + Minute + ":" + Second.ToString("D2");
        }

        public DateTime WeeksReturn(int addweek)
        {
            DateTime finaldate = DateTime.Today.AddDays(7 * addweek);
            while (isholiday(finaldate))
                    finaldate = finaldate.AddDays(7);
            return finaldate;
        }

        public DateTime MonthsReturn(int addmonth)
        {
            DateTime finaldate = DateTime.Today.AddDays(28 * addmonth);
            while (isholiday(finaldate))
                finaldate = finaldate.AddDays(7); 
            return finaldate;
        }

        private double timetodec(TimeSpan inputtime)
        {
            double hours = inputtime.Hours;
            double minutes = inputtime.Minutes;
            double seconds = inputtime.Seconds;
            return hours + (minutes / 60) + (seconds / 3600);
        }

        private int workdaytot(DateTime startdate)
        {
            int workdays = 0;
            for (double count = 0; count <= startdate.AddMonths(1).AddDays(-startdate.Day).Day - startdate.Day; count++)
            {
                if ((int)startdate.AddDays(count).DayOfWeek > 0 && (int)startdate.AddDays(count).DayOfWeek <= 5 && !isholiday(startdate.AddDays(count)))
                    workdays++;
            }
            return workdays;
        }

        public DateTime WeekdayReturn(double aimday)
        {
            double toadd = (double)DateTime.Today.DayOfWeek;
            if (aimday <= toadd)
            {
                toadd = aimday - toadd;
                toadd += 7;
            }
            else
                toadd = aimday - toadd;
            DateTime finaldate = DateTime.Today.AddDays(toadd);
            while (isholiday(finaldate))
                finaldate = finaldate.AddDays(7);
            return finaldate;
        }

        public DateTime YearsReturn(byte addyear)
        {
            DateTime finaldate = DateTime.Today.AddDays(364 * addyear);
            while (isholiday(finaldate))
                finaldate = finaldate.AddDays(7);
            return finaldate;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(CallToday.Text, out CallsTodayVal);
            CPDBase = CallGoalVal / wdthismonth;
            CPDAdj = (CallGoalVal - CallsToDateVal) / wdremain;
            CPHBase = CPDBase / 8;
            CPHAdj = CPDAdj / 8;
            TPCBase = 1 / CPHBase;
            TPCAdj = 1 / CPHAdj;
            TimePerCall.Text = dectoHHMM(TPCAdj);
        }

        private bool checkarray(int[] values, int input)
        {
            for (int count = 0; count < values.Length; count++)
            {
                if (input == values[count])
                    return true;
            }
            return false;
        }
        private bool inrange(int low, int value, int high)
        {
            if (value <= high && value >= low)
                return true;
            return false;
        }

        private void PCode_TextChanged(object sender, EventArgs e)
        {
            int PoCoVal;
            int[] zoneOne = { 398, 399, 475 };
            int[] zoneTwo = { 323, 324, 473, 474, 476, 477, 885 };
            int[] zoneThree = { 836, 837 };
            int[] zoneFour = { 835, 838 };
            int[] zoneSix = { 967, 968 };
            int.TryParse(PCode.Text, out PoCoVal);
            if (checkarray(zoneOne, PoCoVal) || inrange(5, PoCoVal, 323) || inrange(325, PoCoVal, 349) || inrange(376, PoCoVal, 379) || inrange(400, PoCoVal, 472) || inrange(478, PoCoVal, 499) || inrange(569, PoCoVal, 588))
                TZone.Text = "1";
            else if (checkarray(zoneTwo, PoCoVal) || inrange(350, PoCoVal, 375) || inrange(380, PoCoVal, 397) || inrange(500, PoCoVal, 567) || inrange(600, PoCoVal, 689) || inrange(694, PoCoVal, 798))
                TZone.Text = "2";
            else if (checkarray(zoneThree, PoCoVal) || inrange(590, PoCoVal, 599) || inrange(690, PoCoVal, 693) || inrange(799, PoCoVal, 834) || inrange(840, PoCoVal, 884) || inrange(889, PoCoVal, 898))
                TZone.Text = "3";
            else if (checkarray(zoneFour, PoCoVal) || inrange(900, PoCoVal, 961) || inrange(970, PoCoVal, 994))
                TZone.Text = "4";
            else if (inrange(995, PoCoVal, 999))
                TZone.Text = "5";
            else if (checkarray(zoneSix, PoCoVal))
                TZone.Text = "6";
            else
                TZone.Text = "???";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            CallsTodayVal++;
            CallToday.Text = "" + (CallsTodayVal);
        }

        private void MinusOne_Click(object sender, EventArgs e)
        {
            CallsTodayVal--;
            CallToday.Text = "" + (CallsTodayVal);
        }
        private void OnExitSave(object sender, EventArgs e)
        {

        }

        private void PlusTen_Click(object sender, EventArgs e)
        {
            CallsTodayVal +=10;
            CallToday.Text = "" + (CallsTodayVal);
        }

        private void MinusTen_Click(object sender, EventArgs e)
        {
            CallsTodayVal-=10;
            CallToday.Text = "" + (CallsTodayVal);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Weekday)
                Clipboard.SetText(WeekdayReturn(1).ToString("MM/dd/yyyy"));
            if (Weeks)
                Clipboard.SetText(WeeksReturn(1).ToString("MM/dd/yyyy"));
            if (Months)
                Clipboard.SetText(MonthsReturn(1).ToString("MM/dd/yyyy"));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Weekday)
                Clipboard.SetText(WeekdayReturn(2).ToString("MM/dd/yyyy"));
            if (Weeks)
                Clipboard.SetText(WeeksReturn(2).ToString("MM/dd/yyyy"));
            if (Months)
                Clipboard.SetText(MonthsReturn(2).ToString("MM/dd/yyyy"));
            if (Years)
                Clipboard.SetText(YearsReturn(2).ToString("MM/dd/yyyy"));
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Weekday)
                Clipboard.SetText(WeekdayReturn(3).ToString("MM/dd/yyyy"));
            if (Weeks)
                Clipboard.SetText(WeeksReturn(3).ToString("MM/dd/yyyy"));
            if (Months)
                Clipboard.SetText(MonthsReturn(3).ToString("MM/dd/yyyy"));
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (Weekday)
                Clipboard.SetText(WeekdayReturn(4).ToString("MM/dd/yyyy"));
            if (Weeks)
                Clipboard.SetText(WeeksReturn(4).ToString("MM/dd/yyyy"));
            if (Months)
                Clipboard.SetText(MonthsReturn(4).ToString("MM/dd/yyyy"));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (Weekday)
                Clipboard.SetText(WeekdayReturn(5).ToString("MM/dd/yyyy"));
            if (Weeks)
                Clipboard.SetText(WeeksReturn(5).ToString("MM/dd/yyyy"));
            if (Months)
                Clipboard.SetText(MonthsReturn(5).ToString("MM/dd/yyyy"));
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (Weeks)
                Clipboard.SetText(WeeksReturn(6).ToString("MM/dd/yyyy"));
            if (Months)
                Clipboard.SetText(MonthsReturn(6).ToString("MM/dd/yyyy"));
        }

        private void WeekdayBut_Click_1(object sender, EventArgs e)
        {
            Weekday = true;
            Weeks = false;
            Months = false;
            Years = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = false;
            button1.Text = "Mon";
            button2.Text = "Tue";
            button3.Text = "Wed";
            button4.Text = "Thu";
            button5.Text = "Fri";
        }

        private void WeeksBut_Click_1(object sender, EventArgs e)
        {
            Weekday = false;
            Weeks = true;
            Months = false;
            Years = false;
            button1.Text = "1";
            button2.Text = "2";
            button3.Text = "3";
            button4.Text = "4";
            button5.Text = "5";
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
        }

        private void MonthsBut_Click_1(object sender, EventArgs e)
        {
            Weekday = false;
            Weeks = false;
            Months = true;
            Years = false;
            button1.Text = "1";
            button2.Text = "2";
            button3.Text = "3";
            button4.Text = "4";
            button5.Text = "5";
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
        }

        private void TimeShortPlus_Click(object sender, EventArgs e)
        {
            timeshort = timeshort.Add(halfhour);
            TShort.Text = dectoHHMM(timetodec(timeshort));
        }

        private void TimeShortMinus_Click(object sender, EventArgs e)
        {
            timeshort = timeshort.Subtract(halfhour);
            TShort.Text = dectoHHMM(timetodec(timeshort));
        }

        private void TodayBut_Click(object sender, EventArgs e)
        { 
            Clipboard.SetText(DateTime.Today.ToString("MM/dd/yyyy"));
        }

        private void TomorrowBut_Click(object sender, EventArgs e)
        {
            if (isholiday(DateTime.Today.AddDays(1)))
                MessageBox.Show("This date is a holiday!");
            else
                Clipboard.SetText(DateTime.Today.AddDays(1).ToString("MM/dd/yyyy"));
        }

        private void WSPBut_Click(object sender, EventArgs e)
        {
            DateTime Targday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            for (byte count = 0; count < 7; count++)
            {
                Targday = Targday.AddDays(1);
                if (isholiday(Targday))
                    count--;
            }
            Clipboard.SetText(Targday.ToString("MM/dd/yyyy"));
        }

        private void PTOFormSpawn_Click(object sender, EventArgs e)
        {
            Form2 PTOForm = new Form2();
            PTOForm.Show();
        }

        private void OneWeekBut_Click(object sender, EventArgs e)
        {
            if (isholiday(WeeksReturn(1)))
                MessageBox.Show("This date is a holiday!"); 
            else
                Clipboard.SetText(WeeksReturn(1).ToString("MM/dd/yyyy"));
        }

        private void SetCallGoal_Click(object sender, EventArgs e)
        {

            double.TryParse(CallGoal.Text, out CallGoalVal);
            CPDBase = CallGoalVal / wdthismonth;
            CPDAdj = (CallGoalVal - CallsToDateVal) / wdremain;
            CPHBase = CPDBase / 8;
            CPHAdj = CPDAdj / 8;
            TPCBase = 1 / CPHBase;
            TPCAdj = 1 / CPHAdj;
            TimePerCall.Text = dectoHHMM(TPCAdj);
            SetGoalVal = true;
            if (!TmSrt && SetGoalVal && SetCTDVal)
            {
                Timer Repetez = new Timer();
                Repetez.Tick += new EventHandler(TimeStream);
                Repetez.Interval = 50;
                Repetez.Enabled = true;
                TmSrt = true;
            }
        }

        private void CTDSetBut_Click(object sender, EventArgs e)
        {
            double.TryParse(CallToDate.Text, out CallsToDateVal);
            CPDBase = CallGoalVal / wdthismonth;
            CPDAdj = (CallGoalVal - CallsToDateVal) / wdremain;
            CPHBase = CPDBase / 8;
            CPHAdj = CPDAdj / 8;
            TPCBase = 1 / CPHBase;
            TPCAdj = 1 / CPHAdj;
            SetCTDVal = true;
            if (!TmSrt && SetGoalVal && SetCTDVal)
            {
                Timer Repetez = new Timer();
                Repetez.Tick += new EventHandler(TimeStream);
                Repetez.Interval = 50;
                Repetez.Enabled = true;
                TmSrt = true;
            }
        }

        private void IntroBut_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DateTime.Today.ToString("M/dd/yy")+" called them,");
        }

        private void YearBut_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(YearsReturn(1).ToString("MM/dd/yyyy"));
        }

    }
}
