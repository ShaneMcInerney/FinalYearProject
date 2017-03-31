using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Managers;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System.Collections.Generic;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{


    public class BaseFragment : Fragment
    {
        #region Fields

        private View m_fragView;
        private SupprtToolbar m_toolbar;
        private AlertDialog.Builder m_alertBuilder;
        private Dialog m_dialog;

        #endregion //Fields


        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_fragView
        /// </summary>
        public View FragView
        {
            get
            {
                return m_fragView;
            }

            set
            {
                m_fragView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_toolbar
        /// </summary>
        public SupprtToolbar Toolbar
        {
            get
            {
                return m_toolbar;
            }

            set
            {
                m_toolbar = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_alertBuilder
        /// </summary>
        public AlertDialog.Builder AlertBuilder
        {
            get
            {
                return m_alertBuilder;
            }

            set
            {
                m_alertBuilder = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_dialog
        /// </summary>
        public Dialog Dialog
        {
            get
            {
                return m_dialog;
            }

            set
            {
                m_dialog = value;
            }
        }

        #endregion //Property Accessors

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            AlertBuilder = new AlertDialog.Builder(Activity);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        /// <summary>
        /// Load the passed in fragment
        /// </summary>
        public void LoadFragment(Fragment fragment)
        {
            //set transaction manager
            FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            //set transition
            fragmentTransaction.SetTransition(FragmentTransit.FragmentFade);
            //set fragment
            var frag = fragment;
            //replace fagment in frame
            fragmentTransaction.Replace(Resource.Id.drawer_frame, frag);
            //commit transaction
            fragmentTransaction.Commit();
        }

        public void InflateView(LayoutInflater inflater, ViewGroup container, int resource)
        {
            this.FragView = inflater.Inflate(resource, container, false);
        }
        /*    /// <summary>
            /// 
            /// </summary>
            /// <param name="weightEntries"></param>
            /// <param name="graphType"></param>
            private void UpdateListViewAndPlotView<T>(List<T> entries, GraphType graphType, ListViewHelper<T> listViewHelper, ListView listView, OxyPlot.IPlotView plot)
            {
                //new list view helper
                listViewHelper = new ListViewHelper<T>(listView, entries);

                //Populatings Weight Entries from database
                listViewHelper.PopulateListFromDatabase();

                //Display Weight Entries in List View
                listView = listViewHelper.DisplayListAsSimpleListView(Activity.ApplicationContext);
                listView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
                if(T.GetType()==typeof(WeightEntry))
                {

                }
                //setting plot view model
                WeightHistoryPlotView.Model = GlobalUtilities.GraphManager.CreateWeightPlotModel("Weight History", entries, graphType);
            }*/
    }
}