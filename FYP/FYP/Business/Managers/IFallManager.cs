using FYP.Business.Models;
using System;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface IFallManager
    {

        /// <summary>
        /// Delet all falls in DB
        /// </summary>
        void DeleteAllFalls();


        /// <summary>
        /// Delete fall in DB
        /// </summary>
        /// <param name="id">ID of fall to delete</param>
        /// <returns>ID of deleted fall</returns>
        int DeleteFall(int id);


        /// <summary>
        /// If fall entry exists in DB
        /// </summary>
        /// <returns></returns>
        bool FallExists();


        /// <summary>
        /// Get all falls in the DB
        /// </summary>
        /// <returns>All falls in the DB</returns>
        IEnumerable<Fall> GetAllFalls();


        /// <summary>
        /// Get fall from DB by ID
        /// </summary>
        /// <param name="id">ID of fall to retrieve</param>
        /// <returns>Instance of the fall class with matching ID</returns>
        Fall GetFall(int id);


        /// <summary>
        /// Save fall in the aplication's DB
        /// </summary>
        /// <param name="fall">Fall to save to DB</param>
        /// <returns>ID of the saved fal</returns>
        int SaveFall(Fall fall);


        /// <summary>
        /// Get the last fall in the DB
        /// </summary>
        /// <returns>Last fall in the DB</returns>
        Fall GetLatestFall();


        /// <summary>
        /// Get all falls between two dates
        /// </summary>
        /// <param name="start">start of period</param>
        /// <param name="end">end of period</param>
        /// <returns>all fals between the given dates</returns>
        IEnumerable<Fall> GetFallsBetweenDates(DateTime start, DateTime end);


        /// <summary>
        /// Get all falls for the current month
        /// </summary>
        /// <returns>all falls for current month</returns>
        IEnumerable<Fall> GetFallsForCurrentMonth();


        /// <summary>
        /// Get all falls for the current week
        /// </summary>
        /// <returns>All falls for the current week</returns>
        IEnumerable<Fall> GetFallsForCurrentWeek();

        /// <summary>
        /// Get all falls for current year
        /// </summary>
        /// <returns>All falls in the current year</returns>
        IEnumerable<Fall> GetFallsForCurrentYear();

    }
}
