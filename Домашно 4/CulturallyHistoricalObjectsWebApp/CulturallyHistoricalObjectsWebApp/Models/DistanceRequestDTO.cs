using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class DistanceRequestDTO
    {
        public List<HistoricalCulturalObjects> FilteredObjects { get; set; }
        public double UserLatitude { get; set; }
        public double UserLongitude { get; set; }
    }
}