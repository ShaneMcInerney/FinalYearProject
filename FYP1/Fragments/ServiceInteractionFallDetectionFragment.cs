using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FYP_Droid.Services;
using FYP_Droid.Utilities;
using OxyPlot.Xamarin.Android;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class ServiceInteractionFallDetectionFragment : BaseFragment
    {

        #region Fields

        private PlotView m_realtimeAccelerometerGraph;
        private Button m_startAccelerometerBtn;
        private Button m_stopAccelerometerBtn;
        private AccelerometerDataReceiver m_graphReceiver;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_startAccelerometerBtn
        /// </summary>
        public Button StartAccelerometerBtn
        {
            get
            {
                return m_startAccelerometerBtn;
            }

            set
            {
                m_startAccelerometerBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stopAccelerometerBtn
        /// </summary>
        public Button StopAccelerometerBtn
        {
            get
            {
                return m_stopAccelerometerBtn;
            }

            set
            {
                m_stopAccelerometerBtn = value;
            }
        }


        /// <summary>
        /// Gets/Sets m_realtimeAccelerometerGraph
        /// </summary>
        public PlotView RealtimeAccelerometerGraph
        {
            get
            {
                return m_realtimeAccelerometerGraph;
            }

            set
            {
                m_realtimeAccelerometerGraph = value;
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


        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public override void OnDestroy()
        {
            //unregister graph receiver
            Activity.UnregisterReceiver(GraphReceiver);
            base.OnDestroy();
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
            //inflate layout
            InflateView(inflater, container, Resource.Layout.ServiceRealtimeAccelerometerView);
            //set up UI variables
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
            //return frag view
            return FragView;
        }

        /// <summary>
        /// Setup this classes UI variables
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Fall Detector";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);
            this.RealtimeAccelerometerGraph = FragView.FindViewById<PlotView>(Resource.Id.realtimeAccelerometerDisplay);
            this.StartAccelerometerBtn = FragView.FindViewById<Button>(Resource.Id.startAccelerometerBtn);
            this.StopAccelerometerBtn = FragView.FindViewById<Button>(Resource.Id.stopAccelerometerBtn);
        }

        /// <summary>
        /// Setup this classes utility variables
        /// </summary>
        private void SetupUtilityVariables()
        {
            GraphReceiver = new AccelerometerDataReceiver();
            Activity.RegisterReceiver(GraphReceiver, new IntentFilter("graph"));
        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //graph data received
            GraphReceiver.DataReceivedEvent += delegate
             {
                 //getting received vector megnitude
                 double receivedVectorMagnitude = GraphReceiver.ReceivedVector;
                 //update graph model
                 this.RealtimeAccelerometerGraph.Model = GlobalUtilities.GraphManager.CreateRealtimeAccelerometerGraph("Fall Detector Output", receivedVectorMagnitude);
             };

            //stat btn clicked
            StartAccelerometerBtn.Click += (s, e) =>
            {
                //if fall detector is not running
                if (FallDetectorService.IsRunning == false)
                {
                    //start service
                    Activity.StartService(new Intent(Activity.ApplicationContext, typeof(FallDetectorService)));
                    //toast user
                    Toast.MakeText(Activity, "Starting Fall Detector!", ToastLength.Short).Show();
                }
                else
                {
                    //toast user
                    Toast.MakeText(Activity, "Fall Detector is Already Running!", ToastLength.Short).Show();
                }
            };

            //stop btn clicked
            StopAccelerometerBtn.Click += (s, e) =>
            {
                //if fall detetor is running
                if (FallDetectorService.IsRunning)
                {
                    //stop service
                    Activity.StopService(new Intent(Activity.ApplicationContext, typeof(FallDetectorService)));
                    //toast user
                    Toast.MakeText(Activity, "Stopping Fall Detector!", ToastLength.Short).Show();
                }
                else
                {
                    //toast user
                    Toast.MakeText(Activity, "Fall Detector is Not Running!", ToastLength.Short).Show();
                }
            };
        }

        #endregion //Methods


    }
}