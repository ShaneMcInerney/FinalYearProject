using FYP.Business.Models;
using System;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface ISleepAlarmManager
    {

        /// <summary>
        /// Delete all sleep alarms in DB
        /// </summary>
        void DeleteAllSleepAlarms();


        /// <summary>
        /// Delete sleep alarm in DB by ID
        /// </summary>
        /// <param name="id">ID of sleep alarm to delete</param>
        /// <returns>ID of deleted alarm</returns>
        int DeleteSleepAlarm(int id);


        /// <summary>
        /// Get all sleep alarms in DB
        /// </summary>
        /// <returns></returns>
        IEnumerable<SleepAlarm> GetAllSleepAlarm();


        /// <summary>
        /// Get all smart alarm enabled alarms
        /// </summary>
        /// <returns></returns>
        IEnumerable<SleepAlarm> GetAllSleepAlarmsWithSmartAlarmEnabled();


        /// <summary>
        /// Get all sleep alarms with smart alarm enabd for given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<SleepAlarm> GetAllSleepAlarmsWithSmartAlarmEnabledForDate(DateTime date);


        /// <summary>
        /// Check smart alarm exists in DB
        /// </summary>
        /// <returns></returns>
        bool SmartAlarmExists();

        /// <summary>
        /// Gets sleep alarm by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SleepAlarm GetSleepAlarm(int id);


        /// <summary>
        /// Get latest sleep alarm in DB
        /// </summary>
        /// <returns></returns>
        SleepAlarm GetLastestSleepAlarm();


        /// <summary>
        /// Save sleep alarm in DB
        /// </summary>
        /// <param name="sleepAlarm"></param>
        /// <returns></returns>
        int SaveSleepAlarm(SleepAlarm sleepAlarm);

        /// <summary>
        /// Check if sleep alarm exists in DB
        /// </summary>
        /// <returns></returns>
        bool SleepAlarmExists();

    }
}
