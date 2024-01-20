using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distance_Microservice.Service
{
    public class DistanceService
    {
        static double toRadians(double angleIn10thofaDegree)
        {
            return angleIn10thofaDegree * Math.PI / 180;
        }

        public ClosestFavoriteDTO calculateShortest(DistanceRequestDTO toCalculate)
        {
            List<HistoricalCulturalObjects> objects = toCalculate.FilteredObjects;

            objects = objects.OrderBy(o => o.id).ToList();

            //In objects we have the historical cultural objects that remained after filtering, if there are none, we return null.
            if (objects.Count <= 0)
            {
                return null;
            }
            //the rest of the code is for calculating the distance of an objet based of it's location and the user's location
            //(longitude and latitude)

            HistoricalCulturalObjects closest = objects.ElementAt(0);

            bool flag = true;
            double minDistance = 9999.99;

            ClosestFavoriteDTO temp = new ClosestFavoriteDTO();

            foreach (HistoricalCulturalObjects obj in objects)
            {
                double lon1 = toRadians(toCalculate.UserLongitude);
                double lat1 = toRadians(toCalculate.UserLatitude);

                double lon2 = toRadians(obj.lon);
                double lat2 = toRadians(obj.lat);

                // Haversine formula 
                double dlon = lon2 - lon1;
                double dlat = lat2 - lat1;
                double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);

                double c = 2 * Math.Asin(Math.Sqrt(a));

                double r = 6371;
                double distance = c * r;

                distance = Math.Round(distance, 2);

                if (flag)
                {
                    temp.historicalCulturalObject = obj;
                    temp.distance = distance;
                    flag = false;
                }
                else
                {

                    if (distance < minDistance)
                    {
                        temp.historicalCulturalObject = obj;
                        temp.distance = distance;
                        minDistance = distance;
                    }
                }
            }

            return temp;
        }
    }
}
