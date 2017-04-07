using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business.Managers
{
   public class UserManager
    {
        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserManager()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="database"></param>
        public UserManager(AppDatabase database)
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

        #region Methods

        /// <summary>
        /// Delete user from DB
        /// </summary>
        public void DeleteUser()
        {
            AppDatabase.DeleteUser();
        }

        /// <summary>
        /// Get user from databse
        /// </summary>
        /// <returns>user from database</returns>
        public User GetUser()
        {
            return AppDatabase.GetUser();
        }

        /// <summary>
        /// Save user to DB
        /// </summary>
        /// <param name="user">User to save</param>
        /// <returns>id of saved user</returns>
        public int SaveUser(User user)
        {
            return AppDatabase.SaveUser(user);
        }

        /// <summary>
        /// Check user exists in DB
        /// </summary>
        /// <returns>bool based on user existence in DB</returns>
        public bool UserExists()
        {
            return AppDatabase.UserExists();
        }

        #endregion //Methods
    }
}
