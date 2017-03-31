using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using Refractored.Xam.Vibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FYP_Droid.Activities
{
    [Activity(Label = "FallDetectedActivity")]
    public class PotentialFallDetectedActivity : Activity
    {

        #region Fields

        private Button m_sendEmergencyMsgBtn;
        private Button m_userIsOkBtn;
        private TextView m_countDownTxtView;

        private Timer m_timer;

        private int m_countDownValue = 15;
        private bool m_messageSent;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_sendEmergencyMsgBtn
        /// </summary>
        public Button SendEmergencyMsgBtn
        {
            get
            {
                return m_sendEmergencyMsgBtn;
            }

            set
            {
                m_sendEmergencyMsgBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_userIsOkBtn
        /// </summary>
        public Button UserIsOkBtn
        {
            get
            {
                return m_userIsOkBtn;
            }

            set
            {
                m_userIsOkBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_timer
        /// </summary>
        public Timer Timer
        {
            get
            {
                return m_timer;
            }

            set
            {
                m_timer = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_countDownTxtView
        /// </summary>
        public TextView CountDownTxtView
        {
            get
            {
                return m_countDownTxtView;
            }

            set
            {
                m_countDownTxtView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_countDownValue
        /// </summary>
        public int CountDownValue
        {
            get
            {
                return m_countDownValue;
            }

            set
            {
                m_countDownValue = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_messageSent
        /// </summary>
        public bool MessageSent
        {
            get
            {
                return m_messageSent;
            }

            set
            {
                m_messageSent = value;
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
            //set content view
            SetContentView(Resource.Layout.PotentialFallDetectedView);
            //Setup UI Variables
            SetupUIVariables();
            //Setup Utility Variables
            SetupUtilityVariables();
            //Handle Events
            EventHandlers();
        }

        /// <summary>
        /// Setup UI variables
        /// </summary>
        private void SetupUIVariables()
        {
            this.m_sendEmergencyMsgBtn = FindViewById<Button>(Resource.Id.sendEmergencyMessage);
            this.m_userIsOkBtn = FindViewById<Button>(Resource.Id.falseAlarmBtn);
            this.m_countDownTxtView = FindViewById<TextView>(Resource.Id.countDownTxtView);
        }

        /// <summary>
        /// Setup utility variables for use within this class
        /// </summary>
        private void SetupUtilityVariables()
        {

            this.m_messageSent = false;
            this.m_timer = new Timer();
            this.m_timer.Interval = 1000;
            this.m_timer.Enabled = true;
            this.Timer.Elapsed += OnTimedEvent;
            this.m_timer.Start();
            var v = CrossVibrate.Current;
            //setting vibration
            v.Vibration(1500);
            //add flags for windows
            Window.AddFlags(WindowManagerFlags.KeepScreenOn |
               WindowManagerFlags.DismissKeyguard |
               WindowManagerFlags.ShowWhenLocked |
               WindowManagerFlags.TurnScreenOn);

        }

        /// <summary>
        /// Handle all events
        /// </summary>
        private void EventHandlers()
        {
            //on send emergency button click
            SendEmergencyMsgBtn.Click += (s, e) =>
            {
                //stop the timer
                Timer.Stop();
                //create new fall event
                var fall = new Fall(DateTime.Now);
                //save the fall event
                GlobalUtilities.FallManager.SaveFall(fall);
                //send emergency message
                SendEmergencyMessageToContacts();
                //if the alarm is not alredy playig
                if (GlobalUtilities.AlarmManager.IsPlaying == false)
                {
                    //setup the media player
                    GlobalUtilities.AlarmManager.SetupMediaPlayer(this);
                    //play the fall alarm
                    GlobalUtilities.AlarmManager.PlayFallAlarm();
                    //trigger vibration
                    CrossVibrate.Current.Vibration(2000);
                }
            };

            //user okay clicked
            UserIsOkBtn.Click += (s, e) =>
            {
                //stop the count down timer
                Timer.Stop();
                //if the alarm is playing
                if (GlobalUtilities.AlarmManager.IsPlaying)
                {
                    //stop the alarm
                    GlobalUtilities.AlarmManager.StopFallAlarm();
                }

                //creating new intent
                Intent nextActivity = new Intent(this, typeof(NaivgationActivity));
                //starting new activity
                StartActivity(nextActivity);
                //restarting fall detector service
                StartService(new Intent(this, typeof(FallDetectorService)));
                //output to user
                Toast.MakeText(this, "Starting Fall Detetor Again", ToastLength.Short).Show();
            };

        }

        /// <summary>
        /// When timer elapses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //decrement countdown value
            CountDownValue--;
            //run on user interface thread
            RunOnUiThread(() =>
            {
                //update ui
                CountDownTxtView.Text = CountDownValue.ToString();
                //if the countdown reaches zero
                if (CountDownValue == 0)
                {
                    //stop the timer
                    Timer.Stop();
                    //if message has not been sent
                    if (MessageSent == false)
                    {
                        //send message to emergency contacts
                        SendEmergencyMessageToContacts();
                        //mark message sent as true
                        MessageSent = true;
                        //create a new fall entry
                        var fall = new Fall(DateTime.Now);
                        //setup the media player
                        GlobalUtilities.AlarmManager.SetupMediaPlayer(this);
                        //play alarm
                        GlobalUtilities.AlarmManager.PlayFallAlarm();
                        //save fall to database
                        GlobalUtilities.FallManager.SaveFall(fall);
                    }
                    //set count down value
                    CountDownValue = 15;
                }
            });
        }

        /// <summary>
        /// Sends emergency message to all emergency contacts
        /// </summary>
        private void SendEmergencyMessageToContacts()
        {
            if (GlobalUtilities.EmergencyContactManager.EmergencyContactExists())
            {
                //setting context
                GlobalUtilities.EmergencyMessageManager.Context = this;
                //getting list of emergency contacts from db
                List<EmergencyContact> emergencyContacts = GlobalUtilities.EmergencyContactManager.GetAllEmergencyContacts().ToList<EmergencyContact>();
                //getting message from db
                EmergencyMessage message = GlobalUtilities.EmergencyMessageManager.GetEmergencyMessage();
                //send message to each contact
                GlobalUtilities.EmergencyMessageManager.SendEmergencyMessageToList(emergencyContacts, message);
            }
        }
        #endregion //Methods
    }
}