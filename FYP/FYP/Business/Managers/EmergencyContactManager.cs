using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business.Managers
{
    public class EmergencyContactManager:BaseManager
    {
        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmergencyContactManager()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="database">Instance of the applcation database</param>
        public EmergencyContactManager(AppDatabase database)
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
        /// Get emergency contact by id
        /// </summary>
        /// <param name="id">id of contact to retrieve</param>
        /// <returns>instance of the emergency contact class</returns>
        public EmergencyContact GetEmergencyContact(int id)
        {
            return AppDatabase.GetEmergencyContact(id);
        }

        /// <summary>
        /// Get all emergency contacts in db
        /// </summary>
        /// <returns>Inumerable of emergency contacts in database</returns>
        public IEnumerable<EmergencyContact> GetAllEmergencyContacts()
        {
            return AppDatabase.GetAllEmergencyContacts();
        }

        /// <summary>
        /// Save emergency contact to the DB
        /// </summary>
        /// <param name="emergencyContact"></param>
        /// <returns>Id of savedcontact</returns>
        public int SaveEmergencyContact(EmergencyContact emergencyContact)
        {
            return AppDatabase.SaveEmergencyContact(emergencyContact);
        }

        /// <summary>
        /// Delete emergency contact in DB by ID
        /// </summary>
        /// <param name="id">ID of contact to delete</param>
        /// <returns>ID of deleted contact</returns>
        public int DeleteEmergencyContact(int id)
        {
            return AppDatabase.DeleteEmergencyContact(id);
        }

        /// <summary>
        /// Delete all contacts in the DB
        /// </summary>
        public void DeleteAllEmergencyContacts()
        {
            AppDatabase.DeleteAllEmergencyContacts();
        }

        /// <summary>
        /// Check if emergency contact exists in the DB
        /// </summary>
        /// <returns>bool indicating the existence of an emergency contact</returns>
        public bool EmergencyContactExists()
        {
            return AppDatabase.EmergencyContactExists();
        }
        #endregion //Methods
    }
}
