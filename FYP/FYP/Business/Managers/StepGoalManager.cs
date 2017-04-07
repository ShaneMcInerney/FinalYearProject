using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Business.Managers
{
    public class StepGoalManager:BaseManager
    {

        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Contructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public StepGoalManager()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="database"></param>
        public StepGoalManager(AppDatabase database)
        {
            AppDatabase = database;
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
        /// Get step goal in the DB
        /// </summary>
        /// <param name="id">ID of the step goal to retrieve</param>
        /// <returns>insance of the step goal class</returns>
        public StepGoal GetStepGoal(int id)
        {
            return AppDatabase.GetStepGoal(id);
        }

        /// <summary>
        /// Get all setp goals in the DB
        /// </summary>
        /// <returns>all step goals in the DB</returns>
        public IEnumerable<StepGoal> GetAllStepGoals()
        {
            return AppDatabase.GetAllStepGoals();
        }

        /// <summary>
        /// Saves step goal to DB
        /// </summary>
        /// <param name="stepGoal">Step goal to save</param>
        /// <returns>ID of the step goal saved</returns>
        public int SaveStepGoal(StepGoal stepGoal)
        {
            return AppDatabase.SaveStepGoal(stepGoal);
        }

        /// <summary>
        /// Delete step goal in DB
        /// </summary>
        /// <param name="id">Id of entry to be deleted</param>
        /// <returns>ID f deleted entry</returns>
        public int DeleteStepGoal(int id)
        {
            return AppDatabase.DeleteStepGoal(id);
        }

        /// <summary>
        /// Delete all step goals in DB
        /// </summary>
        public void DeleteAllStepGoals()
        {
            AppDatabase.DeleteAllStepGoals();
        }

        /// <summary>
        /// Indicates the existance of the a step goal in the DB
        /// </summary>
        /// <returns>bool indicating existance of step oal</returns>
        public bool StepGoalExists()
        {
            return AppDatabase.StepGoalExists();
        }

        /// <summary>
        /// Get all hourly step goals
        /// </summary>
        /// <returns>hourly step goals</returns>
        public IEnumerable<StepGoal> GetAllHourlyStepGoals()
        {
            return GetAllStepGoals().Where(X => X.GoalType == StepGoalType.Hourly);
        }

        /// <summary>
        /// Get all daily step goals
        /// </summary>
        /// <returns>daily step goals</returns>
        public IEnumerable<StepGoal> GetAllDailyStepGoals()
        {
            return GetAllStepGoals().Where(X => X.GoalType == StepGoalType.Daily);
        }

        /// <summary>
        /// Gets all weekly step goals
        /// </summary>
        /// <returns>all monthly step goals</returns>
        public IEnumerable<StepGoal> GetAllWeeklyStepGoals()
        {
            return GetAllStepGoals().Where(X => X.GoalType == StepGoalType.Weekly);
        }

        /// <summary>
        /// Get all monthly step goals
        /// </summary>
        /// <returns>all monthly step goals</returns>
        public IEnumerable<StepGoal> GetAllMonthlyStepGoals()
        {
            return GetAllStepGoals().Where(X => X.GoalType == StepGoalType.Monthly);
        }

        /// <summary>
        /// Get all yearly step goals
        /// </summary>
        /// <returns>list of all yearly step goals</returns>
        public IEnumerable<StepGoal> GetAllYearlyStepGoals()
        {
            return GetAllStepGoals().Where(X => X.GoalType == StepGoalType.Yearly);
        }



        #endregion //Methods
    }
}
