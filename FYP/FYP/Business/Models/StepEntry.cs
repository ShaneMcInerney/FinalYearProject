using System;

namespace FYP.Business.Models
{
    public class StepEntry : BaseEntity
    {
        #region Fields

        private Int64 m_count;
        private TimeSpan m_time;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public StepEntry()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="m_count"> number of steps taken</param>
        /// <param name="date">date of step entry</param>
        /// <param name="m_time"> time for entry</param>
        public StepEntry(long m_count, DateTime date, TimeSpan m_time)
        {
            this.m_count = m_count;
            this.m_time = m_time;
            this.Date = date;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_count
        /// </summary>
        public long Count
        {
            get
            {
                return m_count;
            }

            set
            {
                m_count = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_time
        /// </summary>
        public TimeSpan Time
        {
            get
            {
                return m_time;
            }

            set
            {
                m_time = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// To string override method, basically a to csv line method
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            var output = this.Date.Day + "/" + this.Date.Month + "/" + this.Date.Year + " " + this.Date.Hour + ":00" + ", Steps: " + this.Count;
            return output;
        }

        #endregion //Methods
    }
}
