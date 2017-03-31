using System;

namespace FYP.Business.Models
{
    /// <summary>
    /// enum to handle the tyes of goals that can be created
    /// </summary>
    public enum StepGoalType
    {
        Hourly = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
    }

    public class StepGoal : BaseEntity
    {

        #region Fields

        private Int64 m_amount;
        private StepGoalType m_goalType;
        private bool m_goalComplete = false;
        private DateTime m_finishBy;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public StepGoal()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="m_amount">amount of steps</param>
        /// <param name="m_goalType">the step goal type</param>
        /// <param name="m_goalComplete">whether </param>
        /// <param name="date"></param>
        public StepGoal(long m_amount, StepGoalType m_goalType, bool m_goalComplete, DateTime date)
        {
            this.m_amount = m_amount;
            this.m_goalType = m_goalType;
            this.m_goalComplete = m_goalComplete;
            this.Date = date;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public long Amount
        {
            get
            {
                return m_amount;
            }

            set
            {
                m_amount = value;
            }
        }

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public StepGoalType GoalType
        {
            get
            {
                return m_goalType;
            }

            set
            {
                m_goalType = value;
            }
        }

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public bool GoalComplete
        {
            get
            {
                return m_goalComplete;
            }

            set
            {
                m_goalComplete = value;
            }
        }

        public DateTime FinishBy
        {
            get
            {
                return m_finishBy;
            }

            set
            {
                m_finishBy = value;
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
            var output = "Date: " + this.Date + ", Amount: " + this.Amount + ", Goal Completed: " + this.GoalComplete + ", Goal Period: " + this.GoalType.ToString();
            return output;
        }

        #endregion //Methods
    }
}
