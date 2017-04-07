using Android.App;
using Android.Content;
using Android.Telephony;
using FYP.Business.Managers;
using FYP.Business.Models;
using FYP.DataAccess;
using FYP_Droid.Utilities;
using System;
using System.Collections.Generic;

namespace FYP_Droid.Business.Managers
{
    public class EmergencyMessageManagerAndroid : EmergencyMessageManager
    {
        #region Fields

        private Context m_context;

        #endregion //Fields

        #region Constructors

        public EmergencyMessageManagerAndroid()
        {

        }

        public EmergencyMessageManagerAndroid(AppDatabase database)
        {
            this.AppDatabase = database;
        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_context
        /// </summary>
        public Context Context
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

        #endregion //Property Accessors

        #region Mehtods

        /// <summary>
        /// Sends an emergency message, in the background, to an individual contact
        /// </summary>
        public void SendEmergencyMessage(EmergencyContact contact, EmergencyMessage message)
        {
            //send sms
            SmsManager.Default.SendTextMessage(contact.ContactNumber, null, message.Message, null, null);
        }


        /// <summary>
        /// Sends an emergency message, in the background, to a list of emergency contacts
        /// </summary>
        public async void SendEmergencyMessageToList(IEnumerable<EmergencyContact> contactList, EmergencyMessage message)
        {
            String SENT = "SMS_SENT";
            String DELIVERED = "SMS_DELIVERED";
            string locationContent = "";
            //if the message is to have location included
            if (message.IncludeLocation == true)
            {
                //if connected to network
                if (ConnectivityHelper.ConnectedToNetwork(this.Context))
                {
                    //get address
                    locationContent += await GlobalUtilities.LocationManager.GetAddress(this.Context);
                }
                else
                {
                    //get gps co ordinates
                    var location = await GlobalUtilities.LocationManager.GetGPSCoordinates(this.Context);

                    locationContent += location.Latitude.ToString() + "," + location.Longitude.ToString();
                }
                //append location to message
                message.Message += "\n" + locationContent;
            }

            //sent pending intent
            PendingIntent sentPI = PendingIntent.GetBroadcast(this.Context, 0, new Intent(SENT), 0);
            //delivered pending intent
            PendingIntent deliveredPI = PendingIntent.GetBroadcast(this.Context, 0, new Intent(DELIVERED), 0);
            //for each contact in contacts list
            foreach (var contact in contactList)
            {
                //send text message
                SmsManager.Default.SendTextMessage(contact.ContactNumber, null, message.Message, sentPI, deliveredPI);
            }
        }
        #endregion //Methods
    }
}