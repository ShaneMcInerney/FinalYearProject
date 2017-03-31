using System;

namespace FYP.Business.Models
{
    public class SleepAlarm : BaseEntity
    {
        #region Fields

        private bool m_smartAlarmEnabled;
        private DateTime m_alarmTime;
        private bool m_onMon;
        private bool m_onTues;
        private bool m_onWed;
        private bool m_onThurs;
        private bool m_onFri;
        private bool m_onSat;
        private bool m_onSun;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SleepAlarm()
        {

        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="m_smartAlarmEnabled">bool if smart alarm is enabled</param>
        /// <param name="m_alarmTime">Date and time for alarm</param>
        /// <param name="m_onMon">bool for alarm trigger on monday</param>
        /// <param name="m_onTues">bool for alarm trigger on tuesday</param>
        /// <param name="m_onWed">bool for alarm trigger on wednesday</param>
        /// <param name="m_onThurs">bool for alarm trigger on thursday</param>
        /// <param name="m_onFri">bool for alarm trigger on friday</param>
        /// <param name="m_onSat">bool for alarm trigger on saturday</param>
        /// <param name="m_onSun">bool for alarm trigger on sunday</param>
        public SleepAlarm(bool m_smartAlarmEnabled, DateTime m_alarmTime, bool m_onMon, bool m_onTues, bool m_onWed, bool m_onThurs, bool m_onFri, bool m_onSat, bool m_onSun)
        {
            this.m_smartAlarmEnabled = m_smartAlarmEnabled;
            this.m_alarmTime = m_alarmTime;
            this.m_onMon = m_onMon;
            this.m_onTues = m_onTues;
            this.m_onWed = m_onWed;
            this.m_onThurs = m_onThurs;
            this.m_onFri = m_onFri;
            this.m_onSat = m_onSat;
            this.m_onSun = m_onSun;
        }

        #endregion Constructors

        #region  Property Accessors

        /// <summary>
        /// Gets/Sets m_smartAlarmEnabled
        /// </summary>
        public bool SmartAlarmEnabled
        {
            get
            {
                return m_smartAlarmEnabled;
            }

            set
            {
                m_smartAlarmEnabled = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_alarmTime
        /// </summary>
        public DateTime AlarmTime
        {
            get
            {
                return m_alarmTime;
            }

            set
            {
                m_alarmTime = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onMon
        /// </summary>
        public bool OnMon
        {
            get
            {
                return m_onMon;
            }

            set
            {
                m_onMon = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onTues
        /// </summary>
        public bool OnTues
        {
            get
            {
                return m_onTues;
            }

            set
            {
                m_onTues = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onWed
        /// </summary>
        public bool OnWed
        {
            get
            {
                return m_onWed;
            }

            set
            {
                m_onWed = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onThurs
        /// </summary>
        public bool OnThurs
        {
            get
            {
                return m_onThurs;
            }

            set
            {
                m_onThurs = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onFri
        /// </summary>
        public bool OnFri
        {
            get
            {
                return m_onFri;
            }

            set
            {
                m_onFri = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onSat
        /// </summary>
        public bool OnSat
        {
            get
            {
                return m_onSat;
            }

            set
            {
                m_onSat = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_onSun
        /// </summary>
        public bool OnSun
        {
            get
            {
                return m_onSun;
            }

            set
            {
                m_onSun = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Outputting class to string
        /// </summary>
        /// <returns>returns string representation of class</returns>
        public override string ToString()
        {
            var output = "Time: " + this.AlarmTime.Hour + ":" + this.AlarmTime.Minute + ", Days: " + CheckedDaysToStrings();
            if (this.AlarmTime.Minute < 10)
            {
                //workaround for issue displaying minutes
                output = "Time: " + this.AlarmTime.Hour + ":" + 0 + this.AlarmTime.Minute + ", Days: " + CheckedDaysToStrings();
            }

            return output;
        }

        /// <summary>
        /// Sets output string based on days set for alarm
        /// </summary>
        /// <returns>output string based on days selected</returns>
        public string CheckedDaysToStrings()
        {
            string daysForAlarm = "";
            //if monday checked
            if (OnMon)
            {
                daysForAlarm += " Mo,";
            }
            //if tuesday checked
            if (OnTues)
            {
                daysForAlarm += " Tu,";
            }
            //if wednesday checked
            if (OnWed)
            {
                daysForAlarm += " We,";
            }
            //if thursday checked
            if (OnThurs)
            {
                daysForAlarm += " Th,";
            }
            //if friday checked
            if (OnFri)
            {
                daysForAlarm += " Fr,";
            }
            //if saturday checked
            if (OnSat)
            {
                daysForAlarm += " Sa,";
            }
            //if sunday checked
            if (OnSun)
            {
                daysForAlarm += " Su";
            }
            //return list of days checked in string format
            return daysForAlarm;
        }

        #endregion //Methods

    }
}
