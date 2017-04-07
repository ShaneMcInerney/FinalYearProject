using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FYP.Business.Managers
{
    public abstract class BaseManager
    {
        #region Methods

        //Gets the start date of the year
        public DateTime GetStartOfYear()
        {
            //set current date
            DateTime currentDayOfTheYear = DateTime.Now;
            //set start of year
            var startOfTheYear = new DateTime(currentDayOfTheYear.Year, 1, 1);

            return startOfTheYear;
        }

        //Gets the start date of the month
        public DateTime GetStartOfMonth()
        {
            //set current date
            DateTime currentDayOfTheMonth = DateTime.Now;
            //get start of the month date
            var startOfTheMonth = new DateTime(currentDayOfTheMonth.Year, currentDayOfTheMonth.Month, 1);

            return startOfTheMonth;
        }

        //Gets the start date of the week
        public DateTime GetStartOfWeek()
        {
            //getting culture info
            CultureInfo ci = CultureInfo.CurrentCulture;
            //getting first day of week
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
            //getting today
            DayOfWeek today = DateTime.Now.DayOfWeek;
            //setting start of week 
            DateTime startOfWeek = DateTime.Now.AddDays(-(today - firstDayOfWeek));
            return startOfWeek;
        }

        //Gets the start date of the week
        public DateTime GetCurrentDayOfWeek()
        {
            //getting culture info
            CultureInfo ci = CultureInfo.CurrentCulture;
            //getting first day of week
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
            //getting today
            DayOfWeek today = DateTime.Now.DayOfWeek;
            //setting start of week 
            DateTime startOfWeek = DateTime.Now.AddDays(-(today - firstDayOfWeek));
            return startOfWeek;
        }

        //Gets the start date of the week
        public int GetDayOfWeek(DateTime date)
        {
            //getting culture info
            CultureInfo ci = CultureInfo.CurrentCulture;
            //getting first day of week
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;
            //getting today
            
            DayOfWeek day = date.DayOfWeek;
            int dayNum = DaySwitcher(day);
            
            return dayNum;
        }

        private int DaySwitcher(DayOfWeek dayOfTheWeek)
        {
            if (dayOfTheWeek == DayOfWeek.Sunday)
            {
                return 1;
            }
            if (dayOfTheWeek ==DayOfWeek.Monday)
            {
                return 2;
            }
            if (dayOfTheWeek == DayOfWeek.Tuesday)
            {
                return 3;
            }
            if (dayOfTheWeek == DayOfWeek.Wednesday)
            {
                return 4;
            }
            if (dayOfTheWeek == DayOfWeek.Thursday)
            {
                return 5;
            }
            if (dayOfTheWeek == DayOfWeek.Friday)
            {
                return 6;
            }
            if (dayOfTheWeek == DayOfWeek.Saturday)
            {
                return 7;
            }
            else return 0;
        }

        #endregion //Methods
    }
}
