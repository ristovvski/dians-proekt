using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class FilterDTO
    {
        public string name { get; set; }
        public List<ObjectTypesModel> types {  get; set; }
        public int? type_id {  get; set; }

        public ObjectTypesModel ObjectTypesModel { get; set; }

        public double userLatitude { get; set; }

        public double userLongitude { get; set; }
    }
}