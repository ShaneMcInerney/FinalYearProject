using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class HistoryDetectedFallsFragment : BaseFragment
    {
        #region Fields

        private RadioButton m_weekFallRadBtn;
        private RadioButton m_monthFallRadBtn;
        private RadioButton m_yearFallRadBtn;
        private ListViewHelper<Fall> m_listViewHelper;
        private ListView m_fallListView;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_listViewHelper
        /// </summary>
        public ListViewHelper<Fall> ListViewHelper
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

        /// <summary>
        /// Gets/Sets m_fallListView
        /// </summary>
        public ListView FallListView
        {
            get
            {
                return m_fallListView;
            }

            set
            {
                m_fallListView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weekFallRadBtn
        /// </summary>
        public RadioButton WeekFallRadBtn
        {
            get
            {
                return m_weekFallRadBtn;
            }

            set
            {
                m_weekFallRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_monthFallRadBtn
        /// </summary>
        public RadioButton MonthFallRadBtn
        {
            get
            {
                return m_monthFallRadBtn;
            }

            set
            {
                m_monthFallRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_yearFallRadBtn
        /// </summary>
        public RadioButton YearFallRadBtn
        {
            get
            {
                return m_yearFallRadBtn;
            }

            set
            {
                m_yearFallRadBtn = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate fragview
            InflateView(inflater, container, Resource.Layout.HistoryDetectedFallsView);
            //Setup UI Variables
            SetupUIVariables();
            //setup variables
            SetupVariables();
            //handle events
            EventHandlers();
            //return frag view
            return FragView;
        }

        /// <summary>
        /// setup UI variables for use in this class
        /// </summary>
        private void SetupUIVariables()
        {
            //setting up tool bar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Previous Falls";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            //set up UI variables
            WeekFallRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.weeklyFallHistoryRadBtn);
            MonthFallRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.monthlyFallHistoryRadBtn);
            YearFallRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.yearlyFallHistoryRadBtn);
            FallListView = FragView.FindViewById<ListView>(Resource.Id.detectedFallsLstView);
            WeekFallRadBtn.Checked = true;
        }

        /// <summary>
        /// Setup variables for use in this class
        /// </summary>
        private void SetupVariables()
        {
            //new list view helper
            ListViewHelper = new ListViewHelper<Fall>(FallListView, GlobalUtilities.FallManager.GetAllFalls());
            //populate list
            ListViewHelper.PopulateListFromDatabase();
            //set background colour
            FallListView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //set lst view
            FallListView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
            //get falls for week
            var thisWeeksSleepEntries = GlobalUtilities.FallManager.GetFallsForCurrentWeek().ToList();
            //update list view
            UpdateListView(thisWeeksSleepEntries);
        }

        /// <summary>
        /// Handles events
        /// </summary>
        private void EventHandlers()
        {
            //on week rad button click
            WeekFallRadBtn.Click += (s, e) =>
            {
                //configure checks
                WeekFallRadBtn.Checked = true;
                MonthFallRadBtn.Checked = false;
                YearFallRadBtn.Checked = false;
                //get falls for week
                var thisWeeksSleepEntries = GlobalUtilities.FallManager.GetFallsForCurrentWeek().ToList();
                //update list view
                UpdateListView(thisWeeksSleepEntries);
            };
            //on month rad button click
            MonthFallRadBtn.Click += (s, e) =>
            {
                //configure checks
                WeekFallRadBtn.Checked = false;
                MonthFallRadBtn.Checked = true;
                YearFallRadBtn.Checked = false;
                //get falls for month
                var thisMonthsSleepEntries = GlobalUtilities.FallManager.GetFallsForCurrentMonth().ToList();
                //update list view
                UpdateListView(thisMonthsSleepEntries);
            };
            //on year rad button click
            YearFallRadBtn.Click += (s, e) =>
            {
                //configure checks
                WeekFallRadBtn.Checked = false;
                MonthFallRadBtn.Checked = false;
                YearFallRadBtn.Checked = true;
                //get falls for year
                var thisYearsSleepEntries = GlobalUtilities.FallManager.GetFallsForCurrentYear().ToList();
                //update list view
                UpdateListView(thisYearsSleepEntries);
            };
        }


        /// <summary>
        /// Updates list view
        /// </summary>
        /// <param name="listOfFalls">List of falls to to display in list view</param>
        private void UpdateListView(List<Fall> listOfFalls)
        {
            //set listview helper
            ListViewHelper = new ListViewHelper<Fall>(FallListView, listOfFalls);
            //Populatings Weight Entries from database
            ListViewHelper.PopulateListFromDatabase();
            //Display Weight Entries in List View
            FallListView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
        }
        #endregion //Methods
    }
}