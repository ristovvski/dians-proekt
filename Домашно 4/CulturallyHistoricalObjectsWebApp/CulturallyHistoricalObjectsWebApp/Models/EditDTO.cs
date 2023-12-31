using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class EditDTO
    {
        public HistoricalCulturalObjects historicalCultralObject { get; set; }

        public List<ObjectTypesModel> objectTypes { get; set; }

        public int objectTypeId {  get; set; }

        public List<Region> regions {  get; set; }

        public int region_id { get; set; }
    }
}