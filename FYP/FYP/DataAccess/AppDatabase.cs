using FYP.Business.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP.DataAccess
{
    /// <summary>
    /// AppDatabase uses ADO.NET to create the tables for this application (create,read,update,delete methods)
    /// </summary>
    public class AppDatabase
    {
        #region Fields

        static object s_locker = new object();
        private SQLiteConnection m_database;
        private string m_path;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AppDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        public AppDatabase(SQLiteConnection conn)
        {
            Database = conn;
            // create the tables
            Database.CreateTable<EmergencyContact>();
            Database.CreateTable<WeightEntry>();
            Database.CreateTable<AccelerometerReading>();
            Database.CreateTable<StepEntry>();
            Database.CreateTable<StepGoal>();
            Database.CreateTable<EmergencyMessage>();
            Database.CreateTable<User>();
            Database.CreateTable<Fall>();
            Database.CreateTable<CalibrationSample>();
            Database.CreateTable<SleepAlarm>();
            Database.CreateTable<SleepEntry>();

        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets s_locker
        /// </summary>
        public static object S_locker
        {
            get
            {
                return s_locker;
            }

            set
            {
                s_locker = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_database
        /// </summary>
        public SQLiteConnection Database
        {
            get
            {
                return m_database;
            }

            set
            {
                m_database = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_path
        /// </summary>
        public string Path
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

        #endregion //Property Acessors

        #region Methods

        #region Weight Entry Methods

        /// <summary>
        /// Get allweight entries in db
        /// </summary>
        /// <returns>Enumerable of weight entries</returns>
        public IEnumerable<WeightEntry> GetAllWeightEntries()
        {
            lock (S_locker)
            {
                //all entries to list
                var output = (from i in Database.Table<WeightEntry>() select i).ToList();
                //return list
                return output;
            }
        }

        /// <summary>
        /// Retrieve Latest Weight Entry in Db
        /// </summary>
        /// <returns></returns>
        public WeightEntry GetLatestWeightEntry()
        {
            lock (S_locker)
            {
                //delete all in list
                return Database.Table<WeightEntry>().LastOrDefault<WeightEntry>();
            }
        }

        /// <summary>
        /// Get weight entry for given id
        /// </summary>
        /// <param name="id">id of weight entry to retrieve</param>
        /// <returns>weight entry matching given id</returns>
        public WeightEntry GetWeightEntry(int id)
        {
            lock (S_locker)
            {
                //get entry that matches given id
                return Database.Table<WeightEntry>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Save/Update weight entry in db
        /// </summary>
        /// <param name="weightEntry">weight entry to update/save</param>
        /// <returns>id of saved weight entry</returns>
        public int SaveWeightEntry(WeightEntry weightEntry)
        {
            lock (S_locker)
            {
                //if id is not equal 0
                if (weightEntry.ID != 0)
                {
                    //update entry
                    Database.Update(weightEntry);
                    return weightEntry.ID;
                }
                else
                {
                    //otherwise insert new entry
                    return Database.Insert(weightEntry);
                }
            }
        }

        /// <summary>
        /// Delete weight entry saved in sb
        /// </summary>
        /// <param name="id">id of weight entry to delete</param>
        /// <returns>id of deleted weight entry</returns>
        public int DeleteWeightEntry(int id)
        {
            lock (S_locker)
            {
                //detlet enty by id
                return Database.Delete<WeightEntry>(id);
            }
        }

        /// <summary>
        /// Delete all weight entries in db
        /// </summary>
        public void DeleteAllWeightEntries()
        {
            lock (S_locker)
            {
                //delete all entries
                Database.DeleteAll<WeightEntry>();
            }
        }

        /// <summary>
        /// Check if weight entry exists in db
        /// </summary>
        /// <returns>bool whether or not weight entry exists</returns>
        public bool WeightEntryExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<WeightEntry>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //Weight Entry Methods

        #region Emergency Contact Methods

        /// <summary>
        /// Retrieves all emergency contacts from the db
        /// </summary>
        /// <returns>Enumerable of emergency cntacts</returns>
        public IEnumerable<EmergencyContact> GetAllEmergencyContacts()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<EmergencyContact>() select i).ToList();
            }
        }

        /// <summary>
        /// Retrieve emergency contact for given id
        /// </summary>
        /// <param name="id">id of emergency contact to retrieve</param>
        /// <returns>Emergency contact with matching id</returns>
        public EmergencyContact GetEmergencyContact(int id)
        {
            lock (S_locker)
            {
                return Database.Table<EmergencyContact>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Save/Update emergency contact in db
        /// </summary>
        /// <param name="emergencyContact">emergency contact to save/update</param>
        /// <returns>id of saved contact</returns>
        public int SaveEmergencyContact(EmergencyContact emergencyContact)
        {
            lock (S_locker)
            {
                if (emergencyContact.ID != 0)
                {
                    Database.Update(emergencyContact);
                    return emergencyContact.ID;
                }
                else
                {
                    return Database.Insert(emergencyContact);
                }
            }
        }

        /// <summary>
        /// Delete emergency contact in db
        /// </summary>
        /// <param name="id">id of emergency contact to be deleted</param>
        /// <returns>id of deleted contact</returns>
        public int DeleteEmergencyContact(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<EmergencyContact>(id);
            }
        }

        /// <summary>
        /// Delete all emergency contacts in db
        /// </summary>
        public void DeleteAllEmergencyContacts()
        {
            lock (S_locker)
            {
                Database.DeleteAll<EmergencyContact>();
            }
        }

        /// <summary>
        /// Check that emergency contact exists in db
        /// </summary>
        /// <returns>bool of emergency contacts existance</returns>
        public bool EmergencyContactExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<EmergencyContact>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion //Emergency Contact Methods

        #region Emergency Message Methods

        /// <summary>
        /// Retrieve emergency message from db
        /// </summary>
        /// <returns> emergency message</returns>
        public EmergencyMessage GetEmergencyMessage()
        {
            lock (S_locker)
            {
                return Database.Table<EmergencyMessage>().FirstOrDefault();
            }
        }

        /// <summary>
        /// Save/Udpdate emergency message in sb
        /// </summary>
        /// <param name="emergencyMessage"> emergency message to save/update</param>
        public void SaveEmergencyMessage(EmergencyMessage emergencyMessage)
        {
            lock (S_locker)
            {
                if (emergencyMessage.ID != 0)
                {
                    Database.Update(emergencyMessage);
                }
                else
                {
                    Database.Insert(emergencyMessage);
                }
            }
        }

        /// <summary>
        /// Delete stored emergency message
        /// </summary>
        public void DeleteEmergencyMessage()
        {
            Database.DeleteAll<EmergencyMessage>();
        }

        /// <summary>
        /// Check if emergency message exists in db
        /// </summary>
        /// <returns>bool of existance of emergency message in db</returns>
        public bool EmergencyMessageExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<EmergencyMessage>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        #endregion //Emergency Message Methods

        #region Accelerometer Data Methods

        /// <summary>
        /// Gets all accelerometer readings in database
        /// </summary>
        /// <returns>An enumerable collection of accelerometer readings</returns>
        public IEnumerable<AccelerometerReading> GetAllAccelerometerReadings()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<AccelerometerReading>() select i).ToList();
            }
        }

        /// <summary>
        /// Get accelerometer reading by id
        /// </summary>
        /// <param name="id">id of reading to be retrieved</param>
        /// <returns>accelerometer reading</returns>
        public AccelerometerReading GetAccelerometerReading(int id)
        {
            lock (S_locker)
            {
                return Database.Table<AccelerometerReading>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Saves/adds a new accelerometer reading to the database
        /// </summary>
        /// <param name="accelerometerReading">accelerometer reading to save</param>
        /// <returns>the id of the saved accelerometer</returns>
        public int SaveAccelerometerReading(AccelerometerReading accelerometerReading)
        {
            lock (S_locker)
            {
                if (accelerometerReading.ID != 0)
                {
                    Database.Update(accelerometerReading);
                    return accelerometerReading.ID;
                }
                else
                {
                    return Database.Insert(accelerometerReading);
                }
            }
        }

        public void BulkAccelerometerReadingsSave(List<AccelerometerReading> accelerometerReadings)
        {
            //within method for transaction
            Database.RunInTransaction(() =>
            { //database calls inside the transaction
                for (var i = 0; i < accelerometerReadings.Count; i++)
                {
                    var reading = accelerometerReadings.ElementAt<AccelerometerReading>(i);
                    Database.Insert(reading);
                }
            });
            Database.Commit();
        }

        /// <summary>
        /// DELETES ALL ACCELEROMETER READINGS, BE CAREFUL!!
        /// </summary>
        public void DeleteAllAccelerometerReadings()
        {

            Database.RunInTransaction(() =>
            { //database calls inside the transaction
                Database.DeleteAll<AccelerometerReading>();
            });
            Database.Commit();
        }

        /// <summary>
        /// Delete an individual accelerometer reading by id
        /// </summary>
        /// <param name="id"> id of reading to be deleted </param>
        /// <returns> id of deleted item </returns>
        public int DeleteAccelerometerReading(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<AccelerometerReading>(id);
            }
        }

        /// <summary>
        /// Checks if accelerometer reading exists in db
        /// </summary>
        /// <returns>ool of existance of accelerometer reading in db</returns>
        public bool AccelerometerReadingExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<AccelerometerReading>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //Accelerometer Data Methods

        #region StepEntry Methods

        /// <summary>
        /// Retrieve all step entries in db
        /// </summary>
        /// <returns>Enumerable of step entries</returns>
        public IEnumerable<StepEntry> GetAllStepEntries()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<StepEntry>() select i).ToList();
            }
        }

        /// <summary>
        /// Retrieve step entry by id
        /// </summary>
        /// <param name="id">Id of entry to retrieve</param>
        /// <returns>Step entry matching given id</returns>
        public StepEntry GetStepEntry(int id)
        {
            lock (S_locker)
            {
                return Database.Table<StepEntry>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Save/Update step entry in db
        /// </summary>
        /// <param name="stepEntry">step entry to save/update</param>
        /// <returns>id of save step entry</returns>
        public int SaveStepEntry(StepEntry stepEntry)
        {
            lock (S_locker)
            {
                if (stepEntry.ID != 0)
                {
                    Database.Update(stepEntry);
                    return stepEntry.ID;
                }
                else
                {
                    return Database.Insert(stepEntry);
                }
            }
        }

        /// <summary>
        /// Delete all step entries in db
        /// </summary>
        public void DeleteAllStepEntries()
        {
            lock (S_locker)
            {
                Database.DeleteAll<StepEntry>();
            }
        }

        /// <summary>
        /// Delete step entry in db by id
        /// </summary>
        /// <param name="id">id of step entry to delete</param>
        /// <returns>id of deleted step entry</returns>
        public int DeleteStepEntry(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<StepEntry>(id);
            }
        }

        /// <summary>
        /// Check if step entry exists in db
        /// </summary>
        /// <returns>bool wheter step entry exists in db or not</returns>
        public bool StepEntryExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<StepEntry>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //StepEntry Methods

        #region StepGoal Methods

        /// <summary>
        /// Retrieve step goal by id
        /// </summary>
        /// <param name="id">id of step goal to get</param>
        /// <returns>Step goal that matches id</returns>
        public StepGoal GetStepGoal(int id)
        {
            lock (S_locker)
            {
                return Database.Table<StepGoal>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Retrieves all step goals in db
        /// </summary>
        /// <returns>Enumerable of step goals</returns>
        public IEnumerable<StepGoal> GetAllStepGoals()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<StepGoal>() select i).ToList();
            }
        }

        /// <summary>
        /// Save.Update ste goal in db
        /// </summary>
        /// <param name="stepGoal">step goal to sav/update</param>
        /// <returns>id of saved step goal</returns>
        public int SaveStepGoal(StepGoal stepGoal)
        {
            lock (S_locker)
            {
                if (stepGoal.ID != 0)
                {
                    Database.Update(stepGoal);
                    return stepGoal.ID;
                }
                else
                {
                    return Database.Insert(stepGoal);
                }
            }
        }

        /// <summary>
        /// Delete all step goals in the db
        /// </summary>
        public void DeleteAllStepGoals()
        {
            lock (S_locker)
            {
                Database.DeleteAll<StepGoal>();
            }
        }

        /// <summary>
        /// Delete step goal in db by id
        /// </summary>
        /// <param name="id">id of step goal to delete</param>
        /// <returns>id of deleted step goal</returns>
        public int DeleteStepGoal(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<StepGoal>(id);
            }
        }

        /// <summary>
        /// Check if step goal exists
        /// </summary>
        /// <returns>bool whether or not step goal exists in db</returns>
        public bool StepGoalExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<StepGoal>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //StepGoal Methods

        #region Fall Methods

        /// <summary>
        /// Retrieve Latest Weight Entry in Db
        /// </summary>
        /// <returns></returns>
        public Fall GetLatestFall()
        {
            lock (S_locker)
            {
                return Database.Table<Fall>().LastOrDefault<Fall>();
            }
        }

        /// <summary>
        /// Retreive sleep entry by id
        /// </summary>
        /// <param name="id">id of sleep entry to retrieve</param>
        /// <returns>sleep entry</returns>
        public Fall GetFall(int id)
        {
            lock (S_locker)
            {
                return Database.Table<Fall>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Retrieve all sleep entries in db
        /// </summary>
        /// <returns>Enumerable of sleep entries</returns>
        public IEnumerable<Fall> GetAllFalls()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<Fall>() select i).ToList();
            }
        }

        /// <summary>
        /// Save/Update sleep entry in db
        /// </summary>
        /// <param name="fall">sleep entry to save/update</param>
        /// <returns>id of saved sleep entry</returns>
        public int SaveFall(Fall fall)
        {
            lock (S_locker)
            {
                if (fall.ID != 0)
                {
                    Database.Update(fall);
                    return fall.ID;
                }
                else
                {
                    return Database.Insert(fall);
                }
            }
        }

        /// <summary>
        /// Delete all step entries in db
        /// </summary>
        public void DeleteAllFalls()
        {
            lock (S_locker)
            {
                Database.DeleteAll<Fall>();
            }
        }

        /// <summary>
        /// Delete step entry in db by id
        /// </summary>
        /// <param name="id">id of sleep entry to delete</param>
        /// <returns>id of deleted sleep entry</returns>
        public int DeleteFall(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<Fall>(id);
            }
        }

        /// <summary>
        /// Check if sleep entry exists in db
        /// </summary>
        /// <returns>bool of sleep entry existing in db</returns>
        public bool FallExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<Fall>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //Fall Methods

        #region User Methods


        public User GetUser()
        {
            lock (S_locker)
            {
                return Database.Table<User>().FirstOrDefault();
            }
        }


        public int SaveUser(User user)
        {
            lock (S_locker)
            {
                if (user.ID != 0)
                {
                    Database.Update(user);
                    return user.ID;
                }
                else
                {
                    return Database.Insert(user);
                }
            }
        }

        public void DeleteUser()
        {
            lock (S_locker)
            {
                Database.DeleteAll<User>();
            }
        }

        public bool UserExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<User>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion //Fall Methods

        #region Calibration Methods

        public CalibrationSample GetWalkingSample()
        {
            lock (S_locker)
            {
                return Database.Table<CalibrationSample>().Where(x => x.ID == 0).FirstOrDefault();
            }
        }

        public CalibrationSample GetUpstairsSample()
        {
            lock (S_locker)
            {
                return Database.Table<CalibrationSample>().Where(x => x.ID == 1).FirstOrDefault();
            }
        }

        public CalibrationSample GetDownstairsSample()
        {
            lock (S_locker)
            {
                return Database.Table<CalibrationSample>().Where(x => x.ID == 2).FirstOrDefault();
            }
        }

        public int SaveCalibrationSample(CalibrationSample example)
        {
            lock (S_locker)
            {
                if (CalibrationDataExists())
                {
                    Database.Update(example);
                    return example.ID;
                }
                else
                {
                    return Database.Insert(example);
                }
            }
        }

        public List<AccelerometerReading> GetAccelerometerReadingsForCalibrationSample(CalibrationSample example)
        {
            List<AccelerometerReading> readingsToReturn = new List<AccelerometerReading>();
            string[] stringValues = example.AccelerometerIds.Split(',');
            List<int> accelerometerId = new List<int>();

            for (int i = 0; i < stringValues.Length; i++)
            {
                accelerometerId.Add(Int32.Parse(stringValues[i]));
            }
            foreach (int i in accelerometerId)
            {
                readingsToReturn.Add(GetAccelerometerReading(i));
            }
            return readingsToReturn;
        }

        public bool CalibrationDataExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<CalibrationSample>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //Calibration Methods

        #region Sleep Alarm Methods

        /// <summary>
        /// Get Sleep alarm by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Sleep alarm for given id</returns>
        public SleepAlarm GetSleepAlarm(int id)
        {
            lock (S_locker)
            {
                return Database.Table<SleepAlarm>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Retrieves all sleep alarms in db
        /// </summary>
        /// <returns>Enumerable of sleep alarms</returns>
        public IEnumerable<SleepAlarm> GetAllSleepAlarms()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<SleepAlarm>() select i).ToList();
            }
        }

        /// <summary>
        /// Save/Update sleep alarms in db
        /// </summary>
        /// <param name="stepGoal">sleep alarms to save/update</param>
        /// <returns>id of saved sleep alarms</returns>
        public int SaveSleepAlarm(SleepAlarm sleepAlarm)
        {
            lock (S_locker)
            {
                if (sleepAlarm.ID != 0)
                {
                    Database.Update(sleepAlarm);
                    return sleepAlarm.ID;
                }
                else
                {
                    return Database.Insert(sleepAlarm);
                }
            }
        }

        /// <summary>
        /// Delete all sleep alarms in the db
        /// </summary>
        public void DeleteAllSleepAlarms()
        {
            lock (S_locker)
            {
                Database.DeleteAll<SleepAlarm>();
            }
        }

        /// <summary>
        /// Delete sleep alarm in db by id
        /// </summary>
        /// <param name="id">id of sleep alarm to delete</param>
        /// <returns>id of deleted sleep alarm</returns>
        public int DeleteSleepAlarm(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<SleepAlarm>(id);
            }
        }

        /// <summary>
        /// Check if sleep alarm exists
        /// </summary>
        /// <returns>bool whether or not sleep alarm exists in db</returns>
        public bool SmartAlarmExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<SleepAlarm>().Where(x => x.SmartAlarmEnabled == true) select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Check if sleep alarm exists
        /// </summary>
        /// <returns>bool whether or not sleep alarm exists in db</returns>
        public bool SleepAlarmExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<SleepAlarm>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //Sleep Alarm Methods

        #region Sleep Methods

        /// <summary>
        /// Retrieve Latest Weight Entry in Db
        /// </summary>
        /// <returns></returns>
        public SleepEntry GetLatestSleepEntry()
        {
            lock (S_locker)
            {
                return Database.Table<SleepEntry>().LastOrDefault();
            }
        }

        /// <summary>
        /// Retreive sleep entry by id
        /// </summary>
        /// <param name="id">id of sleep entry to retrieve</param>
        /// <returns>sleep entry</returns>
        public SleepEntry GetSleepEntry(int id)
        {
            lock (S_locker)
            {
                return Database.Table<SleepEntry>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Retrieve all sleep entries in db
        /// </summary>
        /// <returns>Enumerable of sleep entries</returns>
        public IEnumerable<SleepEntry> GetAllSleepEntries()
        {
            lock (S_locker)
            {
                return (from i in Database.Table<SleepEntry>() select i).ToList();
            }
        }

        /// <summary>
        /// Save/Update sleep entry in db
        /// </summary>
        /// <param name="sleepEntry">sleep entry to save/update</param>
        /// <returns>id of saved sleep entry</returns>
        public int SaveSleepEntry(SleepEntry sleepEntry)
        {
            lock (S_locker)
            {
                if (sleepEntry.ID != 0)
                {
                    Database.Update(sleepEntry);
                    return sleepEntry.ID;
                }
                else
                {
                    return Database.Insert(sleepEntry);
                }
            }
        }

        /// <summary>
        /// Delete all step entries in db
        /// </summary>
        public void DeleteAllSleepEntries()
        {
            lock (S_locker)
            {
                Database.DeleteAll<SleepEntry>();
            }
        }

        /// <summary>
        /// Delete step entry in db by id
        /// </summary>
        /// <param name="id">id of sleep entry to delete</param>
        /// <returns>id of deleted sleep entry</returns>
        public int DeleteSleepEntry(int id)
        {
            lock (S_locker)
            {
                return Database.Delete<Fall>(id);
            }
        }

        /// <summary>
        /// Check if sleep entry exists in db
        /// </summary>
        /// <returns>bool of sleep entry existing in db</returns>
        public bool SleepEntryExists()
        {
            lock (S_locker)
            {
                var count = (from i in Database.Table<SleepEntry>() select i).ToList().Count;
                if (count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion //Sleep Methods

        #endregion //Methods


    }
}



