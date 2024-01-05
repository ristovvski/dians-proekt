using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    //EditDTO is a DataTransportObject used to transport data from the Edit view and to the Edit view
    public class EditAndCreateDTO
    {
        //The historical cultural object that we want to edit
        public HistoricalCulturalObjects historicalCultralObject { get; set; }

        //The types and regions that we can assign to the object displayed in the view in a dropdown.

        public List<ObjectTypesModel> objectTypes { get; set; }

        public List<Region> regions {  get; set; }

        //it's ids are the values in the form and they are assigned to objectTypeId and region_id.
        public int objectTypeId { get; set; }

        public int region_id { get; set; }
    }
}