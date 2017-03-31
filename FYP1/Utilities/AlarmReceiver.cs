using Android.Content;
using FYP_Droid.Activities;
using System;

namespace FYP_Droid.Utilities
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {

        public Action<Context, Intent> OnReceiveImpl { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        public override void OnReceive(Context context, Intent intent)
        {

            //if sleep length extra
            if (intent.HasExtra("SleepLength"))
            {
                //start sleep alarm activity
                var newActivity = new Intent(context, typeof(SleepAlarmActivity));

                newActivity.PutExtra("SleepLength", intent.GetStringExtra("SleepLength"));
                newActivity.AddFlags(ActivityFlags.NewTask);

                context.StartActivity(newActivity);
            }
            //if normal alarm extra
            if (intent.HasExtra("NormalAlarm"))
            {
                //start sleep alarm activity
                var newActivity = new Intent(context, typeof(SleepAlarmActivity));

                newActivity.AddFlags(ActivityFlags.NewTask);

                context.StartActivity(newActivity);
            }
            //if detected activity extra
            if (intent.HasExtra("DetectedActivity"))
            {
                OnReceiveImpl(context, intent);
            }
        }
    }
}