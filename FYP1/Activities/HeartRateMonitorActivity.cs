using Android.App;
using Android.Hardware;
using Android.Media;
using Android.OS;
using Android.Views;
using Android.Widget;
using ApxLabs.FastAndroidCamera;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FYP_Droid.Activities
{
    [Activity(Label = "HeartRateMonitorActivity")]
    public class HeartRateMonitorActivity : Activity, Camera.IPreviewCallback, ImageReader.IOnImageAvailableListener
    {
        #region Fields

        private Camera.Parameters m_parameters;
        private Stopwatch m_heartRateTimer;
        private AlertDialog.Builder m_alertBuilder;
        private Dialog m_dialog;
        private Camera m_camera;
        private SurfaceView m_textureView;
        private Button m_checkHeartRate;
        private bool m_cameraFlashLightOn;
        private MediaRecorder m_recorder;
        string m_path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/heartTest.mp4";
        private List<Android.Graphics.Bitmap> m_bitmapList;
        private List<int> m_avgRedValues;
        private ImageReader m_heartRateReader;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_camera
        /// </summary>
        public Camera Camera
        {
            get
            {
                return m_camera;
            }

            set
            {
                m_camera = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_textureView
        /// </summary>
        public SurfaceView TextureView
        {
            get
            {
                return m_textureView;
            }

            set
            {
                m_textureView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_checkHeartRate
        /// </summary>
        public Button CheckHeartRate
        {
            get
            {
                return m_checkHeartRate;
            }

            set
            {
                m_checkHeartRate = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_cameraFlashLightOn
        /// </summary>
        public bool CameraFlashLightOn
        {
            get
            {
                return m_cameraFlashLightOn;
            }

            set
            {
                m_cameraFlashLightOn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_recorder
        /// </summary>
        public MediaRecorder Recorder
        {
            get
            {
                return m_recorder;
            }

            set
            {
                m_recorder = value;
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
        /// Gets/Sets m_heartRateTimer
        /// </summary>
        public Stopwatch HeartRateTimer
        {
            get
            {
                return m_heartRateTimer;
            }

            set
            {
                m_heartRateTimer = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_parameters
        /// </summary>
        public Camera.Parameters Parameters
        {
            get
            {
                return m_parameters;
            }

            set
            {
                m_parameters = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_bitmapList
        /// </summary>
        public List<Android.Graphics.Bitmap> BitmapList
        {
            get
            {
                return m_bitmapList;
            }

            set
            {
                m_bitmapList = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_avgRedValues
        /// </summary>
        public List<int> AvgRedValues
        {
            get
            {
                return m_avgRedValues;
            }

            set
            {
                m_avgRedValues = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_heartRateReader
        /// </summary>
        public ImageReader HeartRateReader
        {
            get
            {
                return m_heartRateReader;
            }

            set
            {
                m_heartRateReader = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //set layout
            SetContentView(Resource.Layout.HeartRateMonitorActivity);
            //setup ui variables
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
        }

        /// <summary>
        /// Setting up UI variables to be used within this class
        /// </summary>
        private void SetupUIVariables()
        {
            TextureView = FindViewById<SurfaceView>(Resource.Id.textureStream);
            CheckHeartRate = FindViewById<Button>(Resource.Id.CheckHeartRate);
        }

        /// <summary>
        /// Setting up variables to be used within this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            AvgRedValues = new List<int>();
            HeartRateTimer = new Stopwatch();

            BitmapList = new List<Android.Graphics.Bitmap>();
            AlertBuilder = new AlertDialog.Builder(this);
        }

        /// <summary>
        /// Wrapper method to handle all event delegates
        /// </summary>
        private void EventHandlers()
        {
            //check heart rate button clicked
            CheckHeartRate.Click += (s, e) =>
              {
                  //set alert builder title
                  AlertBuilder.SetTitle("Heart Rate Monitor");
                  //set alert message
                  AlertBuilder.SetMessage("Place your index finger over the front facing camera and flashlight, leave your finger in place, until the camera");
                  //set positive button action
                  AlertBuilder.SetPositiveButton("Begin", (senderAlert, args) =>
                  {
                      //start camera
                      StartCamera();
                      //toast user
                      Toast.MakeText(this, "Starting Heart Rate Monitor!", ToastLength.Short).Show();
                  });
                  //set negative action
                  AlertBuilder.SetNegativeButton("Cancel", (senderAlert, args) =>
                  {
                      //toast user
                      Toast.MakeText(this, "Cancelled Heart Rate Monitoring!", ToastLength.Short).Show();
                  });
                  //create dialog
                  Dialog dialog = AlertBuilder.Create();
                  //show dialog
                  dialog.Show();
              };
        }

        /// <summary>
        /// Stop previewing video
        /// </summary>
        private void StopRecordingVideo()
        {
            //set falsh mode parameter
            Parameters.FlashMode = Camera.Parameters.FlashModeOff;
            //set camera parameteres
            Camera.SetParameters(Parameters);
            //stop camera preview
            Camera.StopPreview();
            //set flashlight on to false
            CameraFlashLightOn = false;
            //stop heart rate timer
            HeartRateTimer.Stop();

            //vars for processing frames
            var listOfAvgRedVals = new List<int>();
            int pixel = 0;
            int redValue = 0;
            var newTimer = new Stopwatch();

            /*    //start new timer
                newTimer.Start();
                //itterate through each frame
                foreach (var img in BitmapList)
                {
                    //var to store total red value of image
                    int imgTotalRed = 0;
                    //process img width
                    for (int i = 0; i < img.Width; i++)
                    {
                        //process image height
                        for (int j = 0; j < img.Height; j++)
                        {
                            //get current pixel
                            pixel = img.GetPixel(i, j);
                            //get red value for pixel
                            redValue = Android.Graphics.Color.GetRedComponent(pixel);
                            //adding to total red value of image
                            imgTotalRed += redValue;
                        }
                    }
                    //add the images average red value to list of average red values
                    listOfAvgRedVals.Add(imgTotalRed / (img.Width * img.Height));
                    //dispose of image
                    img.Dispose();
                }
                //stop new timer
                newTimer.Stop();
                //toaast user
                Toast.MakeText(this, newTimer.Elapsed.ToString(), ToastLength.Long).Show();*/
            // Camera = Camera.Dispose();
        }

        /// <summary>
        /// Start Camera
        /// </summary>
        public void StartCamera()
        {
            //open camera
            Camera = Camera.Open();
            //get camera parameters
            Parameters = Camera.GetParameters();
            //if flashlight is off
            if (!CameraFlashLightOn)
            {
                //set flashmode
                Parameters.FlashMode = Camera.Parameters.FlashModeTorch;
                //set framerate
                Parameters.PreviewFrameRate = 30;
            }
            //set camera parameters
            Camera.SetParameters(Parameters);
            //set display
            Camera.SetPreviewDisplay(TextureView.Holder);
            //start camera preview
            Camera.StartPreview();
            //start heart rate timer
            HeartRateTimer.Start();
            //set number of bytes
            int numBytes = (Parameters.PreviewSize.Width * Parameters.PreviewSize.Height * Android.Graphics.ImageFormat.GetBitsPerPixel(Parameters.PreviewFormat)) / 8;
            //creat new image reader
            HeartRateReader = ImageReader.NewInstance(Parameters.PreviewSize.Width, Parameters.PreviewSize.Height, Android.Graphics.ImageFormatType.Yuv420888, 4);
            //add bytes to callback buffer
            for (uint i = 0; i < 30; ++i)
            {
                using (FastJavaByteArray buffer = new FastJavaByteArray(numBytes))
                {
                    Camera.AddCallbackBuffer(new FastJavaByteArray(numBytes));
                }
            }
            //set preview callback buffer
            Camera.SetPreviewCallbackWithBuffer(this);
        }


        /// <summary>
        /// when a frame is previewed add it to list of images to process
        /// </summary>
        /// <param name="data">byte array to decode to image</param>
        /// <param name="camera">instance of the camera class</param>
        public async void OnPreviewFrame(byte[] data, Camera camera)
        {
            //if the heart rate timer has recorded for 15 seconds
            if (HeartRateTimer.ElapsedMilliseconds >= 15000)
            {
                //reset timer
                HeartRateTimer.Reset();
                //stop timer
                HeartRateTimer.Stop();
                //stop recording video
                StopRecordingVideo();
            }

            using (System.IO.MemoryStream outStream = new System.IO.MemoryStream())
            {
                //setting preview size
                var previewSize = Camera.GetParameters().PreviewSize;
                //setting cam format
                var camFormat = Camera.GetParameters().PreviewFormat;
                //creating new yuv image
                Android.Graphics.YuvImage yuvImage = new Android.Graphics.YuvImage(data, camFormat, previewSize.Width, previewSize.Height, null);
                //compress yuv image to jpg, to outstream
                await yuvImage.CompressToJpegAsync(new Android.Graphics.Rect(0, 0, 320, 240), 100, outStream);
                //create image from stream of bites
                var imageBytes = outStream.ToArray();
                var options = new Android.Graphics.BitmapFactory.Options();
                options.InSampleSize = 4;
                Android.Graphics.Bitmap bitmap = await Android.Graphics.BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length, options);
                //add image to bitmaplist
                BitmapList.Add(bitmap);
                //add data to callback buffer
                camera.AddCallbackBuffer(data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void OnImageAvailable(ImageReader reader)
        {

        }





        #endregion //Methods
    }
}