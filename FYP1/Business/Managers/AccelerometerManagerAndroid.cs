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
    public class AccelerometerManagerAndroid : AccelerometerManager
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
        public AccelerometerManagerAndroid(AppDatabase database):base (database)
        {
            this.m_appDatebase = database;

            this.m_csv = new StringBuilder();

            this.m_bulkAccelerometerReadings = new List<AccelerometerReading>();
        }

        #endregion //Constructors

        #region Property Accessors


        #endregion //Property Accessors

        #region Methods

        #region DB Methods

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