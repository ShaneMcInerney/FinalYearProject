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
        private static WeightManager m_weightManager = new WeightManager(m_appDatabase);
        private static StepManager m_stepManager = new StepManager(m_appDatabase);
        private static StepGoalManager m_stepGoalManager = new StepGoalManager(m_appDatabase);
        private static EmergencyContactManager m_emergencyContactManager = new EmergencyContactManager(m_appDatabase);
        private static EmergencyMessageManagerAndroid m_emergencyMessageManager = new EmergencyMessageManagerAndroid(m_appDatabase);
        private static LocationManagerAndroid m_locationManager = new LocationManagerAndroid();
        private static FallAlarmManagerAndroid m_alarmManager = new FallAlarmManagerAndroid();
        private static UserManager m_userManager = new UserManager(m_appDatabase);
        private static SleepManager m_sleepManager = new SleepManager(m_appDatabase);
        private static SleepAlarmManagerAndroid m_sleepAlarmManager = new SleepAlarmManagerAndroid(m_appDatabase);
        private static CalibrationManager m_calibrationManager = new CalibrationManager(m_appDatabase);
        private static GraphManager m_graphManager = new GraphManager();
        private static FallManager m_fallManager = new FallManager(m_appDatabase);


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
        public static WeightManager WeightManager
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
        public static StepManager StepManager
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
        public static StepGoalManager StepGoalManager
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
        public static EmergencyContactManager EmergencyContactManager
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
        public static UserManager UserManager
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
        public static SleepManager SleepManager
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
        public static CalibrationManager CalibrationManager
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
        public static FallManager FallManager
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