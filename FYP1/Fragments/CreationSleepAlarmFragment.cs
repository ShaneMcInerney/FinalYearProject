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
    public class CreationSleepAlarmFragment : BaseFragment
    {

        #region Fields

        private TextView m_daysForAlarmTxtView;
        private CheckBox m_monChkBox;
        private CheckBox m_tuesChkBox;
        private CheckBox m_wedChkBox;
        private CheckBox m_thursChkBox;
        private CheckBox m_friChkBox;
        private CheckBox m_satChkBox;
        private CheckBox m_sunChkBox;
        private TextView m_timeForAlarmTxtView;
        private Button m_deleteAlarmBtn;
        private CheckBox m_smartAlarmChkBox;
        private Button m_createAlarmBtn;


        private SleepAlarm m_alarmToCreate;
        private DateTime m_timeForAlarm;
        private bool m_inEditMode;
        private string m_stringStream;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_daysForAlarmTxtView
        /// </summary>
        public TextView DaysForAlarmTxtView
        {
            get
            {
                return m_daysForAlarmTxtView;
            }

            set
            {
                m_daysForAlarmTxtView = value;
            }
        }


        /// <summary>
        /// Gets/Sets m_timeForAlarmTxtView
        /// </summary>
        public TextView TimeForAlarmTxtView
        {
            get
            {
                return m_timeForAlarmTxtView;
            }

            set
            {
                m_timeForAlarmTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_deleteAlarmBtn
        /// </summary>
        public Button DeleteAlarmBtn
        {
            get
            {
                return m_deleteAlarmBtn;
            }

            set
            {
                m_deleteAlarmBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_createAlarmBtn
        /// </summary>
        public Button CreateAlarmBtn
        {
            get
            {
                return m_createAlarmBtn;
            }

            set
            {
                m_createAlarmBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_monChkBox
        /// </summary>
        public CheckBox MonChkBox
        {
            get
            {
                return m_monChkBox;
            }

            set
            {
                m_monChkBox = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_tuesChkBox
        /// </summary>
        public CheckBox TuesChkBox
        {
            get
            {
                return m_tuesChkBox;
            }

            set
            {
                m_tuesChkBox = value;
            }
        }

        /// <summary>  
        /// Gets/Sets m_wedChkBox
        /// </summary>
        public CheckBox WedChkBox
        {
            get
            {
                return m_wedChkBox;
            }

            set
            {
                m_wedChkBox = value;
            }
        }

        /// <summary> 
        /// Gets/Sets m_thursChkBox
        /// </summary>
        public CheckBox ThursChkBox
        {
            get
            {
                return m_thursChkBox;
            }

            set
            {
                m_thursChkBox = value;
            }
        }

        /// <summary> 
        /// Gets/Sets m_friChkBox
        /// </summary>
        public CheckBox FriChkBox
        {
            get
            {
                return m_friChkBox;
            }

            set
            {
                m_friChkBox = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_satChkBox
        /// </summary>
        public CheckBox SatChkBox
        {
            get
            {
                return m_satChkBox;
            }

            set
            {
                m_satChkBox = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sunChkBox
        /// </summary>
        public CheckBox SunChkBox
        {
            get
            {
                return m_sunChkBox;
            }

            set
            {
                m_sunChkBox = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_alarmToCreate
        /// </summary>
        public SleepAlarm AlarmToCreate
        {
            get
            {
                return m_alarmToCreate;
            }

            set
            {
                m_alarmToCreate = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_smartAlarmChkBox
        /// </summary>
        public CheckBox SmartAlarmChkBox
        {
            get
            {
                return m_smartAlarmChkBox;
            }

            set
            {
                m_smartAlarmChkBox = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_timeForAlarm
        /// </summary>
        public DateTime TimeForAlarm
        {
            get
            {
                return m_timeForAlarm;
            }

            set
            {
                m_timeForAlarm = value;
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

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflating view
            InflateView(inflater, container, Resource.Layout.CreationSleepAlarmView);
            //if items to be edited
            if (this.Arguments != null)
            {
                Bundle b = this.Arguments;
                //if argument to be edited
                if (b.ContainsKey("SleepAlarm"))
                {
                    //edit mode equals true
                    InEditMode = true;
                    //deserialize alarm
                    StringStream = b.GetString("SleepAlarm");

                    AlarmToCreate = JsonConvert.DeserializeObject<SleepAlarm>(StringStream);
                }
                else
                {
                    //set edit mode to false
                    InEditMode = false;
                }
            }
            //setup UI
            SetupUIVariables();
            //handle events
            EventHandlers();

            return FragView;
        }

        /// <summary>
        /// Set up UI variables for this class
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Create an Alarm";
            DaysForAlarmTxtView = FragView.FindViewById<TextView>(Resource.Id.AlarmDatesDisplayTxtView);
            MonChkBox = FragView.FindViewById<CheckBox>(Resource.Id.monChkBox);
            TuesChkBox = FragView.FindViewById<CheckBox>(Resource.Id.tuesChkBox);
            WedChkBox = FragView.FindViewById<CheckBox>(Resource.Id.wedChkBox);
            ThursChkBox = FragView.FindViewById<CheckBox>(Resource.Id.thursChkBox);
            FriChkBox = FragView.FindViewById<CheckBox>(Resource.Id.friChkBox);
            SatChkBox = FragView.FindViewById<CheckBox>(Resource.Id.satChkBox);
            SunChkBox = FragView.FindViewById<CheckBox>(Resource.Id.sunChkBox);

            TimeForAlarmTxtView = FragView.FindViewById<TextView>(Resource.Id.AlarmTimeTxtView);
            DeleteAlarmBtn = FragView.FindViewById<Button>(Resource.Id.DeleteAlarmBtn);
            SmartAlarmChkBox = FragView.FindViewById<CheckBox>(Resource.Id.smartAlarmChkBox);
            CreateAlarmBtn = FragView.FindViewById<Button>(Resource.Id.CreateNewSleepAlarmBtn);
            //if in edit mode
            if (InEditMode)
            {
                Toolbar.Title = "Edit an Alarm";
                MonChkBox.Checked = AlarmToCreate.OnMon;
                TuesChkBox.Checked = AlarmToCreate.OnTues;
                WedChkBox.Checked = AlarmToCreate.OnWed;
                ThursChkBox.Checked = AlarmToCreate.OnThurs;
                FriChkBox.Checked = AlarmToCreate.OnFri;
                SatChkBox.Checked = AlarmToCreate.OnSat;
                SunChkBox.Checked = AlarmToCreate.OnSun;


                TimeForAlarmTxtView.Text = AlarmToCreate.AlarmTime.TimeOfDay.ToString();

                SmartAlarmChkBox.Checked = AlarmToCreate.SmartAlarmEnabled;

                DeleteAlarmBtn.Enabled = true;
            }
            else
            {
                DeleteAlarmBtn.Enabled = false;
            }

        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //Time for alarm clicked
            TimeForAlarmTxtView.Click += (s, e) =>
            {
                //new instance of the time picker fragment
                TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (DateTime time)
                {
                    //set time for alarm
                    TimeForAlarm = time;
                    //set text
                    TimeForAlarmTxtView.Text = time.ToShortTimeString();
                });
                //show fragment
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };

            //delete alarm button click
            DeleteAlarmBtn.Click += (s, e) =>
            {
                //delete alarm
                GlobalUtilities.SleepAlarmManager.DeleteSleepAlarm(AlarmToCreate.ID);
                //toast user
                Toast.MakeText(Activity, "Alarm Deleted!", ToastLength.Short).Show();
                //load alarm history fragment
                LoadFragment(new HistorySleepAlarmFragment());
            };

            //create alarm button click
            CreateAlarmBtn.Click += (s, e) =>
            {
                ///if time is not set
                if (string.IsNullOrEmpty(TimeForAlarmTxtView.Text))
                {
                    //toast user
                    Toast.MakeText(Activity, "You must pick a Time!", ToastLength.Short).Show();
                }
                //if at least one alarm has not been checked
                if (CheckForAtLeastOneCheckBoxChecked() == false)
                {
                    //toast user
                    Toast.MakeText(Activity, "You must pick atleast one Date!", ToastLength.Short).Show();
                }
                else
                {
                    //if in edit mode
                    if (InEditMode)
                    {
                        //set attributes for alarm to create
                        AlarmToCreate.SmartAlarmEnabled = SmartAlarmChkBox.Checked;
                        AlarmToCreate.AlarmTime = TimeForAlarm;
                        AlarmToCreate.OnMon = MonChkBox.Checked;
                        AlarmToCreate.OnTues = TuesChkBox.Checked;
                        AlarmToCreate.OnWed = WedChkBox.Checked;
                        AlarmToCreate.OnThurs = ThursChkBox.Checked;
                        AlarmToCreate.OnFri = FriChkBox.Checked;
                        AlarmToCreate.OnSat = SatChkBox.Checked;
                        AlarmToCreate.OnSun = SunChkBox.Checked;
                        //save sleep alarm
                        GlobalUtilities.SleepAlarmManager.SaveSleepAlarm(AlarmToCreate);
                        //toast user
                        Toast.MakeText(Activity, "Alarm Saved!", ToastLength.Short).Show();
                    }
                    else
                    {
                        //create new sleep alarm
                        AlarmToCreate = new SleepAlarm(SmartAlarmChkBox.Checked, TimeForAlarm, MonChkBox.Checked, TuesChkBox.Checked, WedChkBox.Checked, ThursChkBox.Checked, FriChkBox.Checked, SatChkBox.Checked, SunChkBox.Checked);
                        //save alarm
                        GlobalUtilities.SleepAlarmManager.SaveSleepAlarm(AlarmToCreate);
                        //toast user
                        Toast.MakeText(Activity, "Alarm Saved!", ToastLength.Short).Show();
                    }

                    //load sleep alarm history fragment
                    LoadFragment(new HistorySleepAlarmFragment());
                }
            };
        }

        /// <summary>
        /// Checks that at least one dat has been selected
        /// </summary>
        /// <returns>bool indicating whether or not a check box has been selected</returns>
        private bool CheckForAtLeastOneCheckBoxChecked()
        {
            //if any boxes checked
            if (MonChkBox.Checked || TuesChkBox.Checked || WedChkBox.Checked || ThursChkBox.Checked || FriChkBox.Checked || SatChkBox.Checked || SunChkBox.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion //Methods


    }
}