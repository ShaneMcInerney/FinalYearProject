namespace FYP.Business.Models
{
    public class EmergencyContact : BaseEntity
    {
        #region fields

        private string m_name;
        private string m_emailAddress;
        private string m_contactNumber;

        #endregion //fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmergencyContact()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="m_name"> name of contact</param>
        /// <param name="m_emailAddress">email address of contact</param>
        /// <param name="m_contactNumber">contact number</param>
        public EmergencyContact(string m_name, string m_emailAddress, string m_contactNumber)
        {
            this.m_name = m_name;
            this.m_emailAddress = m_emailAddress;
            this.m_contactNumber = m_contactNumber;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_name
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }

            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_emailAddress
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return m_emailAddress;
            }

            set
            {
                m_emailAddress = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_contactNumber
        /// </summary>
        public string ContactNumber
        {
            get
            {
                return m_contactNumber;
            }

            set
            {
                m_contactNumber = value;
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
            var output = this.Name;
            return output;
        }

        #endregion //Methods
    }
}
