using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace FYP_Droid.Fragments
{
    public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        #region Fields

        public static readonly string TAG = "X:" + typeof(TimePickerFragment).Name.ToUpper();
        Action<DateTime> m_timeSelectedHandler = delegate { };

        #endregion //Fields

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onTimeSelected"></param>
        /// <returns></returns>
        public static TimePickerFragment NewInstance(Action<DateTime> onTimeSelected)
        {
            TimePickerFragment fragment = new TimePickerFragment();

            fragment.m_timeSelectedHandler = onTimeSelected;

            return fragment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime current = DateTime.Now;

            TimePickerDialog dialog = new TimePickerDialog(Activity, this, current.Hour, current.Minute, true);

            return dialog;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="year"></param>
        /// <param name="monthOfYear"></param>
        /// <param name="dayOfMonth"></param>
        public void OnTimeSet(TimePicker view, int hour, int minutes)
        {
            DateTime current = DateTime.Now;
            DateTime selectedTime = new DateTime(current.Year, current.Month, current.Day, hour, minutes, 0);
            m_timeSelectedHandler(selectedTime);
        }
    }
}