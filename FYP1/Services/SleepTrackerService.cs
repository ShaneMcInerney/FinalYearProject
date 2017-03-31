using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace FYP_Droid.Services
{
    [Service]
    public class SleepTrackerService : Service, ISensorEventListener
    {
        #region Fields

        private static bool m_isRunning = false;
        private const int m_maxNumOfHits = 200;
        private Stopwatch m_sleepTimer;
        private Notification m_notification;
        private Notification.Builder m_notificationBuilder;
        private NotificationManager m_notificationManager;
        private const int m_notificationID = 1;
        private SensorManager m_sensorManager;
        private int m_wakingUpMovements = 0;
        private Timer m_timer;
        private List<SleepAlarm> m_smartAlarms;
        private DateTime m_currentDate;
        private static bool m_smartAlarmsExist;
        private AccelerometerReading m_sensorReading;
        private const double m_highThreshold = 10.2;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_notification
        /// </summary>
        public Notification Notification
        {
            get
            {
                return m_notification;
            }

            set
            {
                m_notification = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_notificationBuilder
        /// </summary>
        public Notification.Builder NotificationBuilder
        {
            get
            {
                return m_notificationBuilder;
            }

            set
            {
                m_notificationBuilder = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_notificationManager
        /// </summary>
        public NotificationManager NotificationManager
        {
            get
            {
                return m_notificationManager;
            }

            set
            {
                m_notificationManager = value;
            }
        }

        /// <summary>
        /// Gets m_notificationID
        /// </summary>
        public static int NotificationID
        {
            get
            {
                return m_notificationID;
            }
        }

        /// <summary>
        /// Gets/Sets m_sensorManager
        /// </summary>
        public SensorManager SensorManager
        {
            get
            {
                return m_sensorManager;
            }

            set
            {
                m_sensorManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_isRunning
        /// </summary>
        public static bool IsRunning
        {
            get
            {
                return m_isRunning;
            }

            set
            {
                m_isRunning = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_wakingUpMovements
        /// </summary>
        public int WakingUpMovements
        {
            get
            {
                return m_wakingUpMovements;
            }

            set
            {
                m_wakingUpMovements = value;
            }
        }

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public Stopwatch SleepTimer
        {
            get
            {
                return m_sleepTimer;
            }

            set
            {
                m_sleepTimer = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_smartAlarmsExist
        /// </summary>
        public static bool SmartAlarmsExist
        {
            get
            {
                return m_smartAlarmsExist;
            }

            set
            {
                m_smartAlarmsExist = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_smartAlarms
        /// </summary>
        public List<SleepAlarm> SmartAlarms
        {
            get
            {
                return m_smartAlarms;
            }

            set
            {
                m_smartAlarms = value;
            }
        }

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public DateTime CurrentDate
        {
            get
            {
                return m_currentDate;
            }

            set
            {
                m_currentDate = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_timer
        /// </summary>
        public Timer Timer
        {
            get
            {
                return m_timer;
            }

            set
            {
                m_timer = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sensorReading
        /// </summary>
        public AccelerometerReading SensorReading
        {
            get
            {
                return m_sensorReading;
            }

            set
            {
                m_sensorReading = value;
            }
        }

        /// <summary>
        /// Gets m_maxNumOfHits
        /// </summary>
        public static int MaxNumOfHits
        {
            get
            {
                return m_maxNumOfHits;
            }
        }

        /// <summary>
        /// Gets m_highThreshold
        /// </summary>
        public static double HighThreshold
        {
            get
            {
                return m_highThreshold;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="flags"></param>
        /// <param name="startId"></param>
        /// <returns></returns>
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //If running already
            if (IsRunning)
            {
                //stop 
                StopSelf();
            }
            else
            {
                //setup vaariables
                SetupVariables();
                //handle events
                EventHandlers();
                //register listeners
                RegisterListeners();
            }


            return StartCommandResult.Sticky;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnDestroy()
        {
            //if timer is ruuning
            if (IsRunning && SleepTimer.IsRunning)
            {
                //stop 
                SleepTimer.Stop();
                //get sleep lenght
                var sleepLength = SleepTimer.Elapsed;
                //create new sleep entry
                var NewSleepEntry = new SleepEntry(sleepLength, 2, DateTime.Now);
                //save sleep entry
                GlobalUtilities.SleepManager.SaveSleepEntry(NewSleepEntry);
            }
            //unregister listeners
            UnregisterListeners();
            //clear notifications
            ClearNotifcations();

            base.OnDestroy();
        }

        /// <summary>
        /// Clear Alll notifications
        /// </summary>
        private void ClearNotifcations()
        {
            NotificationManager.CancelAll();

            NotificationManager.Dispose();

            NotificationBuilder.Dispose();

            Notification.Dispose();
        }

        /// <summary>
        /// Setup Variables for use in this class
        /// </summary>
        private void SetupVariables()
        {

            SleepTimer = new Stopwatch();
            SleepTimer.Reset();
            SleepTimer.Start();
            WakingUpMovements = 0;
            SensorManager = (SensorManager)GetSystemService(Context.SensorService);

            NotificationBuilder = new Notification.Builder(this)
              .SetContentTitle("Sleep Tracker")
              .SetContentText("")
              .SetColor(76)
              .SetOngoing(true)
              .SetSmallIcon(Resource.Drawable.notification_template_icon_bg);

            Notification = NotificationBuilder.Build();

            NotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            NotificationManager.Notify(NotificationID, Notification);

            SmartAlarmsExist = GlobalUtilities.SleepAlarmManager.SmartAlarmExists();

            //if smart alarm exists
            if (SmartAlarmsExist)
            {
                //get smart alarms for date
                SmartAlarms = GlobalUtilities.SleepAlarmManager.GetAllSleepAlarmsWithSmartAlarmEnabledForDate(DateTime.Now).ToList();
            }

        }

        /// <summary>
        /// Register listeners
        /// </summary>
        private void RegisterListeners()
        {
            SensorManager.RegisterListener(this, SensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Normal);
        }

        /// <summary>
        /// Unregister isteners
        /// </summary>
        private void UnregisterListeners()
        {
            SensorManager.UnregisterListener(this, SensorManager.GetDefaultSensor(SensorType.Accelerometer));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="accuracy"></param>
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void OnSensorChanged(SensorEvent e)
        {
            //getting current reading
            SensorReading = new AccelerometerReading(e.Values[0], e.Values[1], e.Values[2]);
            //setting current date
            CurrentDate = DateTime.Now;
            //check if a smart alarm exists in the database
            if (SmartAlarmsExist)
            {
                //iterate through list of alarms returned
                foreach (var alarm in SmartAlarms)
                {
                    //check time to alarm
                    var timeToAlarm = (long)(alarm.AlarmTime.TimeOfDay - CurrentDate.TimeOfDay).TotalMinutes;
                    //if the time remaining to the alarm is thrity minutes or less
                    if (timeToAlarm <= 30)
                    {
                        //check if reading created is high (user moving)
                        if (SensorReading.VectorMagnitude >= HighThreshold)
                        {
                            //increment movement counter
                            WakingUpMovements++;
                            //if detected movements above threshold
                            if (WakingUpMovements >= MaxNumOfHits)
                            {
                                //stop sleep timer
                                SleepTimer.Stop();
                                //get users sleep length
                                var sleepLength = SleepTimer.Elapsed;
                                //reset timer
                                SleepTimer.Reset();
                                //trigger alarm
                                GlobalUtilities.SleepAlarmManager.TriggerSmartAlarm(this, sleepLength);
                                //stop listening
                                UnregisterListeners();
                            }
                        }
                    }
                }
            }
            //intent for graph
            Intent intent = new Intent("graph");
            //put extra
            intent.PutExtra("reading", SensorReading.VectorMagnitude);
            //send broadcast
            SendBroadcast(intent);
        }

        private void EventHandlers()
        {

        }


        #endregion //Methods
    }
}