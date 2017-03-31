using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP_Droid.Services;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class ServiceInteractionCalibrationFragment : BaseFragment
    {

        #region Fields
        //ui vars
        private Button m_calibrateWalkingBtn;
        private Button m_calibrateWalkingUpstairsBtn;
        private Button m_calibrateWalkingDownstairsBtn;

        #endregion //Fields

        #region Property Accessors


        /// <summary>
        /// Gets/Sets m_calibrateWalkingBtn
        /// </summary>
        public Button CalibrateWalkingBtn
        {
            get
            {
                return m_calibrateWalkingBtn;
            }

            set
            {
                m_calibrateWalkingBtn = value;
            }
        }


        /// <summary>
        /// Gets/Sets m_calibrateWalkingUpstairsBtn
        /// </summary>
        public Button CalibrateWalkingUpstairsBtn
        {
            get
            {
                return m_calibrateWalkingUpstairsBtn;
            }

            set
            {
                m_calibrateWalkingUpstairsBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_calibrateWalkingDownstairsBtn
        /// </summary>
        public Button CalibrateWalkingDownstairsBtn
        {
            get
            {
                return m_calibrateWalkingDownstairsBtn;
            }

            set
            {
                m_calibrateWalkingDownstairsBtn = value;
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
            //infate frag view
            InflateView(inflater, container, Resource.Layout.TestCalibView);
            //setup variables
            SetupVariables();
            //handle events
            EventHandlers();
            //return frag view
            return FragView;
        }

        /// <summary>
        /// Setup variles for use in this class
        /// </summary>
        private void SetupVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "Calibration";
            Toolbar.InflateMenu(Resource.Menu.toolbar_menu_edit);

            CalibrateWalkingBtn = FragView.FindViewById<Button>(Resource.Id.startWalkingCalibrationBtn);
            CalibrateWalkingUpstairsBtn = FragView.FindViewById<Button>(Resource.Id.startWalkingUpstairsCalibrationBtn);
            CalibrateWalkingDownstairsBtn = FragView.FindViewById<Button>(Resource.Id.startWalkingDownstairsCalibrationBtn);
            AlertBuilder = new AlertDialog.Builder(Activity);
        }

        /// <summary>
        /// Handle events
        /// </summary>
        private void EventHandlers()
        {
            //walking calibration btn click
            CalibrateWalkingBtn.Click += (s, e) =>
            {
                //if calibration hasnt started
                if (CalibrateWalkingBtn.Text.ToLower() == "start walking calibration")
                {
                    //diable other buttons
                    CalibrateWalkingUpstairsBtn.Enabled = false;
                    CalibrateWalkingDownstairsBtn.Enabled = false;
                    //set title
                    AlertBuilder.SetTitle("Walking Calibration");
                    //set message
                    AlertBuilder.SetMessage("Place your phone where it would be when normally walking, and take approximately 15 steps, preferably in a straight line ");
                    //set positive button
                    AlertBuilder.SetPositiveButton("Begin", (senderAlert, args) =>
                    {
                        //set btn text
                        CalibrateWalkingBtn.Text = "Stop Calibration";
                        //intent to start service
                        Intent serviceToStart = new Intent(Activity.ApplicationContext, typeof(CalibrationService));
                        //add extra
                        serviceToStart.PutExtra("Walking", true);
                        //start service
                        Activity.StartService(serviceToStart);
                        //toast user
                        Toast.MakeText(Activity, "Started Calibration Service!", ToastLength.Short).Show();
                    });
                    //set negative button
                    AlertBuilder.SetNegativeButton("Cancel", (senderAlert, args) =>
                    {
                        //set btn text
                        CalibrateWalkingBtn.Text = "Start Walking Calibration";
                        //enable buttons
                        CalibrateWalkingUpstairsBtn.Enabled = true;
                        CalibrateWalkingDownstairsBtn.Enabled = true;
                    });
                    //create dialog
                    Dialog dialog = AlertBuilder.Create();
                    //show dialog
                    dialog.Show();
                }
                else
                {
                    //set btn text
                    CalibrateWalkingBtn.Text = "Start Walking Calibration";
                    //stop service
                    Activity.StopService(new Intent(Activity.ApplicationContext, typeof(CalibrationService)));
                    //toast user
                    Toast.MakeText(Activity, "Stopped Calibration Service!!", ToastLength.Short).Show();
                    //enable buttons
                    CalibrateWalkingUpstairsBtn.Enabled = true;
                    CalibrateWalkingDownstairsBtn.Enabled = true;
                }
            };

            //walking upstaris calibration click
            CalibrateWalkingUpstairsBtn.Click += (s, e) =>
            {
                //intent to strt service
                Intent serviceToStart = new Intent(Activity.ApplicationContext, typeof(CalibrationService));
                if (CalibrateWalkingUpstairsBtn.Text.ToLower() == "start walking upstairs calibration")
                {
                    //disable btns
                    CalibrateWalkingBtn.Enabled = false;
                    CalibrateWalkingDownstairsBtn.Enabled = false;
                    //set alert title
                    AlertBuilder.SetTitle("Walking Upstairs Calibration");
                    //set aler message
                    AlertBuilder.SetMessage("Place your phone where it would be when normally walking, and walk up a flight of stairs");
                    //set positive button
                    AlertBuilder.SetPositiveButton("Begin", (senderAlert, args) =>
                    {
                        //set btn text
                        CalibrateWalkingUpstairsBtn.Text = "Stop Calibration";
                        //add extra
                        serviceToStart.PutExtra("Upstairs", true);
                        //start service
                        Activity.StartService(new Intent(Activity.ApplicationContext, typeof(CalibrationService)));
                        //toast user
                        Toast.MakeText(Activity, "Started Calibration Service!", ToastLength.Short).Show();
                    });
                    //set negative button
                    AlertBuilder.SetNegativeButton("Cancel", (senderAlert, args) =>
                    {
                        //set btn text
                        CalibrateWalkingUpstairsBtn.Text = "Start Walking Upstairs Calibration";
                        //enable buttons
                        CalibrateWalkingBtn.Enabled = true;
                        CalibrateWalkingDownstairsBtn.Enabled = true;
                    });
                    //create dialog
                    Dialog dialog = AlertBuilder.Create();
                    //show dialog
                    dialog.Show();
                }
                else
                {
                    Activity.StopService(new Intent(Activity.ApplicationContext, typeof(CalibrationService)));
                    //toast user
                    Toast.MakeText(Activity, "Stopped Calibration Service!!", ToastLength.Short).Show();
                    //set btn text
                    CalibrateWalkingUpstairsBtn.Text = "Start Walking Upstairs Calibration";
                    //enable buttons
                    CalibrateWalkingBtn.Enabled = true;
                    CalibrateWalkingDownstairsBtn.Enabled = true;
                }
            };

            //walking downstairs calibration click
            CalibrateWalkingDownstairsBtn.Click += (s, e) =>
            {
                //intent to start service
                Intent serviceToStart = new Intent(Activity.ApplicationContext, typeof(CalibrationService));
                if (CalibrateWalkingDownstairsBtn.Text.ToLower() == "start walking downstairs calibration")
                {
                    //disable buttons
                    CalibrateWalkingBtn.Enabled = false;
                    CalibrateWalkingUpstairsBtn.Enabled = false;
                    //set alert title
                    AlertBuilder.SetTitle("Walking Downstairs Calibration");
                    //set alert message
                    AlertBuilder.SetMessage("Place your phone where it would be when normally walking, and walk down a flight of stairs");
                    //set positive button
                    AlertBuilder.SetPositiveButton("Begin", (senderAlert, args) =>
                    {
                        CalibrateWalkingDownstairsBtn.Text = "Stop Calibration";
                        //add extra
                        serviceToStart.PutExtra("Downstairs", true);
                        //start service
                        Activity.StartService(new Intent(Activity.ApplicationContext, typeof(CalibrationService)));
                        //toast user
                        Toast.MakeText(Activity, "Started Calibration Service!", ToastLength.Short).Show();
                    });
                    //set negative button
                    AlertBuilder.SetNegativeButton("Cancel", (senderAlert, args) =>
                    {
                        //set butotn text
                        CalibrateWalkingDownstairsBtn.Text = "Start Walking Downstairs Calibration";
                        //enable buttons
                        CalibrateWalkingBtn.Enabled = true;
                        CalibrateWalkingUpstairsBtn.Enabled = true;
                    });
                    //create dialog
                    Dialog dialog = AlertBuilder.Create();
                    //show dialog
                    dialog.Show();
                }
                else
                {
                    //stop service
                    Activity.StopService(new Intent(Activity.ApplicationContext, typeof(CalibrationService)));
                    //toast user
                    Toast.MakeText(Activity, "Stopped Calibration Service!!", ToastLength.Short).Show();
                    //set button text
                    CalibrateWalkingDownstairsBtn.Text = "Start Walking Downstairs Calibration";
                    //enable buttons
                    CalibrateWalkingBtn.Enabled = true;
                    CalibrateWalkingUpstairsBtn.Enabled = true;
                }
            };
        }
        #endregion //Methods
    }
}