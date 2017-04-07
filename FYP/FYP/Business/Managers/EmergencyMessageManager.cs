using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business.Managers
{
    public class EmergencyMessageManager:BaseManager
    {
        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Constructors

        public EmergencyMessageManager()
        {

        }

        public EmergencyMessageManager(AppDatabase database)
        {
            this.m_appDatabase = database;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_appDatabase
        /// </summary>
        public AppDatabase AppDatabase
        {
            get
            {
                return m_appDatabase;
            }

            set
            {
                m_appDatabase = value;
            }
        }

        #endregion //Property Accessors

        #region Mehtods

        /// <summary>
        /// Get emergency message from db
        /// </summary>
        /// <returns>Emergency message saved in the app DB</returns>
        public EmergencyMessage GetEmergencyMessage()
        {
            return AppDatabase.GetEmergencyMessage();
        }

        /// <summary>
        /// Save emergency message in db
        /// </summary>
        /// <param name="emergencyMessage"></param>
        public void SaveEmergencyMessage(EmergencyMessage emergencyMessage)
        {
            AppDatabase.SaveEmergencyMessage(emergencyMessage);
        }

        /// <summary>
        /// Delete emergency message from db
        /// </summary>
        public void DeleteEmergencyMessage()
        {
            AppDatabase.DeleteEmergencyMessage();
        }

        /// <summary>
        /// Check if emergency message exists in db
        /// </summary>
        /// <returns></returns>
        public bool EmergencyMessageExists()
        {
            return AppDatabase.EmergencyMessageExists();
        }

        #endregion //Methods
    }
}
