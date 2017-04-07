using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;



namespace FYP.Droid
{
    

    [Activity(Label = "Activity1")]
    public class Activity1 : Activity, ISensorEventListener
    {
        private const int UpdateInterval = 20;

        static readonly object _syncLock = new object();
        SensorManager _sensorManager;
        TextView _sensorTextView;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
          
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
               
            }

          /*  lock (_syncLock)
            {
                _sensorTextView.Text = string.Format("x={0:f}, y={1:f}, y={2:f}",
               e.Values[0], e.Values[1], e.Values[2]);
            }*/
        }

       

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainMenu);
            _sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            _sensorTextView = FindViewById<TextView>(Resource.Id.accelerometer_text);
            /* CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Fastest);


             CrossDeviceMotion.Current.SensorValueChanged += (s,a) =>
             {

                 switch (a.SensorType)
                 {
                     case MotionSensorType.Accelerometer:
                         _sensorTextView.Text = string.Format("A: {0},{1},{2}", ((MotionVector)a.Value).X, ((MotionVector)a.Value).Y, ((MotionVector)a.Value).Z);
                         break;
                     case MotionSensorType.Compass:
                         _sensorTextView.Text = string.Format("H: {0}", a.Value);
                         break;
                 }
             };*/
        }

        protected override void OnResume()
        {
            base.OnResume();
            _sensorManager.RegisterListener(this,

           _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
            SensorDelay.Ui);
        }

      

     

    }
}