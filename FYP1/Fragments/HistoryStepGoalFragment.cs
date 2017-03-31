using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class HistoryStepGoalFragment : BaseFragment
    {

        #region Fields
        //ui vars
        private RadioButton m_hourStepGoalRadBtn;
        private RadioButton m_dailyStepGoalRadBtn;
        private RadioButton m_weekStepGoalRadBtn;
        private RadioButton m_monthStepGoalRadBtn;
        private RadioButton m_yearStepGoalRadBtn;
        private RadioButton m_allStepGoalRadBtn;
        private ListView m_stepGoalListView;
        //utility vars
        private ListViewHelper<StepGoal> m_listViewHelper;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_listViewHelper
        /// </summary>
        public ListViewHelper<StepGoal> ListViewHelper
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
        /// Gets/Sets m_stepGoalListView
        /// </summary>
        public ListView StepGoalListView
        {
            get
            {
                return m_stepGoalListView;
            }

            set
            {
                m_stepGoalListView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_hourStepGoalRadBtn
        /// </summary>
        public RadioButton HourStepGoalRadBtn
        {
            get
            {
                return m_hourStepGoalRadBtn;
            }

            set
            {
                m_hourStepGoalRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weekStepGoalRadBtn
        /// </summary>
        public RadioButton WeekStepGoalRadBtn
        {
            get
            {
                return m_weekStepGoalRadBtn;
            }

            set
            {
                m_weekStepGoalRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_monthStepGoalRadBtn
        /// </summary>
        public RadioButton MonthStepGoalRadBtn
        {
            get
            {
                return m_monthStepGoalRadBtn;
            }

            set
            {
                m_monthStepGoalRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_yearStepGoalRadBtn
        /// </summary>
        public RadioButton YearStepGoalRadBtn
        {
            get
            {
                return m_yearStepGoalRadBtn;
            }

            set
            {
                m_yearStepGoalRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_allStepGoalRadBtn
        /// </summary>
        public RadioButton AllStepGoalRadBtn
        {
            get
            {
                return m_allStepGoalRadBtn;
            }

            set
            {
                m_allStepGoalRadBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_dailyStepGoalRadBtn
        /// </summary>
        public RadioButton DailyStepGoalRadBtn
        {
            get
            {
                return m_dailyStepGoalRadBtn;
            }

            set
            {
                m_dailyStepGoalRadBtn = value;
            }
        }

        #endregion Property Accessors

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
            //inflate frage view
            InflateView(inflater, container, Resource.Layout.HistoryStepGoalView);
            //setup UI variables
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
            //return view
            return FragView;
        }

        /// <summary>
        /// Setup Ui varibles for this class
        /// </summary>
        private void SetupUIVariables()
        {
            //setup toolbar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Step Goal History";
            StepGoalListView = FragView.FindViewById<ListView>(Resource.Id.stepGoalLstView);
            //set up UI
            HourStepGoalRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.hourStepGoalRadBtn);
            DailyStepGoalRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.dailyStepGoalRadBtn);
            WeekStepGoalRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.weekStepGoalRadBtn);
            MonthStepGoalRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.monthStepGoalRadBtn);
            YearStepGoalRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.yearStepGoalRadBtn);
            AllStepGoalRadBtn = FragView.FindViewById<RadioButton>(Resource.Id.allStepGoalHistoryRadBtn);
            AllStepGoalRadBtn.Checked = true;
            HourStepGoalRadBtn.Checked = false;
            DailyStepGoalRadBtn.Checked = false;
            WeekStepGoalRadBtn.Checked = false;
            MonthStepGoalRadBtn.Checked = false;
            YearStepGoalRadBtn.Checked = false;
        }

        /// <summary>
        /// Setup utility vars for this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //Update list view
            UpdateListView(GlobalUtilities.StepGoalManager.GetAllStepGoals().ToList());
        }

        /// <summary>
        /// Handle Events
        /// </summary>
        private void EventHandlers()
        {
            //list item clicked
            StepGoalListView.ItemClick += (s, e) =>
            {
                //new step goal creation frag
                var frag = new CreationStepGoalFragment();
                //selected step goal
                StepGoal selectedStepGoal = ListViewHelper.List.ElementAt<StepGoal>(e.Position);
                //serialize goal
                string output = JsonConvert.SerializeObject(selectedStepGoal);
                Bundle bundle = new Bundle();
                bundle.PutString("StepGoal", output);
                //set arguments
                frag.Arguments = bundle;
                //load fragment
                LoadFragment(frag);
            };


            //hour rad btn clicked
            HourStepGoalRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                HourStepGoalRadBtn.Checked = true;
                DailyStepGoalRadBtn.Checked = false;
                WeekStepGoalRadBtn.Checked = false;
                MonthStepGoalRadBtn.Checked = false;
                YearStepGoalRadBtn.Checked = false;
                AllStepGoalRadBtn.Checked = false;
                //get step goals
                var houryStepGoals = GlobalUtilities.StepGoalManager.GetAllHourlyStepGoals().ToList();
                //update list view
                UpdateListView(houryStepGoals);
            };
            //daily rad btn clicked
            DailyStepGoalRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                HourStepGoalRadBtn.Checked = false;
                DailyStepGoalRadBtn.Checked = true;
                WeekStepGoalRadBtn.Checked = false;
                MonthStepGoalRadBtn.Checked = false;
                YearStepGoalRadBtn.Checked = false;
                AllStepGoalRadBtn.Checked = false;
                //get step goals
                var dailyStepGoals = GlobalUtilities.StepGoalManager.GetAllDailyStepGoals().ToList();
                //update list view
                UpdateListView(dailyStepGoals);
            };
            //week rad btn clicked
            WeekStepGoalRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                HourStepGoalRadBtn.Checked = false;
                DailyStepGoalRadBtn.Checked = false;
                WeekStepGoalRadBtn.Checked = true;
                MonthStepGoalRadBtn.Checked = false;
                YearStepGoalRadBtn.Checked = false;
                AllStepGoalRadBtn.Checked = false;
                //get step goals
                var weeklyStepGoals = GlobalUtilities.StepGoalManager.GetAllWeeklyStepGoals().ToList();
                //update list view
                UpdateListView(weeklyStepGoals);
            };
            //month rad btn clicked
            MonthStepGoalRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                HourStepGoalRadBtn.Checked = false;
                DailyStepGoalRadBtn.Checked = false;
                WeekStepGoalRadBtn.Checked = false;
                MonthStepGoalRadBtn.Checked = true;
                YearStepGoalRadBtn.Checked = false;
                AllStepGoalRadBtn.Checked = false;
                //get step goals
                var monthStepGoals = GlobalUtilities.StepGoalManager.GetAllMonthlyStepGoals().ToList();
                //update list view
                UpdateListView(monthStepGoals);
            };
            //year rad btn clicked
            YearStepGoalRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                HourStepGoalRadBtn.Checked = false;
                DailyStepGoalRadBtn.Checked = false;
                WeekStepGoalRadBtn.Checked = false;
                MonthStepGoalRadBtn.Checked = false;
                YearStepGoalRadBtn.Checked = true;
                AllStepGoalRadBtn.Checked = false;
                //get step goals
                var yearStepGoals = GlobalUtilities.StepGoalManager.GetAllYearlyStepGoals().ToList();
                //update list view
                UpdateListView(yearStepGoals);
            };
            //all rad btn clicked
            AllStepGoalRadBtn.Click += (s, e) =>
            {
                //toggle rad btns
                HourStepGoalRadBtn.Checked = false;
                DailyStepGoalRadBtn.Checked = false;
                WeekStepGoalRadBtn.Checked = false;
                MonthStepGoalRadBtn.Checked = false;
                YearStepGoalRadBtn.Checked = false;
                AllStepGoalRadBtn.Checked = true;
                //get step goals
                var allStepGoals = GlobalUtilities.StepGoalManager.GetAllStepGoals().ToList();
                //update list view
                UpdateListView(allStepGoals);
            };
        }

        /// <summary>
        /// Updates list view
        /// </summary>
        /// <param name="stepGoals"> list of step goals to fill in view</param>
        private void UpdateListView(List<StepGoal> stepGoals)
        {
            //set list view helper
            ListViewHelper = new ListViewHelper<StepGoal>(StepGoalListView, stepGoals);

            //Populatings Weight Entries from database
            ListViewHelper.PopulateListFromDatabase();

            //Display Weight Entries in List View
            StepGoalListView = ListViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
            StepGoalListView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
        }

        #endregion //Methods

    }
}