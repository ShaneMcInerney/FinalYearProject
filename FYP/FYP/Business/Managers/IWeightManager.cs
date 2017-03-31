using FYP.Business.Models;
using System;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface IWeightManager
    {

        /// <summary>
        /// Get weight entry by ID
        /// </summary>
        /// <param name="id">ID of weight entry to retrieve</param>
        /// <returns>Instance of weight entry class</returns>
        WeightEntry GetWeightEntry(int id);

        /// <summary>
        /// Get all weight entries in the DB
        /// </summary>
        /// <returns>All weight entries in the DB</returns>
        IEnumerable<WeightEntry> GetAllWeightEntries();

        /// <summary>
        /// Get first weight entry for given date
        /// </summary>
        /// <param name="date">date to retrieve first weight entry for</param>
        /// <returns>Instance of the weight entry class</returns>
        WeightEntry GetFirstWeightEntryForDate(DateTime date);

        /// <summary>
        /// Get last weight entry fro a given day of the year
        /// </summary>
        /// <param name="dayOfTheYear">day of the year to retrieve last weight entry for</param>
        /// <returns>last entry for the given day of the year</returns>
        WeightEntry GetLastWeightEntryForDayOfYear(int dayOfTheYear);

        /// <summary>
        /// Get weight entries for given date
        /// </summary>
        /// <param name="date">date to retrieve entries for</param>
        /// <returns>entries for given date</returns>
        IEnumerable<WeightEntry> GetWeightEntriesForDate(DateTime date);

        /// <summary>
        /// Save weight entry to DB
        /// </summary>
        /// <param name="weightEntry">weight entry to save</param>
        /// <returns>ID of saved weight entry</returns>
        int SaveWeightEntry(WeightEntry weightEntry);

        /// <summary>
        /// Delete al weight entries in DB by date
        /// </summary>
        /// <param name="date">date to delete entries for</param>
        void DeleteAllWeightEntriesForDate(DateTime date);

        /// <summary>
        /// Delete all weight entries in DB
        /// </summary>
        void DeleteAllWeightEntries();

        /// <summary>
        /// Delete weight entry by id
        /// </summary>
        /// <param name="id">id of entry to delete</param>
        /// <returns>ID of deleted entry</returns>
        int DeleteWeightEntry(int id);

        /// <summary>
        /// Delete all entries in a given list 
        /// </summary>
        /// <param name="listToDelete">List of entries to delete</param>
        void DeleteWeightEntriesInList(IEnumerable<WeightEntry> listToDelete);

        /// <summary>
        /// Checks if weight entry exists in DB
        /// </summary>
        /// <returns> bools based on weight entry existence in DB</returns>
        bool WeightEntryExists();

        /// <summary>
        /// Gets last weight entry in DB
        /// </summary>
        /// <returns>instanc of weight entry class</returns>
        WeightEntry GetLatestWeightEntry();


        /// <summary>
        /// Get all weigh entries between dates
        /// </summary>
        /// <param name="startDate">start of period</param>
        /// <param name="endDate">end of period</param>
        /// <returns>gall weight entries between dates</returns>
        IEnumerable<WeightEntry> GetWeightEntriesBetweenDates(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Gets last weight entry for every day between given dates
        /// </summary>
        /// <param name="startDate">beginning of period</param>
        /// <param name="endDate">end of period</param>
        /// <returns>returns the last weight added by user for each day between dates</returns>
        IEnumerable<WeightEntry> GetLastWeightEntriesBetweenDates(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Get weight entries for current week
        /// </summary>
        /// <returns>weight entries for current week</returns>
        IEnumerable<WeightEntry> GetWeightEntriesForCurrentWeek();


        /// <summary>
        /// Get weight entries for current month
        /// </summary>
        /// <returns>weight entries for current month</returns>
        IEnumerable<WeightEntry> GetWeightEntriesForCurrentMonth();


        /// <summary>
        /// Get weight entries for current year
        /// </summary>
        /// <returns>weight entries for current year</returns>
        IEnumerable<WeightEntry> GetWeightEntriesForCurrentYear();

    }
}
