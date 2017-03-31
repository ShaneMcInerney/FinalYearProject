using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using OxyPlot.Xamarin.Android;
using System;
using System.Diagnostics;

namespace FYP_Droid.Activities
{
    [Activity(Label = "GoingToSleepActivity")]
    public class GoingToSleepActivity : Activity
    {
        #region Fields

        private Button m_wakeUpButton;

        private PlotView m_sleepPlotView;
        private AccelerometerDataReceiver m_graphReceiver;

        private SleepEntry m_newSleepEntry;
        private Stopwatch m_sleepTimer;
        private AlertDialog.Builder m_alertBuilder;
        private Dialog m_dialog;


        private TimeSpan m_sleepLength;
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
        /// Gets/Sets m_sleepPlotView
        /// </summary>
        public PlotView SleepPlotView
        {
            get
            {
                return m_sleepPlotView;
            }

            set
            {
                m_sleepPlotView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_graphReceiver
        /// </summary>
        public AccelerometerDataReceiver GraphReceiver
        {
            get
            {
                return m_graphReceiver;
            }

            set
            {
                m_graphReceiver = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_wakeUpButton
        /// </summary>
        public Button WakeUpButton
        {
            get
            {
                return m_wakeUpButton;
            }

            set
            {
                m_wakeUpButton = value;
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
        /// Gets/Sets m_sleepTimer
        /// </summary>
        public Stopwatch SleepTimer
        {
            get
            {
                return m_sleepTimer;
            }

            set
            {
                m_sleepTimer = value;
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
        /// Gets/Sets m_alerDialogView
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepRadGroup
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepQualiyRadBtnVeryBad
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepQualiyRadBtnBad
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepQualiyRadBtnNeutral
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepQualiyRadBtnGood
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepQualiyRadBtnVeryGood
        /// </summary>
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

        /// <summary>
        /// Gets/Sets m_sleepQuality
        /// </summary>
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


        #endregion //Property Accessors

        #region Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //set layout
            SetContentView(Resource.Layout.GoingToSleepView);
            //setup ui
            SetupUIVariables();
            //setup utilities
            SetupUtilityVariables();
            //handle events
            EventHandlers();
        }


        protected override void OnDestroy()
        {
            UnregisterReceiver(GraphReceiver);
            base.OnDestroy();
        }

        /// <summary>
        /// Setting up variables to be used within this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            SleepTimer = new Stopwatch();
            SleepTimer.Start();
            AlertBuilder = new AlertDialog.Builder(this);
            GraphReceiver = new AccelerometerDataReceiver();
            RegisterReceiver(GraphReceiver, new IntentFilter("graph"));
        }

        /// <summary>
        /// Setting up UI variables to be used within this class
        /// </summary>
        private void SetupUIVariables()
        {
            WakeUpButton = FindViewById<Button>(Resource.Id.wakeUpBtn);
            SleepPlotView = FindViewById<PlotView>(Resource.Id.realtimeSleepDisplay);
            AlerDialogView = LayoutInflater.Inflate(Resource.Layout.AlertDialogSleepQualityView, null);
            SleepRadGroup = AlerDialogView.FindViewById<RadioGroup>(Resource.Id.sleepRadGroup);
            SleepQualiyRadBtnVeryBad = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnVeryBad);
            SleepQualiyRadBtnBad = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnBad);
            SleepQualiyRadBtnNeutral = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnNeutral);
            SleepQualiyRadBtnGood = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnGood);
            SleepQualiyRadBtnVeryGood = AlerDialogView.FindViewById<RadioButton>(Resource.Id.sleepRadBtnVeryGood);
        }

        /// <summary>
        /// Wrapper method to handle all event delegates
        /// </summary>
        private void EventHandlers()
        {
            //data received
            GraphReceiver.DataReceivedEvent += delegate
            {
                //setting receivedmagnitue
                double receivedVectorMagnitude = GraphReceiver.ReceivedVector;
                //setting plot view model
                this.SleepPlotView.Model = GlobalUtilities.GraphManager.CreateRealtimeAccelerometerGraph("My Sleep", receivedVectorMagnitude);
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

            //wake up button clicked
            WakeUpButton.Click += (s, e) =>
            {
                //stop timer
                SleepTimer.Stop();
                //set sleep length
                SleepLength = SleepTimer.Elapsed;
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
                    //stop sleep tracking service
                    StopService(new Intent(this, typeof(SleepTrackerService)));
                    //intent for next activity
                    Intent nextActivity = new Intent(this, typeof(NaivgationActivity));
                    //start navigtion activity
                    StartActivity(nextActivity);
                    //end this activity
                    this.Finish();
                });
                //set action for negative button
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
        }

        #endregion //Methods
    }
}