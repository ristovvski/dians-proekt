using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class ClosestFavoriteDTO
    {
        public HistoricalCulturalObjects historicalCulturalObject { get; set; }

        public double distance { get; set; }

        public List<int> favoritePlacesIds { get; set; }
    }
}