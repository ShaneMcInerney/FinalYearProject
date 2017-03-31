using Android.Content;
using FYP_Droid.Activities;

namespace FYP_Droid.Utilities
{
    [BroadcastReceiver]
    public class FallReceiver : BroadcastReceiver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        public override void OnReceive(Context context, Intent intent)
        {
            //new potentail fall activity
            var newActivity = new Intent(context, typeof(PotentialFallDetectedActivity));
            //add new task flags
            newActivity.AddFlags(ActivityFlags.NewTask);
            //start actiity
            context.StartActivity(newActivity);
        }
    }
}