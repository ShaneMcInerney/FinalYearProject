using FYP.Business.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FYP.DataAccess
{
    public class RefactoredAppDatabase 
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
        public RefactoredAppDatabase(SQLiteConnection conn)
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

        /// <summary>
        /// Get allweight entries in db
        /// </summary>
        /// <returns>Enumerable of weight entries</returns>
        public IEnumerable<T> GetAll<T>() where T : BaseEntity, new()
        {
            lock (S_locker)
            {
                //all entries to list
                var output = (from i in Database.Table<T>() select i).ToList();
                //return list
                return output;
            }
        }

        /// <summary>
        /// Retrieve Latest Weight Entry in Db
        /// </summary>
        /// <returns></returns>
        public T GetLatestEntry<T>() where T : BaseEntity, new()
        {
            lock (S_locker)
            {
                //delete all in list
                return Database.Table<T>().LastOrDefault<T>();
            }
        }

        /// <summary>
        /// Get weight entry for given id
        /// </summary>
        /// <param name="id">id of weight entry to retrieve</param>
        /// <returns>weight entry matching given id</returns>
        public BaseEntity GetEntry<T>(int id) where T: BaseEntity,new()
        {
            lock (S_locker)
            {
                //get entry that matches given id
                return Database.Table<T>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary>
        /// Save/Update weight entry in db
        /// </summary>
        /// <param name="weightEntry">weight entry to update/save</param>
        /// <returns>id of saved weight entry</returns>
        public int SaveEntry<T>(BaseEntity Entry) where T : BaseEntity, new()
        {
            lock (S_locker)
            {
                //if id is not equal 0
                if (Entry.ID != 0)
                {
                    //update entry
                    Database.Update(Entry);
                    return Entry.ID;
                }
                else
                {
                    //otherwise insert new entry
                    return Database.Insert(Entry);
                }
            }
        }
/*
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
        }*/

        #endregion //Methods
    }
}
