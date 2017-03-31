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
    public class DashoardSummaryFragment : BaseFragment
    {
        #region Fields

        //UI vars
        private TextView m_userNameTxtView;
        private TextView m_ageTxtView;
        private TextView m_weightTxtView;
        private TextView m_heightTxtView;
        private TextView m_bodyMassIndexTxtView;
        private TextView m_heartRateTxtView;
        private TextView m_stepsTakenTodayTxtView;
        private TextView m_sleepLengthTxtView;
        private TextView m_sleepQualityTxtView;
        private TextView m_lastFallDateTxtView;
        private Button m_sendForHelpBtn;

        //utility vars
        private User m_appUser;
        private WeightEntry m_latestWeightEntry;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_userNameTxtView
        /// </summary>
        public TextView UserNameTxtView
        {
            get
            {
                return m_userNameTxtView;
            }

            set
            {
                m_userNameTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_ageTxtView
        /// </summary>
        public TextView AgeTxtView
        {
            get
            {
                return m_ageTxtView;
            }

            set
            {
                m_ageTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weightTxtView
        /// </summary>
        public TextView WeightTxtView
        {
            get
            {
                return m_weightTxtView;
            }

            set
            {
                m_weightTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_heightTxtView
        /// </summary>
        public TextView HeightTxtView
        {
            get
            {
                return m_heightTxtView;
            }

            set
            {
                m_heightTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_bodyMassIndexTxtView
        /// </summary>
        public TextView BodyMassIndexTxtView
        {
            get
            {
                return m_bodyMassIndexTxtView;
            }

            set
            {
                m_bodyMassIndexTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_heartRateTxtView
        /// </summary>
        public TextView HeartRateTxtView
        {
            get
            {
                return m_heartRateTxtView;
            }

            set
            {
                m_heartRateTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stepsTakenTodayTxtView
        /// </summary>
        public TextView StepsTakenTodayTxtView
        {
            get
            {
                return m_stepsTakenTodayTxtView;
            }

            set
            {
                m_stepsTakenTodayTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_appUser
        /// </summary>
        public User AppUser
        {
            get
            {
                return m_appUser;
            }

            set
            {
                m_appUser = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_latestWeightEntry
        /// </summary>
        public WeightEntry LatestWeightEntry
        {
            get
            {
                return m_latestWeightEntry;
            }

            set
            {
                m_latestWeightEntry = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepQualityTxtView
        /// </summary>
        public TextView SleepQualityTxtView
        {
            get
            {
                return m_sleepQualityTxtView;
            }

            set
            {
                m_sleepQualityTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepLengthTxtView
        /// </summary>
        public TextView SleepLengthTxtView
        {
            get
            {
                return m_sleepLengthTxtView;
            }

            set
            {
                m_sleepLengthTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_lastFallDateTxtView
        /// </summary>
        public TextView LastFallDateTxtView
        {
            get
            {
                return m_lastFallDateTxtView;
            }

            set
            {
                m_lastFallDateTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sendForHelpBtn
        /// </summary>
        public Button SendForHelpBtn
        {
            get
            {
                return m_sendForHelpBtn;
            }

            set
            {
                m_sendForHelpBtn = value;
            }
        }

        #endregion //Property Accessors

        #region Mehtods

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //iflate fragment view
            InflateView(inflater, container, Resource.Layout.DashboardSummaryView);
            //set up UI variables
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
            //return view
            return FragView;
        }

        /// <summary>
        /// Setup UI variables for this class
        /// </summary>
        private void SetupUIVariables()
        {
            //setting up toolbar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Summary";
            Toolbar.Menu.Clear();
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            SetHasOptionsMenu(true);
            //setting up ui elements
            UserNameTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardUserNameTextView);
            AgeTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardUserAgeTxtView);
            WeightTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardUserWeightTxtVew);
            HeightTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardUserHeightTxtView);
            BodyMassIndexTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardUserBMITxtView);
            HeartRateTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardUserHearRateTxtView);
            StepsTakenTodayTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardStepsTodayTxtView);
            SleepLengthTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardSleepLengthTxtView);
            SleepQualityTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardSleepQualityTxtView);
            LastFallDateTxtView = FragView.FindViewById<TextView>(Resource.Id.DashboardLastFallTxtView);
            SendForHelpBtn = FragView.FindViewById<Button>(Resource.Id.sendForHelpBtn);
        }

        /// <summary>
        /// Setup utility variables for this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //if user exists
            if (GlobalUtilities.UserManager.UserExists())
            {
                //get user
                AppUser = GlobalUtilities.UserManager.GetUser();
            }
            //if weight entry exists
            if (GlobalUtilities.WeightManager.WeightEntryExists())
            {
                //set user weight to latest entry
                AppUser.Weight = GlobalUtilities.WeightManager.GetLatestWeightEntry().Weight;
            }
            //set text for fields
            UserNameTxtView.Text += " " + AppUser.Name;
            AgeTxtView.Text += " " + AppUser.Age;
            WeightTxtView.Text += " " + AppUser.Weight;
            HeightTxtView.Text += " " + AppUser.Height;
            //calculate users bmi
            var BMI = AppUser.CalculateBodyMassIndex();
            BodyMassIndexTxtView.Text += " " + BMI;
            //if user underweight
            if (BMI <= 18.5)
            {
                BodyMassIndexTxtView.Text += " (Underweight)";
            }
            //if user healthy
            if (BMI > 18.5 && BMI <= 24.9)
            {
                BodyMassIndexTxtView.Text += " (Healthy)";
            }
            //if user is overweight
            if (BMI > 25 && BMI <= 29.9)
            {
                BodyMassIndexTxtView.Text += " (Overweight)";
            }
            if (BMI > 30)
            {
                BodyMassIndexTxtView.Text += " (Obese)";
            }

            HeartRateTxtView.Text += " " + AppUser.HeartRate;
            //if step entry exists
            if (GlobalUtilities.StepManager.StepEntryExists())
            {
                StepsTakenTodayTxtView.Text += " " + GlobalUtilities.StepManager.GetTotalStepsForCurrentDate();
            }
            //if sleep entry exists
            if (GlobalUtilities.SleepManager.SleepEntryExists())
            {
                var latestSleepEntry = GlobalUtilities.SleepManager.GetLatestSleepEntry();
                SleepQualityTxtView.Text += " " + GlobalUtilities.SleepManager.SleepQualityToString(latestSleepEntry);
                SleepLengthTxtView.Text += " " + latestSleepEntry.SleepLength.Hours + ":" + latestSleepEntry.SleepLength.Minutes;
            }
            //if fall exists
            if (GlobalUtilities.FallManager.FallExists())
            {
                LastFallDateTxtView.Text += " " + GlobalUtilities.FallManager.GetLatestFall().ToString();
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            //inflate toolbar menu
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_edit:
                    //load user creation fragment
                    LoadFragment(new CreationUserFragment());
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Handle Events
        /// </summary>
        private void EventHandlers()
        {
            //send help clicked
            SendForHelpBtn.Click += (s, e) =>
             {
                 //set context for message manager
                 GlobalUtilities.EmergencyMessageManager.Context = Activity;
                 //get all contacts
                 List<EmergencyContact> emergencyContacts = GlobalUtilities.EmergencyContactManager.GetAllEmergencyContacts().ToList<EmergencyContact>();
                 //get message
                 EmergencyMessage message = GlobalUtilities.EmergencyMessageManager.GetEmergencyMessage();
                 //send message to contacts
                 GlobalUtilities.EmergencyMessageManager.SendEmergencyMessageToList(emergencyContacts, message);
             };
        }

        #endregion //Methods


    }
}