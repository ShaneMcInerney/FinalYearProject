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


        #endregion //Methods
    }
}
