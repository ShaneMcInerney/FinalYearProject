using Android.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace FYP_Droid.Utilities
{
    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        #region Fields

        private Activity m_context;
        private List<string> m_listDataHeader; // header titles

        // child data in format of header title, child title
        private Dictionary<string, List<string>> m_listDataChild;
        ExpandableListView m_expandList;

        #endregion //Fields

        #region Costructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="listDataHeader"></param>
        /// <param name="listChildData"></param>
        /// <param name="mView"></param>
        public ExpandableListAdapter(Activity context, List<string> listDataHeader, Dictionary<string, List<string>> listChildData, ExpandableListView mView)
        {
            Context = context;
            ListDataHeader = listDataHeader;
            ListDataChild = listChildData;
            ExpandList = mView;
        }

        #endregion //Constructors

        #region Property Accessors


        /// <summary>
        /// Gets  ListDataHeader.Count
        /// </summary>
        public override int GroupCount
        {
            get
            {
                return ListDataHeader.Count;
            }
        }

        /// <summary>
        /// returns false
        /// </summary>
        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets/Sets m_context
        /// </summary>
        public Activity Context
        {
            get
            {
                return m_context;
            }

            set
            {
                m_context = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listDataHeader
        /// </summary>
        public List<string> ListDataHeader
        {
            get
            {
                return m_listDataHeader;
            }

            set
            {
                m_listDataHeader = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_listDataChild
        /// </summary>
        public Dictionary<string, List<string>> ListDataChild
        {
            get
            {
                return m_listDataChild;
            }

            set
            {
                m_listDataChild = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_expandList
        /// </summary>
        public ExpandableListView ExpandList
        {
            get
            {
                return m_expandList;
            }

            set
            {
                m_expandList = value;
            }
        }

        #endregion //Property Accessors

        #region Methods


        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <param name="childPosition"></param>
        /// <returns></returns>
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return ListDataChild[ListDataHeader[groupPosition]][childPosition];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <param name="childPosition"></param>
        /// <returns></returns>
        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <returns></returns>
        public override int GetChildrenCount(int groupPosition)
        {
            int childCount = 0;
            try
            {
                childCount = ListDataChild[ListDataHeader[groupPosition]].Count;
            }
            catch (Exception e)
            {
                childCount = 0;
            }

            return childCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <param name="childPosition"></param>
        /// <param name="isLastChild"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            string childText = (string)GetChild(groupPosition, childPosition);
            if (convertView == null)
            {
                convertView = Context.LayoutInflater.Inflate(Resource.Layout.listHeaderView, null);
            }

            TextView txtListChild = (TextView)convertView.FindViewById(Resource.Id.submenu);
            txtListChild.Text = childText;

            return convertView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <returns></returns>
        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return new JavaObjectWrapper<string>() { Obj = ListDataHeader[groupPosition] };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <returns></returns>
        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <param name="isExpanded"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            string headerTitle = ListDataHeader[groupPosition];


            convertView = convertView ?? Context.LayoutInflater.Inflate(Resource.Layout.listHeaderView, null);


            TextView lblListHeader = (TextView)convertView.FindViewById(Resource.Id.submenu);
            lblListHeader.Text = headerTitle;

            return convertView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <param name="childPosition"></param>
        /// <returns></returns>
        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class JavaObjectWrapper<T> : Java.Lang.Object
        {
            public T Obj { get; set; }
        }

        #endregion //Methods

    }
}