using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FYP_Droid.Services
{
    [Service]
    public class FallDetectorService : Service, ISensorEventListener
    {
        #region Fields

        private Timer m_timer;
        private const int m_stableHitCountMax = 500;
        private Notification m_notification;
        private Notification.Builder m_notificationBuilder;
        private NotificationManager m_notificationManager;
        private const int m_notificationID = 1;
        private SensorManager m_sensorManager;
        private static bool m_isRunning = false;
        private List<AccelerometerReading> m_acceleometerReadingsInPossibleFall;
        private Timer m_stableTime;

        #endregion //Fields

        #region Property Accessors

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
        /// Gets/Sets m_acceleometerReadingsInPossibleFall
        /// </summary>
        public List<AccelerometerReading> AcceleometerReadingsInPossibleFall
        {
            get
            {
                return m_acceleometerReadingsInPossibleFall;
            }

            set
            {
                m_acceleometerReadingsInPossibleFall = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stableTime
        /// </summary>
        public Timer StableTime
        {
            get
            {
                return m_stableTime;
            }

            set
            {
                m_stableTime = value;
            }
        }

        /// <summary>
        /// Gets m_stableHitCountMax
        /// </summary>
        public static int StableHitCountMax
        {
            get
            {
                return m_stableHitCountMax;
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
            //starting accelerometer
            //if is already running
            if (IsRunning)
            {
                //stop service
                StopSelf();
            }
            else
            {
                //sertup variables
                SetupVariables();
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
            //set is running to false
            IsRunning = false;
            //save readings in bulk
            GlobalUtilities.AccelerometerManager.BulkSaveAcclerometerReadings();

            base.OnDestroy();
            //unregister listeners
            UnregisterListeners();
            //clear notifications
            ClearNotifcations();
        }

        /// <summary>
        /// Clear all notifications
        /// </summary>
        private void ClearNotifcations()
        {
            //cancel all notifications
            NotificationManager.CancelAll();
            //dispose of manager
            NotificationManager.Dispose();

            NotificationBuilder.Dispose();
            //dispose of notification
            Notification.Dispose();
        }

        /// <summary>
        /// Setup variables for this class
        /// </summary>
        private void SetupVariables()
        {
            IsRunning = true;
            Timer = new Timer(7000);
            Timer.Elapsed += CheckListOfReadingsForPotentialFall;
            StableTime = new Timer(500);

            SensorManager = (SensorManager)GetSystemService(Context.SensorService);

            NotificationBuilder = new Notification.Builder(this)
              .SetContentTitle("Fall Detector is Running")
              .SetContentText("")
              .SetColor(76)
              .SetOngoing(true)
              .SetSmallIcon(Resource.Drawable.notification_template_icon_bg);

            Notification = NotificationBuilder.Build();

            NotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            NotificationManager.Notify(NotificationID, Notification);

            this.m_acceleometerReadingsInPossibleFall = new List<AccelerometerReading>();

        }

        /// <summary>
        /// Registr Accelerometer listener
        /// </summary>
        private void RegisterListeners()
        {
            SensorManager.RegisterListener(this, SensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Fastest);
        }

        /// <summary>
        /// Unregiser accelerometer listener
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
            //creating new reading from accelerometer sensor
            AccelerometerReading reading = new AccelerometerReading(e.Values[0], e.Values[1], e.Values[2]);
            //if the timer is not enabled
            if (Timer.Enabled == false)
            {
                //check if readings vector magnitude is within lower threshold or that the phone has suddenly decellerated
                if (reading.VectorMagnitude <= GlobalUtilities.AccelerometerManager.LowerThreshold + GlobalUtilities.AccelerometerManager.LowerTolerance && reading.VectorMagnitude >= GlobalUtilities.AccelerometerManager.LowerThreshold - GlobalUtilities.AccelerometerManager.LowerTolerance

                    )
                {
                    //start timer to record readings
                    Timer.Start();
                }
            }
            //if timer is enabled
            if (Timer.Enabled)
            {
                //record reading (add to list)
                AcceleometerReadingsInPossibleFall.Add(reading);
            }

            //create intent to send accelerometer data for graphing
            Intent graphIntent = new Intent("graph");
            //attach the vector magnitude 
            graphIntent.PutExtra("reading", reading.VectorMagnitude);
            //send broadcast to display data on graph
            SendBroadcast(graphIntent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void CheckListOfReadingsForPotentialFall(Object source, System.Timers.ElapsedEventArgs e)
        {
            //stop the timer
            Timer.Stop();
            //variable for holding index of high threshold break reading
            var indexOfHighBreak = 0;
            //counter for the number of stable accelerometer readings found
            var stableHitCount = 0;
            //iterate through recorded readings
            foreach (var r in AcceleometerReadingsInPossibleFall)
            {
                //if the readings vector magnitude has broken the high threshold has been broken
                if (r.VectorMagnitude >= GlobalUtilities.AccelerometerManager.UpperThreshold - GlobalUtilities.AccelerometerManager.UpperTolerance)
                {
                    //if high break is higher than  last previously known high break
                    if (r.VectorMagnitude >= AcceleometerReadingsInPossibleFall.ElementAt(indexOfHighBreak).VectorMagnitude)
                    {
                        //set index of current highest known value
                        indexOfHighBreak = AcceleometerReadingsInPossibleFall.IndexOf(r);
                    }
                }
            }
            //variable for storing the number of readings after the index of the highest known threshold break
            var countFromHighToEnd = AcceleometerReadingsInPossibleFall.Count - indexOfHighBreak;
            // list to containt values after the high break
            var remainingAccelerometerValues = AcceleometerReadingsInPossibleFall.GetRange(indexOfHighBreak, countFromHighToEnd);
            // for storing the index of the first stable reading found
            var startingConsecutiveStableIndex = 0;
            //iterate over remaining accelerometer values (after the high threshold break)
            foreach (var r in remainingAccelerometerValues)
            {
                //readings index
                var indexOfR = remainingAccelerometerValues.IndexOf(r);
                //if readings magnitude is within stable range
                if (r.VectorMagnitude <= GlobalUtilities.AccelerometerManager.StableThreshold + GlobalUtilities.AccelerometerManager.StableTolerance && r.VectorMagnitude >= GlobalUtilities.AccelerometerManager.StableThreshold - GlobalUtilities.AccelerometerManager.StableTolerance)
                {
                    //if consecutive reading
                    if (indexOfR == startingConsecutiveStableIndex + 1)
                    {
                        //increment stable count
                        stableHitCount++;
                        //increment the starting index of consecutive stable readings
                        startingConsecutiveStableIndex++;
                    }
                    else
                    {
                        //otherwise change starting index of consecutive readings
                        startingConsecutiveStableIndex = indexOfR;
                    }
                    //if multiple consecutive stable readings found
                    if (stableHitCount >= StableHitCountMax)
                    {
                        //wake the user
                        Intent alarmIntent = new Intent(this, typeof(FallReceiver));
                        //create pending intent
                        PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
                        //new alarm manager instance
                        AlarmManager alarmManager = (AlarmManager)this.GetSystemService(Context.AlarmService);
                        //creating alarm to start fall notification activity
                        alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 1 * 1000, pendingIntent);
                        //stop service
                        StopSelf();
                        break;
                    }
                }
                //otherwise reset stable hit counter
                else stableHitCount = 0;
            }
            //clear recorded list of readings
            AcceleometerReadingsInPossibleFall.Clear();
        }

        #endregion //Methods
    }
}

