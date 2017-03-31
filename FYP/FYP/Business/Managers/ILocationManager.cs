using System.Threading.Tasks;

namespace FYP.Business.Managers
{
    public interface ILocationManager
    {
        /// <summary>
        /// Returns Gps coordinates of phone
        /// </summary>
        /// <returns>string containing gps co ordinates</returns>
        Task<string> GetGPSCoordinates();

        /// <summary>
        /// Returns address based on gps co-ordinates
        /// </summary>
        /// <returns>A best guess of the phones address, based on gps coordinates</returns>
        Task<string> GetAddress();

    }
}
