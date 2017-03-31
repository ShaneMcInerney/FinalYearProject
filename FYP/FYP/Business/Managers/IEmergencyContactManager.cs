using FYP.Business.Models;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface IEmergencyContactManager
    {
        /// <summary>
        /// Get emergency contact by id
        /// </summary>
        /// <param name="id">id of contact to retrieve</param>
        /// <returns>instance of the emergency contact class</returns>
        EmergencyContact GetEmergencyContact(int id);


        /// <summary>
        /// Get all emergency contacts in db
        /// </summary>
        /// <returns>Inumerable of emergency contacts in database</returns>
        IEnumerable<EmergencyContact> GetAllEmergencyContacts();


        /// <summary>
        /// Save emergency contact to the DB
        /// </summary>
        /// <param name="emergencyContact"></param>
        /// <returns>Id of savedcontact</returns>
        int SaveEmergencyContact(EmergencyContact emergencyContact);


        /// <summary>
        /// Delete emergency contact in DB by ID
        /// </summary>
        /// <param name="id">ID of contact to delete</param>
        /// <returns>ID of deleted contact</returns>
        int DeleteEmergencyContact(int id);


        /// <summary>
        /// Delete all contacts in the DB
        /// </summary>
        void DeleteAllEmergencyContacts();


        /// <summary>
        /// Check if emergency contact exists in the DB
        /// </summary>
        /// <returns>bool indicating the existence of an emergency contact</returns>
        bool EmergencyContactExists();

    }
}
