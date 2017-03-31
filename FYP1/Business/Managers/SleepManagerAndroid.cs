using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP_Droid.Business.Managers
{
    public class SleepManagerAndroid : BaseManager, ISleepManager
    {
        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SleepManagerAndroid()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="appDatabase"></param>
        public SleepManagerAndroid(AppDatabase appDatabase)
        {
            this.AppDatabase = appDatabase;
        }
        #endregion //Constructors

        #region Property Accessors

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

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Delete all sleep entries in the DB
        /// </summary>
        public void DeleteAllSleepEntries()
        {
            this.AppDatabase.DeleteAllSleepEntries();
        }

        /// <summary>
        /// Delete sleep enty from teh apps DB
        /// </summary>
        /// <param name="id">ID of sleep entry to be deleted</param>
        /// <returns>ID of deleted sleep entry</returns>
        public int DeleteSleepEntry(int id)
        {
            return this.AppDatabase.DeleteSleepEntry(id);
        }

        /// <summary>
        /// Get latest sleep entry in the applications DB
        /// </summary>
        /// <returns>Most recent slee entry i the application's DB</returns>
        public SleepEntry GetLatestSleepEntry()
        {
            return this.AppDatabase.GetLatestSleepEntry();
        }

        /// <summary>
        /// Get all slep entries in the DB
        /// </summary>
        /// <returns>All sleep entries in the DB</returns>
        public IEnumerable<SleepEntry> GetAllSleepEntries()
        {
            return this.AppDatabase.GetAllSleepEntries();
        }

        /// <summary>
        /// Get sleep entry from the DB
        /// </summary>
        /// <param name="id">id of entry to retrieve</param>
        /// <returns>Instance of the sleep entry class</returns>
        public SleepEntry GetSleepEntry(int id)
        {
            return this.AppDatabase.GetSleepEntry(id);
        }

        /// <summary>
        /// Save sleep entry in the DB
        /// </summary>
        /// <param name="sleepEntry">Sleep entry to save in the DB</param>
        /// <returns>ID of saved sleep entry</returns>
        public int SaveSleepEntry(SleepEntry sleepEntry)
        {
            return this.AppDatabase.SaveSleepEntry(sleepEntry);
        }

        /// <summary>
        /// Check for the existance of a sleep entry in th DB
        /// </summary>
        /// <returns>bool indicating the existance of a sleep entry in the DB</returns>
        public bool SleepEntryExists()
        {
            return this.AppDatabase.SleepEntryExists();
        }

        /// <summary>
        /// Get sleep entries between dates
        /// </summary>
        /// <param name="start">start of period</param>
        /// <param name="end">end of period</param>
        /// <returns>all sleep entries between the given dates</returns>
        public IEnumerable<SleepEntry> GetSleepEntriesBetweenDates(DateTime start, DateTime end)
        {
            return GetAllSleepEntries().Where(x => x.Date.DayOfYear >= start.DayOfYear && x.Date.DayOfYear <= end.DayOfYear);
        }

        /// <summary>
        /// Get sum of sleep in list of sleep entries
        /// </summary>
        /// <param name="sleepEtries">sleep entries to get the total sum of hours for</param>
        /// <returns>sum of hurs of sleep in ist of sleep entries</returns>
        public double GetSumOfHoursOfSleepInList(List<SleepEntry> sleepEtries)
        {
            double sum = 0.0;
            foreach (var s in sleepEtries)
            {
                sum += s.SleepLength.Hours;
            }
            return sum;
        }

        /// <summary>
        /// Get all sleep entries for current
        /// </summary>
        /// <returns>all sleep entries for current month</returns>
        public IEnumerable<SleepEntry> GetSleepEntriesForCurrentMonth()
        {
            //get start of onth
            var startOfTheMonth = GetStartOfMonth();
            var output = GetSleepEntriesBetweenDates(startOfTheMonth, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Get all sleep entries for current week
        /// </summary>
        /// <returns>all sleep entries for current week</returns>
        public IEnumerable<SleepEntry> GetSleepEntriesForCurrentWeek()
        {
            //get start of week
            DateTime startOfWeek = GetStartOfWeek();
            return GetSleepEntriesBetweenDates(startOfWeek, DateTime.Now);
        }

        /// <summary>
        /// Get all sleep entries for current year
        /// </summary>
        /// <returns>//all sleep entries for the current year</returns>
        public IEnumerable<SleepEntry> GetSleepEntriesForCurrentYear()
        {
            //getting start of yeaar
            var startOfTheYear = GetStartOfYear();
            var output = GetSleepEntriesBetweenDates(startOfTheYear, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Convert sleep qaulity to string
        /// </summary>
        /// <param name="sleepEntry">sleep entry t retrieve quality of</param>
        /// <returns>string equivalent of sleep quality</returns>
        public string SleepQualityToString(SleepEntry sleepEntry)
        {
            switch (sleepEntry.SleepQuality)
            {
                case 0:
                    return "Very Bad";
                case 1:
                    return "Bad";
                case 2:
                    return "Average";
                case 3:
                    return "Good";
                case 4:
                    return "Very Good";
                default:
                    return "No Sleep Recorded Yet";
            }
        }

        #endregion //Methods
    }
}