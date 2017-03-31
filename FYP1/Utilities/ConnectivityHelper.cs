using Android.Content;
using Android.Net;

namespace FYP_Droid.Utilities
{
    public static class ConnectivityHelper
    {
        #region Fields

        private static ConnectivityManager m_connectivityManager;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_connectivityManager
        /// </summary>
        public static ConnectivityManager ConnectivityManager
        {
            get
            {
                return m_connectivityManager;
            }

            set
            {
                m_connectivityManager = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool ConnectedToNetwork(Context context)
        {
            //setting connectivity manager
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            //setting net info
            NetworkInfo netInfo = connectivityManager.ActiveNetworkInfo;
            //if net info is null
            if (netInfo == null)
            {
                return false;
            }
            else
            {
                //set online bool
                bool isOnline = netInfo.IsConnected;

                return isOnline;
            }
        }

        #endregion //Methods

    }
}