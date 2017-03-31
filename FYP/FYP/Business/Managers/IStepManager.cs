using FYP.Business.Models;
using System;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface IStepManager
    {
        /// <summary>
        /// Get step entry by ID
        /// </summary>
        /// <param name="id">ID of ntry to retrieve</param>
        /// <returns>step entry</returns>
        StepEntry GetStepEntry(int id);


        /// <summary>
        /// Get all step entries in DB
        /// </summary>
        /// <returns></returns>
        IEnumerable<StepEntry> GetAllStepEntries();


        /// <summary>
        /// Get all step entries for given date
        /// </summary>
        /// <param name="date">date to retrieve entries fr</param>
        /// <returns>step entries for gien date</returns>
        IEnumerable<StepEntry> GetStepEntriesForDate(DateTime date);


        /// <summary>
        /// Get entries for date and hur
        /// </summary>
        /// <param name="date">date to get entries for</param>
        /// <param name="hour">hour to get entry for</param>
        /// <returns>entries for date and hour</returns>
        IEnumerable<StepEntry> GetStepEntriesForDateAndHour(DateTime date, TimeSpan hour);


        /// <summary>
        /// Get entries for date between hours
        /// </summary>
        /// <param name="date">date to retrieve entries or</param>
        /// <param name="startTime">start of period</param>
        /// <param name="endTime">end of period</param>
        /// <returns></returns>
        IEnumerable<StepEntry> GetStepEntriesForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime);


        /// <summary>
        /// Get step entries between dates
        /// </summary>
        /// <param name="startDate">start of period</param>
        /// <param name="endDate">end of period</param>
        /// <returns>list of step entries between dates</returns>
        IEnumerable<StepEntry> GetStepEntriesBetweenDates(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Save step entry to DB
        /// </summary>
        /// <param name="stepEntry">step entry to save</param>
        /// <returns>id of saved entry</returns>
        int SaveStepEntry(StepEntry stepEntry);


        /// <summary>
        /// Delete step entry by id
        /// </summary>
        /// <param name="id">id of entry to delete</param>
        /// <returns>id of deleted entry</returns>
        int DeleteStepEntry(int id);


        /// <summary>
        /// Delete all step entries
        /// </summary>
        void DeleteAllStepEntries();


        /// <summary>
        /// Delete step entries for date
        /// </summary>
        /// <param name="date">date to delete entries for</param>
        void DeleteStepEntriesForDate(DateTime date);


        /// <summary>
        /// Delete all step entries between give dates
        /// </summary>
        /// <param name="startDate">start of period</param>
        /// <param name="endDate">end of period</param>
        void DeleteStepEntriesBetweenDates(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Delete step entries for date and between times
        /// </summary>
        /// <param name="date">date to delete for</param>
        /// <param name="startTime">start of time period</param>
        /// <param name="endTime">end of time period</param>
        void DeleteStepEntriesForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime);


        /// <summary>
        /// Delete all step entries matching date and time
        /// </summary>
        /// <param name="date">date to delete for</param>
        /// <param name="time">time to delete for</param>
        void DeleteStepEntriesForDateAndForHour(DateTime date, TimeSpan time);


        /// <summary>
        /// Delete all step entries in given list
        /// </summary>
        /// <param name="listToDelete">list of entries to delete</param>
        void DeleteAllStepEntriesInList(IEnumerable<StepEntry> listToDelete);


        /// <summary>
        /// Get total ste count in given list of entries
        /// </summary>
        /// <param name="listOfStepEntries">list of entries to get the total number of steps for</param>
        /// <returns>total number of steps in lsit</returns>
        Int64 GetSumOfStepCountsInList(IEnumerable<StepEntry> listOfStepEntries);


        /// <summary>
        /// Get the total nubmer of steps for a given date
        /// </summary>
        /// <param name="date">date to retrieve step count for</param>
        /// <returns>total steps for given date</returns>
        Int64 GetTotalStepsForDate(DateTime date);


        /// <summary>
        /// Get total number of steps for current date
        /// </summary>
        /// <returns>step count for current date</returns>
        Int64 GetTotalStepsForCurrentDate();


        /// <summary>
        /// Get total step count for hour
        /// </summary>
        /// <param name="time">time to retriev stesp for</param>
        /// <returns>step count for given hour</returns>
        Int64 GetTotalStepsForHour(TimeSpan time);


        /// <summary>
        /// Estimate the distance covered by the user based on the number of steps
        /// </summary>
        /// <param name="numSteps">number of steps to estimate the distance for</param>
        /// <returns>an estimate of the distance covered</returns>
        double EstimateDistanceCovered(long numSteps);


        /// <summary>
        /// Checks whether or ot a step entry exists
        /// </summary>
        /// <returns>bool based on the existance of a step entry</returns>
        bool StepEntryExists();


        /// <summary>
        /// Step count for the current day
        /// </summary>
        /// <returns>step count for the current day</returns>
        Int64 GetStepCountForCurrentDay();


        /// <summary>
        /// Get the total number of steps taken for the date
        /// </summary>
        /// <param name="date">date to retrieve steps for</param>
        /// <returns>amoutn of steps for given date</returns>
        Int64 GetStepCountForDay(DateTime date);


        /// <summary>
        /// Get the total number of steps taken for the week
        /// </summary>
        /// <returns>total number of steps taken for the month</returns>
        Int64 GetStepCountForCurrentWeek();


        /// <summary>
        /// Get the total number of steps taken for the month
        /// </summary>
        /// <returns>total number of steps taken for the month</returns>
        Int64 GetStepCountForCurrentMonth();


        /// <summary>
        /// Get the total number of steps taken for the year
        /// </summary>
        /// <returns>the number of steps for the year</returns>
        Int64 GetStepCountForCurrentYear();


        /// <summary>
        /// Get all step entries for current week
        /// </summary>
        /// <returns>all step entries for current week</returns>
        IEnumerable<StepEntry> GetStepEntriesForCurrentWeek();

        /// <summary>
        /// gets all step entries for the current month
        /// </summary>
        /// <returns>step entries for current month</returns>
        IEnumerable<StepEntry> GetStepEntriesForCurrentMonth();


        /// <summary>
        /// Get all step entries for current year
        /// </summary>
        /// <returns>all step entries for current year</returns>
        IEnumerable<StepEntry> GetStepEntriesForCurrentYear();


        /// <summary>
        /// Get the daily total number of steps for each day in current week
        /// </summary>
        /// <param name="stepEntries">step entries for the week</param>
        /// <returns>list of step entries ,one entry per day, with daily total steps</returns>
        List<StepEntry> GetDailyTotalsForCurrentWeek(List<StepEntry> stepEntries);


        /// <summary>
        /// Get the daily total number of steps for ech day in current month
        /// </summary>
        /// <param name="stepEntries">step entries for the month</param>
        /// <returns>list of step entries ,one entry per day, with daily total steps</returns>
        List<StepEntry> GetDailyTotalsForCurrentMonth(List<StepEntry> stepEntries);


        /// <summary>
        /// Gets the daily total number of steps for each day
        /// </summary>
        /// <param name="stepEntries">entries to get daily total for</param>
        /// <returns>a list of new stpe entries, one entry perday, with the stpe total</returns>
        List<StepEntry> GetDailyTotalsForCurrentYear(List<StepEntry> stepEntries);


    }
}
