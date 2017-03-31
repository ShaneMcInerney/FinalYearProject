using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace FYP_Droid.Fragments
{
    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {

        #region Fields

        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
        Action<DateTime> m_dateSelectedHandler = delegate { };

        #endregion //Fields


        #region Property Accessors


        #endregion //Property Accessors


        #region Methods

        /// <summary>
        /// Called when day set
        /// </summary>
        /// <param name="view"></param>
        /// <param name="year"></param>
        /// <param name="monthOfYear"></param>
        /// <param name="dayOfMonth"></param>
        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            //setting selected date
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            //pass date
            m_dateSelectedHandler(selectedDate);
        }

        /// <summary>
        /// New instance of fragment
        /// </summary>
        /// <param name="onDateSelected"></param>
        /// <returns></returns>
        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            //set fragment
            DatePickerFragment fragment = new DatePickerFragment();
            //setting date selected
            fragment.m_dateSelectedHandler = onDateSelected;
            //return fragment
            return fragment;
        }

        /// <summary>
        /// On dialog created
        /// </summary>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            //get current date
            DateTime current = DateTime.Now;
            //new dialog
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, current.Year, current.Month, current.Day);
            //return dialog
            return dialog;
        }

        #endregion //Methods
    }
}