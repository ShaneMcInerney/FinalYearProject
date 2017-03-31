using FYP.Business.Models;
using System;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface IAccelerometerManager
    {
        /// <summary>
        /// Get accelecrometer reading from database
        /// </summary>
        /// <param name="id">id of accelerometer reading to retrieve</param>
        /// <returns>instance f the accelerometer reading class</returns>
        AccelerometerReading GetAccelerometerReading(int id);


        /// <summary>
        /// Get all accelerometer readings from the database
        /// </summary>
        /// <returns>Ienumerble of accelerometer readings</returns>
        IEnumerable<AccelerometerReading> GetAllAccelerometerReadings();


        /// <summary>
        /// Get all accelerometer readings for date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        IEnumerable<AccelerometerReading> GetAccelerometerReadingsForDate(DateTime date);


        /// <summary>
        /// Get readings for date and given hour
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        IEnumerable<AccelerometerReading> GetAccelerometerReadingsForDateAndHour(DateTime date, TimeSpan hour);


        /// <summary>
        /// Get radings for given date, between hours
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        IEnumerable<AccelerometerReading> GetAccelerometerReadingsForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime);


        /// <summary>
        /// Get accelerometer readings betweeen iven dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        IEnumerable<AccelerometerReading> GetAccelerometerReadingsBetweenDates(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Save acclerometer reding to the DB
        /// </summary>
        /// <param name="accelerometerReading"></param>
        /// <returns>id of saved reading</returns>
        int SaveAccelerometerReading(AccelerometerReading accelerometerReading);

        /// <summary>
        /// Save accelerometer readings in bulk
        /// </summary>
        void BulkSaveAcclerometerReadings();


        /// <summary>
        /// Delete an accelerometer from the database
        /// </summary>
        /// <param name="id">id of reading to delete</param>
        /// <returns>id of the deleted reading</returns>
        int DeleteAccelerometerReading(int id);


        /// <summary>
        /// Delete all readings in the database
        /// </summary>
        void DeleteAllAccelerometerReadings();


        /// <summary>
        /// Delete all acccelerometer readings for the given date
        /// </summary>
        /// <param name="date">date to delete readings for</param>
        void DeleteAccelerometerReadingsForDate(DateTime date);


        /// <summary>
        /// Delete all accelerometer readings between the given dates
        /// </summary>
        /// <param name="startDate">the begining of the period to delete for</param>
        /// <param name="endDate"> the end of the period to delete for</param>
        void DeleteAccelerometerReadingsBetweenDates(DateTime startDate, DateTime endDate);


        /// <summary>
        /// Delete all accelerometer readings between hours fr the given date
        /// </summary>
        /// <param name="date">the date to delete redings for</param>
        /// <param name="startTime">the begining of the period to delete for</param>
        /// <param name="endTime">the end of the period to delete for</param>
        void DeleteAccelerometerReadingsForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime);


        /// <summary>
        /// Delete accelerometer readings for date, for given hour
        /// </summary>
        /// <param name="date">date to delete for</param>
        /// <param name="time">time to delete for</param>
        void DeleteAccelerometerReadingsForDateAndForHour(DateTime date, TimeSpan time);


        /// <summary>
        /// Delete all accelerometer readings in a list
        /// </summary>
        /// <param name="listToDelete">the list of readings to delete</param>
        void DeleteAllAccelerometerReadingsInList(IEnumerable<AccelerometerReading> listToDelete);

        /// <summary>
        /// Export readings to a csv 
        /// </summary>
        /// <returns>filename of exported csv</returns>
        string ExportReadingsToCsv(string fileName);


        /// <summary>
        /// Delete csv file containing accelerometer readings
        /// </summary>
        void DeleteAccelerometerReadingsCsvFile();


        /// <summary>
        /// Check if an accelerometer reading exists in the app database
        /// </summary>
        /// <returns></returns>
        bool AccelerometerReadingExists();


    }
}
