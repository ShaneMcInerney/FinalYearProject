using Android.App;
using Android.Provider;
using FYP.Business.Models;
using System.Collections.Generic;

namespace FYP_Droid.Utilities
{
    public class ContactsAdapter
    {
        #region Fields

        List<EmergencyContact> m_contactList;
        private List<string> m_listSelectedItems;
        Activity m_activity;

        #endregion //Fields

        #region Costructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activity"></param>
        public ContactsAdapter(Activity activity)
        {
            this.Activity = activity;
            this.m_listSelectedItems = new List<string>();
            FillContacts();
        }

        #endregion //Property Accessors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_contactList
        /// </summary>
        public List<EmergencyContact> ContactList
        {
            get
            {
                return m_contactList;
            }

            set
            {
                m_contactList = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_activity
        /// </summary>
        public Activity Activity
        {
            get
            {
                return m_activity;
            }

            set
            {
                m_activity = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listSelectedItems
        /// </summary>
        public List<string> ListSelectedItems
        {
            get
            {
                return m_listSelectedItems;
            }

            set
            {
                m_listSelectedItems = value;
            }
        }


        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmergencyContact> FillContacts()
        {
            //path to contacts
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            //contact values to grab
            string[] projection = {

                ContactsContract.Contacts.InterfaceConsts.DisplayName,

                ContactsContract.CommonDataKinds.Phone.Number
            };
            //cursor to move through contacs
            var cursor = Activity.ContentResolver.Query(uri, projection, null, null, null);
            //new contact list
            ContactList = new List<EmergencyContact>();
            //if can move to first
            if (cursor.MoveToFirst())
            {
                do
                {
                    //add contact
                    ContactList.Add(new EmergencyContact
                    {
                        Name = cursor.GetString(cursor.GetColumnIndex(projection[0])),

                        ContactNumber = cursor.GetString(cursor.GetColumnIndex(projection[1]))

                    });
                } while (cursor.MoveToNext());
            }
            //sort list
            ContactList.Sort((x, y) => string.Compare(x.Name, y.Name));

            return ContactList;
        }

        #endregion //Methods
    }
}