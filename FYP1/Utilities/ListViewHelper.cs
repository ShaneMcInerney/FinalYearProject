using Android.Widget;
using System.Collections.Generic;

namespace FYP_Droid.Utilities
{
    public class ListViewHelper<T>
    {
        #region Fields

        private ListView m_listView;
        private List<T> m_list;
        private IEnumerable<T> m_listFromDb;
        private List<string> m_stringList;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="listFromDb"></param>
        public ListViewHelper(ListView listView, IEnumerable<T> listFromDb)
        {
            this.m_listView = listView;
            this.m_list = new List<T>();
            this.m_stringList = new List<string>();
            this.m_listFromDb = listFromDb;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_listView
        /// </summary>
        public ListView ListView
        {
            get
            {
                return m_listView;
            }

            set
            {
                m_listView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_list
        /// </summary>
        public List<T> List
        {
            get
            {
                return m_list;
            }

            set
            {
                m_list = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_stringList
        /// </summary>
        public List<string> StringList
        {
            get
            {
                return m_stringList;
            }

            set
            {
                m_stringList = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listFromDb
        /// </summary>
        public IEnumerable<T> ListFromDb
        {
            get
            {
                return m_listFromDb;
            }

            set
            {
                m_listFromDb = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Populates list with values from the database
        /// </summary>
        public void PopulateListFromDatabase()
        {
            //itterate over list from db
            foreach (var item in ListFromDb)
            {
                //if item is not null
                if (item != null)
                {
                    //add item to list
                    List.Add(item);
                    //added to string list
                    StringList.Add(item.ToString());
                }

            }
        }

        /// <summary>
        /// Displays accelerometer readings in list view
        /// </summary>
        public ListView DisplayListAsSimpleListView(Android.Content.Context context)
        {
            //set list adapter
            ListView.Adapter = new ArrayAdapter(context, Android.Resource.Layout.SimpleListItem1, StringList.ToArray());
            //return list view
            return ListView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ListView DisplayListAsSelectDialogMultiChoiceView(Android.Content.Context context)
        {
            //set list adapter
            ListView.Adapter = new ArrayAdapter(context, Android.Resource.Layout.SelectDialogMultiChoice, StringList.ToArray());
            //return list view
            return ListView;
        }


        #endregion //Methods
    }
}