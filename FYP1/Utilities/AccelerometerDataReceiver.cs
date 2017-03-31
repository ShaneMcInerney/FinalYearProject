using Android.Content;

namespace FYP_Droid.Utilities
{

    [BroadcastReceiver]
    public class AccelerometerDataReceiver : BroadcastReceiver
    {
        #region Fields

        private double m_receivedVector;
        public delegate void SomeDataReceivedHandler();
        public event SomeDataReceivedHandler DataReceivedEvent;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_receivedVector
        /// </summary>
        public double ReceivedVector
        {
            get
            {
                return m_receivedVector;
            }

            set
            {
                m_receivedVector = value;
            }
        }

        #endregion //Property Accessors

        #region Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        public override void OnReceive(Context context, Intent intent)
        {

            ReceivedVector = intent.GetDoubleExtra("reading", 0.0);
            DataReceivedEvent();

        }

        #endregion //Methods

    }
}