using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using Refractored.Xam.Vibrate;
using System;
using System.Timers;

namespace FYP_Droid.Services
{
    [Service]
    public class StepCounterService : Service, ISensorEventListener
    {

        #region Fields

        private static bool m_isRunning = false;
        private int m_stepCount = 0;
        private int m_firstValueReceived = 0;
        private int m_lastHour = 0;

        private Notification m_notification;
        private Notification.Builder m_notificationBuilder;
        private NotificationManager m_notificationManager;

        private const int m_notificationID = 0;
        private const int m_hourlyGoalsNotificationID = 0;
        private const int m_weeklyGoalsNotificationID = 0;
        private const int m_WeeklyGoalsNotificationID = 0;
        private Sensor m_sensor;
        private SensorManager m_sensorManager;
        private Timer m_timer;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_stepCount
        /// </summary>
        public int StepCount
        {
            get
            {
                return m_stepCount;
            }

            set
            {
                m_stepCount = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_firstValueReceived
        /// </summary>
        public int FirstValueReceived
        {
            get
            {
                return m_firstValueReceived;
            }

            set
            {
                m_firstValueReceived = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_lastHour
        /// </summary>
        public int LastHour
        {
            get
            {
                return m_lastHour;
            }

            set
            {
                m_lastHour = value;
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
        /// Gets/Sets m_sensor
        /// </summary>
        public Sensor Sensor
        {
            get
            {
                return m_sensor;
            }

            set
            {
                m_sensor = value;
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
            //if already running
            if (IsRunning)
            {
                //stop
                StopSelf();
            }
            else
            {
                //setup variables
                SetupVariables();
                //register listeners
                RegisterListeners();
                //handle events
                EventHandlers();
            }


            return StartCommandResult.NotSticky;
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
            base.OnDestroy();
            //set running to false
            IsRunning = false;
            //save hourly step count
            SaveHourlyCount(new StepEntry(StepCount, DateTime.Now, DateTime.Now.TimeOfDay));
            //unregister listeners
            UnregisterListeners();
            //clear notifications
            ClearNotifcations();
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
            //if no steps counted yet, recor the first value received, as the sensor accumulates values
            if (FirstValueReceived == 0)
            {
                //storing the value for the first time, to e used to calculate difference of steps
                FirstValueReceived = (int)e.Values[0];
            }
            //calculating steps taken (subtracting the first value received, from current value)
            StepCount = (int)e.Values[0] - FirstValueReceived;
            //adding calculated steps to amount for the day so far
            var realtimeCount = GetDailyTotalSoFar() + StepCount;
            //setting title of notiiction
            NotificationBuilder.SetContentTitle(realtimeCount.ToString() + " Steps Taken Today");
            //setting text of notification
            NotificationBuilder.SetContentText(
                "Estimated Distance Covered: " +
                //estimating distance covered, based on users stride length
                GlobalUtilities.StepManager.EstimateDistanceCovered(realtimeCount).ToString() + " Metres");
            //Build a notification object with updated content:
            Notification = NotificationBuilder.Build();
            //Publish the new notification with the existing ID:
            NotificationManager.Notify(NotificationID, Notification);


        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterListeners()
        {
            //register step counter
            SensorManager.RegisterListener(this, Sensor, SensorDelay.Normal);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UnregisterListeners()
        {
            //unregister step counter
            SensorManager = (SensorManager)GetSystemService(Context.SensorService);

            SensorManager.UnregisterListener(this);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearNotifcations()
        {
            NotificationManager.CancelAll();

            NotificationManager.Dispose();

            NotificationBuilder.Dispose();

            Notification.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetupVariables()
        {
            IsRunning = true;
            Timer = new Timer(1000);

            Timer.Start();

            SensorManager = (SensorManager)GetSystemService(Context.SensorService);

            Sensor = SensorManager.GetDefaultSensor(SensorType.StepCounter);

            NotificationBuilder = new Notification.Builder(this)
               .SetContentTitle("Today's Step Count")
               .SetContentText("")
               .SetColor(76)
               .SetOngoing(true)
               .SetSmallIcon(Resource.Drawable.notification_template_icon_bg);

            Notification = NotificationBuilder.Build();

            NotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            NotificationManager.Notify(NotificationID, Notification);

            GlobalUtilities.StepManager.StrideLength = GlobalUtilities.UserManager.GetUser().StrideLength;
        }

        /// <summary>
        /// 
        /// </summary>
        private void EventHandlers()
        {
            Timer.Elapsed += new ElapsedEventHandler(TimedEvent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void TimedEvent(object source, ElapsedEventArgs e)
        {

            if (LastHour < DateTime.Now.Hour || (LastHour == 23 && DateTime.Now.Hour == 0))
            {
                //set last hour to now
                LastHour = DateTime.Now.Hour;
                //save hourly count
                SaveHourlyCount(new StepEntry(StepCount, DateTime.Now, DateTime.Now.TimeOfDay));
                //get list of hourly step goals
                var listOfHourlyStepGoals = GlobalUtilities.StepGoalManager.GetAllHourlyStepGoals();
                //itterate ver list
                foreach (var stepGoal in listOfHourlyStepGoals)
                {
                    //if step gaosl is greater than step goal amount
                    if (StepCount >= stepGoal.Amount)
                    {
                        //complete goal
                        stepGoal.GoalComplete = true;
                        //notification title
                        NotificationBuilder.SetContentTitle("Congratulations! You have met your Hourly Step Goal!");
                        //notification text
                        NotificationBuilder.SetContentText("Tou took: " + StepCount + " Steps This Hour," + " You walked roughly: " + GlobalUtilities.StepManager.EstimateDistanceCovered(StepCount).ToString());
                        //save step goal
                        GlobalUtilities.StepGoalManager.SaveStepGoal(stepGoal);
                        //vibrate
                        CrossVibrate.Current.Vibration();
                    }
                }

                var listOfDailyStepGoals = GlobalUtilities.StepGoalManager.GetAllDailyStepGoals();
                var dailyTotalSteps = GlobalUtilities.StepManager.GetStepCountForCurrentDay();
                foreach (var stepGoal in listOfDailyStepGoals)
                {
                    if (StepCount + dailyTotalSteps >= stepGoal.Amount)
                    {
                        stepGoal.GoalComplete = true;
                        NotificationBuilder.SetContentTitle("Congratulations! You have met your Daily Step Goal!");
                        NotificationBuilder.SetContentText("Tou took: " + StepCount + " Steps Today," + " You walked roughly: " + GlobalUtilities.StepManager.EstimateDistanceCovered(StepCount + dailyTotalSteps).ToString());
                        GlobalUtilities.StepGoalManager.SaveStepGoal(stepGoal);
                        CrossVibrate.Current.Vibration();
                    }
                }

                var listOfWeeklyStepGoals = GlobalUtilities.StepGoalManager.GetAllWeeklyStepGoals();
                var weeklyTotalSteps = GlobalUtilities.StepManager.GetStepCountForCurrentWeek();
                foreach (var stepGoal in listOfWeeklyStepGoals)
                {
                    if (StepCount + weeklyTotalSteps >= stepGoal.Amount)
                    {
                        stepGoal.GoalComplete = true;
                        NotificationBuilder.SetContentTitle("Congratulations! You have met your Weekly Step Goal!");
                        NotificationBuilder.SetContentText("Tou took: " + StepCount + " Steps This Week," + " You walked roughly: " + GlobalUtilities.StepManager.EstimateDistanceCovered(StepCount + weeklyTotalSteps).ToString());
                        GlobalUtilities.StepGoalManager.SaveStepGoal(stepGoal);
                        CrossVibrate.Current.Vibration();
                    }
                }

                var listOfMonthlyStepGoals = GlobalUtilities.StepGoalManager.GetAllMonthlyStepGoals();
                var monthlyTotalSteps = GlobalUtilities.StepManager.GetStepCountForCurrentMonth();
                foreach (var stepGoal in listOfMonthlyStepGoals)
                {
                    if (StepCount + monthlyTotalSteps >= stepGoal.Amount)
                    {
                        stepGoal.GoalComplete = true;
                        NotificationBuilder.SetContentTitle("Congratulations! You have met your Monthly Step Goal!");
                        NotificationBuilder.SetContentText("Tou took: " + StepCount + " Steps This Month," + " You walked roughly: " + GlobalUtilities.StepManager.EstimateDistanceCovered(StepCount + monthlyTotalSteps).ToString());
                        GlobalUtilities.StepGoalManager.SaveStepGoal(stepGoal);
                        CrossVibrate.Current.Vibration();
                    }
                }

                var listOfYearlyStepGoals = GlobalUtilities.StepGoalManager.GetAllYearlyStepGoals();
                var yearlyTotalSteps = GlobalUtilities.StepManager.GetStepCountForCurrentYear();
                foreach (var stepGoal in listOfYearlyStepGoals)
                {
                    if (StepCount + yearlyTotalSteps >= stepGoal.Amount)
                    {
                        stepGoal.GoalComplete = true;
                        NotificationBuilder.SetContentTitle("Congratulations! You have met your Yearly Step Goal!");
                        NotificationBuilder.SetContentText("Tou took: " + StepCount + " Steps This Year," + " You walked roughly: " + GlobalUtilities.StepManager.EstimateDistanceCovered(StepCount + yearlyTotalSteps).ToString());
                        GlobalUtilities.StepGoalManager.SaveStepGoal(stepGoal);
                        CrossVibrate.Current.Vibration();

                    }
                }

                // Build a notification object with updated content:
                Notification = NotificationBuilder.Build();

                // Publish the new notification with the existing ID:
                NotificationManager.Notify(NotificationID, Notification);

                StepCount = 0;

                FirstValueReceived = 0;
            }

            NotificationManager.Notify(NotificationID, Notification);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Int64 GetDailyTotalSoFar()
        {
            Int64 dailyTotalStepCount = GlobalUtilities.StepManager.GetTotalStepsForCurrentDate();

            return dailyTotalStepCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Int64 GetWeeklyTotalSoFar()
        {
            Int64 weeklyTotalStepCount = GlobalUtilities.StepManager.GetStepCountForCurrentWeek();

            return weeklyTotalStepCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepEntry"></param>
        private void SaveHourlyCount(StepEntry stepEntry)
        {
            GlobalUtilities.StepManager.SaveStepEntry(stepEntry);
        }

        #endregion //Methods
    }
}