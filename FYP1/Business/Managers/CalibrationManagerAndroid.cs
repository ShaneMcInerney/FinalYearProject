using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using System.Collections.Generic;

namespace FYP_Droid.Business.Managers
{
    public class CalibrationManagerAndroid : ICalibrationManager
    {

        #region Fields

        private AppDatabase m_appDatabase;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CalibrationManagerAndroid()
        {

        }

        /// <summary>
        /// Class Constuctor
        /// </summary>
        /// <param name="database">Instance of the app data base class</param>
        public CalibrationManagerAndroid(AppDatabase database)
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
        /// Get all the accelerometer readings in calibration sample
        /// </summary>
        /// <param name="sample">calibration sample to retrieve data of</param>
        /// <returns>List of readings contained in calibration sample</returns>
        public List<AccelerometerReading> GetAccelerometerReadingsForCalibrationSample(CalibrationSample sample)
        {
            return this.AppDatabase.GetAccelerometerReadingsForCalibrationSample(sample);
        }

        /// <summary>
        /// Save calibration sample to db
        /// </summary>
        /// <param name="sample">the calibration sample to save</param>
        /// <returns>id of the saved sample</returns>
        public int SaveCalibrationSample(CalibrationSample sample)
        {
            return this.AppDatabase.SaveCalibrationSample(sample);
        }

        #endregion //Methods

    }
}