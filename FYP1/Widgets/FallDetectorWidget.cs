using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using FYP_Droid.Services;

namespace FYP_Droid.Widgets
{
    [BroadcastReceiver(Label = "HellApp Widget")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class FallDetectorWidget : AppWidgetProvider
    {

        /// <summary>
        /// This method is called when the 'updatePeriodMillis' from the AppwidgetProvider passes,
        /// or the user manually refreshes/resizes.
        /// </summary>
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(FallDetectorWidget)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }

        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            // Retrieve the widget layout
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.FallDetectorWidget);
            //set text view
            SetTextViewText(widgetView);
            //register clicks
            RegisterClicks(context, appWidgetIds, widgetView);
            //return widget view
            return widgetView;
        }

        private void SetTextViewText(RemoteViews widgetView)
        {

        }

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            var intent = new Intent(context, typeof(FallDetectorService));

            // Register click event for the Background
            var piBackground = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            widgetView.SetOnClickPendingIntent(Resource.Id.widgetStartAccelerometerBtn, GetPendingSelfIntent(context, "START"));


            // Register click event for the Announcement-icon
            //set pending intent on click
            widgetView.SetOnClickPendingIntent(Resource.Id.widgetStopAccelerometerBtn, GetPendingSelfIntent(context, "STOP"));

        }

        private PendingIntent GetPendingSelfIntent(Context context, string extra)
        {
            //intent for fall detector service
            var intent = new Intent(context, typeof(FallDetectorService));

            intent.PutExtra(extra, true);

            return PendingIntent.GetService(context, 0, intent, 0);
        }

        /// <summary>
        /// This method is called when clicks are registered.
        /// </summary>
        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);
            // Check if the click is from the "start" button
            if (intent.HasExtra("Start"))
            {
                if (FallDetectorService.IsRunning == false)
                {
                    //start service
                    context.StartService(new Intent(context, typeof(FallDetectorService)));
                }
            }
            // Check if the click is from the "start" button
            if (intent.HasExtra("STOP"))
            {
                if (FallDetectorService.IsRunning)
                {
                    //stop service
                    context.StopService(new Intent(context, typeof(FallDetectorService)));
                }

            }

        }
    }
}