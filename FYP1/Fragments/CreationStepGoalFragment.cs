using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using System;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class CreationStepGoalFragment : BaseFragment
    {

        #region Fields

        private TextView m_stepGoalAmountTxtView;
        private Spinner m_stepGoalTypeSpinner;
        private Button m_deleteStepGoalBtn;
        private Button m_saveStepGoalBtn;

        private Int64 m_stepGoalAmount;
        private StepGoalType m_stepGoalPeriod = StepGoalType.Hourly;
        private bool m_inEditMode;
        private string m_stringStream;
        private StepGoal m_stepGoalToEdit;
        private bool m_itemSelected = false;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_stepGoalAmountTxtView
        /// </summary>
        public TextView StepGoalAmountTxtView
        {
            get
            {
                return m_stepGoalAmountTxtView;
            }

            set
            {
                m_stepGoalAmountTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stepGoalTypeSpinner
        /// </summary>
        public Spinner StepGoalTypeSpinner
        {
            get
            {
                return m_stepGoalTypeSpinner;
            }

            set
            {
                m_stepGoalTypeSpinner = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_saveStepGoalBtn
        /// </summary>
        public Button SaveStepGoalBtn
        {
            get
            {
                return m_saveStepGoalBtn;
            }

            set
            {
                m_saveStepGoalBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stepGoalAmount
        /// </summary>
        public long StepGoalAmount
        {
            get
            {
                return m_stepGoalAmount;
            }

            set
            {
                m_stepGoalAmount = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stepGoalPeriod
        /// </summary>
        public StepGoalType StepGoalPeriod
        {
            get
            {
                return m_stepGoalPeriod;
            }

            set
            {
                m_stepGoalPeriod = value;
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

        /// <summary>
        /// Gets/Sets m_stepGoalToEdit
        /// </summary>
        public StepGoal StepGoalToEdit
        {
            get
            {
                return m_stepGoalToEdit;
            }

            set
            {
                m_stepGoalToEdit = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_deleteStepGoalBtn
        /// </summary>
        public Button DeleteStepGoalBtn
        {
            get
            {
                return m_deleteStepGoalBtn;
            }

            set
            {
                m_deleteStepGoalBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_itemSelected
        /// </summary>
        public bool ItemSelected
        {
            get
            {
                return m_itemSelected;
            }

            set
            {
                m_itemSelected = value;
            }
        }



        #endregion //Property Accessors

        #region Methods

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflates fragment view
            InflateView(inflater, container, Resource.Layout.CreationStepGoalView);
            //if arguments passed in
            if (this.Arguments != null)
            {
                Bundle b = this.Arguments;
                //if step goal included
                if (b.ContainsKey("StepGoal"))
                {
                    //set edit mode true
                    InEditMode = true;
                    //deserialize step goal
                    StringStream = b.GetString("StepGoal");

                    StepGoalToEdit = JsonConvert.DeserializeObject<StepGoal>(StringStream);
                    //item selected true
                    ItemSelected = true;
                }
                else
                {
                    //set edit mode to false
                    InEditMode = false;
                }
            }
            //setup UI variables
            SetupUIVariables();
            //handle events
            EventHandlers();

            return FragView;
        }

        /// <summary>
        /// Sets up UI variables fr this class
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Create a New Step Goal";
            StepGoalAmountTxtView = FragView.FindViewById<TextView>(Resource.Id.stepCountGoalEditTxt);

            StepGoalTypeSpinner = FragView.FindViewById<Spinner>(Resource.Id.stepGoalTimeSpinner);

            SaveStepGoalBtn = FragView.FindViewById<Button>(Resource.Id.stepGoalSaveBtn);

            DeleteStepGoalBtn = FragView.FindViewById<Button>(Resource.Id.deleteStepGoalBtn);

            var adapter = ArrayAdapter.CreateFromResource(Activity.ApplicationContext, Resource.Array.stepGoals, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            StepGoalTypeSpinner.Adapter = adapter;
            AlertBuilder = new AlertDialog.Builder(Activity);

            if (InEditMode)
            {
                Toolbar.Title = "Edit Step Goal";
                StepGoalAmountTxtView.Text = StepGoalToEdit.Amount.ToString();

                StepGoalTypeSpinner.SetSelection((int)StepGoalToEdit.GoalType);

                DeleteStepGoalBtn.Enabled = true;
            }
            else
            {
                DeleteStepGoalBtn.Enabled = false;
            }
        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //spinner item selected
            StepGoalTypeSpinner.ItemSelected += (s, e) =>
            {
                //set item selected to true
                ItemSelected = true;
                //set spinner
                Spinner spinner = (Spinner)s;
                //set selected
                string selected = spinner.GetItemAtPosition(e.Position).ToString();
                //set goal perios
                StepGoalPeriod = StepGoalTypeSwitcher(selected);
            };

            //save button clicked
            SaveStepGoalBtn.Click += (s, e) =>
            {
                //if field is empty
                if (String.IsNullOrEmpty(StepGoalAmountTxtView.Text) == true || String.IsNullOrWhiteSpace(StepGoalAmountTxtView.Text) == true)
                {
                    //toast user
                    Toast.MakeText(Activity.ApplicationContext, "Please Fill Out All Fields!", ToastLength.Short).Show();
                }
                else
                {
                    //if item is not selected
                    if (ItemSelected == false)
                    {
                        //default to hourly step goal type
                        StepGoalPeriod = StepGoalType.Hourly;
                    }
                    //set step goal amount
                    StepGoalAmount = Convert.ToInt64(StepGoalAmountTxtView.Text);
                    //if edit mode
                    if (InEditMode)
                    {
                        //set step amount
                        StepGoalToEdit.Amount = StepGoalAmount;
                        //set goal type
                        StepGoalToEdit.GoalType = StepGoalPeriod;
                        //save step goal
                        GlobalUtilities.StepGoalManager.SaveStepGoal(StepGoalToEdit);
                    }
                    else
                    {
                        //save step goal
                        GlobalUtilities.StepGoalManager.SaveStepGoal(new StepGoal(StepGoalAmount, StepGoalPeriod, false, DateTime.Now));
                    }
                    //toast user
                    Toast.MakeText(Activity.ApplicationContext, "Goal Saved!", ToastLength.Short).Show();
                    //load dashboard
                    LoadFragment(new DashoardSummaryFragment());
                }
            };

            //delete button clicked
            DeleteStepGoalBtn.Click += (s, e) =>
            {
                //prompt
                AlertBuilder.SetTitle("Are You Sure");
                AlertBuilder.SetMessage("Are you sure you want to delete this step goal?");
                //set positive button
                AlertBuilder.SetPositiveButton("Yes", (senderAlert, args) =>
                {
                    //delete step gal
                    GlobalUtilities.StepGoalManager.DeleteStepGoal(StepGoalToEdit.ID);
                    //toast user
                    Toast.MakeText(Activity, "Step Goal Deleted!", ToastLength.Short).Show();
                    //load history step goal
                    LoadFragment(new HistoryStepGoalFragment());
                });
                //set negative button
                AlertBuilder.SetNegativeButton("No", (senderAlert, args) =>
                {

                });
                //build dialog
                Dialog dialog = AlertBuilder.Create();
                //show dialog
                dialog.Show();
            };
        }

        /// <summary>
        /// Switches step goal type based on input string
        /// </summary>
        /// <param name="goal">gaol string</param>
        /// <returns>a step goal type</returns>
        private StepGoalType StepGoalTypeSwitcher(string goal)
        {
            switch (goal)
            {
                case "Hourly":
                    return StepGoalType.Hourly;
                case "Daily":
                    return StepGoalType.Daily;
                case "Weekly":
                    return StepGoalType.Weekly;
                case "Monthly":
                    return StepGoalType.Monthly;
                case "Yearly":
                    return StepGoalType.Yearly;
                default:
                    throw new Exception("Invalid StepGoal Type");
            }
        }

        #endregion //Methods



    }
}