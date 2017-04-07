using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business.Managers
{
    public class SleepAlarmManager:BaseManager
    {

        #region Fields

        private AppDatabase m_appDatabase;
        private bool m_isPlaying = false;
        public delegate void SleepAlarmHandler(object myObject, AlarmCreatedArgs myArgs);
        public event SleepAlarmHandler OnAlarmCreated;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="database"></param>
        public SleepAlarmManager(AppDatabase database)
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

       

        /// <summary>
        /// Gets/Sets m_isPlaying
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return m_isPlaying;
            }

            set
            {
                m_isPlaying = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Delete all sleep alarms in DB
        /// </summary>
        public void DeleteAllSleepAlarms()
        {
            this.AppDatabase.DeleteAllSleepAlarms();
        }

        /// <summary>
        /// Delete sleep alarm in DB by ID
        /// </summary>
        /// <param name="id">ID of sleep alarm to delete</param>
        /// <returns>ID of deleted alarm</returns>
        public int DeleteSleepAlarm(int id)
        {
            return this.AppDatabase.DeleteSleepAlarm(id);
        }

        /// <summary>
        /// Get all sleep alarms in DB
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SleepAlarm> GetAllSleepAlarm()
        {
            return this.AppDatabase.GetAllSleepAlarms();
        }

        /// <summary>
        /// Get all smart alarm enabled alarms
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SleepAlarm> GetAllSleepAlarmsWithSmartAlarmEnabled()
        {
            return this.AppDatabase.GetAllSleepAlarms().Where(x => x.SmartAlarmEnabled);
        }

        /// <summary>
        /// Get all sleep alarms with smart alarm enabd for given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<SleepAlarm> GetAllSleepAlarmsWithSmartAlarmEnabledForDate(DateTime date)
        {
            return this.AppDatabase.GetAllSleepAlarms().Where(x => x.SmartAlarmEnabled && x.AlarmTime.DayOfYear == date.DayOfYear || x.AlarmTime.DayOfYear == date.DayOfYear + 1);
        }

        /// <summary>
        /// Check smart alarm exists in DB
        /// </summary>
        /// <returns></returns>
        public bool SmartAlarmExists()
        {
            return this.AppDatabase.SmartAlarmExists();
        }

        /// <summary>
        /// Gets sleep alarm by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SleepAlarm GetSleepAlarm(int id)
        {
            return this.AppDatabase.GetSleepAlarm(id);
        }

        /// <summary>
        /// Get latest sleep alarm in DB
        /// </summary>
        /// <returns></returns>
        public SleepAlarm GetLastestSleepAlarm()
        {
            return this.AppDatabase.GetAllSleepAlarms().LastOrDefault();
        }

        /// <summary>
        /// Save sleep alarm in DB
        /// </summary>
        /// <param name="sleepAlarm"></param>
        /// <returns></returns>
        public int SaveSleepAlarm(SleepAlarm sleepAlarm)
        {
            AlarmCreatedArgs args = new AlarmCreatedArgs(true);
            int id = this.AppDatabase.SaveSleepAlarm(sleepAlarm);
            OnAlarmCreated(this, args);
            return id;
        }

        /// <summary>
        /// Check if sleep alarm exists in DB
        /// </summary>
        /// <returns></returns>
        public bool SleepAlarmExists()
        {
            return this.AppDatabase.SleepAlarmExists();
        }

      
        #endregion //Methods
    }
}
