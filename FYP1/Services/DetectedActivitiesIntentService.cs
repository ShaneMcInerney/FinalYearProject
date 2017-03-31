using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Support.V4.Content;
using System.Collections.Generic;
using System.Linq;

namespace FYP_Droid.Services
{
    /*
     * This class cotains code, (slightly modified) from a Xamarin Andorid Google Services Activity Recognition API Sample
     * https://github.com/xamarin/monodroid-samples/tree/master/google-services/Location/ActivityRecognition
     */
    [Service(Exported = false)]
    public class DetectedActivitiesIntentService : IntentService
    {
        #region Fields

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DetectedActivitiesIntentService()
        {

        }

        #endregion //Constructors

        #region Property Accessors

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Handle incoing intent
        /// </summary>
        /// <param name="intent"></param>
        protected override void OnHandleIntent(Intent intent)
        {
            //setting result
            var result = ActivityRecognitionResult.ExtractResult(intent);
            //setting local intent
            var localIntent = new Intent("BROADCAST_ACTION");
            //creating list of detected activities
            IList<DetectedActivity> detectedActivities = result.ProbableActivities;
            //put extra 
            localIntent.PutExtra("ACTIVITY_EXTRA", detectedActivities.ToArray());
            //send broadcast
            LocalBroadcastManager.GetInstance(this).SendBroadcast(localIntent);
        }

        #endregion //Methods





    }
}