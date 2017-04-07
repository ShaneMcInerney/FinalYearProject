using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP_Droid.Business.Managers
{
    public class WeightManagerAndroid : BaseManager, IWeightManager
    {

        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WeightManagerAndroid()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="database"></param>
        public WeightManagerAndroid(AppDatabase database)
        {
            this.m_appDatabase = database;
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
        /// Get weight entry by ID
        /// </summary>
        /// <param name="id">ID of weight entry to retrieve</param>
        /// <returns>Instance of weight entry class</returns>
        public WeightEntry GetWeightEntry(int id)
        {
            return AppDatabase.GetWeightEntry(id);
        }

        /// <summary>
        /// Get all weight entries in the DB
        /// </summary>
        /// <returns>All weight entries in the DB</returns>
        public IEnumerable<WeightEntry> GetAllWeightEntries()
        {
            return AppDatabase.GetAllWeightEntries();
        }

        /// <summary>
        /// Get first weight entry for given date
        /// </summary>
        /// <param name="date">date to retrieve first weight entry for</param>
        /// <returns>Instance of the weight entry class</returns>
        public WeightEntry GetFirstWeightEntryForDate(DateTime date)
        {
            return AppDatabase.GetAllWeightEntries().FirstOrDefault(x => x.Date.Date == date.Date);
        }

        /// <summary>
        /// Get last weight entry fro a given day of the year
        /// </summary>
        /// <param name="dayOfTheYear">day of the year to retrieve last weight entry for</param>
        /// <returns>last entry for the given day of the year</returns>
        public WeightEntry GetLastWeightEntryForDayOfYear(int dayOfTheYear)
        {
            return AppDatabase.GetAllWeightEntries().LastOrDefault(x => x.Date.DayOfYear == dayOfTheYear);
        }

        /// <summary>
        /// Get last weight entry fro a given day of the year
        /// </summary>
        /// <param name="dayOfTheYear">day of the year to retrieve last weight entry for</param>
        /// <returns>last entry for the given day of the year</returns>
        public WeightEntry GetLastWeightEntryForDate(DateTime date)
        {
            return AppDatabase.GetAllWeightEntries().LastOrDefault(x => x.Date.DayOfYear == date.DayOfYear);
        }

        /// <summary>
        /// Get weight entries for given date
        /// </summary>
        /// <param name="date">date to retrieve entries for</param>
        /// <returns>entries for given date</returns>
        public IEnumerable<WeightEntry> GetWeightEntriesForDate(DateTime date)
        {
            return AppDatabase.GetAllWeightEntries().Where(x => x.Date.Date == date.Date);
        }

        /// <summary>
        /// Save weight entry to DB
        /// </summary>
        /// <param name="weightEntry">weight entry to save</param>
        /// <returns>ID of saved weight entry</returns>
        public int SaveWeightEntry(WeightEntry weightEntry)
        {
            return AppDatabase.SaveWeightEntry(weightEntry);
        }

        /// <summary>
        /// Delete al weight entries in DB by date
        /// </summary>
        /// <param name="date">date to delete entries for</param>
        public void DeleteAllWeightEntriesForDate(DateTime date)
        {
            var listToDelete = AppDatabase.GetAllWeightEntries().Where(x => x.Date == date);

            DeleteWeightEntriesInList(listToDelete);
        }

        /// <summary>
        /// Delete all weight entries in DB
        /// </summary>
        public void DeleteAllWeightEntries()
        {
            var listToDelete = AppDatabase.GetAllWeightEntries();

            DeleteWeightEntriesInList(listToDelete);
        }

        /// <summary>
        /// Delete weight entry by id
        /// </summary>
        /// <param name="id">id of entry to delete</param>
        /// <returns>ID of deleted entry</returns>
        public int DeleteWeightEntry(int id)
        {
            return AppDatabase.DeleteWeightEntry(id);
        }

        /// <summary>
        /// Delete all entries in a given list 
        /// </summary>
        /// <param name="listToDelete">List of entries to delete</param>
        public void DeleteWeightEntriesInList(IEnumerable<WeightEntry> listToDelete)
        {
            foreach (var item in listToDelete)
            {
                AppDatabase.DeleteWeightEntry(item.ID);
            }
        }

        /// <summary>
        /// Checks if weight entry exists in DB
        /// </summary>
        /// <returns> bools based on weight entry existence in DB</returns>
        public bool WeightEntryExists()
        {
            return AppDatabase.WeightEntryExists();
        }

        /// <summary>
        /// Gets last weight entry in DB
        /// </summary>
        /// <returns>instanc of weight entry class</returns>
        public WeightEntry GetLatestWeightEntry()
        {
            return AppDatabase.GetLatestWeightEntry();
        }


        /// <summary>
        /// Get all weigh entries between dates
        /// </summary>
        /// <param name="startDate">start of period</param>
        /// <param name="endDate">end of period</param>
        /// <returns>gall weight entries between dates</returns>
        public IEnumerable<WeightEntry> GetWeightEntriesBetweenDates(DateTime startDate, DateTime endDate)
        {
            var output = AppDatabase.GetAllWeightEntries().Where(x => x.Date.DayOfYear >= startDate.DayOfYear && x.Date.DayOfYear <= endDate.DayOfYear).ToList();
            return output;
        }

        /// <summary>
        /// Gets last weight entry for every day between given dates
        /// </summary>
        /// <param name="startDate">beginning of period</param>
        /// <param name="endDate">end of period</param>
        /// <returns>returns the last weight added by user for each day between dates</returns>
        public IEnumerable<WeightEntry> GetLastWeightEntriesBetweenDates(DateTime startDate, DateTime endDate)
        {
            //new list for output
            List<WeightEntry> output = new List<WeightEntry>();
            //get starting day of the year
            var dayOfYear = startDate.DayOfYear;

            /* for (var date = startOfWeek; date <= DateTime.Now; date = date.AddDays(1))
             {
                 //get amount for day
                 var amount = GetTotalStepsForDate(date);
                 //add step entry to daily totals
                 dailiyTotals.Add(new StepEntry(amount, date, new TimeSpan(24, 0, 0)));
             }*/
            var date = startDate;
            //while in date range
            while (date>= startDate && date <= endDate)
            {
                //get last entry for that day
                var latestEntryForDate = GetLastWeightEntryForDate(date);
                //if entry is not null
                if (latestEntryForDate != null)
                {
                    //add to list
                    output.Add(latestEntryForDate);
                }
                //increment day of the year
                date=date.AddDays(1);
            }
            return output;
        }

        /// <summary>
        /// Get weight entries for current week
        /// </summary>
        /// <returns>weight entries for current week</returns>
        public IEnumerable<WeightEntry> GetWeightEntriesForCurrentWeek()
        {
            //setting start of week 
            DateTime startOfWeek = GetStartOfWeek();
            //get entries between dates
            var output = GetLastWeightEntriesBetweenDates(startOfWeek, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Get weight entries for current month
        /// </summary>
        /// <returns>weight entries for current month</returns>
        public IEnumerable<WeightEntry> GetWeightEntriesForCurrentMonth()
        {
            //get start of the month date
            var startOfTheMonth = GetStartOfMonth();
            //get entries between dates
            var output = GetLastWeightEntriesBetweenDates(startOfTheMonth, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Get weight entries for current year
        /// </summary>
        /// <returns>weight entries for current year</returns>
        public IEnumerable<WeightEntry> GetWeightEntriesForCurrentYear()
        {
            //set start of the year date
            var startOfTheYear = GetStartOfYear();
            //get entries between dates           
            var output = GetLastWeightEntriesBetweenDates(startOfTheYear, DateTime.Now);
            return output;
        }

        #endregion //Methods

    }
}