using FYP.Business.Models;
using System.Collections.Generic;

namespace FYP.Business.Managers
{
    public interface ICalibrationManager
    {
        /// <summary>
        /// Get all the accelerometer readings in calibration sample
        /// </summary>
        /// <param name="sample">calibration sample to retrieve data of</param>
        /// <returns>List of readings contained in calibration sample</returns>
        int SaveCalibrationSample(CalibrationSample sample);

        /// <summary>
        /// Save calibration sample to db
        /// </summary>
        /// <param name="sample">the calibration sample to save</param>
        /// <returns>id of the saved sample</returns>
        List<AccelerometerReading> GetAccelerometerReadingsForCalibrationSample(CalibrationSample sample);


    }
}
