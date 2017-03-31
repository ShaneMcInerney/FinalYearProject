using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP_Droid.Activities;
using FYP_Droid.Services;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class ServiceInteractionSleepTrackerFragment : BaseFragment
    {

        #region Fields
        //ui vars
        private Button m_startSleepTrackerBtn;
        private Button m_stopSleepTrackerBtn;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_startSleepTrackerBtn
        /// </summary>
        public Button StartSleepTrackerBtn
        {
            get
            {
                return m_startSleepTrackerBtn;
            }

            set
            {
                m_startSleepTrackerBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stopSleepTrackerBtn
        /// </summary>
        public Button StopSleepTrackerBtn
        {
            get
            {
                return m_stopSleepTrackerBtn;
            }

            set
            {
                m_stopSleepTrackerBtn = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate frag view
            InflateView(inflater, container, Resource.Layout.ServiceSleepTrackerView);
            //set up variables
            SetupVariables();
            //handle events
            EventHandlers();
            //retur frag view
            return FragView;
        }



        /// <summary>
        /// Setup ariables for use in ths class
        /// </summary>
        private void SetupVariables()
        {
            //set up toolbar
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Sleep Tracker";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            //set up buttons
            this.StartSleepTrackerBtn = FragView.FindViewById<Button>(Resource.Id.startSleepTrackerBtn);
            this.StopSleepTrackerBtn = FragView.FindViewById<Button>(Resource.Id.stopSleepTrackerBtn);
        }

        /// <summary>
        /// handle events
        /// </summary>
        private void EventHandlers()
        {
            //start button clicked
            StartSleepTrackerBtn.Click += (s, e) =>
            {
                //set alert title
                AlertBuilder.SetTitle("Prepare Sleep Tracker");
                //set alert message
                AlertBuilder.SetMessage("When you are ready, press start and place your phone face down on your bed.");
                //set positive button
                AlertBuilder.SetPositiveButton("Start", (senderAlert, args) =>
                {
                    //toast user
                    Toast.MakeText(Activity, "Starting Sleep Tracker!", ToastLength.Short).Show();
                    //start sleep tracking service
                    Activity.StartService(new Intent(Activity.ApplicationContext, typeof(SleepTrackerService)));
                    //start activity
                    Activity.StartActivity(new Intent(Activity.ApplicationContext, typeof(GoingToSleepActivity)));
                });
                //set negative button
                AlertBuilder.SetNegativeButton("Cancel", (senderAlert, args) =>
                {

                });
                //create dialog
                Dialog dialog = AlertBuilder.Create();
                //show dialog
                dialog.Show();
            };

            //stop btn clicked
            StopSleepTrackerBtn.Click += (s, e) =>
            {
                //toast user
                Toast.MakeText(Activity, "Stopping Sleep Tracker!", ToastLength.Short).Show();
                //stop service
                Activity.StopService(new Intent(Activity.ApplicationContext, typeof(SleepTrackerService)));
            };
        }

        #endregion //Methods


    }
}