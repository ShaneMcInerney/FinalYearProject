using Android.Content;
using Android.Gms.Location;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace FYP_Droid.Utilities
{
    /*
     * This class cotains code, (slightly modified) from a Xamarin Andorid Google Services Activity Recognition API Sample
     * https://github.com/xamarin/monodroid-samples/tree/master/google-services/Location/ActivityRecognition
     */
    public class DetectedActivitiesAdapter : ArrayAdapter<DetectedActivity>
    {
        #region Fields

        private View m_inflatedView;
        private TextView m_activityNameTxtView;
        private TextView m_activityPercentageTxtView;
        private ProgressBar m_activityProgressBar;
        private int m_itemPosition;
        private DetectedActivity m_activityDetected;
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

        public DetectedActivitiesAdapter(Context context, IList<DetectedActivity> detectedActivities) : base(context, 0, detectedActivities)
        {

        }

        #endregion //Constructors

        #region Property Accessors

        public TextView ActivityNameTxtView
        {
            get
            {
                return m_activityNameTxtView;
            }

            set
            {
                m_activityNameTxtView = value;
            }
        }

        public TextView ActivityPercentageTxtView
        {
            get
            {
                return m_activityPercentageTxtView;
            }

            set
            {
                m_activityPercentageTxtView = value;
            }
        }

        public ProgressBar ActivityProgressBar
        {
            get
            {
                return m_activityProgressBar;
            }

            set
            {
                m_activityProgressBar = value;
            }
        }

        public View InflatedView
        {
            get
            {
                return m_inflatedView;
            }

            set
            {
                m_inflatedView = value;
            }
        }

        public int ItemPosition
        {
            get
            {
                return m_itemPosition;
            }

            set
            {
                m_itemPosition = value;
            }
        }

        public DetectedActivity ActivityDetected
        {
            get
            {
                return m_activityDetected;
            }

            set
            {
                m_activityDetected = value;
            }
        }

        public int[] MonitoredActivities
        {
            get
            {
                return m_monitoredActivities;
            }
        }

        #endregion //Property Accessors

        #region Methods

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //inflate vie
            InflatedView = LayoutInflater.From(Context).Inflate(Resource.Xml.DetectedActivityLayout, parent, false);
            //get item position
            ItemPosition = position;
            //set up variables
            SetupVariables();
            //handle events
            EventHandlers();
            //return inflated view
            return InflatedView;
        }

        /// <summary>
        /// Setup variables
        /// </summary>
        private void SetupVariables()
        {
            ActivityDetected = GetItem(ItemPosition);
            ActivityNameTxtView = InflatedView.FindViewById<TextView>(Resource.Id.detected_activity_name);
            ActivityPercentageTxtView = InflatedView.FindViewById<TextView>(Resource.Id.detected_activity_confidence_level);
            ActivityProgressBar = InflatedView.FindViewById<ProgressBar>(Resource.Id.detected_activity_progress_bar);

            ActivityNameTxtView.Text = ActivityStringSwitcher(ActivityDetected.Type);

            ActivityPercentageTxtView.Text = ActivityDetected.Confidence + "%";

            ActivityProgressBar.Progress = ActivityDetected.Confidence;
        }

        private void EventHandlers()
        {

        }

        /// <summary>
        /// Update detected activities
        /// </summary>
        /// <param name="detectedActivities"></param>
        internal void UpdateActivities(IList<DetectedActivity> detectedActivities)
        {
            //new dicionary for detected activities
            var detectedActivitiesMap = new Dictionary<int, int>();
            //itterate detected activities
            foreach (var activity in detectedActivities)
            {
                //add activity to map
                detectedActivitiesMap.Add(activity.Type, activity.Confidence);
            }
            //create temporary list of detected activities
            var tempList = new List<DetectedActivity>();
            //loop for monitored activities
            for (int i = 0; i < MonitoredActivities.Length; i++)
            {
                //activity confidence
                int confidence = 0;
                //if monitored activity found
                if (detectedActivitiesMap.ContainsKey(MonitoredActivities[i]))
                {
                    //set confidence
                    confidence = detectedActivitiesMap[MonitoredActivities[i]];
                }
                //add activity
                tempList.Add(new DetectedActivity(MonitoredActivities[i], confidence));
            }
            //claer
            Clear();
            //add all in tmep list
            AddAll(tempList);
        }

        /// <summary>
        /// Switch types of detected activities
        /// </summary>
        /// <param name="detectedActivityType"></param>
        /// <returns>ctivity string</returns>
        public string ActivityStringSwitcher(int detectedActivityType)
        {
            switch (detectedActivityType)
            {
                case DetectedActivity.InVehicle:
                    return "In Vehicle";
                case DetectedActivity.OnBicycle:
                    return "On Bicycle";
                case DetectedActivity.OnFoot:
                    return "On Foot";
                case DetectedActivity.Running:
                    return "Running";
                case DetectedActivity.Still:
                    return "Standing";
                case DetectedActivity.Tilting:
                    return "Tilting";
                case DetectedActivity.Unknown:
                    return "Unknown";
                case DetectedActivity.Walking:
                    return "Walking";
                default:
                    return "Unidentifiable";
            }
        }
        #endregion //Methods



    }
}