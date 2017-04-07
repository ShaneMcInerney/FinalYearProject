using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP_Droid.Business.Managers
{
    public class StepManagerAndroid : BaseManager, IStepManager
    {

        #region Fields

        private AppDatabase m_appDatabase;
        private Int64 m_stepsToday = 0;
        private Int64 m_hourlySteps = 0;
        private Int64 m_lastStepCount = 0;
        private double m_strideLength;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public StepManagerAndroid()
        {

        }

        /// <summary>
        /// Class Constuctor
        /// </summary>
        /// <param name="database"></param>
        public StepManagerAndroid(AppDatabase database)
        {
            this.m_appDatabase = database;
        }


        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_stepsToday
        /// </summary>
        public long StepsToday
        {
            get
            {
                return m_stepsToday;
            }

            set
            {
                m_stepsToday = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_hourlySteps
        /// </summary>
        public long HourlySteps
        {
            get
            {
                return m_hourlySteps;
            }

            set
            {
                m_hourlySteps = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_lastStepCount
        /// </summary>
        public long LastStepCount
        {
            get
            {
                return m_lastStepCount;
            }

            set
            {
                m_lastStepCount = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_appDatabase
        /// </summary>
        public AppDatabase AppDatabase
        {
            get
            {
                return m_appDatabase;
            }

            set
            {
                m_appDatabase = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_strideLength
        /// </summary>
        public double StrideLength
        {
            get
            {
                return m_strideLength;
            }
            set
            {
                this.m_strideLength = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Get step entry by ID
        /// </summary>
        /// <param name="id">ID of ntry to retrieve</param>
        /// <returns>step entry</returns>
        public StepEntry GetStepEntry(int id)
        {
            return AppDatabase.GetStepEntry(id);
        }

        /// <summary>
        /// Get all step entries in DB
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StepEntry> GetAllStepEntries()
        {
            return AppDatabase.GetAllStepEntries();
        }

        /// <summary>
        /// Get all step entries for given date
        /// </summary>
        /// <param name="date">date to retrieve entries fr</param>
        /// <returns>step entries for gien date</returns>
        public IEnumerable<StepEntry> GetStepEntriesForDate(DateTime date)
        {
            var stepsForDate = AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear == date.Date.DayOfYear);
            return stepsForDate;
        }

        /// <summary>
        /// Get entries for date and hur
        /// </summary>
        /// <param name="date">date to get entries for</param>
        /// <param name="hour">hour to get entry for</param>
        /// <returns>entries for date and hour</returns>
        public IEnumerable<StepEntry> GetStepEntriesForDateAndHour(DateTime date, TimeSpan hour)
        {
            return AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear == date.DayOfYear && x.Date.Hour == hour.Hours);
        }

        /// <summary>
        /// Get entries for date between hours
        /// </summary>
        /// <param name="date">date to retrieve entries or</param>
        /// <param name="startTime">start of period</param>
        /// <param name="endTime">end of period</param>
        /// <returns></returns>
        public IEnumerable<StepEntry> GetStepEntriesForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear == date.DayOfYear && x.Date.Hour >= startTime.Hours && x.Date.Hour <= endTime.Hours);
        }

        /// <summary>
        /// Get step entries between dates
        /// </summary>
        /// <param name="startDate">start of period</param>
        /// <param name="endDate">end of period</param>
        /// <returns>list of step entries between dates</returns>
        public IEnumerable<StepEntry> GetStepEntriesBetweenDates(DateTime startDate, DateTime endDate)
        {
            return AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear >= startDate.DayOfYear && x.Date.DayOfYear <= endDate.DayOfYear);
        }

        /// <summary>
        /// Save step entry to DB
        /// </summary>
        /// <param name="stepEntry">step entry to save</param>
        /// <returns>id of saved entry</returns>
        public int SaveStepEntry(StepEntry stepEntry)
        {
            return AppDatabase.SaveStepEntry(stepEntry);
        }

        /// <summary>
        /// Delete step entry by id
        /// </summary>
        /// <param name="id">id of entry to delete</param>
        /// <returns>id of deleted entry</returns>
        public int DeleteStepEntry(int id)
        {
            return AppDatabase.DeleteStepEntry(id);
        }

        /// <summary>
        /// Delete all step entries
        /// </summary>
        public void DeleteAllStepEntries()
        {
            AppDatabase.DeleteAllStepEntries();
        }

        /// <summary>
        /// Delete step entries for date
        /// </summary>
        /// <param name="date">date to delete entries for</param>
        public void DeleteStepEntriesForDate(DateTime date)
        {
            var listToDelete = AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear == date.DayOfYear);

            DeleteAllStepEntriesInList(listToDelete);
        }

        /// <summary>
        /// Delete all step entries between give dates
        /// </summary>
        /// <param name="startDate">start of period</param>
        /// <param name="endDate">end of period</param>
        public void DeleteStepEntriesBetweenDates(DateTime startDate, DateTime endDate)
        {
            var listToDelete = AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear >= startDate.DayOfYear && x.Date.DayOfYear <= endDate.DayOfYear);

            DeleteAllStepEntriesInList(listToDelete);
        }

        /// <summary>
        /// Delete step entries for date and between times
        /// </summary>
        /// <param name="date">date to delete for</param>
        /// <param name="startTime">start of time period</param>
        /// <param name="endTime">end of time period</param>
        public void DeleteStepEntriesForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var listToDelete = AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear == date.DayOfYear && x.Date.Hour >= startTime.Hours && x.Date.Hour <= endTime.Hours);

            DeleteAllStepEntriesInList(listToDelete);
        }

        /// <summary>
        /// Delete all step entries matching date and time
        /// </summary>
        /// <param name="date">date to delete for</param>
        /// <param name="time">time to delete for</param>
        public void DeleteStepEntriesForDateAndForHour(DateTime date, TimeSpan time)
        {
            var listToDelete = AppDatabase.GetAllStepEntries().Where(x => x.Date.DayOfYear == date.DayOfYear && x.Date.Hour == time.Hours);

            DeleteAllStepEntriesInList(listToDelete);
        }

        /// <summary>
        /// Delete all step entries in given list
        /// </summary>
        /// <param name="listToDelete">list of entries to delete</param>
        public void DeleteAllStepEntriesInList(IEnumerable<StepEntry> listToDelete)
        {
            foreach (var item in listToDelete)
            {
                AppDatabase.DeleteStepEntry(item.ID);
            }
        }

        /// <summary>
        /// Get total ste count in given list of entries
        /// </summary>
        /// <param name="listOfStepEntries">list of entries to get the total number of steps for</param>
        /// <returns>total number of steps in lsit</returns>
        public Int64 GetSumOfStepCountsInList(IEnumerable<StepEntry> listOfStepEntries)
        {
            Int64 totalSteps = 0;

            foreach (var entry in listOfStepEntries)
            {
                totalSteps += entry.Count;
            }

            return totalSteps;
        }

        /// <summary>
        /// Get the total nubmer of steps for a given date
        /// </summary>
        /// <param name="date">date to retrieve step count for</param>
        /// <returns>total steps for given date</returns>
        public Int64 GetTotalStepsForDate(DateTime date)
        {
            var listOfEntries = GetStepEntriesForDate(date);

            return GetSumOfStepCountsInList(listOfEntries);
        }

        /// <summary>
        /// Get total number of steps for current date
        /// </summary>
        /// <returns>step count for current date</returns>
        public Int64 GetTotalStepsForCurrentDate()
        {
            //get entries for current date
            var listOfEntries = GetStepEntriesForDate(DateTime.Now);

            if (listOfEntries != null && listOfEntries.Any() != false)
            {
                //get sum of step count in list
                return GetSumOfStepCountsInList(listOfEntries);
            }

            else return 0;
        }

        /// <summary>
        /// Get total step count for hour
        /// </summary>
        /// <param name="time">time to retriev stesp for</param>
        /// <returns>step count for given hour</returns>
        public Int64 GetTotalStepsForHour(TimeSpan time)
        {
            return GetSumOfStepCountsInList(GetStepEntriesForDateAndHour(DateTime.Now, time));
        }

        /// <summary>
        /// Estimate the distance covered by the user based on the number of steps
        /// </summary>
        /// <param name="numSteps">number of steps to estimate the distance for</param>
        /// <returns>an estimate of the distance covered</returns>
        public double EstimateDistanceCovered(long numSteps)
        {
            var distanceEstimate = StrideLength * numSteps;

            return Math.Round(distanceEstimate, 2);
        }

        /// <summary>
        /// Checks whether or ot a step entry exists
        /// </summary>
        /// <returns>bool based on the existance of a step entry</returns>
        public bool StepEntryExists()
        {
            return AppDatabase.StepEntryExists();
        }

        /// <summary>
        /// Step count for the current day
        /// </summary>
        /// <returns>step count for the current day</returns>
        public Int64 GetStepCountForCurrentDay()
        {
            return GetSumOfStepCountsInList(GetStepEntriesForDate(DateTime.Now));
        }

        /// <summary>
        /// Get the total number of steps taken for the date
        /// </summary>
        /// <param name="date">date to retrieve steps for</param>
        /// <returns>amoutn of steps for given date</returns>
        public Int64 GetStepCountForDay(DateTime date)
        {
            DateTime currentDay = date;
            return GetSumOfStepCountsInList(GetStepEntriesForDate(currentDay));
        }

        /// <summary>
        /// Get the total number of steps taken for the week
        /// </summary>
        /// <returns>total number of steps taken for the month</returns>
        public Int64 GetStepCountForCurrentWeek()
        {
            //get start of week
            DateTime startOfWeek = GetStartOfWeek();
            return GetSumOfStepCountsInList(GetStepEntriesBetweenDates(startOfWeek, DateTime.Now));
        }

        /// <summary>
        /// Get the total number of steps taken for the month
        /// </summary>
        /// <returns>total number of steps taken for the month</returns>
        public Int64 GetStepCountForCurrentMonth()
        {
            //setting start of the month
            var startOfTheMonth = GetStartOfMonth();
            var output = GetStepEntriesBetweenDates(startOfTheMonth, DateTime.Now);
            return GetSumOfStepCountsInList(output);
        }

        /// <summary>
        /// Get the total number of steps taken for the year
        /// </summary>
        /// <returns>the number of steps for the year</returns>
        public Int64 GetStepCountForCurrentYear()
        {
            //set start of year
            var startOfTheYear = GetStartOfYear();

            var output = GetStepEntriesBetweenDates(startOfTheYear, DateTime.Now);
            return GetSumOfStepCountsInList(output);
        }

        /// <summary>
        /// Get all step entries for current week
        /// </summary>
        /// <returns>all step entries for current week</returns>
        public IEnumerable<StepEntry> GetStepEntriesForCurrentWeek()
        {
            //getting start of week date
            DateTime startOfWeek = GetStartOfWeek();
            return GetStepEntriesBetweenDates(startOfWeek, DateTime.Now);
        }

        /// <summary>
        /// gets all step entries for the current month
        /// </summary>
        /// <returns>step entries for current month</returns>
        public IEnumerable<StepEntry> GetStepEntriesForCurrentMonth()
        {
            //get start of month
            var startOfTheMonth = GetStartOfMonth();
            var output = GetStepEntriesBetweenDates(startOfTheMonth, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Get all step entries for current year
        /// </summary>
        /// <returns>all step entries for current year</returns>
        public IEnumerable<StepEntry> GetStepEntriesForCurrentYear()
        {
            //Get start of the year
            var startOfTheYear = GetStartOfYear();
            var output = GetStepEntriesBetweenDates(startOfTheYear, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Get the daily total number of steps for each day in current week
        /// </summary>
        /// <param name="stepEntries">step entries for the week</param>
        /// <returns>list of step entries ,one entry per day, with daily total steps</returns>
        public List<StepEntry> GetDailyTotalsForCurrentWeek(List<StepEntry> stepEntries)
        {
            //getting start of week
            DateTime startOfWeek = GetStartOfWeek();
            //new list ofdaily total step entries
            List<StepEntry> dailiyTotals = new List<StepEntry>();
            //set year
            var year = DateTime.Now.Year;
            //set month
            var month = DateTime.Now.Month;
            //for each day of the week up to current
            for(var date =startOfWeek;date<=DateTime.Now;date=date.AddDays(1))
            {
                //get amount for day
                var amount = GetTotalStepsForDate(date);
                //add step entry to daily totals
                dailiyTotals.Add(new StepEntry(amount, date, new TimeSpan(24, 0, 0)));
            }
          /*  for (int i = startOfWeek.Day; i <= DateTime.Now.Day; i++)
            {
                //new date for day
                DateTime d = new DateTime(year, month, i);
                //get amount for day
                var amount = GetTotalStepsForDate(d);
                //add step entry to daily totals
                dailiyTotals.Add(new StepEntry(amount, d, new TimeSpan(24, 0, 0)));
            }*/
            return dailiyTotals;
        }

        /// <summary>
        /// Get the daily total number of steps for ech day in current month
        /// </summary>
        /// <param name="stepEntries">step entries for the month</param>
        /// <returns>list of step entries ,one entry per day, with daily total steps</returns>
        public List<StepEntry> GetDailyTotalsForCurrentMonth(List<StepEntry> stepEntries)
        {
            //get start of month
            var startOfTheMonth = GetStartOfMonth();
            //new list of daily totals
            List<StepEntry> dailiyTotals = new List<StepEntry>();
            //set year
            var year = DateTime.Now.Year;
            //set month
            var month = DateTime.Now.Month;
            //foreach day of the month
            for (int i = startOfTheMonth.Day; i <= DateTime.Now.Day; i++)
            {
                //set date
                DateTime d = new DateTime(year, month, i);
                //get amount of steps for date
                var amount = GetTotalStepsForDate(d);
                //add new step entry to daily totals
                dailiyTotals.Add(new StepEntry(amount, d, new TimeSpan(24, 0, 0)));
            }
            return dailiyTotals;
        }

        /// <summary>
        /// Gets the daily total number of steps for each day
        /// </summary>
        /// <param name="stepEntries">entries to get daily total for</param>
        /// <returns>a list of new stpe entries, one entry perday, with the stpe total</returns>
        public List<StepEntry> GetDailyTotalsForCurrentYear(List<StepEntry> stepEntries)
        {
            //set start of year
            var startOfTheYear = GetStartOfYear();
            //new list of step etries to hold daily totals
            List<StepEntry> dailiyTotals = new List<StepEntry>();
            //current year 
            var year = DateTime.Now.Year;
            //current month
            var month = DateTime.Now.Month;
            //for every month up to and including this month
            for (int i = startOfTheYear.Month; i <= month; i++)
            {
                //for each day in that month
                for (int j = 1; j <= DateTime.DaysInMonth(year, i); j++)
                {
                    //set date
                    DateTime d = new DateTime(year, i, j);
                    //get the nuber of steps for that date
                    var amount = GetTotalStepsForDate(d);
                    //add a new step entry to dail totals
                    dailiyTotals.Add(new StepEntry(amount, d, new TimeSpan(24, 0, 0)));
                }
            }
            return dailiyTotals;
        }
        #endregion //Methods

    }
}