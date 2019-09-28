using System;

namespace FindMyHouse
{
    public static class DistanceExtension
    {
        public static double DistanceTo(this House house, double sHlat, double sHlon)
        {
            try
            {
                if (house.coords == null || house.coords.lat == 0 || house.coords.lon == 0)
                    return 0;
                var baseRad = Math.PI * sHlat / 180;
                var targetRad = Math.PI * house.coords.lat / 180;
                var thetaRad = Math.PI * (sHlon - house.coords.lon) / 180;

                double distance = Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                    Math.Cos(targetRad) * Math.Cos(thetaRad);

                distance = Math.Acos(distance);

                distance = distance * 180 / Math.PI;
                distance = distance * 60 * 1.1515;

                return distance * 1.609344;
            }
            catch { return 0; }
        }
    }
}
