using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using Newtonsoft.Json;
using Refractored.Xam.Vibrate;
using System;

namespace FYP_Droid.Activities
{
    [Activity(Label = "SleepAlarmActivity")]
    public class SleepAlarmActivity : Activity
    {
        #region Fields

        private Button m_cancelSleepAlarmBtn;
        private TimeSpan m_sleepLength;
        private SleepEntry m_newSleepEntry;
        private AlertDialog.Builder m_alertBuilder;
        private Dialog m_dialog;

        private int m_sleepQuality = 2;

        private View m_alerDialogView;
        private RadioGroup m_sleepRadGroup;
        private RadioButton m_sleepQualiyRadBtnVeryBad;
        private RadioButton m_sleepQualiyRadBtnBad;
        private RadioButton m_sleepQualiyRadBtnNeutral;
        private RadioButton m_sleepQualiyRadBtnGood;
        private RadioButton m_sleepQualiyRadBtnVeryGood;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_cancelSleepAlarmBtn
        /// </summary>
        public Button CancelSleepAlarmBtn
        {
            get
            {
                return m_cancelSleepAlarmBtn;
            }

            set
            {
                m_cancelSleepAlarmBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_newSleepEntry
        /// </summary>
        public SleepEntry NewSleepEntry
        {
            get
            {
                return m_newSleepEntry;
            }

            set
            {
                m_newSleepEntry = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_alertBuilder
        /// </summary>
        public AlertDialog.Builder AlertBuilder
        {
            get
            {
                return m_alertBuilder;
            }

            set
            {
                m_alertBuilder = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_dialog
        /// </summary>
        public Dialog Dialog
        {
            get
            {
                return m_dialog;
            }

            set
            {
                m_dialog = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepLength
        /// </summary>
        public TimeSpan SleepLength
        {
            get
            {
                return m_sleepLength;
            }

            set
            {
                m_sleepLength = value;
            }
        }

        public int SleepQuality
        {
            get
            {
                return m_sleepQuality;
            }

            set
            {
                m_sleepQuality = value;
            }
        }

        public View AlerDialogView
        {
            get
            {
                return m_alerDialogView;
            }

            set
            {
                m_alerDialogView = value;
            }
        }

        public RadioGroup SleepRadGroup
        {
            get
            {
                return m_sleepRadGroup;
            }

            set
            {
                m_sleepRadGroup = value;
            }
        }

        public RadioButton SleepQualiyRadBtnVeryBad
        {
            get
            {
                return m_sleepQualiyRadBtnVeryBad;
            }

            set
            {
                m_sleepQualiyRadBtnVeryBad = value;
            }
        }

        public RadioButton SleepQualiyRadBtnBad
        {
            get
            {
                return m_sleepQualiyRadBtnBad;
            }

            set
            {
                m_sleepQualiyRadBtnBad = value;
            }
        }

        public RadioButton SleepQualiyRadBtnNeutral
        {
            get
            {
                return m_sleepQualiyRadBtnNeutral;
            }

            set
            {
                m_sleepQualiyRadBtnNeutral = value;
            }
        }

        public RadioButton SleepQualiyRadBtnGood
        {
            get
            {
                return m_sleepQualiyRadBtnGood;
            }

            set
            {
                m_sleepQualiyRadBtnGood = value;
            }
        }

        public RadioButton SleepQualiyRadBtnVeryGood
        {
            get
            {
                return m_sleepQualiyRadBtnVeryGood;
            }

            set
            {
                m_sleepQualiyRadBtnVeryGood = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //if sleep length has been added as an extra
            if (this.Intent.HasExtra("SleepLength"))
            {
                //deserialize extra, set sleep length
                SleepLength = JsonConvert.DeserializeObject<TimeSpan>(this.Intent.GetStringExtra("SleepLength"));
            }

            base.OnCreate(savedInstanceState);
            //add window flags
            Window.AddFlags(WindowManagerFlags.KeepScreenOn |
               WindowManagerFlags.DismissKeyguard |
               WindowManagerFlags.ShowWhenLocked |
               WindowManagerFlags.TurnScreenOn);
            //set layout for view
            SetContentView(Resource.Layout.SleepAlarmInteractionView);
            //set up UI variables
            SetupUIVariables();
            //set up utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
        }

        /// <summary>
        /// setup ui variables
        /// </summary>
        private void SetupUIVariables()
        {
            CancelSleepAlarmBtn = FindViewById<Button>(Resource.Id.cancelSleepAlarmBtn);
            AlertBuilder = new AlertDialog.Builder(this);
            AlerDialogView = LayoutInflater.Inflate(Resource.Layout.AlertDialogSleepQualityView, null);
            SleepRadGroup = AlerDialogView.FindViewById<RadioGroup>(Resource.Id.sleepRadGroup);
            SleepQualiyRadBtnVeryBad = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnVeryBad);
            SleepQualiyRadBtnBad = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnBad);
            SleepQualiyRadBtnNeutral = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnNeutral);
            SleepQualiyRadBtnGood = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnGood);
            SleepQualiyRadBtnVeryGood = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnVeryGood);

        }

        /// <summary>
        /// Setup up utility variables for use in this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            var v = CrossVibrate.Current;
            //vibrate phone
            v.Vibration();
            v.Vibration();
            v.Vibration();
            v.Vibration();
            //setup media player
            GlobalUtilities.SleepAlarmManager.SetupMediaPlayer(this);
            //play alarm (wake user)
            GlobalUtilities.SleepAlarmManager.PlaySleepAlarm();
            SleepQuality = 2;
        }

        /// <summary>
        /// Handle all events
        /// </summary>
        private void EventHandlers()
        {
            //if user cancels alarm
            CancelSleepAlarmBtn.Click += (s, e) =>
            {
                //stop sleep alarm
                GlobalUtilities.SleepAlarmManager.StopSleepAlarm();
                //set alert title
                AlertBuilder.SetTitle("Rate Your Sleep");
                //set alert view
                AlertBuilder.SetView(AlerDialogView);
                //set action for positive button
                AlertBuilder.SetPositiveButton("Save", (senderAlert, args) =>
                {
                    //create new sleep entry
                    NewSleepEntry = new SleepEntry(SleepLength, SleepQuality, DateTime.Now);
                    //save sleep entry
                    GlobalUtilities.SleepManager.SaveSleepEntry(NewSleepEntry);
                    SleepTrackerService.IsRunning = false;
                    //stop sleep tracking service
                    StopService(new Intent(this, typeof(SleepTrackerService)));
                    //intent for next activity
                    Intent nextActivity = new Intent(this, typeof(NaivgationActivity));
                    //start navigtion activity
                    StartActivity(nextActivity);
                    //end this activity
                    this.Finish();
                });
                AlertBuilder.SetNegativeButton("Skip", (senderAlert, args) =>
                {
                    //crete new sleep entry
                    NewSleepEntry = new SleepEntry(SleepLength, SleepQuality, DateTime.Now);
                    //save sleep entry
                    GlobalUtilities.SleepManager.SaveSleepEntry(NewSleepEntry);
                    //stop sleep tracking service
                    StopService(new Intent(this, typeof(SleepTrackerService)));
                    //intent for navigation activity
                    Intent nextActivity = new Intent(this, typeof(NaivgationActivity));
                    //start activity
                    StartActivity(nextActivity);
                    //end this activity
                    this.Finish();
                });
                //build dialog
                Dialog dialog = AlertBuilder.Create();
                //show dialog
                dialog.Show();
            };

            //sleep quality selected
            SleepRadGroup.CheckedChange += (se, ev) =>
            {
                //switch fro selected quality
                switch (ev.CheckedId)
                {
                    //very bad quality
                    case Resource.Id.sleepRadBtnVeryBad:
                        SleepQuality = 0;
                        break;
                    //bad quality
                    case Resource.Id.sleepRadBtnBad:
                        SleepQuality = 1;
                        break;
                    //neutral quality
                    case Resource.Id.sleepRadBtnNeutral:
                        SleepQuality = 2;
                        break;
                    //good quality
                    case Resource.Id.sleepRadBtnGood:
                        SleepQuality = 3;
                        break;
                    //very good quality
                    case Resource.Id.sleepRadBtnVeryGood:
                        SleepQuality = 4;
                        break;
                }
            };
        }

        #endregion //Methods

    }
}