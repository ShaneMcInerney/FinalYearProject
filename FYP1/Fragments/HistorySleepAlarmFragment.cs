using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class HistorySleepAlarmFragment : BaseFragment
    {

        #region Fields

        //ui vars
        private ListView m_sleepAlarmsListView;
        private Button m_createNewAlarmBtn;
        //util vars
        private ListViewHelper<SleepAlarm> m_listViewHelper;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_sleepAlarmsListView
        /// </summary>
        public ListView SleepAlarmsListView
        {
            get
            {
                return m_sleepAlarmsListView;
            }

            set
            {
                m_sleepAlarmsListView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_createNewAlarmBtn
        /// </summary>
        public Button CreateNewAlarmBtn
        {
            get
            {
                return m_createNewAlarmBtn;
            }

            set
            {
                m_createNewAlarmBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listViewHelper
        /// </summary>
        public ListViewHelper<SleepAlarm> ListViewHelper
        {
            get
            {
                return m_listViewHelper;
            }

            set
            {
                m_listViewHelper = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate frag view
            InflateView(inflater, container, Resource.Layout.HistorySleepAlarmView);
            //setup ui variables
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
            //retur frag view
            return FragView;
        }


        /// <summary>
        /// Setup UI variables for use with this class
        /// </summary>
        private void SetupUIVariables()
        {
            //set up toolbar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Alarms";
            Toolbar.Menu.Clear();
            SetHasOptionsMenu(true);
            //set up UI elements
            SleepAlarmsListView = FragView.FindViewById<ListView>(Resource.Id.SleepAlarmsListView);
            CreateNewAlarmBtn = FragView.FindViewById<Button>(Resource.Id.CreateNewSleepAlarmBtn);

        }

        /// <summary>
        /// Setup utility variables for use in this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //set list view helper
            ListViewHelper = new ListViewHelper<SleepAlarm>(SleepAlarmsListView, GlobalUtilities.SleepAlarmManager.GetAllSleepAlarm());
            //Populatings Weight Entries from database
            ListViewHelper.PopulateListFromDatabase();
            //Display Weight Entries in List View
            SleepAlarmsListView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
            //Set background colour
            SleepAlarmsListView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //sleep alarm list item clicked
            SleepAlarmsListView.ItemClick += (s, e) =>
            {

                //set selected sleep alarm
                SleepAlarm selectedSleepAlarm = ListViewHelper.List.ElementAt<SleepAlarm>(e.Position);
                //new creation alarm frag
                var frag = new CreationSleepAlarmFragment();
                //serialize sleep alarm
                string output = JsonConvert.SerializeObject(selectedSleepAlarm);
                //new bundle
                Bundle bundle = new Bundle();
                //attach extra tobundle
                bundle.PutString("SleepAlarm", output);
                //set frag arguments
                frag.Arguments = bundle;
                //load fragment
                LoadFragment(frag);
            };

            //create a new aarm btn click
            CreateNewAlarmBtn.Click += (s, e) =>
            {
                //load sleep alarm creation frag
                LoadFragment(new CreationSleepAlarmFragment());
            };
        }
        #endregion //Methods


    }
}