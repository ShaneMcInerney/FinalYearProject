using Android.Content;
using Android.Locations;
using System.Threading.Tasks;

namespace FYP_Droid.Business.Managers
{
    public class LocationManagerAndroid //: ILocationManager
    {
        #region Fields

        private Geocoder m_geoCoder;
        private static LocationManager m_locationManager;
        private Location m_location;
        private Criteria m_locationCriteria;

        #endregion //Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LocationManagerAndroid()
        {

        }

        #endregion //Constructors

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_geoCoder
        /// </summary>
        public Geocoder GeoCoder
        {
            get
            {
                return m_geoCoder;
            }

            set
            {
                m_geoCoder = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_locationManager
        /// </summary>
        public static LocationManager LocationManager
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
        /// Gets/Sets m_location
        /// </summary>
        public Location Location
        {
            get
            {
                return m_location;
            }

            set
            {
                m_location = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_locationCriteria
        /// </summary>
        public Criteria LocationCriteria
        {
            get
            {
                return m_locationCriteria;
            }

            set
            {
                m_locationCriteria = value;
            }
        }


        #endregion //Property Accessors

        #region Methods

        /// <summary>
        /// Get an estimated address of the user's location
        /// </summary>
        /// <param name="context">Application context</param>
        /// <returns>string of the users address</returns>
        public async Task<string> GetAddress(Context context)
        {
            //get gps coordinates
            await GetGPSCoordinates(context);
            //default address to return string
            string addressToReturn = "Unable to determine users location.";
            //if location is not null
            if (Location != null)
            {
                addressToReturn = "";
                //new geocoder
                GeoCoder = new Geocoder(context);
                //get possible addresses
                var possibleAddresses = await GeoCoder.GetFromLocationAsync(Location.Latitude, Location.Longitude, 1);
                foreach (var address in possibleAddresses)
                {
                    //append address
                    addressToReturn += address.FeatureName + "\n";
                    addressToReturn += address.Thoroughfare + "\n";
                    addressToReturn += address.CountryName + "\n";
                    addressToReturn += address.SubLocality + "\n";
                    addressToReturn += address.AdminArea + "\n";
                }
            }
            //return address
            return addressToReturn;
        }

        /// <summary>
        /// Get GPS coordinates for user's current location
        /// </summary>
        /// <param name="context">application context</param>
        /// <returns>the users location</returns>
        public async Task<Location> GetGPSCoordinates(Context context)
        {
            //setting location manager
            LocationManager = (LocationManager)context.GetSystemService(Context.LocationService);
            //creating new location criteria
            LocationCriteria = new Criteria();
            //setting location caccuracy
            LocationCriteria.Accuracy = Accuracy.Coarse;
            //setting location power requirement
            LocationCriteria.PowerRequirement = Power.Low;
            //get best location provider for criteria
            string locationProvider = LocationManager.GetBestProvider(LocationCriteria, true);
            //run task 
            Task t = Task.Run(() =>
             {
                //get the user's last knwn location
                Location = LocationManager.GetLastKnownLocation(locationProvider);
             }
            );
            //wait for task to end
            t.Wait();

            //return location
            return Location;
        }


        #endregion //Methods
    }
}