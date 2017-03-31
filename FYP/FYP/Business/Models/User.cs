using SQLite;
using System;

namespace FYP.Business.Models
{
    /// <summary>
    /// enum to handle users gender
    /// </summary>
    public enum Gender
    {
        Male,
        Female
    }

    public class User
    {
        #region Fields

        private string m_name;
        private int m_age;
        private double m_height;
        private double m_weight;
        private DateTime m_dob;
        private double m_strideLength;
        private double m_bodyMassIndex;
        private int m_heartRate;
        private Gender m_genderType;
        private int m_sleepQuality;

        #endregion //Fields

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <param name="age"> user's age</param>
        /// <param name="dateOfBirth"> user's date of birth</param>
        /// <param name="height"> user's height</param>
        /// <param name="weight"> user's weight</param>
        /// <param name="genderType"> user's gender</param>
        public User(string name, int age, DateTime dateOfBirth, double height, double weight, Gender genderType)
        {
            this.m_name = name;
            this.m_age = age;
            this.Dob = dateOfBirth;
            this.m_height = height;
            this.m_weight = weight;
            this.GenderType = genderType;
            this.m_bodyMassIndex = CalculateBodyMassIndex();
            this.m_strideLength = CalculateStrideLength(this.GenderType);
        }


        #endregion //Constructor

        #region Property Accessors

        /// <summary>
        /// Gets/Sets ID
        /// </summary>
        [PrimaryKey]
        public int ID { get; set; }

        /// <summary>
        /// Gets/Sets m_name
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }

            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_age
        /// </summary>
        public int Age
        {
            get
            {
                return m_age;
            }

            set
            {
                m_age = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_height
        /// </summary>
        public double Height
        {
            get
            {
                return m_height;
            }

            set
            {
                m_height = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weight
        /// </summary>
        public double Weight
        {
            get
            {
                return m_weight;
            }

            set
            {
                m_weight = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_strideLength
        /// </summary>
        public double StrideLength
        {
            get
            {
                return m_strideLength;
            }

            set
            {
                m_strideLength = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_bodyMassIndex
        /// </summary>
        public double BodyMassIndex
        {
            get
            {
                return m_bodyMassIndex;
            }

            set
            {
                m_bodyMassIndex = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_dob
        /// </summary>
        public DateTime Dob
        {
            get
            {
                return m_dob;
            }

            set
            {
                m_dob = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_genderType
        /// </summary>
        public Gender GenderType
        {
            get
            {
                return m_genderType;
            }

            set
            {
                m_genderType = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_heartRate
        /// </summary>
        public int HeartRate
        {
            get
            {
                return m_heartRate;
            }

            set
            {
                m_heartRate = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepQuality
        /// </summary>
        public int SleepQuality
        {
            get
            {
                return m_sleepQuality;
            }

            set
            {
                m_sleepQuality = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Calculates a users Body Mass Index
        /// </summary>
        /// <returns>double, the users body mass index</returns>
        public double CalculateBodyMassIndex()
        {
            //getting user's height in metres
            var heightInMetres = this.m_height / 100;
            //BMI calculation formula
            var BMI = this.m_weight / ((heightInMetres) * (heightInMetres));
            //rounding bmi value
            return Math.Round(BMI, 2);
        }

        /// <summary>
        /// Calculates a users stride length
        /// </summary>
        /// <param name="genderType">The users gender</param>
        /// <returns>double, the users stride length</returns>
        public double CalculateStrideLength(Gender genderType)
        {
            //setting strie variable
            double stride = 0.0;
            //if user is male
            if (genderType == Gender.Male)
            {
                //perform stride calculatons
                stride = this.m_height * 0.415;
            }
            else
            {
                //perform stride calculatons
                stride = this.m_height * 0.413;
            }
            //return stride length
            return stride / 100;
        }


        #endregion //Methods
    }
}
