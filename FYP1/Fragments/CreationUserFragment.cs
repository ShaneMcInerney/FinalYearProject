using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class CreationUserFragment : BaseFragment
    {

        #region Fields

        private EditText m_userNameEditTxt;
        private EditText m_userAgeEditTxt;
        private EditText m_userDOBEditTxt;
        private EditText m_userWeightEditTxt;
        private EditText m_userHeightEditTxt;
        private Spinner m_genderSpinner;
        private Button m_createUserBtn;

        private User m_appUser;
        private bool m_inEditMode;
        private string m_stringStream;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_userNameEditTxt
        /// </summary>
        public EditText UserNameEditTxt
        {
            get
            {
                return m_userNameEditTxt;
            }

            set
            {
                m_userNameEditTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_userAgeEditTxt
        /// </summary>
        public EditText UserAgeEditTxt
        {
            get
            {
                return m_userAgeEditTxt;
            }

            set
            {
                m_userAgeEditTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_userDOBEditTxt
        /// </summary>
        public EditText UserDOBEditTxt
        {
            get
            {
                return m_userDOBEditTxt;
            }

            set
            {
                m_userDOBEditTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_userWeightEditTxt
        /// </summary>
        public EditText UserWeightEditTxt
        {
            get
            {
                return m_userWeightEditTxt;
            }

            set
            {
                m_userWeightEditTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_userHeightEditTxt
        /// </summary>
        public EditText UserHeightEditTxt
        {
            get
            {
                return m_userHeightEditTxt;
            }

            set
            {
                m_userHeightEditTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_createUserBtn
        /// </summary>
        public Button CreateUserBtn
        {
            get
            {
                return m_createUserBtn;
            }

            set
            {
                m_createUserBtn = value;
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
        /// Gets/Sets m_genderSpinner
        /// </summary>
        public Spinner GenderSpinner
        {
            get
            {
                return m_genderSpinner;
            }

            set
            {
                m_genderSpinner = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_inEditMode
        /// </summary>
        public bool InEditMode
        {
            get
            {
                return m_inEditMode;
            }

            set
            {
                m_inEditMode = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stringStream
        /// </summary>
        public string StringStream
        {
            get
            {
                return m_stringStream;
            }

            set
            {
                m_stringStream = value;
            }
        }

        #endregion //Property Accessors

        #region Methods




        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate frage view          
            InflateView(inflater, container, Resource.Layout.CreationUserView);
            //setup variables
            SetupUtilityVariables();
            //set up UI vars
            SetupUIVariables();
            //handle events
            EventHandlers();
            //return frag view
            return FragView;
        }

        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "User Configuration";

            this.UserNameEditTxt = FragView.FindViewById<EditText>(Resource.Id.userNameEditTxt);
            this.UserAgeEditTxt = FragView.FindViewById<EditText>(Resource.Id.userAgeEditTxt);
            this.UserDOBEditTxt = FragView.FindViewById<EditText>(Resource.Id.userDOBEditTxt);

            this.UserWeightEditTxt = FragView.FindViewById<EditText>(Resource.Id.userWeightEditTxt);
            this.UserHeightEditTxt = FragView.FindViewById<EditText>(Resource.Id.userHeightEditTxt);
            this.GenderSpinner = FragView.FindViewById<Spinner>(Resource.Id.userGenderSelectorSpinner);
            this.CreateUserBtn = FragView.FindViewById<Button>(Resource.Id.createNewUserBtn);

            var adapter = ArrayAdapter.CreateFromResource(Activity.ApplicationContext, Resource.Array.genders, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            GenderSpinner.Adapter = adapter;

            if (InEditMode)
            {
                UserNameEditTxt.Text = AppUser.Name;
                UserNameEditTxt.Hint = "";
                UserAgeEditTxt.Text = AppUser.Age.ToString();
                UserAgeEditTxt.Hint = "";
                UserDOBEditTxt.Text = AppUser.Dob.ToString();
                UserDOBEditTxt.Hint = "";
                UserWeightEditTxt.Text = GlobalUtilities.WeightManager.GetLatestWeightEntry().Weight.ToString();
                UserWeightEditTxt.Hint = "";
                UserHeightEditTxt.Text = AppUser.Height.ToString();
                UserHeightEditTxt.Hint = "";
                GetGenderFromSpinner(AppUser.GenderType.ToString());
                CreateUserBtn.Text = "Update";
            }
            else
            {
                this.AppUser = new User();
            }
        }

        /// <summary>
        /// Set up variables for this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //if user exists
            if (GlobalUtilities.UserManager.UserExists())
            {
                //edit mode equals true
                InEditMode = true;
                //set app user
                AppUser = GlobalUtilities.UserManager.GetUser();
            }
            else
            {
                //edit mode equals false
                InEditMode = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void EventHandlers()
        {
            //create user btn clicked
            CreateUserBtn.Click += (s, e) =>
            {
                //if all fields filled out
                if (CheckAllFieldsFilledOut())
                {
                    //set user attributes
                    AppUser.Name = UserNameEditTxt.Text;
                    AppUser.Age = Int32.Parse(UserAgeEditTxt.Text);
                    AppUser.Dob = DateTime.Parse(UserDOBEditTxt.Text);
                    AppUser.Weight = Double.Parse(UserWeightEditTxt.Text);
                    AppUser.Height = Double.Parse(UserHeightEditTxt.Text);
                    AppUser.GenderType = GetGenderFromSpinner(GenderSpinner.SelectedItem.ToString());
                    AppUser.StrideLength = AppUser.CalculateStrideLength(AppUser.GenderType);
                    //delete user
                    GlobalUtilities.UserManager.DeleteUser();
                    //save user
                    GlobalUtilities.UserManager.SaveUser(AppUser);
                    //save weight entry
                    GlobalUtilities.WeightManager.SaveWeightEntry(new WeightEntry(AppUser.Weight, DateTime.Now));
                    //load dashboard fragment
                    LoadFragment(new DashoardSummaryFragment());
                }
            };

            //user dob text fied clicked
            UserDOBEditTxt.Click += (s, e) =>
             {
                 //new instance of date picker fragment
                 DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                 {
                     //set dob text
                     UserDOBEditTxt.Text = time.ToLongDateString();
                 });
                 //show fragment
                 frag.Show(FragmentManager, DatePickerFragment.TAG);
             };


        }

        /// <summary>
        /// Check all fields in view are filled out
        /// </summary>
        /// <returns>bool indicating whether all fields are filled out</returns>
        private bool CheckAllFieldsFilledOut()
        {
            //checks if empty or whitespace
            if (string.IsNullOrWhiteSpace(UserNameEditTxt.Text) || string.IsNullOrEmpty(UserNameEditTxt.Text))
            {
                //toast user
                Toast.MakeText(Activity.ApplicationContext, "Please Enter Your Name", ToastLength.Short).Show();
                return false;
            }
            //checks if empty or whitespace
            if (string.IsNullOrWhiteSpace(UserAgeEditTxt.Text) || string.IsNullOrEmpty(UserAgeEditTxt.Text))
            {
                //toast user
                Toast.MakeText(Activity.ApplicationContext, "Please Enter Your Age", ToastLength.Short).Show();
                return false;
            }
            //checks if empty or whitespace
            if (string.IsNullOrWhiteSpace(UserDOBEditTxt.Text) || string.IsNullOrEmpty(UserDOBEditTxt.Text))
            {
                //toast user
                Toast.MakeText(Activity.ApplicationContext, "Please Enter Your Date of Birth", ToastLength.Short).Show();
                return false;
            }
            //checks if empty or whitespace
            if (string.IsNullOrWhiteSpace(UserWeightEditTxt.Text) || string.IsNullOrEmpty(UserWeightEditTxt.Text))
            {
                //toast user
                Toast.MakeText(Activity.ApplicationContext, "Please Enter Your Weight", ToastLength.Short).Show();
                return false;
            }
            //checks if empty or whitespace
            if (string.IsNullOrWhiteSpace(UserHeightEditTxt.Text) || string.IsNullOrEmpty(UserHeightEditTxt.Text))
            {
                //toast user
                Toast.MakeText(Activity.ApplicationContext, "Please Enter Your Height", ToastLength.Short).Show();
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retrieve user's gender from spinner
        /// </summary>
        /// <param name="spinnerSelection">selected item in spinner</param>
        /// <returns>Gender of the user</returns>
        private Gender GetGenderFromSpinner(string spinnerSelection)
        {
            //if male
            if (spinnerSelection.ToLower() == "male")
            {
                return Gender.Male;
            }
            else
            {
                return Gender.Female;
            }
        }

        #endregion //Methods

    }
}