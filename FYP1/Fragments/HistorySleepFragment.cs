using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Managers;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using OxyPlot.Xamarin.Android;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class HistorySleepFragment : BaseFragment
    {
        #region Fields
        //ui vars
        private RadioButton m_weekSleepRadBtn;
        private RadioButton m_monthSleepRadBtn;
        private RadioButton m_yearSleepRadBtn;
        private ListView m_sleepHistoryLstView;
        private ListViewHelper<SleepEntry> m_listViewHelper;
        private PlotView m_sleepHistoryPlotView;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_weekSleepRadBtn
        /// </summary>
        public RadioButton WeekSleepRadBtn
        {
            get
            {
                return m_weekSleepRadBtn;
            }

            set
            {
                m_weekSleepRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_monthSleepRadBtn
        /// </summary>
        public RadioButton MonthSleepRadBtn
        {
            get
            {
                return m_monthSleepRadBtn;
            }

            set
            {
                m_monthSleepRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_yearSleepRadBtn
        /// </summary>
        public RadioButton YearSleepRadBtn
        {
            get
            {
                return m_yearSleepRadBtn;
            }

            set
            {
                m_yearSleepRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepHistoryLstView
        /// </summary>
        public ListView SleepHistoryLstView
        {
            get
            {
                return m_sleepHistoryLstView;
            }

            set
            {
                m_sleepHistoryLstView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listViewHelper
        /// </summary>
        public ListViewHelper<SleepEntry> ListViewHelper
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
        /// Gets/Sets m_sleepHistoryPlotView
        /// </summary>
        public PlotView SleepHistoryPlotView
        {
            get
            {
                return m_sleepHistoryPlotView;
            }

            set
            {
                m_sleepHistoryPlotView = value;
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
            //inflare fragment view
            InflateView(inflater, container, Resource.Layout.HistorySleepView);
            //set up variables
            SetupVariables();
            //handle events
            EventHandlers();
            //retur view
            return FragView;
        }

        /// <summary>
        /// Setup Variables for use in this class
        /// </summary>
        private void SetupVariables()
        {
            //set up toolbar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Sleep History";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            //set up ui
            SleepHistoryLstView = FragView.FindViewById<ListView>(Resource.Id.sleepHistoryListView);
            SleepHistoryPlotView = FragView.FindViewById<PlotView>(Resource.Id.sleepHistoryPlotViewModel);
            //get step entries for week
            var thisWeeksSleepEntries = GlobalUtilities.SleepManager.GetSleepEntriesForCurrentWeek().ToList();
            //update list view and plot
            UpdateListViewAndPlotView(thisWeeksSleepEntries, GraphType.Week);
            //set background colour
            SleepHistoryLstView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //set plot view model
            SleepHistoryPlotView.Model = GlobalUtilities.GraphManager.CreateSleepPlotModel("Sleep History", thisWeeksSleepEntries, GraphType.Week);
            //set up buttons
            WeekSleepRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.weekSleepHistoryRadBtn);
            MonthSleepRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.monthSleepHistoryRadBtn);
            YearSleepRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.yearSleepHistoryRadBtn);
            WeekSleepRadBtn.Checked = true;
        }

        /// <summary>
        /// Handle Events
        /// </summary>
        private void EventHandlers()
        {
            //week rad btn clicked
            WeekSleepRadBtn.Click += (s, e) =>
            {
                //toggle btns
                WeekSleepRadBtn.Checked = true;
                MonthSleepRadBtn.Checked = false;
                YearSleepRadBtn.Checked = false;
                //get sleep entriesfor current week
                var thisWeeksSleepEntries = GlobalUtilities.SleepManager.GetSleepEntriesForCurrentWeek().ToList();
                //update list view and plot
                UpdateListViewAndPlotView(thisWeeksSleepEntries, GraphType.Week);
            };
            //month rad btn clicked
            MonthSleepRadBtn.Click += (s, e) =>
            {
                //toggle btns
                WeekSleepRadBtn.Checked = false;
                MonthSleepRadBtn.Checked = true;
                YearSleepRadBtn.Checked = false;
                //get sleep entriesfor current month
                var thisMonthsSleepEntries = GlobalUtilities.SleepManager.GetSleepEntriesForCurrentMonth().ToList();
                UpdateListViewAndPlotView(thisMonthsSleepEntries, GraphType.Month);
            };
            //year rad btn clicked
            YearSleepRadBtn.Click += (s, e) =>
            {
                //toggle btns
                WeekSleepRadBtn.Checked = false;
                MonthSleepRadBtn.Checked = false;
                YearSleepRadBtn.Checked = true;
                //get sleep entriesfor current year
                var thisYearsSleepEntries = GlobalUtilities.SleepManager.GetSleepEntriesForCurrentYear().ToList();
                //update list view and plot
                UpdateListViewAndPlotView(thisYearsSleepEntries, GraphType.Year);
            };

        }

        /// <summary>
        /// Update plot and model for this view
        /// </summary>
        /// <param name="sleepEntries"></param>
        /// <param name="graphType"></param>
        private void UpdateListViewAndPlotView(List<SleepEntry> sleepEntries, GraphType graphType)
        {
            //new list helper
            ListViewHelper = new ListViewHelper<SleepEntry>(SleepHistoryLstView, sleepEntries);

            //Populatings Weight Entries from database
            ListViewHelper.PopulateListFromDatabase();

            //Display Weight Entries in List View
            SleepHistoryLstView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
            //update mdel
            SleepHistoryPlotView.Model = GlobalUtilities.GraphManager.CreateSleepPlotModel("Sleep History", sleepEntries, graphType);
        }

        #endregion //Methods
    }
}