using Android.Gms.Location;
using System.Collections.Generic;

namespace FYP_Droid.Utilities
{
    /*
     * This class cotains code, (slightly modified) from a Xamarin Andorid Google Services Activity Recognition API Sample
     * https://github.com/xamarin/monodroid-samples/tree/master/google-services/Location/ActivityRecognition
     */
    public class SerializableDetectedActivities : Java.Lang.Object, Java.IO.ISerializable
    {
        #region Fields

        List<DetectedActivity> detectedActivities;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets detectedActivities
        /// </summary>
        public List<DetectedActivity> DetectedActivities
        {
            get
            {
                return detectedActivities;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectedActivities"></param>
        public SerializableDetectedActivities(List<DetectedActivity> detectedActivities)
        {
            //set detected activities 
            this.detectedActivities = detectedActivities;
        }

        #endregion //Methods
    }
}