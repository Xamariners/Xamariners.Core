using System;
using Xamarin.Essentials;


namespace Xamariners.Core.Common.Helpers
{
    public static class GeolocationHelpers
    {
        public enum GeoUnit
        {
            Kilometer,
            Meter,
            Mile,
            Nautical
        }

        public static double GetDistanceToEx(this Location fromLocation, Location toLocation, GeoUnit geoUnit = GeoUnit.Meter)
        {  
            return GetDistance(fromLocation.Latitude, fromLocation.Longitude, toLocation.Latitude, toLocation.Longitude,
                geoUnit);
        }

        /// <summary>
        ///     Returns the distance between the latitude and longitude coordinates that are specified by this GeoCoordinate and
        ///     another specified GeoCoordinate.
        /// </summary>
        /// <returns>
        ///     The distance between the two coordinates, in meters.
        /// </returns>
        /// <param name="other">The GeoCoordinate for the location to calculate the distance to.</param>
        public static double GetDistanceTo(this Location fromLocation, Location toLocation)
        {
            if (double.IsNaN(fromLocation.Latitude) || double.IsNaN(fromLocation.Longitude) || double.IsNaN(toLocation.Latitude) ||
                double.IsNaN(toLocation.Longitude))
            {
                throw new ArgumentException("Argument latitude or longitude is not a number");
            }

            var d1 = fromLocation.Latitude * (Math.PI / 180.0);
            var num1 = fromLocation.Longitude * (Math.PI / 180.0);
            var d2 = toLocation.Latitude * (Math.PI / 180.0);
            var num2 = toLocation.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }


        /// <summary>
        /// get distance
        /// </summary>
        /// <param name="latitude1"></param>
        /// <param name="longitude1"></param>
        /// <param name="latitude2"></param>
        /// <param name="longitude2"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2, GeoUnit unit)
        {

            double theta = longitude1 - longitude2;

            double dist = Math.Sin(Deg2Rad(latitude1)) * Math.Sin(Deg2Rad(latitude2))
                          + Math.Cos(Deg2Rad(latitude1)) * Math.Cos(Deg2Rad(latitude2)) * Math.Cos(Deg2Rad(theta));

            dist = Math.Acos(dist);

            dist = Rad2Deg(dist);

            // in miles
            dist = dist * 60 * 1.1515;

            if (unit == GeoUnit.Kilometer )
                dist = dist * 1.609344;
            else if (unit == GeoUnit.Nautical)
                dist = dist * 0.8684;

            return (dist);
        }

        private static double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
