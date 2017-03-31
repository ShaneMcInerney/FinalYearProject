using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    /*
     * This class cotains code, (slightly modified) from a Xamarin Andorid Google Services Activity Recognition API Sample
     * https://github.com/xamarin/monodroid-samples/tree/master/google-services/Location/ActivityRecognition
     */
    public class DetectedActivitiesFragment : BaseFragment, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        #region Fields
        //UI variables
        private Button m_requestActivityUpdatesButton;
        private Button m_removeActivityUpdatesButton;
        //utility variables
        private ActivityDetectionBroadcastReceiver m_broadcastReceiver;
        private GoogleApiClient m_googleApiClient;
        private PendingIntent m_activityDetectionPendingIntent;
        private ListView m_detectedActivitiesListView;
        private DetectedActivitiesAdapter m_adapter;
        private List<DetectedActivity> m_detectedActivities;
        private ISharedPreferences m_sharedPreferencesInstance;
        private bool m_updatesRequestedState;
        private readonly int[] m_monitoredActivities = {
            DetectedActivity.Still,
            DetectedActivity.OnFoot,
            DetectedActivity.Walking,
            DetectedActivity.Running,
            DetectedActivity.OnBicycle,
            DetectedActivity.InVehicle,
            DetectedActivity.Tilting,
            DetectedActivity.Unknown
        };

        #endregion //Fields

        #region Constructors


        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets m_sharedPreferencesInstance
        /// </summary>
        public ISharedPreferences SharedPreferencesInstance
        {
            get
            {
                m_sharedPreferencesInstance = Activity.GetSharedPreferences("SHARED_PREFERENCES", FileCreationMode.Private);
                return m_sharedPreferencesInstance;
            }
        }

        /// <summary>
        /// Gets/Sets m_updatesRequestedState
        /// </summary>
        public bool UpdatesRequestedState
        {
            get
            {
                m_updatesRequestedState = SharedPreferencesInstance.GetBoolean("ACTIVITY_UPDATES_REQUESTED", false);
                return m_updatesRequestedState;
            }
            set
            {
                m_updatesRequestedState = value;
                SharedPreferencesInstance.Edit().PutBoolean("ACTIVITY_UPDATES_REQUESTED", m_updatesRequestedState).Commit();
            }
        }

        /// <summary>
        /// Gets/Sets m_broadcastReceiver
        /// </summary>
        public ActivityDetectionBroadcastReceiver BroadcastReceiver
        {
            get
            {
                return m_broadcastReceiver;
            }

            set
            {
                m_broadcastReceiver = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_googleApiClient
        /// </summary>
        public GoogleApiClient GoogleApiClient
        {
            get
            {
                return m_googleApiClient;
            }

            set
            {
                m_googleApiClient = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_requestActivityUpdatesButton
        /// </summary>
        public Button RequestActivityUpdatesButton
        {
            get
            {
                return m_requestActivityUpdatesButton;
            }

            set
            {
                m_requestActivityUpdatesButton = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_removeActivityUpdatesButton
        /// </summary>
        public Button RemoveActivityUpdatesButton
        {
            get
            {
                return m_removeActivityUpdatesButton;
            }

            set
            {
                m_removeActivityUpdatesButton = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_detectedActivitiesListView
        /// </summary>
        public ListView DetectedActivitiesListView
        {
            get
            {
                return m_detectedActivitiesListView;
            }

            set
            {
                m_detectedActivitiesListView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_adapter
        /// </summary>
        public DetectedActivitiesAdapter Adapter
        {
            get
            {
                return m_adapter;
            }

            set
            {
                m_adapter = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_detectedActivities
        /// </summary>
        public List<DetectedActivity> DetectedActivities
        {
            get
            {
                return m_detectedActivities;
            }

            set
            {
                m_detectedActivities = value;
            }
        }

        /// <summary>
        /// Gets m_monitoredActivities
        /// </summary>
        public int[] MonitoredActivities
        {
            get
            {
                return m_monitoredActivities;
            }
        }

        /// <summary>
        /// Gets m_activityDetectionPendingIntent
        /// </summary>
        public PendingIntent ActivityDetectionPendingIntent
        {
            get
            {
                if (m_activityDetectionPendingIntent != null)
                {
                    return m_activityDetectionPendingIntent;
                }
                var intent = new Intent(Activity, typeof(DetectedActivitiesIntentService));

                return PendingIntent.GetService(Activity, 0, intent, PendingIntentFlags.UpdateCurrent);
            }
        }

        #endregion //Property Accessors

        #region Methods

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate fragview
            InflateView(inflater, container, Resource.Layout.DetectedActivityView);
            //set up UI variables
            SetupUIVariables();
            //set up variales
            SetupUtilityVariables(savedInstanceState);
            //handle events
            EventHandlers();
            //return frag view
            return FragView;
        }

        /// <summary>
        /// Setup UI variables for this class
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Activities";
            RequestActivityUpdatesButton = FragView.FindViewById<Button>(Resource.Id.request_activity_updates_button);
            RemoveActivityUpdatesButton = FragView.FindViewById<Button>(Resource.Id.remove_activity_updates_button);
            DetectedActivitiesListView = FragView.FindViewById<ListView>(Resource.Id.detected_activities_listview);
        }

        /// <summary>
        /// Setup utilirt variables for use in this class
        /// </summary>
        /// <param name="savedInstanceState"></param>
        private void SetupUtilityVariables(Bundle savedInstanceState)
        {
            //if updates requested
            if (UpdatesRequestedState)
            {
                RequestActivityUpdatesButton.Enabled = false;
                RemoveActivityUpdatesButton.Enabled = true;
            }
            else
            {
                RequestActivityUpdatesButton.Enabled = true;
                RemoveActivityUpdatesButton.Enabled = false;
            }
            //new broadcast receiver
            BroadcastReceiver = new ActivityDetectionBroadcastReceiver();
            //if instance state is not null and cotains key
            if (savedInstanceState != null && savedInstanceState.ContainsKey("DETECTED_ACTIVITIES"))
            {
                //set deteted activities
                DetectedActivities = ((SerializableDetectedActivities)savedInstanceState.GetSerializable("DETECTED_ACTIVITIES")).DetectedActivities;
            }
            else
            {
                //detected activities equals new list
                DetectedActivities = new List<DetectedActivity>();
                //for all monitored activities
                for (int i = 0; i < MonitoredActivities.Length; i++)
                {
                    //add detectedactivity
                    DetectedActivities.Add(new DetectedActivity(MonitoredActivities[i], 0));
                }
            }
            //set adapter
            Adapter = new DetectedActivitiesAdapter(Activity, DetectedActivities);
            //set adapter to above
            DetectedActivitiesListView.Adapter = Adapter;
            //create api client
            CreateApiClient();
        }

        /// <summary>
        /// Handle Events
        /// </summary>
        private void EventHandlers()
        {
            //requesting updates clicked
            RequestActivityUpdatesButton.Click += async (s, e) =>
            {
                //if api is connected
                if (!GoogleApiClient.IsConnected)
                {
                    //toast user
                    Toast.MakeText(Activity, "Currently Not Connected", ToastLength.Short).Show();
                    return;
                }
                //set status
                var status = await ActivityRecognition.ActivityRecognitionApi.RequestActivityUpdatesAsync(GoogleApiClient, 0, ActivityDetectionPendingIntent);
                //handle status result
                HandleResult(status);
                //disable button
                RequestActivityUpdatesButton.Enabled = false;
                //enable button
                RemoveActivityUpdatesButton.Enabled = true;
                //toast user
                Toast.MakeText(Activity, "Requesting Activity Updates", ToastLength.Short).Show();

            };
            //remove updates clicked
            RemoveActivityUpdatesButton.Click += async (s, e) =>
            {
                //if client is not conected
                if (!GoogleApiClient.IsConnected)
                {
                    //toast user
                    Toast.MakeText(Activity, "Currently Not Connected", ToastLength.Short).Show();
                    return;
                }
                //set status
                var status = await ActivityRecognition.ActivityRecognitionApi.RemoveActivityUpdatesAsync(GoogleApiClient, ActivityDetectionPendingIntent);
                //handle result
                HandleResult(status);
                //enable button
                RequestActivityUpdatesButton.Enabled = true;
                ///disable button
                RemoveActivityUpdatesButton.Enabled = false;
                //toast user
                Toast.MakeText(Activity, "Stopping Activity Updates", ToastLength.Short).Show();
            };

            //on receive
            BroadcastReceiver.OnReceiveImpl = (context, intent) =>
            {
                //set updated activities
                var updatedActivities = intent.GetParcelableArrayExtra("ACTIVITY_EXTRA").Cast<DetectedActivity>().ToList();
                //update activity list
                UpdateDetectedActivitiesList(updatedActivities);
            };

        }

        /// <summary>
        /// Setup Api Client
        /// </summary>
        protected void CreateApiClient()
        {
            //create api client
            GoogleApiClient = new GoogleApiClient.Builder(Activity)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(ActivityRecognition.API)
                .Build();
        }

        public override void OnStart()
        {
            base.OnStart();
            GoogleApiClient.Connect();
        }

        public override void OnStop()
        {
            base.OnStop();
            GoogleApiClient.Disconnect();
        }

        public override void OnResume()
        {
            base.OnResume();
            LocalBroadcastManager.GetInstance(Activity).RegisterReceiver(BroadcastReceiver, new IntentFilter("BROADCAST_ACTION"));
        }

        public override void OnPause()
        {
            LocalBroadcastManager.GetInstance(Activity).UnregisterReceiver(BroadcastReceiver);
            base.OnPause();
        }

        public void OnConnected(Bundle connectionHint)
        {

        }

        public void OnConnectionSuspended(int cause)
        {

            GoogleApiClient.Connect();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {

        }

        public void HandleResult(Statuses status)
        {
            if (status.IsSuccess)
            {
                bool requestingUpdates = !UpdatesRequestedState;

                UpdatesRequestedState = requestingUpdates;
            }
        }


        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutSerializable("DETECTED_ACTIVITIES", new SerializableDetectedActivities(DetectedActivities));
            base.OnSaveInstanceState(outState);
        }

        protected void UpdateDetectedActivitiesList(IList<DetectedActivity> detectedActivities)
        {
            Adapter.UpdateActivities(detectedActivities);
        }

        #endregion ///Methods
    }
}