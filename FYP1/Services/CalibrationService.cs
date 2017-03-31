using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using FYP.Business.Models;
using FYP_Droid.Utilities;

namespace FYP_Droid.Services
{
    [Service]
    public class CalibrationService : Service, ISensorEventListener
    {

        #region Fields

        private SensorManager m_sensorManager;
        private CalibrationSample m_calibrationDataSample;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_sensorManager
        /// </summary>
        public SensorManager SensorManager
        {
            get
            {
                return m_sensorManager;
            }

            set
            {
                m_sensorManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_calibrationDataSample
        /// </summary>
        public CalibrationSample CalibrationDataSample
        {
            get
            {
                return m_calibrationDataSample;
            }

            set
            {
                m_calibrationDataSample = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="flags"></param>
        /// <param name="startId"></param>
        /// <returns></returns>
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            //create new calibration sample
            CalibrationDataSample = new CalibrationSample();
            //starting accelerometer
            //if walking sample
            if (intent.HasExtra("Walking"))
            {
                CalibrationDataSample.CalibrationDataType = CalibrationType.Walking;
                CalibrationDataSample.ID = 0;
            }
            //if waslking downstairs sample
            if (intent.HasExtra("Downstairs"))
            {
                CalibrationDataSample.CalibrationDataType = CalibrationType.Walking;
                CalibrationDataSample.ID = 1;
            }
            //if walking upstaris sample
            if (intent.HasExtra("Upstairs"))
            {
                CalibrationDataSample.CalibrationDataType = CalibrationType.Walking;
                CalibrationDataSample.ID = 2;
            }
            //setup variables
            SetupVariables();
            //register listeners
            RegisterListeners();

            return StartCommandResult.Sticky;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override IBinder OnBind(Intent intent)
        {

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnDestroy()
        {
            //unregister listeners
            UnregisterListeners();
            ///save calibration sample
            GlobalUtilities.CalibrationManager.SaveCalibrationSample(CalibrationDataSample);
        }

        /// <summary>
        /// Setup variables
        /// </summary>
        private void SetupVariables()
        {
            //setup sensor manager
            SensorManager = (SensorManager)GetSystemService(Context.SensorService);
        }

        /// <summary>
        /// Register Listeners
        /// </summary>
        private void RegisterListeners()
        {
            //register accelerometer listener
            SensorManager.RegisterListener(this, SensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Ui);
        }

        /// <summary>
        /// Unregister Listeners
        /// </summary>
        private void UnregisterListeners()
        {
            //unregister accelerometer listener
            SensorManager.UnregisterListener(this, SensorManager.GetDefaultSensor(SensorType.Accelerometer));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="accuracy"></param>
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void OnSensorChanged(SensorEvent e)
        {
            //create new reading from data
            AccelerometerReading reading = new AccelerometerReading(e.Values[0], e.Values[1], e.Values[2]);
            //add accelerometer to calibration sample
            CalibrationDataSample.AccelerometerIds += reading.ID + ",";
        }

        #endregion //Methods

    }
}