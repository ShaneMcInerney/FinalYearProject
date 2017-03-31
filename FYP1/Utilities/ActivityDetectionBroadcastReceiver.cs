using Android.Content;
using System;

namespace FYP_Droid.Utilities
{
    /*
     * This class cotains code, (slightly modified) from a Xamarin Andorid Google Services Activity Recognition API Sample
     * https://github.com/xamarin/monodroid-samples/tree/master/google-services/Location/ActivityRecognition
     */
    [BroadcastReceiver]
    public class ActivityDetectionBroadcastReceiver : BroadcastReceiver
    {
        public Action<Context, Intent> OnReceiveImpl { get; set; }

        public override void OnReceive(Context context, Intent intent)
        {
            OnReceiveImpl(context, intent);
        }
    }
}