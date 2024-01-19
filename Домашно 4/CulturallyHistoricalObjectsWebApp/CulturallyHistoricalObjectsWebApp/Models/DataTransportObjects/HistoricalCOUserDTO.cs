using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    //HIstorical Cultural Objects / User DataTransportObject

    //The purpose of this object is to pass to the view for FindAll the filtered historical cultural objects and the IDs of the 
    //historical cultural objects that are favorite for the user that is logged in.

    //We retrieve the Id of the user that is logged in in the Historical CUltural Objects controller
    //check FindAll method there for further explanation.
    public class HistoricalCOUserDTO
    {
        public List<int> favoritePlacesIds {  get; set; }
        public List<HistoricalCulturalObjects> HistoricalCulturalObjects { get; set; }
    }
}