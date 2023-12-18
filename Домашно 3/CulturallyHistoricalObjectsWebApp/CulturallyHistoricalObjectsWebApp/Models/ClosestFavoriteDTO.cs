using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class ClosestFavoriteDTO
    {
        public ClosestDTO closestDistance {  get; set; }

        public List<int> favoritePlacesIds { get; set; }
    }
}