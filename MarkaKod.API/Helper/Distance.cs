namespace MarkaKod.API
{
    public static class Distance
    {
        private static double rad2deg(double rad)
        {

            return rad / Math.PI * 180.0;

        }

        private static double deg2rad(double deg)
        {

            return deg * Math.PI / 180.0;

        }

        public static double CalculateDistance(double lat1, double long1, double lat2, double long2)
        {

            double teta_degeri = long1 - long2;
            double mil = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
            Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(teta_degeri));

            mil = Math.Acos(mil);
            mil = rad2deg(mil);
            mil = mil * 60 * 1.1515;

            double kilometre = mil * 1.609344;

            return kilometre;
        }
    }
}
