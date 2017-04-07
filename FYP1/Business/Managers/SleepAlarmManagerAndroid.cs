using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP_Droid.Business.Managers
{
    public class SleepAlarmManagerAndroid : SleepAlarmManager
    {

        #region Fields

        private AppDatabase m_appDatabase;
        MediaPlayer m_mediaPlayer;
        AudioManager m_audioManager;
        private bool m_isPlaying = false;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="database"></param>
        public SleepAlarmManagerAndroid(AppDatabase database):base (database)
        {
            this.m_appDatabase = database;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_mediaPlayer
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get
            {
                return m_mediaPlayer;
            }

            set
            {
                m_mediaPlayer = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_audioManager
        /// </summary>
        public AudioManager AudioManager
        {
            get
            {
                return m_audioManager;
            }

            set
            {
                m_audioManager = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Set alarm to wake user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sleepAlarm"></param>
        public void WakeUser(Context context, SleepAlarm sleepAlarm)
        {
            //alarm intent
            Intent alarmIntent = new Intent(context, typeof(AlarmReceiver));
            //add normal alarm extra
            alarmIntent.PutExtra("NormalAlarm", "NormalAlarm");
            //pending intent
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            //get current date and time
            DateTime currentDate = DateTime.Today;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            //set alarm manager
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            //setting number of days to day
            int daysToMonday = ((int)DayOfWeek.Monday - (int)currentDate.DayOfWeek + 7) % 7;
            int daysToTuesday = ((int)DayOfWeek.Tuesday - (int)currentDate.DayOfWeek + 7) % 7;
            int daysToWednesday = ((int)DayOfWeek.Wednesday - (int)currentDate.DayOfWeek + 7) % 7;
            int daysToThursday = ((int)DayOfWeek.Thursday - (int)currentDate.DayOfWeek + 7) % 7;
            int daysToFriday = ((int)DayOfWeek.Friday - (int)currentDate.DayOfWeek + 7) % 7;
            int daysToSaturday = ((int)DayOfWeek.Saturday - (int)currentDate.DayOfWeek + 7) % 7;
            int daysToSunday = ((int)DayOfWeek.Sunday - (int)currentDate.DayOfWeek + 7) % 7;

            //check for current day of week

            if (sleepAlarm.OnMon && currentDate.DayOfWeek == DayOfWeek.Monday)
            {
                //get time to alarm
                long timeToMonAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                //set alarm
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToMonAlarm, pendingIntent);
            }
            if (sleepAlarm.OnTues && currentDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                long timeToTuesAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToTuesAlarm, pendingIntent);
            }
            if (sleepAlarm.OnWed && currentDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                long timeToWedAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToWedAlarm, pendingIntent);
            }
            if (sleepAlarm.OnThurs && currentDate.DayOfWeek == DayOfWeek.Thursday)
            {
                long timeToThursAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToThursAlarm, pendingIntent);
            }
            if (sleepAlarm.OnFri && currentDate.DayOfWeek == DayOfWeek.Friday)
            {
                long timeToFriAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToFriAlarm, pendingIntent);
            }
            if (sleepAlarm.OnSat && currentDate.DayOfWeek == DayOfWeek.Saturday)
            {
                long timeToSatAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToSatAlarm, pendingIntent);
            }
            if (sleepAlarm.OnSun && currentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                long timeToSunAlarm = (long)(sleepAlarm.AlarmTime.TimeOfDay - currentTime).TotalMilliseconds;
                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + timeToSunAlarm, pendingIntent);
            }

        }

        /// <summary>
        /// Trigger smart alarm
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sleepLength"></param>
        public void TriggerSmartAlarm(Context context, TimeSpan sleepLength)
        {
            //alarm intent
            Intent alarmIntent = new Intent(context, typeof(AlarmReceiver));
            //serailize sleep length
            string output = JsonConvert.SerializeObject(sleepLength);
            //put extra in intent
            alarmIntent.PutExtra("SleepLength", output);
            //pending intent
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            //set alarm manager
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            //set alarm
            alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);
            //stop sleep tracker service
            context.StopService(new Intent(context, typeof(SleepTrackerService)));
        }


        /// <summary>
        /// Setup Media Player
        /// </summary>
        /// <param name="context"></param>
        public void SetupMediaPlayer(Context context)
        {
            this.m_audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            this.m_audioManager.SetStreamVolume(Stream.Music, m_audioManager.GetStreamMaxVolume(Stream.Music), 0);
            this.m_mediaPlayer = MediaPlayer.Create(context, Resource.Raw.SleepAlarmSound);
        }

        /// <summary>
        /// Play sleep alarm
        /// </summary>
        public void PlaySleepAlarm()
        {
            this.IsPlaying = true;
            this.MediaPlayer.Looping = true;
            this.m_mediaPlayer.Start();
        }

        /// <summary>
        /// Stop sleep alarm
        /// </summary>
        public void StopSleepAlarm()
        {
            this.m_mediaPlayer.Stop();
            this.IsPlaying = false;
        }
        #endregion //Methods

    }
}