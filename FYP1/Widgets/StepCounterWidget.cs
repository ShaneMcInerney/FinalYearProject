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
    public class StepCounterWidget : AppWidgetProvider
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appWidgetIds"></param>
        /// <returns></returns>
        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            // Retrieve the widget layout. This is a RemoteViews, so we can't use 'FindViewById'
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.FallDetectorWidget);

            SetTextViewText(widgetView);
            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widgetView"></param>
        private void SetTextViewText(RemoteViews widgetView)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appWidgetIds"></param>
        /// <param name="widgetView"></param>
        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            var intent = new Intent(context, typeof(StepCounterService));

            // Register click event for the Background
            var piBackground = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            widgetView.SetOnClickPendingIntent(Resource.Id.widgetStartAccelerometerBtn, GetPendingSelfIntent(context, "START"));


            // Register click event for the Announcement-icon

            widgetView.SetOnClickPendingIntent(Resource.Id.widgetStopAccelerometerBtn, GetPendingSelfIntent(context, "STOP"));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        private PendingIntent GetPendingSelfIntent(Context context, string extra)
        {

            var intent = new Intent(context, typeof(StepCounterService));

            intent.PutExtra(extra, true);

            return PendingIntent.GetService(context, 0, intent, 0);
        }

        /// <summary>
        /// This method is called when clicks are registered.
        /// </summary>
        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);


            // Check if the click is from the "Announcement" button
            if (intent.HasExtra("Start"))
            {
                if (StepCounterService.IsRunning == false)
                {
                    context.StartService(new Intent(context, typeof(StepCounterService)));
                }
            }
            if (intent.HasExtra("STOP"))
            {
                if (StepCounterService.IsRunning)
                    context.StopService(new Intent(context, typeof(StepCounterService)));
            }

        }
    }
}