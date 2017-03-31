using FYP.Business.Models;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface IStepGoalManager
    {
        /// <summary>
        /// Get step goal in the DB
        /// </summary>
        /// <param name="id">ID of the step goal to retrieve</param>
        /// <returns>insance of the step goal class</returns>
        StepGoal GetStepGoal(int id);


        /// <summary>
        /// Get all setp goals in the DB
        /// </summary>
        /// <returns>all step goals in the DB</returns>
        IEnumerable<StepGoal> GetAllStepGoals();


        /// <summary>
        /// Saves step goal to DB
        /// </summary>
        /// <param name="stepGoal">Step goal to save</param>
        /// <returns>ID of the step goal saved</returns>
        int SaveStepGoal(StepGoal stepGoal);


        /// <summary>
        /// Delete step goal in DB
        /// </summary>
        /// <param name="id">Id of entry to be deleted</param>
        /// <returns>ID f deleted entry</returns>
        int DeleteStepGoal(int id);


        /// <summary>
        /// Delete all step goals in DB
        /// </summary>
        void DeleteAllStepGoals();


        /// <summary>
        /// Indicates the existance of the a step goal in the DB
        /// </summary>
        /// <returns>bool indicating existance of step oal</returns>
        bool StepGoalExists();


        /// <summary>
        /// Get all hourly step goals
        /// </summary>
        /// <returns>hourly step goals</returns>
        IEnumerable<StepGoal> GetAllHourlyStepGoals();


        /// <summary>
        /// Get all daily step goals
        /// </summary>
        /// <returns>daily step goals</returns>
        IEnumerable<StepGoal> GetAllDailyStepGoals();


        /// <summary>
        /// Gets all weekly step goals
        /// </summary>
        /// <returns>all monthly step goals</returns>
        IEnumerable<StepGoal> GetAllWeeklyStepGoals();


        /// <summary>
        /// Get all monthly step goals
        /// </summary>
        /// <returns>all monthly step goals</returns>
        IEnumerable<StepGoal> GetAllMonthlyStepGoals();


        /// <summary>
        /// Get all yearly step goals
        /// </summary>
        /// <returns>list of all yearly step goals</returns>
        IEnumerable<StepGoal> GetAllYearlyStepGoals();


    }
}
