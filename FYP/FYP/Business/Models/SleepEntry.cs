using System;

namespace FYP.Business.Models
{
    /// <summary>
    /// enum for handling sleep rating
    /// </summary>
    public enum SleepQualityRating
    {
        VeryBad,
        Bad,
        Neutral,
        Good,
        VeryGood
    }

    public class SleepEntry : BaseEntity
    {
        #region Fields

        private TimeSpan m_sleepLength;
        private int m_sleepQuality;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SleepEntry()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="sleepLength"> lenght of time user slept for</param>
        /// <param name="sleepQuality"> quality of sleep achieved by user</param>
        /// <param name="date"> date of the sleep entry</param>
        public SleepEntry(TimeSpan sleepLength, int sleepQuality, DateTime date)
        {
            this.m_sleepLength = sleepLength;
            this.m_sleepQuality = sleepQuality;
            this.Date = date;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_sleepLength
        /// </summary>
        public TimeSpan SleepLength
        {
            get
            {
                return m_sleepLength;
            }

            set
            {
                m_sleepLength = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepQuality
        /// </summary>
        public int SleepQuality
        {
            get
            {
                return m_sleepQuality;
            }

            set
            {
                m_sleepQuality = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Outputs class to string
        /// </summary>
        /// <returns>class in string format</returns>
        public override string ToString()
        {
            return "Date: " + this.Date.Day + "/" + this.Date.Month + "/" + this.Date.Year + " Length: " + this.SleepLength.Hours + ":" + this.SleepLength.Minutes;
        }

        #endregion //Methods
    }
}
