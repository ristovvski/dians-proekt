using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class HistoricalCOUserDTO
    {
        public List<int> favoritePlacesIds {  get; set; }
        public List<HistoricalCulturalObjects> HistoricalCulturalObjects { get; set; }
    }
}