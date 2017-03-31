using FYP.Business.Models;
using System;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface ISleepManager
    {
        /// <summary>
        /// Delete all sleep entries in the DB
        /// </summary>
        void DeleteAllSleepEntries();


        /// <summary>
        /// Delete sleep enty from teh apps DB
        /// </summary>
        /// <param name="id">ID of sleep entry to be deleted</param>
        /// <returns>ID of deleted sleep entry</returns>
        int DeleteSleepEntry(int id);


        /// <summary>
        /// Get latest sleep entry in the applications DB
        /// </summary>
        /// <returns>Most recent slee entry i the application's DB</returns>
        SleepEntry GetLatestSleepEntry();


        /// <summary>
        /// Get all slep entries in the DB
        /// </summary>
        /// <returns>All sleep entries in the DB</returns>
        IEnumerable<SleepEntry> GetAllSleepEntries();


        /// <summary>
        /// Get sleep entry from the DB
        /// </summary>
        /// <param name="id">id of entry to retrieve</param>
        /// <returns>Instance of the sleep entry class</returns>
        SleepEntry GetSleepEntry(int id);


        /// <summary>
        /// Save sleep entry in the DB
        /// </summary>
        /// <param name="sleepEntry">Sleep entry to save in the DB</param>
        /// <returns>ID of saved sleep entry</returns>
        int SaveSleepEntry(SleepEntry sleepEntry);


        /// <summary>
        /// Check for the existance of a sleep entry in th DB
        /// </summary>
        /// <returns>bool indicating the existance of a sleep entry in the DB</returns>
        bool SleepEntryExists();


        /// <summary>
        /// Get sleep entries between dates
        /// </summary>
        /// <param name="start">start of period</param>
        /// <param name="end">end of period</param>
        /// <returns>all sleep entries between the given dates</returns>
        IEnumerable<SleepEntry> GetSleepEntriesBetweenDates(DateTime start, DateTime end);


        /// <summary>
        /// Get sum of sleep in list of sleep entries
        /// </summary>
        /// <param name="sleepEtries">sleep entries to get the total sum of hours for</param>
        /// <returns>sum of hurs of sleep in ist of sleep entries</returns>
        double GetSumOfHoursOfSleepInList(List<SleepEntry> sleepEtries);


        /// <summary>
        /// Get all sleep entries for current
        /// </summary>
        /// <returns>all sleep entries for current month</returns>
        IEnumerable<SleepEntry> GetSleepEntriesForCurrentMonth();


        /// <summary>
        /// Get all sleep entries for current week
        /// </summary>
        /// <returns>all sleep entries for current week</returns>
        IEnumerable<SleepEntry> GetSleepEntriesForCurrentWeek();


        /// <summary>
        /// Get all sleep entries for current year
        /// </summary>
        /// <returns>//all sleep entries for the current year</returns>
        IEnumerable<SleepEntry> GetSleepEntriesForCurrentYear();


        /// <summary>
        /// Convert sleep qaulity to string
        /// </summary>
        /// <param name="sleepEntry">sleep entry t retrieve quality of</param>
        /// <returns>string equivalent of sleep quality</returns>
        string SleepQualityToString(SleepEntry sleepEntry);

    }
}
