using Android.App;
using Android.Content;
using Android.OS;
using FYP_Droid.Utilities;

namespace FYP_Droid.Services
{
    [Service]
    public class SleepAlarmService : Service
    {
        #region Fields

        private static bool m_isRunning = false;

        #endregion //Fields


        #region Property Accessors

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
            //Setup Variables
            SetupVariables();
            //Handle events
            EventHandlers();


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

            base.OnDestroy();
        }



        /// <summary>
        /// 
        /// </summary>
        private void SetupVariables()
        {

        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //On alarm created
            GlobalUtilities.SleepAlarmManager.OnAlarmCreated += (s, e) =>
              {
                  //get new alarm
                  var newAlarm = GlobalUtilities.SleepAlarmManager.GetLastestSleepAlarm();
                  //make trigger to wake user
                  GlobalUtilities.SleepAlarmManager.WakeUser(this, newAlarm);
              };
        }

        #endregion //Methods

    }
}