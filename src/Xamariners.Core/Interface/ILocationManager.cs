using Xamarin.Essentials;
using Xamariners.Core.Common.Model;

namespace Xamariners.Core.Interface
{
    using System;

    public interface ILocationManager
    {
        Location GetCoordinate();
    
        void StartLocationUpdates ();

        /// <summary>
        /// Requests access to location services
        /// </summary>
        bool RequestLocationAccess(Action onAuthorizedAction = null, Action onDeniedAction = null);

        /// <summary>
        /// Verifies if access to location services is authorized
        /// </summary>
        bool IsLocationAccessAuthorized();

        /// <summary>
        /// Verifies if access to location services is denied
        /// </summary>
        bool IsLocationAccessDenied();

        /// <summary>
        /// Verifies if access to location services is not determined
        /// </summary>
        bool IsLocationAccessNotDetermined();
    }

    public class LocationUpdatedEventArgs : EventArgs
    {
        public Location Coordinates;

        public LocationUpdatedEventArgs(Location coordinates)
        {
            this.Coordinates = coordinates;
        }
    }
}