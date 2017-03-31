using System;

namespace FYP.Business.Managers
{
    public class AlarmCreatedArgs : EventArgs
    {
        #region Fields

        private bool m_created;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="created"> bool indicating that an alarm was created</param>
        public AlarmCreatedArgs(bool created)
        {
            this.Created = created;
        }

        #endregion //Construtors

        #region Property Accessors

        /// <summary>
        /// Gets/sets m_created
        /// </summary>
        public bool Created
        {
            get
            {
                return m_created;
            }

            set
            {
                m_created = value;
            }
        }

        #endregion //Property Accessors
    }
}
