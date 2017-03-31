using FYP.Business.Managers;
using FYP.DataAccess;
using FYP_Droid.Business;
using FYP_Droid.Business.Managers;
using SQLite;
using System;

namespace FYP_Droid.Utilities
{
    public static class GlobalUtilities
    {
        #region Fields

        private static FileHelperAndroid m_fileHelper = new FileHelperAndroid();
        private static string m_fileName = "FallDetectorDB.db3";

        private static string m_path = FileHelper.GetLocalFilePath(FileName);
        private static SQLiteConnection m_conn = new SQLiteConnection(Path);
        private static AppDatabase m_appDatabase = new AppDatabase(m_conn);

        private static AccelerometerManagerAndroid m_accelerometerManager = new AccelerometerManagerAndroid(m_appDatabase);
        private static WeightManagerAndroid m_weightManager = new WeightManagerAndroid(m_appDatabase);
        private static StepManagerAndroid m_stepManager = new StepManagerAndroid(m_appDatabase);
        private static StepGoalManagerAndroid m_stepGoalManager = new StepGoalManagerAndroid(m_appDatabase);
        private static EmergencyContactManagerAndroid m_emergencyContactManager = new EmergencyContactManagerAndroid(m_appDatabase);
        private static EmergencyMessageManagerAndroid m_emergencyMessageManager = new EmergencyMessageManagerAndroid(m_appDatabase);
        private static LocationManagerAndroid m_locationManager = new LocationManagerAndroid();
        private static FallAlarmManagerAndroid m_alarmManager = new FallAlarmManagerAndroid();
        private static UserManagerAndroid m_userManager = new UserManagerAndroid(m_appDatabase);
        private static SleepManagerAndroid m_sleepManager = new SleepManagerAndroid(m_appDatabase);
        private static SleepAlarmManagerAndroid m_sleepAlarmManager = new SleepAlarmManagerAndroid(m_appDatabase);
        private static CalibrationManagerAndroid m_calibrationManager = new CalibrationManagerAndroid(m_appDatabase);
        private static GraphManager m_graphManager = new GraphManager();
        private static FallManagerAndroid m_fallManager = new FallManagerAndroid(m_appDatabase);


        #endregion //Fields

        #region Property Acessors

        /// <summary>
        /// Gets/Sets m_fileHelper
        /// </summary>
        public static FileHelperAndroid FileHelper
        {
            get
            {
                return m_fileHelper;
            }

            set
            {
                m_fileHelper = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_fileName
        /// </summary>
        public static string FileName
        {
            get
            {
                return m_fileName;
            }

            set
            {
                m_fileName = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_path
        /// </summary>
        public static string Path
        {
            get
            {
                return m_path;
            }

            set
            {
                m_path = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_conn
        /// </summary>
        public static SQLiteConnection Conn
        {
            get
            {
                return m_conn;
            }

            set
            {
                m_conn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_accelerometerManager
        /// </summary>
        public static AccelerometerManagerAndroid AccelerometerManager
        {
            get
            {
                return m_accelerometerManager;
            }

            set
            {
                m_accelerometerManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_weightManager
        /// </summary>
        public static WeightManagerAndroid WeightManager
        {
            get
            {
                return m_weightManager;
            }

            set
            {
                m_weightManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets
        /// </summary>
        public static StepManagerAndroid StepManager
        {
            get
            {
                return m_stepManager;
            }

            set
            {
                m_stepManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stepGoalManager
        /// </summary>
        public static StepGoalManagerAndroid StepGoalManager
        {
            get
            {
                return m_stepGoalManager;
            }

            set
            {
                m_stepGoalManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_emergencyContactManager
        /// </summary>
        public static EmergencyContactManagerAndroid EmergencyContactManager
        {
            get
            {
                return m_emergencyContactManager;
            }

            set
            {
                m_emergencyContactManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_emergencyMessageManager
        /// </summary>
        public static EmergencyMessageManagerAndroid EmergencyMessageManager
        {
            get
            {
                return m_emergencyMessageManager;
            }

            set
            {
                m_emergencyMessageManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_locationManager
        /// </summary>
        public static LocationManagerAndroid LocationManager
        {
            get
            {
                return m_locationManager;
            }

            set
            {
                m_locationManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_alarmManager
        /// </summary>
        public static FallAlarmManagerAndroid AlarmManager
        {
            get
            {
                return m_alarmManager;
            }

            set
            {
                m_alarmManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_userManager
        /// </summary>
        public static UserManagerAndroid UserManager
        {
            get
            {
                return m_userManager;
            }

            set
            {
                m_userManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepManager
        /// </summary>
        public static SleepManagerAndroid SleepManager
        {
            get
            {
                return m_sleepManager;
            }

            set
            {
                m_sleepManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_sleepAlarmManager
        /// </summary>
        public static SleepAlarmManagerAndroid SleepAlarmManager
        {
            get
            {
                return m_sleepAlarmManager;
            }

            set
            {
                m_sleepAlarmManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_calibrationManager
        /// </summary>
        public static CalibrationManagerAndroid CalibrationManager
        {
            get
            {
                return m_calibrationManager;
            }

            set
            {
                m_calibrationManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_graphManager
        /// </summary>
        public static GraphManager GraphManager
        {
            get
            {
                return m_graphManager;
            }

            set
            {
                m_graphManager = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_fallManager
        /// </summary>
        public static FallManagerAndroid FallManager
        {
            get
            {
                return m_fallManager;
            }

            set
            {
                m_fallManager = value;
            }
        }

        #endregion //Property Accessors

        #region Methods



        #endregion //Methods

    }
}