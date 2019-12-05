using System;
using System.Globalization;

namespace App.Core.Convertors
{
    public static class DateConvertor
    {

        public static string ToShamsi(this DateTime value)
        {
            var pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
        }

        public static DateTime ToShamsiParameter(this DateTime value)
        {
            var pc = new PersianCalendar();
            return new DateTime(pc.GetYear(value), pc.GetMonth(value), 1);
        }

        public static string ToLongShamsi(this DateTime value)
        {
            var pc = new PersianCalendar();
            var month = MonthToShamsi(pc.GetMonth(value));
            var day = ShamsiLongDays(pc.GetDayOfMonth(value));
            var weekDay = DayOfWeekShamsi(pc.GetDayOfWeek(value));
            return day + " " + month + " ماه " + pc.GetYear(value);
        }

        public static string ToLongShamsiArchive(this DateTime value)
        {
            var pc = new PersianCalendar();
            var month = MonthToShamsi(pc.GetMonth(value));
            return  month + " " + pc.GetYear(value);
        }
        public static string ToLongShamsiWithWeek(this DateTime value)
        {
            var pc = new PersianCalendar();
            var month = MonthToShamsi(pc.GetMonth(value));
            var weekDay = DayOfWeekShamsi(pc.GetDayOfWeek(value));
            return weekDay + " " + pc.GetDayOfMonth(value) + " " + month + " ماه " + pc.GetYear(value);
        }

        public static string DayOfWeekShamsi(DayOfWeek weekDay)
        {
            var dayWeek = "";
            switch (weekDay)
            {
                case DayOfWeek.Sunday:
                    dayWeek = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    dayWeek = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    dayWeek = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    dayWeek = "چهارشنبه";
                    break;
                case DayOfWeek.Thursday:
                    dayWeek = "پنجشنبه";
                    break;
                case DayOfWeek.Friday:
                    dayWeek = "جمعه";
                    break;
                case DayOfWeek.Saturday:
                    dayWeek = "شنبه";
                    break;
            }
            return dayWeek;
        }
        public static string ShamsiLongDays(int day)
        {
            var longDay = "";
            switch (day)
            {
                case 1:
                    longDay = "اول";
                    break;
                case 2:
                    longDay = "دوم";
                    break;
                case 3:
                    longDay = "سوم";
                    break;
                case 4:
                    longDay = "چهارم";
                    break;
                case 5:
                    longDay = "پنجم";
                    break;
                case 6:
                    longDay = "ششم";
                    break;
                case 7:
                    longDay = "هفتم";
                    break;
                case 8:
                    longDay = "هشتم";
                    break;
                case 9:
                    longDay = "نهم";
                    break;
                case 10:
                    longDay = "دهم";
                    break;
                case 11:
                    longDay = "یازدهم";
                    break;
                case 12:
                    longDay = "دوازدهم";
                    break;
                case 13:
                    longDay = "سیزدهم";
                    break;
                case 14:
                    longDay = "چهاردهم";
                    break;
                case 15:
                    longDay = "پانزدهم";
                    break;
                case 16:
                    longDay = "شانزدهم";
                    break;
                case 17:
                    longDay = "هفدهم";
                    break;
                case 18:
                    longDay = "هجدهم";
                    break;
                case 19:
                    longDay = "نوزدهم";
                    break;
                case 20:
                    longDay = "بیستم";
                    break;
                case 21:
                    longDay = "بیست و یکم";
                    break;
                case 22:
                    longDay = "بیست و دوم";
                    break;
                case 23:
                    longDay = "بیست و سوم";
                    break;
                case 24:
                    longDay = "بیست و چهارم";
                    break;
                case 25:
                    longDay = "بیست و پنجم";
                    break;
                case 26:
                    longDay = "بیست وششم";
                    break;
                case 27:
                    longDay = "بیست و هفتم";
                    break;
                case 28:
                    longDay = "بیست و هشتم";
                    break;
                case 29:
                    longDay = "بیست و نهم";
                    break;
                case 30:
                    longDay = "سی ام";
                    break;
                case 31:
                    longDay = "سی و یکم";
                    break;
            }
            return longDay;
        }
        public static string MonthToShamsi(int month)
        {
            var longMonth = "";
            if (month == 1)
                longMonth = "فروردین";
            else if (month == 2)
                longMonth = "اردیبهشت";
            else if (month == 3)
                longMonth = "خرداد";
            else if (month == 4)
                longMonth = "تیر";
            else if (month == 5)
                longMonth = "مرداد";
            else if (month == 6)
                longMonth = "شهریور";
            else if (month == 7)
                longMonth = "مهر";
            else if (month == 8)
                longMonth = "آبان";
            else if (month == 9)
                longMonth = "آذر";
            else if (month == 10)
                longMonth = "دی";
            else if (month == 11)
                longMonth = "بهمن";
            else if (month == 12) longMonth = "اسفند";

            return longMonth;
        }

        public static DateTime ToMiladiDate(this DateTime dt)
        {
            var pc = new PersianCalendar();
            return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
        }
        public static int PersianIntToEn(string persianNumbers)
        {
            var englishNumbers = "";
            for (var i = 0; i < persianNumbers.Length; i++)
            {
                englishNumbers += char.GetNumericValue(persianNumbers, i);
            }
            return Convert.ToInt32(englishNumbers);
        }
    }
}
