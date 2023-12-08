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

        public Dictionary<string, ClosestDTO> distance(FilterDTO model, List<HistoricalCulturalObjects> objects)
        {

            
            
            Dictionary<string, ClosestDTO> objectsByType = new Dictionary<string, ClosestDTO>();

            objects = objects.OrderBy(o => o.id).ToList();

            foreach (HistoricalCulturalObjects obj in objects)
            {
                ClosestDTO temp = new ClosestDTO();

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
                temp.distance = Math.Round(distance, 2);
                temp.HistoricalCulturalObjects = obj;

                if (!objectsByType.ContainsKey(obj.type.type))
                {
                    objectsByType[obj.type.type] = temp;
                }
                else
                {

                    // Compare the calculated distance with the existing distance for the type
                    if (temp.distance < objectsByType[obj.type.type].distance)
                    {
                        // If the new distance is smaller, update the value in the dictionary
                        objectsByType[obj.type.type] = temp;
                    }
                }
            }

            return objectsByType;
        }
    }
}