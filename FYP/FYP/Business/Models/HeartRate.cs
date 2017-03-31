namespace FYP.Business.Models
{
    public class HeartRate : BaseEntity
    {
        #region Fields

        private double m_beatsPerMinute;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_beatsPerMinute
        /// </summary>
        public double BeatsPerMinute
        {
            get
            {
                return m_beatsPerMinute;
            }

            set
            {
                m_beatsPerMinute = value;
            }
        }

        #endregion //Property Accessors
    }
}
