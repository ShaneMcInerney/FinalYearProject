using System;

namespace FYP.Business.Models
{
    public class AccelerometerReading : BaseEntity
    {

        #region Fields

        private double m_x;
        private double m_y;
        private double m_z;
        private double m_vectorMagnitude;
        private TimeSpan m_time;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccelerometerReading()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="x"> x axis reading</param>
        /// <param name="y"> y axis reading</param>
        /// <param name="z"> z axis reading</param>
        public AccelerometerReading(double x, double y, double z)
        {
            //rounding/setting x value
            this.m_x = Math.Round(x, 2);
            //rounding/setting y value
            this.m_y = Math.Round(y, 2);
            //rounding/setting z value 
            this.m_z = Math.Round(z, 2);
            //setting date to current
            this.Date = DateTime.Now;
            //setting time to current
            this.m_time = DateTime.Now.TimeOfDay;
            //setting vector magntude
            this.m_vectorMagnitude = Math.Round(CalculateMagnitudeOfVector(this.m_x, this.m_y, this.m_z), 2);
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_x
        /// </summary>
        public double X
        {
            get
            {
                return m_x;
            }

            set
            {
                m_x = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_y
        /// </summary>
        public double Y
        {
            get
            {
                return m_y;
            }

            set
            {
                m_y = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_z
        /// </summary>
        public double Z
        {
            get
            {
                return m_z;
            }

            set
            {
                m_z = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_time
        /// </summary>
        public TimeSpan Time
        {
            get
            {
                return m_time;
            }

            set
            {
                m_time = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_vectorMagnitude
        /// </summary>
        public double VectorMagnitude
        {
            get
            {
                return m_vectorMagnitude;
            }

            set
            {
                m_vectorMagnitude = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Calculates the magnitude of a three dimensional vector
        /// </summary>
        /// <param name="x"> vector x value </param>
        /// <param name="y"> vector y value </param>
        /// <param name="z"> vector z value </param>
        /// <returns> magnitude of the vector </returns>
        private double CalculateMagnitudeOfVector(double x, double y, double z)
        {
            //multiplying vectors, summing them up
            var vectorMagnitudeSquared = (x * x) + (y * y) + (z * z);
            //getting square root of above
            var vecMag = Math.Sqrt(vectorMagnitudeSquared);
            //return vector magnitude
            return vecMag;
        }

        /// <summary>
        /// To string override method, basically a to csv line method
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "x: " + X + " y: " + Y + " z: " + Z + " Magnitude: " + VectorMagnitude;
        }

        #endregion //Methods
    }
}
