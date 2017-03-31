using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP.Business.Managers;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using OxyPlot.Xamarin.Android;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class HistoryWeightFragment : BaseFragment
    {

        #region Fields

        //ui vars
        private RadioButton m_weekWeightRadBtn;
        private RadioButton m_monthWeightRadBtn;
        private RadioButton m_yearWeightRadBtn;
        private ListView m_weightEntriesListView;
        private PlotView m_weightHistoryPlotView;
        //util vars
        private ListViewHelper<WeightEntry> m_listViewHelper;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_weightEntriesListView
        /// </summary>
        public ListView WeightEntriesListView
        {
            get
            {
                return m_weightEntriesListView;
            }

            set
            {
                m_weightEntriesListView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listViewHelper
        /// </summary>
        public ListViewHelper<WeightEntry> ListViewHelper
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
        /// Gets/Sets m_weightHistoryPlotView
        /// </summary>
        public PlotView WeightHistoryPlotView
        {
            get
            {
                return m_weightHistoryPlotView;
            }

            set
            {
                m_weightHistoryPlotView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weekWeightRadBtn
        /// </summary>
        public RadioButton WeekWeightRadBtn
        {
            get
            {
                return m_weekWeightRadBtn;
            }

            set
            {
                m_weekWeightRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_monthWeightRadBtn
        /// </summary>
        public RadioButton MonthWeightRadBtn
        {
            get
            {
                return m_monthWeightRadBtn;
            }

            set
            {
                m_monthWeightRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_yearWeightRadBtn
        /// </summary>
        public RadioButton YearWeightRadBtn
        {
            get
            {
                return m_yearWeightRadBtn;
            }

            set
            {
                m_yearWeightRadBtn = value;
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
            //inflate frag view
            InflateView(inflater, container, Resource.Layout.HistoryWeightView);
            //set up UI vars
            SetupUIVariables();
            //setup utility vars
            SetupUtilityVariables();
            //event handlers
            EventHandlers();
            //return frag view
            return FragView;
        }

        /// <summary>
        /// Set up UI vars for this class
        /// </summary>
        private void SetupUIVariables()
        {
            //initialising variables
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Weight History";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            WeightEntriesListView = FragView.FindViewById<ListView>(Resource.Id.weightHistoryListView);
            //set up ui
            WeekWeightRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.weekWeightHistoryRadBtn);
            MonthWeightRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.monthWeightHistoryRadBtn);
            YearWeightRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.yearWeightHistoryRadBtn);
            WeekWeightRadBtn.Checked = true;
            WeightHistoryPlotView = FragView.FindViewById<PlotView>(Resource.Id.weightHistoryPlotViewModel);
        }

        /// <summary>
        /// set up utility vars for this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //get weight entries
            var weightEntries = GlobalUtilities.WeightManager.GetWeightEntriesForCurrentWeek().ToList();
            //update list view and plot view
            UpdateListViewAndPlotView(weightEntries, GraphType.Week);
        }

        /// <summary>
        /// 
        /// </summary>
        private void EventHandlers()
        {
            //week rad btn click
            WeekWeightRadBtn.Click += (s, e) =>
             {
                 //toggle btns
                 WeekWeightRadBtn.Checked = true;
                 MonthWeightRadBtn.Checked = false;
                 YearWeightRadBtn.Checked = false;
                 //get weight entries
                 var thisWeeksWeightEntries = GlobalUtilities.WeightManager.GetWeightEntriesForCurrentWeek().ToList();
                 //update graph and list view
                 UpdateListViewAndPlotView(thisWeeksWeightEntries, GraphType.Week);
             };
            //week rad btn click
            MonthWeightRadBtn.Click += (s, e) =>
            {
                //toggle btns
                WeekWeightRadBtn.Checked = false;
                MonthWeightRadBtn.Checked = true;
                YearWeightRadBtn.Checked = false;
                //get weight entries
                var thisMonthsWeightEntries = GlobalUtilities.WeightManager.GetWeightEntriesForCurrentMonth().ToList();
                //update graph and list view
                UpdateListViewAndPlotView(thisMonthsWeightEntries, GraphType.Month);
            };
            //week rad btn click
            YearWeightRadBtn.Click += (s, e) =>
            {
                //toggle btns
                WeekWeightRadBtn.Checked = false;
                MonthWeightRadBtn.Checked = false;
                YearWeightRadBtn.Checked = true;
                //get weight entries
                var thisYearsWeightEntries = GlobalUtilities.WeightManager.GetWeightEntriesForCurrentYear().ToList();
                //update graph and list view
                UpdateListViewAndPlotView(thisYearsWeightEntries, GraphType.Year);
            };

            //weight entry clicked
            WeightEntriesListView.ItemClick += (s, e) =>
            {
                //new weight creation frag
                var frag = new CreationWeightFragment();
                //setting selected weight entry
                WeightEntry selectedWeightEntry = ListViewHelper.List.ElementAt<WeightEntry>(e.Position);
                //serialize entry
                string output = JsonConvert.SerializeObject(selectedWeightEntry);
                Bundle bundle = new Bundle();
                bundle.PutString("WeightEntry", output);
                //attach args to frag
                frag.Arguments = bundle;
                //load fragment
                LoadFragment(frag);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weightEntries"></param>
        /// <param name="graphType"></param>
        private void UpdateListViewAndPlotView(List<WeightEntry> weightEntries, GraphType graphType)
        {
            //new list view helper
            ListViewHelper = new ListViewHelper<WeightEntry>(WeightEntriesListView, weightEntries);

            //Populatings Weight Entries from database
            ListViewHelper.PopulateListFromDatabase();

            //Display Weight Entries in List View
            WeightEntriesListView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
            WeightEntriesListView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //setting plot view model
            WeightHistoryPlotView.Model = GlobalUtilities.GraphManager.CreateWeightPlotModel("Weight History", weightEntries, graphType);
        }

        #endregion //Methods


    }
}