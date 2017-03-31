using System;

namespace FYP.Business.Models
{
    public class Fall : BaseEntity
    {
        #region Fields

        private string m_accelerometerIds;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Fall()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="date">date of the occurence of the fall</param>
        public Fall(DateTime date)
        {
            this.Date = date;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/sets m_accelerometerIds
        /// </summary>
        public string AccelerometerIds
        {
            get
            {
                return m_accelerometerIds;
            }

            set
            {
                m_accelerometerIds = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Writes the important parts of the class to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Date.Day + "/" + this.Date.Month + "/" + this.Date.Year + " " + this.Date.Hour + ":" + this.Date.Minute;
        }

        #endregion //Methods
    }
}
