namespace FYP.Business.Models
{
    public class EmergencyMessage : BaseEntity
    {
        #region Fields

        private string m_message;
        private bool m_includeLocation;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmergencyMessage()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="m_message"> message body </param>
        /// <param name="m_includeLocation"> to include location or not</param>
        public EmergencyMessage(string m_message, bool m_includeLocation)
        {
            this.Message = m_message;
            this.IncludeLocation = m_includeLocation;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_message
        /// </summary>
        public string Message
        {
            get
            {
                return m_message;
            }

            set
            {
                m_message = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_includeLocation
        /// </summary>
        public bool IncludeLocation
        {
            get
            {
                return m_includeLocation;
            }

            set
            {
                m_includeLocation = value;
            }
        }

        #endregion //Property Accessors

        #region Methods



        #endregion //Methods
    }
}
