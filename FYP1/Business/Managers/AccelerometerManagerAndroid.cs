using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FYP_Droid.Business.Managers
{
    public class AccelerometerManagerAndroid : IAccelerometerManager
    {
        #region Fields

        private AppDatabase m_appDatebase;
        private StringBuilder m_csv;
        private double m_lowerThreshold = 3;
        private double m_stableThreshold = 9.8;
        private double m_upperThreshold = 20.0;
        private List<AccelerometerReading> m_bulkAccelerometerReadings;
        private double m_lowerTolerance = 0.85;
        private double m_stableTolerance = 2.0;
        private double m_upperTolerance = 5.0;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccelerometerManagerAndroid()
        {

        }

        /// <summary>
        /// Class Constuctor
        /// </summary>
        /// <param name="database">application database instance</param>
        public AccelerometerManagerAndroid(AppDatabase database)
        {
            this.m_appDatebase = database;

            this.m_csv = new StringBuilder();

            this.m_bulkAccelerometerReadings = new List<AccelerometerReading>();
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_appDatebase
        /// </summary>
        public AppDatabase AppDatabase
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

        /// <summary>
        /// Gets/Sets m_csv
        /// </summary>
        public StringBuilder Csv
        {
            get
            {
                return m_csv;
            }

            set
            {
                m_csv = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_lowerThreshold
        /// </summary>
        public double LowerThreshold
        {
            get
            {
                return m_lowerThreshold;
            }

            set
            {
                m_lowerThreshold = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_upperThreshold
        /// </summary>
        public double UpperThreshold
        {
            get
            {
                return m_upperThreshold;
            }

            set
            {
                m_upperThreshold = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_bulkAccelerometerReadings
        /// </summary>
        public List<AccelerometerReading> BulkAccelerometerReadings
        {
            get
            {
                return m_bulkAccelerometerReadings;
            }

            set
            {
                m_bulkAccelerometerReadings = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_lowerTolerance
        /// </summary>
        public double LowerTolerance
        {
            get
            {
                return m_lowerTolerance;
            }

            set
            {
                m_lowerTolerance = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stableTolerance
        /// </summary>
        public double StableTolerance
        {
            get
            {
                return m_stableTolerance;
            }

            set
            {
                m_stableTolerance = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_upperTolerance
        /// </summary>
        public double UpperTolerance
        {
            get
            {
                return m_upperTolerance;
            }

            set
            {
                m_upperTolerance = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stableThreshold
        /// </summary>
        public double StableThreshold
        {
            get
            {
                return m_stableThreshold;
            }

            set
            {
                m_stableThreshold = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        #region DB Methods

        /// <summary>
        /// Get accelecrometer reading from database
        /// </summary>
        /// <param name="id">id of accelerometer reading to retrieve</param>
        /// <returns>instance f the accelerometer reading class</returns>
        public AccelerometerReading GetAccelerometerReading(int id)
        {
            return AppDatabase.GetAccelerometerReading(id);
        }

        /// <summary>
        /// Get all accelerometer readings from the database
        /// </summary>
        /// <returns>Ienumerble of accelerometer readings</returns>
        public IEnumerable<AccelerometerReading> GetAllAccelerometerReadings()
        {
            return AppDatabase.GetAllAccelerometerReadings();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        public IEnumerable<AccelerometerReading> GetAccelerometerReadingsForDate(DateTime date)
        {
            return AppDatabase.GetAllAccelerometerReadings().Where(X => X.Date.Date == date.Date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        public IEnumerable<AccelerometerReading> GetAccelerometerReadingsForDateAndHour(DateTime date, TimeSpan hour)
        {
            return AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date == date.Date && x.Date.Hour == hour.Hours);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        public IEnumerable<AccelerometerReading> GetAccelerometerReadingsForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date == date.Date && x.Date.Hour >= startTime.Hours && x.Date.Hour <= endTime.Hours);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Ienumerble of accelerometer readings</returns>
        public IEnumerable<AccelerometerReading> GetAccelerometerReadingsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accelerometerReading"></param>
        /// <returns>id of saved reading</returns>
        public int SaveAccelerometerReading(AccelerometerReading accelerometerReading)
        {
            //add reading to list of readings to be deleted in bulk
            BulkAccelerometerReadings.Add(accelerometerReading);
            //if there are more than 1000 readings 
            if (BulkAccelerometerReadings.Count >= 1000)
            {
                //save them in bulk
                BulkSaveAcclerometerReadings();
            }

            return 0;
        }

        /// <summary>
        /// Save accelerometer readings in bulk
        /// </summary>
        public void BulkSaveAcclerometerReadings()
        {
            //save readings in bulk
            AppDatabase.BulkAccelerometerReadingsSave(BulkAccelerometerReadings);
            //clear bulk list
            BulkAccelerometerReadings.Clear();
        }

        /// <summary>
        /// Delete an accelerometer from the database
        /// </summary>
        /// <param name="id">id of reading to delete</param>
        /// <returns>id of the deleted reading</returns>
        public int DeleteAccelerometerReading(int id)
        {
            return AppDatabase.DeleteAccelerometerReading(id);
        }

        /// <summary>
        /// Delete all readings in the database
        /// </summary>
        public void DeleteAllAccelerometerReadings()
        {
            AppDatabase.DeleteAllAccelerometerReadings();
        }

        /// <summary>
        /// Delete all acccelerometer readings for the given date
        /// </summary>
        /// <param name="date">date to delete readings for</param>
        public void DeleteAccelerometerReadingsForDate(DateTime date)
        {
            var listToDelete = AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date == date.Date);
            //delete all in list
            DeleteAllAccelerometerReadingsInList(listToDelete);
        }

        /// <summary>
        /// Delete all accelerometer readings between the given dates
        /// </summary>
        /// <param name="startDate">the begining of the period to delete for</param>
        /// <param name="endDate"> the end of the period to delete for</param>
        public void DeleteAccelerometerReadingsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var listToDelete = AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);
            //delete all in list
            DeleteAllAccelerometerReadingsInList(listToDelete);
        }

        /// <summary>
        /// Delete all accelerometer readings between hours fr the given date
        /// </summary>
        /// <param name="date">the date to delete redings for</param>
        /// <param name="startTime">the begining of the period to delete for</param>
        /// <param name="endTime">the end of the period to delete for</param>
        public void DeleteAccelerometerReadingsForDateBetweenHours(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var listToDelete = AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date == date.Date && x.Date.Hour >= startTime.Hours && x.Date.Hour <= endTime.Hours);
            //delete all in list
            DeleteAllAccelerometerReadingsInList(listToDelete);
        }

        /// <summary>
        /// Delete accelerometer readings for date, for given hour
        /// </summary>
        /// <param name="date">date to delete for</param>
        /// <param name="time">time to delete for</param>
        public void DeleteAccelerometerReadingsForDateAndForHour(DateTime date, TimeSpan time)
        {
            var listToDelete = AppDatabase.GetAllAccelerometerReadings().Where(x => x.Date.Date == date.Date && x.Date.Hour == time.Hours);
            //delete all in list
            DeleteAllAccelerometerReadingsInList(listToDelete);
        }

        /// <summary>
        /// Delete all accelerometer readings in a list
        /// </summary>
        /// <param name="listToDelete">the list of readings to delete</param>
        public void DeleteAllAccelerometerReadingsInList(IEnumerable<AccelerometerReading> listToDelete)
        {
            //iterate through list
            foreach (var item in listToDelete)
            {
                //delete item
                AppDatabase.DeleteAccelerometerReading(item.ID);
            }
        }

        /// <summary>
        /// Export readings to a csv 
        /// </summary>
        /// <returns>filename of exported csv</returns>
        public string ExportReadingsToCsv(string fileName)
        {
            //get all readings
            var readingsList = GetAllAccelerometerReadings();
            //setting path
            var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            //setting documents
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //setting filename
            var filename = System.IO.Path.Combine(path.ToString(), fileName + ".txt");
            //itterate through list of readings
            foreach (AccelerometerReading reading in readingsList)
            {
                //write reading to csv line
                string lineToAdd = reading.ID + "," + reading.Date.ToString("d") + "," + reading.Time + "," + reading.X + "," + reading.Y + "," + reading.Z + "," + reading.VectorMagnitude;
                //appen to string builder
                Csv.Append(lineToAdd);
                //append new line
                Csv.AppendLine();
            }
            //delete file
            File.Delete(filename);
            //write csv to file
            File.WriteAllText(filename, Csv.ToString());
            //return filename
            return filename;
        }

        /// <summary>
        /// Delete csv file containing accelerometer readings
        /// </summary>
        public void DeleteAccelerometerReadingsCsvFile()
        {
            //setting path
            var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            //setting documents
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //setting filename
            var filename = System.IO.Path.Combine(path.ToString(), "SittingExamples.txt");
            //delete file
            File.Delete(filename);
        }

        /// <summary>
        /// Check if an accelerometer reading exists in the app database
        /// </summary>
        /// <returns></returns>
        public bool AccelerometerReadingExists()
        {
            return AppDatabase.AccelerometerReadingExists();
        }

        #endregion //DB METHODS


        #endregion //Methods
    }
}