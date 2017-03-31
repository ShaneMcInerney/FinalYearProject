namespace FYP.Business.Models
{
    public enum CalibrationType
    {
        Walking,
        Upstairs,
        Downstairs
    }

    public class CalibrationSample : BaseEntity
    {

        #region Fields

        private string m_accelerometerIds;
        private CalibrationType m_calibrationDataType;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CalibrationSample()
        {
            this.AccelerometerIds = "";
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_accelerometerIds
        /// </summary>
        public string AccelerometerIds
        {
            get
            {
                return m_accelerometerIds;
            }

            set
            {
                m_accelerometerIds = value;
            }
        }

        /// <summary>
        /// Gets/Sets accelerometer blobbed ids
        /// </summary>
        public string AccelerometerIdsBlobbed { get; set; }

        /// <summary>
        /// Gets/Sets m_calibrationDataType
        /// </summary>
        public CalibrationType CalibrationDataType
        {
            get
            {
                return m_calibrationDataType;
            }

            set
            {
                m_calibrationDataType = value;
            }
        }

        #endregion //Property Accessors
    }
}
