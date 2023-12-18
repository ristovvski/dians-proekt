using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Service
{
    public class DistanceService
    {


        static double toRadians(double angleIn10thofaDegree)
        {
            return angleIn10thofaDegree * Math.PI / 180;
        }

        public ClosestDTO distance(FilterDTO model, List<HistoricalCulturalObjects> objects)
        {
            objects = objects.OrderBy(o => o.id).ToList();

            HistoricalCulturalObjects closest = objects.ElementAt(0);

            bool flag = true;
            double minDistance = 9999.99;

            ClosestDTO temp = new ClosestDTO();

            foreach (HistoricalCulturalObjects obj in objects)
            {
                double lon1 = toRadians(model.userLongitude);
                double lat1 = toRadians(model.userLatitude);

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
                    temp.HistoricalCulturalObjects = obj;
                    temp.distance = distance;
                    flag = false;
                }
                else
                {

                    if (distance < minDistance)
                    {
                        temp.HistoricalCulturalObjects = obj;
                        temp.distance = distance;
                        minDistance = distance;
                    }
                }

                Console.WriteLine(obj.name + " " + distance + " \n");

            }

            return temp;
        }
    }
}