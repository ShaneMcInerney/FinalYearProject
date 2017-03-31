using SQLite;
using System;

namespace FYP.Business.Models
{
    public abstract class BaseEntity
    {
        #region Fields

        private DateTime m_date;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_date
        /// </summary>
        public DateTime Date
        {
            get
            {
                return m_date;
            }

            set
            {
                m_date = value;
            }
        }

        /// <summary>
        /// Gets/Sets ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        #endregion //Property Accessors

    }
}
