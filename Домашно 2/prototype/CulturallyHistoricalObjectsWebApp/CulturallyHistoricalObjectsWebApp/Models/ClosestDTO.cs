using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class ClosestDTO
    {
        public HistoricalCulturalObjects HistoricalCulturalObjects { get; set; }
        public double distance {  get; set; }
    }
}