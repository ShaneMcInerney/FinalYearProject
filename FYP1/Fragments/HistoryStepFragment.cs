using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP.Business.Managers;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using OxyPlot.Xamarin.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class HistoryStepFragment : BaseFragment
    {

        #region Fields
        //ui vars
        private RadioButton m_weekStepRadBtn;
        private RadioButton m_monthStepRadBtn;
        private RadioButton m_yearStepRadBtn;
        private ListView m_stepHistoryLstView;
        private PlotView m_stepHistoryPlotView;
        //utility vars
        private ListViewHelper<StepEntry> m_listViewHelper;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_stepHistoryLstView
        /// </summary>
        public ListView StepHistoryLstView
        {
            get
            {
                return m_stepHistoryLstView;
            }

            set
            {
                m_stepHistoryLstView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listViewHelper
        /// </summary>
        public ListViewHelper<StepEntry> ListViewHelper
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
        /// Gets/Sets m_stepHistoryPlotView
        /// </summary>
        public PlotView StepHistoryPlotView
        {
            get
            {
                return m_stepHistoryPlotView;
            }

            set
            {
                m_stepHistoryPlotView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weekStepRadBtn
        /// </summary>
        public RadioButton WeekStepRadBtn
        {
            get
            {
                return m_weekStepRadBtn;
            }

            set
            {
                m_weekStepRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_monthStepRadBtn
        /// </summary>
        public RadioButton MonthStepRadBtn
        {
            get
            {
                return m_monthStepRadBtn;
            }

            set
            {
                m_monthStepRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_yearStepRadBtn
        /// </summary>
        public RadioButton YearStepRadBtn
        {
            get
            {
                return m_yearStepRadBtn;
            }

            set
            {
                m_yearStepRadBtn = value;
            }
        }


        #endregion //Property Acessors

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
            InflateView(inflater, container, Resource.Layout.HistoryStepView);
            //set up UI variables
            SetupUIVariables();
            //setup variables
            SetupUtilityVariables();
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
            //set up toolbar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Step History";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            //setup ui
            StepHistoryLstView = FragView.FindViewById<ListView>(Resource.Id.stepHistoryLstView);
            StepHistoryPlotView = FragView.FindViewById<PlotView>(Resource.Id.stepHistoryPlotViewModel);
            WeekStepRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.weekStepHistoryRadBtn);
            MonthStepRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.monthStepHistoryRadBtn);
            YearStepRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.yearStepHistoryRadBtn);
            WeekStepRadBtn.Checked = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetupUtilityVariables()
        {
            //step entries for the week
            var thisWeeksStepEntries = GlobalUtilities.StepManager.GetStepEntriesForCurrentWeek().ToList();
            //daily totals
            List<StepEntry> dailiyTotals = GlobalUtilities.StepManager.GetDailyTotalsForCurrentWeek(thisWeeksStepEntries);
            //update list view and pot
            UpdateListViewAndPlotView(dailiyTotals, GraphType.Week);
        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //week rad btn click
            WeekStepRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                WeekStepRadBtn.Checked = true;
                MonthStepRadBtn.Checked = false;
                YearStepRadBtn.Checked = false;
                //get weeks step entries
                var thisWeeksStepEntries = GlobalUtilities.StepManager.GetStepEntriesForCurrentWeek().ToList();
                //get daily totals
                List<StepEntry> dailiyTotals = GlobalUtilities.StepManager.GetDailyTotalsForCurrentWeek(thisWeeksStepEntries);
                //update list and plot view
                UpdateListViewAndPlotView(dailiyTotals, GraphType.Week);
            };

            //month rad btn click
            MonthStepRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                WeekStepRadBtn.Checked = false;
                MonthStepRadBtn.Checked = true;
                YearStepRadBtn.Checked = false;
                //get month step entries
                var thisMonthsStepEntries = GlobalUtilities.StepManager.GetStepEntriesForCurrentMonth().ToList();
                //get daily totals
                List<StepEntry> dailiyTotals = GlobalUtilities.StepManager.GetDailyTotalsForCurrentMonth(thisMonthsStepEntries);
                //update list and plot view
                UpdateListViewAndPlotView(dailiyTotals, GraphType.Month);
            };

            //year rad btn click
            YearStepRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                WeekStepRadBtn.Checked = false;
                MonthStepRadBtn.Checked = false;
                YearStepRadBtn.Checked = true;
                //get years step entries
                var thisYearsStepEntries = GlobalUtilities.StepManager.GetStepEntriesForCurrentYear().ToList();
                //get daily totals
                List<StepEntry> dailiyTotals = GlobalUtilities.StepManager.GetDailyTotalsForCurrentYear(thisYearsStepEntries);
                //update list and plot view
                UpdateListViewAndPlotView(dailiyTotals, GraphType.Year);
            };

        }

        /// <summary>
        /// Update list view and plot model
        /// </summary>
        /// <param name="stepEntries">step entires to plot</param>
        /// <param name="graphType">graph type</param>
        private void UpdateListViewAndPlotView(List<StepEntry> stepEntries, GraphType graphType)
        {
            //new list view helper
            ListViewHelper = new ListViewHelper<StepEntry>(StepHistoryLstView, stepEntries);

            //Populatings Weight Entries from database
            ListViewHelper.PopulateListFromDatabase();

            //Display Weight Entries in List View
            StepHistoryLstView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
            StepHistoryLstView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //set plot model
            StepHistoryPlotView.Model = GlobalUtilities.GraphManager.CreateStepPlotModel("Step History", stepEntries, graphType);
        }

        #endregion //Methods


    }
}