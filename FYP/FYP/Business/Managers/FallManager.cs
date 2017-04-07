using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business.Managers
{
    public class FallManager:BaseManager
    {
        #region Fields

        private AppDatabase m_appDatebase;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FallManager()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="appDatabase"></param>
        public FallManager(AppDatabase appDatabase)
        {
            this.AppDatebase = appDatabase;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_appDatebase
        /// </summary>
        public AppDatabase AppDatebase
        {
            get
            {
                return m_appDatebase;
            }

            set
            {
                m_appDatebase = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Delet all falls in DB
        /// </summary>
        public void DeleteAllFalls()
        {
            this.AppDatebase.DeleteAllFalls();
        }

        /// <summary>
        /// Delete fall in DB
        /// </summary>
        /// <param name="id">ID of fall to delete</param>
        /// <returns>ID of deleted fall</returns>
        public int DeleteFall(int id)
        {
            return this.AppDatebase.DeleteFall(id);
        }

        /// <summary>
        /// If fall entry exists in DB
        /// </summary>
        /// <returns></returns>
        public bool FallExists()
        {
            return this.AppDatebase.FallExists();
        }

        /// <summary>
        /// Get all falls in the DB
        /// </summary>
        /// <returns>All falls in the DB</returns>
        public IEnumerable<Fall> GetAllFalls()
        {
            return this.AppDatebase.GetAllFalls();
        }

        /// <summary>
        /// Get fall from DB by ID
        /// </summary>
        /// <param name="id">ID of fall to retrieve</param>
        /// <returns>Instance of the fall class with matching ID</returns>
        public Fall GetFall(int id)
        {
            return this.AppDatebase.GetFall(id);
        }

        /// <summary>
        /// Save fall in the aplication's DB
        /// </summary>
        /// <param name="fall">Fall to save to DB</param>
        /// <returns>ID of the saved fal</returns>
        public int SaveFall(Fall fall)
        {
            return this.AppDatebase.SaveFall(fall);
        }

        /// <summary>
        /// Get the last fall in the DB
        /// </summary>
        /// <returns>Last fall in the DB</returns>
        public Fall GetLatestFall()
        {
            return GetAllFalls().LastOrDefault();
        }

        /// <summary>
        /// Get all falls between two dates
        /// </summary>
        /// <param name="start">start of period</param>
        /// <param name="end">end of period</param>
        /// <returns>all fals between the given dates</returns>
        public IEnumerable<Fall> GetFallsBetweenDates(DateTime start, DateTime end)
        {
            return GetAllFalls().Where(x => x.Date.DayOfYear >= start.DayOfYear && x.Date.DayOfYear <= end.DayOfYear);
        }

        /// <summary>
        /// Get all falls for the current month
        /// </summary>
        /// <returns>all falls for current month</returns>
        public IEnumerable<Fall> GetFallsForCurrentMonth()
        {
            //Get start of month
            var startOfTheMonth = GetStartOfMonth();
            var output = GetFallsBetweenDates(startOfTheMonth, DateTime.Now);
            return output;
        }

        /// <summary>
        /// Get all falls for the current week
        /// </summary>
        /// <returns>All falls for the current week</returns>
        public IEnumerable<Fall> GetFallsForCurrentWeek()
        {
            //get start of week
            DateTime startOfWeek = GetStartOfWeek();
            return GetFallsBetweenDates(startOfWeek, DateTime.Now);
        }

        /// <summary>
        /// Get all falls for current year
        /// </summary>
        /// <returns>All falls in the current year</returns>
        public IEnumerable<Fall> GetFallsForCurrentYear()
        {
            //get start of year
            var startOfTheYear = GetStartOfYear();
            var output = GetFallsBetweenDates(startOfTheYear, DateTime.Now);
            return output;
        }

        #endregion //Methods
    }
}
