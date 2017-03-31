using FYP.Business.Models;

namespace FYP.Business.Managers
{
    public interface IEmergencyMessageManager
    {
        /// <summary>
        /// Retrieve the emergency message from the db
        /// </summary>
        /// <returns>stored emergency message</returns>
        EmergencyMessage GetEmergencyMessage();

        /// <summary>
        /// Save/Update emergency message
        /// </summary>
        /// <param name="emergencyMessage">Emergency message to saave</param>
        void SaveEmergencyMessage(EmergencyMessage emergencyMessage);

        /// <summary>
        /// Delete stored emergency message
        /// </summary>
        void DeleteEmergencyMessage();

        /// <summary>
        /// Check if emergency message exists in db
        /// </summary>
        /// <returns>bool whether or not emergency message exists in the db</returns>
        bool EmergencyMessageExists();
    }
}
