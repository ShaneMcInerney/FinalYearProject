using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP_Droid.Services;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class ServiceInteractionStepCounterFragment : BaseFragment
    {

        #region Fields

        //ui vars
        private Button m_startStepCounterBtn;
        private Button m_stopStepCounterBtn;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_startStepCounterBtn
        /// </summary>
        public Button StartStepCounterBtn
        {
            get
            {
                return m_startStepCounterBtn;
            }

            set
            {
                m_startStepCounterBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stopStepCounterBtn
        /// </summary>
        public Button StopStepCounterBtn
        {
            get
            {
                return m_stopStepCounterBtn;
            }

            set
            {
                m_stopStepCounterBtn = value;
            }
        }

        #endregion //Proprty Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //Inflate frag view
            InflateView(inflater, container, Resource.Layout.ServiceStepCounterView);
            //setup variables
            SetupVariables();
            //handle events
            EventHandlers();
            //return frag view
            return FragView;
        }

        /// <summary>
        /// Setup this classes variables
        /// </summary>
        private void SetupVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Step Tracker";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            StartStepCounterBtn = FragView.FindViewById<Button>(Resource.Id.startStepCounterBtn);
            StopStepCounterBtn = FragView.FindViewById<Button>(Resource.Id.stopStepCounterBtn);
        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //start btn clicked
            StartStepCounterBtn.Click += (sender, args) =>
            {
                //if step tracker is not running
                if (StepCounterService.IsRunning == false)
                {
                    //start service
                    Activity.StartService(new Intent(Activity.ApplicationContext, typeof(StepCounterService)));
                    //toast user
                    Toast.MakeText(Activity, "Step Tracker Started!", ToastLength.Short).Show();
                    //load dashboard
                    LoadFragment(new DashoardSummaryFragment());
                }
                else
                {
                    //toast user
                    Toast.MakeText(Activity, "Step Tracker is Already Runnning!", ToastLength.Short).Show();
                }
            };

            //stop btn clicked
            StopStepCounterBtn.Click += (sender, args) =>
            {
                //if step tracker is running
                if (StepCounterService.IsRunning)
                {
                    //stop service
                    Activity.StopService(new Intent(Activity.ApplicationContext, typeof(StepCounterService)));
                    //toast user
                    Toast.MakeText(Activity, "Step Tracker Stopped!", ToastLength.Short).Show();
                    //load dashboard
                    LoadFragment(new DashoardSummaryFragment());
                }
                else
                {
                    //toast user
                    Toast.MakeText(Activity, "Step Tracker is Not Runnning!", ToastLength.Short).Show();
                }
            };
        }



        #endregion //Methods


    }
}