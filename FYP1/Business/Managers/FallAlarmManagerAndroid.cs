using Android.Content;
using Android.Media;
using FYP.Business.Managers;

namespace FYP_Droid.Business.Managers
{
    public class FallAlarmManagerAndroid : IFallAlarmManager
    {

        #region Fields

        MediaPlayer m_mediaPlayer;
        AudioManager m_audioManager;
        private bool m_isPlaying = false;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FallAlarmManagerAndroid()
        {

        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_mediaPlayer
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get
            {
                return m_mediaPlayer;
            }

            set
            {
                m_mediaPlayer = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_audioManager
        /// </summary>
        public AudioManager AudioManager
        {
            get
            {
                return m_audioManager;
            }

            set
            {
                m_audioManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_isPlaying
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return m_isPlaying;
            }

            set
            {
                m_isPlaying = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Prepare Media Player
        /// </summary>
        /// <param name="context"></param>
        public void SetupMediaPlayer(Context context)
        {
            this.m_audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            this.m_audioManager.SetStreamVolume(Stream.Music, m_audioManager.GetStreamMaxVolume(Stream.Music), 0);
            this.m_mediaPlayer = MediaPlayer.Create(context, Resource.Raw.AlarmSound);

        }

        /// <summary>
        /// Play fall alarm
        /// </summary>
        public void PlayFallAlarm()
        {
            this.IsPlaying = true;
            this.MediaPlayer.Looping = true;
            this.m_mediaPlayer.Start();
        }

        /// <summary>
        /// Stop fall alarm playing
        /// </summary>
        public void StopFallAlarm()
        {
            this.m_mediaPlayer.Stop();
            this.IsPlaying = false;
        }

        #endregion //Methods

    }
}