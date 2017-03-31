using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Fragments;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using System;
using System.Collections.Generic;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Activities
{
    [Activity(Label = "MainTestingMenuActivity", MainLauncher = true)]
    public class NaivgationActivity : AppCompatActivity
    {
        #region Fields


        private DrawerLayout m_drawerLayout;
        private NavigationView m_navigationView;
        private SupprtToolbar m_toolBar;
        private ExpandableListView m_expandableList;

        private int m_groupCount;
        List<string> listDataHeader;
        Dictionary<string, List<String>> listDataChild;
        ExpandableListAdapter menuAdapter;


        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_drawerLayout
        /// </summary>
        public DrawerLayout DrawerLayout
        {
            get
            {
                return m_drawerLayout;
            }

            set
            {
                m_drawerLayout = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_navigationView
        /// </summary>
        public NavigationView NavigationView
        {
            get
            {
                return m_navigationView;
            }

            set
            {
                m_navigationView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_toolBar
        /// </summary>
        public SupprtToolbar ToolBar
        {
            get
            {
                return m_toolBar;
            }

            set
            {
                m_toolBar = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_groupCount
        /// </summary>
        public ExpandableListView ExpandableList
        {
            get
            {
                return m_expandableList;
            }

            set
            {
                m_expandableList = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_groupCount
        /// </summary>
        public int GroupCount
        {
            get
            {
                return m_groupCount;
            }

            set
            {
                m_groupCount = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //setting layout
            SetContentView(Resource.Layout.NavigationView);
            //setup ui
            SetupUIVariables();
            //setup utilities
            SetupUtilityVariables();
            //FillDBWithSampleData();
            //hande events
            EventHandelers();
        }

        /// <summary>
        /// Setup variables that will be used throughout this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //prepare list data
            PrepareListData();
            //setting exandable list adapter
            menuAdapter = new ExpandableListAdapter(this, listDataHeader, listDataChild, ExpandableList);
            //setting group count
            GroupCount = menuAdapter.GroupCount;
            //set adapter for expandable list
            ExpandableList.SetAdapter(menuAdapter);
            //creating new fragment transaction
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            //set the fragment transaction
            fragmentTransaction.SetTransition(FragmentTransit.FragmentFade);

            //if a user exists
            if (GlobalUtilities.UserManager.UserExists())
            {
                //handle fragment transaction
                HandleFragmentTransaction(new DashoardSummaryFragment());
            }
            else
            {
                //handle fragment transaction
                HandleFragmentTransaction(new CreationUserFragment());
            }
            //start sleep alarm service
            StartService(new Intent(this, typeof(SleepAlarmService)));
        }

        /// <summary>
        /// Setting up variables for use with this classes UI
        /// </summary>
        private void SetupUIVariables()
        {
            //setting up toolbar
            ToolBar = FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            ToolBar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            SetSupportActionBar(ToolBar);

            //Enable support action bar to display hamburger
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "My Summary";
            //setting up UI elements
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            NavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            ExpandableList = FindViewById<ExpandableListView>(Resource.Id.navigationmenu);

            //setting up expandable list design
            ExpandableList.SetBackgroundColor(Android.Graphics.Color.LightGray);
            ExpandableList.Divider.SetTint(Android.Graphics.Color.Black);
        }

        /// <summary>
        /// Handle all events
        /// </summary>
        private void EventHandelers()
        {
            //handling a group click in expandable list
            ExpandableList.GroupClick += (s, e) =>
              {
                  //if user exists
                  if (GlobalUtilities.UserManager.UserExists())
                  {
                      //if the group is expanded
                      if (ExpandableList.IsGroupExpanded(e.GroupPosition))
                      {
                          //collapse group
                          ExpandableList.CollapseGroup(e.GroupPosition);
                      }
                      //if the group is not expanded and its not the zero element
                      else if (!ExpandableList.IsGroupExpanded(e.GroupPosition) && e.GroupPosition != 0)
                      {
                          //itierate through list
                          for (int i = 0; i < GroupCount; i++)
                          {
                              //collapse group
                              ExpandableList.CollapseGroup(i);
                          }
                          //expand group at position
                          ExpandableList.ExpandGroup(e.GroupPosition);
                      }
                      //if the use clicked the zero element ("home")
                      else if (e.GroupPosition == 0)
                      {
                          //handle fragment transaction
                          HandleFragmentTransaction(new DashoardSummaryFragment());
                      }
                  }

              };

            //expandable child list item clicked
            ExpandableList.ChildClick += (s, e) =>
            {
                //if user exists
                if (GlobalUtilities.UserManager.UserExists())
                {
                    //switch for group clicked
                    switch (e.GroupPosition)
                    {
                        //fall management
                        case 1:
                            //switch for child clicked
                            switch (e.ChildPosition)
                            {

                                case 0:
                                    //fall detector
                                    HandleFragmentTransaction(new ServiceInteractionFallDetectionFragment());
                                    break;

                                case 1:
                                    //message configuration
                                    HandleFragmentTransaction(new CreationContactMessageFragment());
                                    break;

                                case 2:
                                    //contact selection
                                    HandleFragmentTransaction(new CreationEmergencyContactsListFragment());
                                    break;

                                case 3:
                                    //fall history
                                    HandleFragmentTransaction(new HistoryDetectedFallsFragment());
                                    break;
                            }
                            break;
                        //my steps
                        case 2:
                            //switch for child clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //step tracker
                                    HandleFragmentTransaction(new ServiceInteractionStepCounterFragment());
                                    break;

                                case 1:
                                    //step history
                                    HandleFragmentTransaction(new HistoryStepFragment());
                                    break;
                            }
                            break;
                        //step goals
                        case 3:
                            //switch for child clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //step goal creation
                                    HandleFragmentTransaction(new CreationStepGoalFragment());
                                    break;

                                case 1:
                                    //step goal history
                                    HandleFragmentTransaction(new HistoryStepGoalFragment());
                                    break;
                            }
                            break;
                        //my weight
                        case 4:
                            //switch for child clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //weight entry
                                    HandleFragmentTransaction(new CreationWeightFragment());
                                    break;

                                case 1:
                                    //weight history
                                    HandleFragmentTransaction(new HistoryWeightFragment());
                                    break;
                            }
                            break;
                        //my sleep
                        case 5:
                            //switch for child clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //sleep tracker
                                    HandleFragmentTransaction(new ServiceInteractionSleepTrackerFragment());
                                    break;

                                case 1:
                                    //sleep alarms
                                    HandleFragmentTransaction(new HistorySleepAlarmFragment());
                                    break;
                                case 2:
                                    //sleep history
                                    HandleFragmentTransaction(new HistorySleepFragment());
                                    break;
                            }
                            break;
                        //my heart rate
                        case 6:
                            //switch for child clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //heart rate monitor
                                    StartActivity(typeof(HeartRateMonitorActivity));
                                    break;
                                case 1:
                                    //hear rate history
                                    Toast.MakeText(this, "Feature In Development", ToastLength.Short).Show();
                                    break;
                            }
                            break;
                        //my ativities
                        case 7:
                            //witch for child clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //detected ativities
                                    HandleFragmentTransaction(new DetectedActivitiesFragment());
                                    break;
                            }
                            break;
                        //calibration 
                        case 8:
                            //switch for chid clicked
                            switch (e.ChildPosition)
                            {
                                case 0:
                                    //calibration
                                    HandleFragmentTransaction(new ServiceInteractionCalibrationFragment());
                                    break;
                            }
                            break;
                    }
                    //close navigation drawer
                    DrawerLayout.CloseDrawers();
                }
            };
        }

        /// <summary>
        /// Handles clicking of navigation menu
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //switch for item id
            switch (item.ItemId)
            {
                //home selected
                case Android.Resource.Id.Home:
                    //if drawer is open
                    if (DrawerLayout.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start) == true)
                    {
                        //close drawer
                        DrawerLayout.CloseDrawers();
                    }
                    else
                    {
                        //open drawer
                        DrawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    }
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Preparing navigation drawer list
        /// </summary>
        private void PrepareListData()
        {
            //headings of nav drawer
            listDataHeader = new List<string>();
            //children of nav drawer
            listDataChild = new Dictionary<string, List<String>>();
            //setting headings 
            string item0 = "Home";
            listDataHeader.Add(item0);
            string item1 = "Fall Management";
            listDataHeader.Add(item1);

            string item3 = "My Steps";
            listDataHeader.Add(item3);

            string item4 = "My Step Goals";
            listDataHeader.Add(item4);

            string item5 = "My Weight";
            listDataHeader.Add(item5);

            string item6 = "My Sleep";
            listDataHeader.Add(item6);

            string item7 = "My Heart Rate";
            listDataHeader.Add(item7);

            string item8 = "My Activites";
            listDataHeader.Add(item8);

            string item9 = "Calibration";
            listDataHeader.Add(item9);

            // Adding child data
            List<String> heading2 = new List<string>();
            heading2.Add("Fall Detector");
            heading2.Add("Edit Emergency Message");
            heading2.Add("Set Emergency Contacts");
            heading2.Add("My Previous Falls");

            List<String> heading3 = new List<String>();
            heading3.Add("Step Tracker");
            heading3.Add("Step History");

            List<String> heading4 = new List<String>();
            heading4.Add("New Step Goal");
            heading4.Add("Step Goal History");

            List<String> heading5 = new List<String>();
            heading5.Add("New Weight Entry");
            heading5.Add("Weight History");

            List<String> heading6 = new List<String>();
            heading6.Add("Sleep Tracking");
            heading6.Add("Alarms");
            heading6.Add("Sleep History");

            List<String> heading7 = new List<String>();
            heading7.Add("Check My Heart Rate");
            heading7.Add("Heart Rate History");

            List<String> heading8 = new List<String>();
            heading8.Add("My Detected Activities");

            List<String> heading9 = new List<String>();
            heading9.Add("Callibrate Fall Detector");


            // Header, Child data
            listDataChild.Add(listDataHeader[0], null);
            listDataChild.Add(listDataHeader[1], heading2);
            listDataChild.Add(listDataHeader[2], heading3);
            listDataChild.Add(listDataHeader[3], heading4);
            listDataChild.Add(listDataHeader[4], heading5);
            listDataChild.Add(listDataHeader[5], heading6);
            listDataChild.Add(listDataHeader[6], heading7);
            listDataChild.Add(listDataHeader[7], heading8);
            listDataChild.Add(listDataHeader[8], heading9);
        }

        /// <summary>
        /// Handles fragment transactions
        /// </summary>
        /// <param name="frag"> The fragment to replace the current fragment</param>
        public void HandleFragmentTransaction(Fragment frag)
        {
            //create new fragment transaction
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            //set fragment transition
            fragmentTransaction.SetTransition(FragmentTransit.FragmentFade);
            //replace fragment currently in layout frame
            fragmentTransaction.Replace(Resource.Id.drawer_frame, frag);
            //commit transaction
            fragmentTransaction.Commit();
            //close drawer
            DrawerLayout.CloseDrawers();
        }

        public void FillDBWithSampleData()
        {
            GlobalUtilities.WeightManager.DeleteAllWeightEntries();
            GlobalUtilities.StepManager.DeleteAllStepEntries();
            GlobalUtilities.SleepManager.DeleteAllSleepEntries();

            var weightEntries = new List<WeightEntry>();
            var stepEntries = new List<StepEntry>();
            var sleepEntries = new List<SleepEntry>();
            var fallEntries = new List<Fall>();

            Random random = new Random();

            //fill weight
            for (int i = 1; i <= 31; i++)
            {
                var weightVal = Math.Round(random.NextDouble() * (74 - 76) + 74, 2);
                DateTime d = new DateTime(2017, 1, i);
                weightEntries.Add(new WeightEntry(weightVal, d));
            }

            //fill weight
            for (int i = 1; i <= 28; i++)
            {
                var weightVal = Math.Round(random.NextDouble() * (73 - 75) + 73, 2);
                DateTime d = new DateTime(2017, 2, i);
                weightEntries.Add(new WeightEntry(weightVal, d));
            }

            //fill weight
            for (int i = 1; i <= 28; i++)
            {
                var weightVal = Math.Round(random.NextDouble() * (71 - 73) + 71, 2);
                DateTime d = new DateTime(2017, 3, i);
                weightEntries.Add(new WeightEntry(weightVal, d));
            }

            //fill steps
            for (int i = 1; i <= 31; i++)
            {
                for (int j = 9; j <= 23; j++)
                {
                    var stepAmount = random.Next(1, 2000);

                    DateTime d = new DateTime(2017, 1, i, j, 0, 0);
                    stepEntries.Add(new StepEntry((long)stepAmount, d, d.TimeOfDay));
                }
            }

            //fill steps
            for (int i = 1; i <= 28; i++)
            {
                for (int j = 9; j <= 23; j++)
                {
                    var stepAmount = random.Next(1, 2000);

                    DateTime d = new DateTime(2017, 2, i, j, 0, 0);
                    stepEntries.Add(new StepEntry((long)stepAmount, d, d.TimeOfDay));
                }
            }

            //fill steps
            for (int i = 1; i <= 29; i++)
            {
                for (int j = 9; j <= 23; j++)
                {
                    var stepAmount = random.Next(1, 2000);

                    DateTime d = new DateTime(2017, 3, i, j, 0, 0);
                    stepEntries.Add(new StepEntry((long)stepAmount, d, d.TimeOfDay));
                }
            }
            //fill sleepEntries
            for (int i = 1; i <= 31; i++)
            {
                var randHour = random.Next(6, 9);
                var randMin = random.Next(6, 60);
                var ranSec = random.Next(6, 60);
                TimeSpan timeSpan = new TimeSpan(randHour, randMin, ranSec);
                var sleepQuality = random.Next(0, 4);
                DateTime d = new DateTime(2017, 1, i);
                sleepEntries.Add(new SleepEntry(timeSpan, sleepQuality, d));
            }
            //fill sleepEntries
            for (int i = 1; i <= 28; i++)
            {
                var randHour = random.Next(6, 9);
                var randMin = random.Next(6, 60);
                var ranSec = random.Next(6, 60);
                TimeSpan timeSpan = new TimeSpan(randHour, randMin, ranSec);
                var sleepQuality = random.Next(0, 4);
                DateTime d = new DateTime(2017, 2, i);
                sleepEntries.Add(new SleepEntry(timeSpan, sleepQuality, d));
            }
            //fill sleepEntries
            for (int i = 1; i <= 28; i++)
            {
                var randHour = random.Next(6, 9);
                var randMin = random.Next(6, 60);
                var ranSec = random.Next(6, 60);
                TimeSpan timeSpan = new TimeSpan(randHour, randMin, ranSec);
                var sleepQuality = random.Next(0, 4);
                DateTime d = new DateTime(2017, 3, i);
                sleepEntries.Add(new SleepEntry(timeSpan, sleepQuality, d));
            }

            foreach (var w in weightEntries)
            {
                GlobalUtilities.WeightManager.SaveWeightEntry(w);
            }
            foreach (var s in stepEntries)
            {
                GlobalUtilities.StepManager.SaveStepEntry(s);
            }
            foreach (var w in sleepEntries)
            {
                GlobalUtilities.SleepManager.SaveSleepEntry(w);
            }



        }
        #endregion //Methods
    }
}