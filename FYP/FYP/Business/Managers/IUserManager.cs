using FYP.Business.Models;

namespace FYP.Business.Managers
{
    public interface IUserManager
    {

        /// <summary>
        /// Get User from db
        /// </summary>
        /// <returns></returns>
        User GetUser();

        /// <summary>
        /// Save/update user in db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int SaveUser(User user);

        /// <summary>
        /// Delete User form DB
        /// </summary>
        /// <returns></returns>
        void DeleteUser();

        /// <summary>
        /// Check user exists in DB
        /// </summary>
        /// <returns>bool based on user existence in DB</returns>
        bool UserExists();
    }
}
