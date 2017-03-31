using System;

namespace FYP.Business.Models
{
    public class WeightEntry : BaseEntity
    {
        #region fields

        private double m_weight;

        #endregion //fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WeightEntry()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="weight">weight value</param>
        /// <param name="date"> date for entry</param>
        public WeightEntry(double weight, DateTime date)
        {
            this.m_weight = weight;
            this.Date = date;
        }

        #endregion //Constructors

        #region Property Accessors

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


        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// To string override method, basically a to csv line method
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            var output = "Date: " + this.Date.Day + "/" + this.Date.Month + "/" + this.Date.Year + /*", Time: " + this.Date.Hour+":"+this.Date.Minute+*/ ", Weight (kg): " + this.Weight;
            return output;
        }


        #endregion //Methods
    }
}
