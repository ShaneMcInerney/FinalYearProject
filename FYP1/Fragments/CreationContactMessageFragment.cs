using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class CreationContactMessageFragment : BaseFragment
    {
        #region Fields

        private EditText m_emergencyMessageEditTxt;
        private CheckBox m_includeLocationChkBx;
        private Button m_saveEmergencyMessageBtn;

        private InputMethodManager m_inputManager;

        private string m_messageTxt;
        private bool m_includeLocation = false;

        private const string m_defaultMessage = "Help, I've fallen and require assistance";
        private bool m_inEditMode;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_emergencyMessageEditTxt
        /// </summary>
        public EditText EmergencyMessageEditTxt
        {
            get
            {
                return m_emergencyMessageEditTxt;
            }

            set
            {
                m_emergencyMessageEditTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_includeLocationChkBx
        /// </summary>
        public CheckBox IncludeLocationChkBx
        {
            get
            {
                return m_includeLocationChkBx;
            }

            set
            {
                m_includeLocationChkBx = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_saveEmergencyMessageBtn
        /// </summary>
        public Button SaveEmergencyMessageBtn
        {
            get
            {
                return m_saveEmergencyMessageBtn;
            }

            set
            {
                m_saveEmergencyMessageBtn = value;
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
        /// Gets/Sets m_messageTxt
        /// </summary>
        public string MessageTxt
        {
            get
            {
                return m_messageTxt;
            }

            set
            {
                m_messageTxt = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_includeLocation
        /// </summary>
        public bool IncludeLocation
        {
            get
            {
                return m_includeLocation;
            }

            set
            {
                m_includeLocation = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_defaultMessage
        /// </summary>
        public string DefaultMessage
        {
            get
            {
                return m_defaultMessage;
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

        #endregion //Property Accessors

        #region Methods

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //set fragment view
            InflateView(inflater, container, Resource.Layout.CreationContactMessageView);
            //setup UI variables
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle event
            EventHandlers();

            return FragView;
        }

        /// <summary>
        /// sets up the fragments UI
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);

            Toolbar.Title = "My Emergency Message";

            InputManager = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);

            EmergencyMessageEditTxt = FragView.FindViewById<EditText>(Resource.Id.emergencyMessageEditTxt);

            IncludeLocationChkBx = FragView.FindViewById<CheckBox>(Resource.Id.includeLocationChkBox);

            SaveEmergencyMessageBtn = FragView.FindViewById<Button>(Resource.Id.saveMessageBtn);
        }

        /// <summary>
        /// Setting up utility variables for use within this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            bool MessageExists = GlobalUtilities.EmergencyMessageManager.EmergencyMessageExists();

            if (MessageExists)
            {
                //get message from db
                EmergencyMessage message = GlobalUtilities.EmergencyMessageManager.GetEmergencyMessage();
                //set text
                EmergencyMessageEditTxt.Text = message.Message;
                //handle location check box
                IncludeLocationChkBx.Checked = message.IncludeLocation;
            }
            else
            {
                //default text
                EmergencyMessageEditTxt.Text = "Help I have fallen and require assistance";
                //default checkbox value
                IncludeLocationChkBx.Checked = true;
            }
        }

        /// <summary>
        /// Contains all event handlers
        /// </summary>
        private void EventHandlers()
        {
            //Handles clicking of save button
            SaveEmergencyMessageBtn.Click += (s, e) =>
            {
                //delete messae
                GlobalUtilities.EmergencyMessageManager.DeleteEmergencyMessage();
                //get message text
                MessageTxt = EmergencyMessageEditTxt.Text;
                //include location?
                IncludeLocation = IncludeLocationChkBx.Checked;
                //make sure fields filled out
                if (string.IsNullOrWhiteSpace(MessageTxt) || string.IsNullOrEmpty(MessageTxt))
                {
                    //use efault message
                    MessageTxt = this.DefaultMessage;
                    //toast user
                    Toast.MakeText(Activity.ApplicationContext, "Please Fill Out The Emergency Message", ToastLength.Short).Show();
                }
                else
                {
                    //save message
                    GlobalUtilities.EmergencyMessageManager.SaveEmergencyMessage(new EmergencyMessage(MessageTxt, IncludeLocation));
                    //Toast user
                    Toast.MakeText(Activity.ApplicationContext, "Message Saved!", ToastLength.Short).Show();
                    //load dashboard fragment
                    LoadFragment(new DashoardSummaryFragment());
                }
            };
        }


        #endregion //Methods


    }
}