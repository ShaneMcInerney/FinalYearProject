using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using System;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class CreationWeightFragment : BaseFragment
    {

        #region Fields

        //layout vars
        private EditText m_weightField;
        private TextView m_dateTextView;
        private Button m_weightSubmissionBtn;
        private Button m_deleteWeightEntryBtn;

        //utility vars
        private Double m_weightVal;
        private DateTime m_dateVal;
        private InputMethodManager m_inputManager;
        private bool m_inEditMode;
        private WeightEntry m_weightEntryToEdit;
        private string m_stringStream;
        private User m_appUser;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_weightField
        /// </summary>
        public EditText WeightField
        {
            get
            {
                return m_weightField;
            }

            set
            {
                m_weightField = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_dateTextView
        /// </summary>
        public TextView DateTextView
        {
            get
            {
                return m_dateTextView;
            }

            set
            {
                m_dateTextView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weightSubmissionBtn
        /// </summary>
        public Button WeightSubmissionBtn
        {
            get
            {
                return m_weightSubmissionBtn;
            }

            set
            {
                m_weightSubmissionBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weightVal
        /// </summary>
        public double WeightVal
        {
            get
            {
                return m_weightVal;
            }

            set
            {
                m_weightVal = value;
            }
        }

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public DateTime DateVal
        {
            get
            {
                return m_dateVal;
            }

            set
            {
                m_dateVal = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_inputManager
        /// </summary>
        public InputMethodManager InputManager
        {
            get
            {
                return m_inputManager;
            }

            set
            {
                m_inputManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_deleteWeightEntryBtn
        /// </summary>
        public Button DeleteWeightEntryBtn
        {
            get
            {
                return m_deleteWeightEntryBtn;
            }

            set
            {
                m_deleteWeightEntryBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weightEntryToEdit
        /// </summary>
        public WeightEntry WeightEntryToEdit
        {
            get
            {
                return m_weightEntryToEdit;
            }

            set
            {
                m_weightEntryToEdit = value;
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

        #endregion //Property Accessors

        #region Methods

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate fragment layut
            InflateView(inflater, container, Resource.Layout.CreationWeightView);
            //if arguments included
            if (this.Arguments != null)
            {
                Bundle b = this.Arguments;
                //if has weiht entry in argument
                if (b.ContainsKey("WeightEntry"))
                {
                    //set edit mode t true
                    InEditMode = true;
                    //deserialize weight entry
                    StringStream = b.GetString("WeightEntry");

                    WeightEntryToEdit = JsonConvert.DeserializeObject<WeightEntry>(StringStream);
                    //get user
                    AppUser = GlobalUtilities.UserManager.GetUser();
                }
                else
                {
                    //set edit mode false
                    InEditMode = false;
                }
            }
            //setup UI Variables
            SetupUIVariables();
            //handle events
            EventHandlers();
            //return fragment
            return FragView;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "New Weight Entry";

            //Initializing vars
            WeightField = FragView.FindViewById<EditText>(Resource.Id.weightEntryEditText);
            DateTextView = FragView.FindViewById<TextView>(Resource.Id.weightEntryDateTxtView);
            WeightSubmissionBtn = FragView.FindViewById<Button>(Resource.Id.weightEntrySubmissionBtn);
            DeleteWeightEntryBtn = FragView.FindViewById<Button>(Resource.Id.deleteWeightEntryBtn);

            //Create your application here
            InputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);

            //if in edit mode
            if (InEditMode)
            {
                //set title
                Toolbar.Title = "Edit Weight Entry";
                //set field text
                WeightField.Text = WeightEntryToEdit.Weight.ToString();
                //set field text
                DateTextView.Text = WeightEntryToEdit.Date.ToString();
                //set button text
                WeightSubmissionBtn.Text = "Update";
                //enable button
                DeleteWeightEntryBtn.Enabled = true;
                //create alert dialog
                AlertBuilder = new AlertDialog.Builder(Activity);
            }
            else
            {
                DeleteWeightEntryBtn.Enabled = false;
                //defaulting date to current date
                DateTextView.Text = DateTime.Now.ToString();
            }
            //convert edit text to date 
            DateVal = Convert.ToDateTime(DateTextView.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        private void EventHandlers()
        {
            //on weight entry text changed
            WeightField.AfterTextChanged += (s, e) =>
            {
                try
                {
                    //parce weight from text field
                    WeightVal = Double.Parse(WeightField.Text);

                }
                catch (Exception ex)
                {
                    //toast user
                    Toast.MakeText(Activity.ApplicationContext, "Invalid Value Entered", ToastLength.Short).Show();
                }
            };

            //on date text view changed
            DateTextView.TextChanged += (s, e) =>
            {
                //date vaule from text field
                DateVal = Convert.ToDateTime(DateTextView.Text);
            };

            //on submit button click
            WeightSubmissionBtn.Click += (s, e) =>
            {
                //if any fields not filled out
                if (string.IsNullOrEmpty(DateTextView.Text) || string.IsNullOrWhiteSpace(DateTextView.Text) || string.IsNullOrEmpty(WeightField.Text) || string.IsNullOrWhiteSpace(WeightField.Text))
                {
                    //toast user
                    Toast.MakeText(Activity.ApplicationContext, "Please Fill in All Fields", ToastLength.Short).Show();
                }
                else
                {
                    //if in edit mode
                    if (InEditMode)
                    {
                        //set weight date
                        WeightEntryToEdit.Date = DateVal;
                        //set weight value
                        WeightEntryToEdit.Weight = WeightVal;
                        //save wegiht entry
                        GlobalUtilities.WeightManager.SaveWeightEntry(WeightEntryToEdit);
                        //delete user
                        GlobalUtilities.UserManager.DeleteUser();
                        //set user weight
                        AppUser.Weight = WeightVal;
                        //save user
                        GlobalUtilities.UserManager.SaveUser(AppUser);
                    }
                    else
                    {
                        //weight 
                        WeightEntry weight = new WeightEntry(WeightVal, DateVal);
                        //saving new weight entry to database
                        GlobalUtilities.WeightManager.SaveWeightEntry(weight);
                    }
                    //toast user
                    Toast.MakeText(Activity.ApplicationContext, "Weight Entry Saved!", ToastLength.Short).Show();
                    //loasd dashboard
                    LoadFragment(new DashoardSummaryFragment());
                }

            };

            //on text view clicked
            DateTextView.Click += (s, e) =>
            {
                //new instance of date picker fragment
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    //set text of date
                    DateTextView.Text = time.ToShortDateString();
                });
                //show fragment
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            //on Select a date button click
            DeleteWeightEntryBtn.Click += (s, e) =>
            {
                //prompt
                AlertBuilder.SetTitle("Are You Sure");
                AlertBuilder.SetMessage("Are you sure you want to delete this weight entry?");
                //set positive button
                AlertBuilder.SetPositiveButton("Yes", (senderAlert, args) =>
                {
                    //delete weight entry
                    GlobalUtilities.WeightManager.DeleteWeightEntry(WeightEntryToEdit.ID);
                    //toast user
                    Toast.MakeText(Activity, "Weight Entry Deleted!", ToastLength.Short).Show();
                    //load wieght history
                    LoadFragment(new HistoryWeightFragment());
                });
                //set negative button
                AlertBuilder.SetNegativeButton("No", (senderAlert, args) =>
                {

                });
                //bulid alert
                Dialog dialog = AlertBuilder.Create();
                //show dialog
                dialog.Show();
            };
        }

        #endregion //Methods




    }
}