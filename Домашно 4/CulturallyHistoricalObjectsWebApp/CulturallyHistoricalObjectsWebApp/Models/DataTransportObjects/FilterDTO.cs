using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    //FilterDTO is a DataTransportObject used for passing data to the view and from the view back to the controller.
    public class FilterDTO
    {
        //In FilterDTO.name is stored the name by which the user filters in the filter form that we have in the Index page.
        public string name { get; set; }
        //In the List of ObjectTypesModel and Regions we display in a dropdown the types and regions by which the user
        //can filter.
        public List<ObjectTypesModel> types {  get; set; }
        public List<Region> regions {  get; set; }

        //In type_id and region_id we have the ids of the region and type by which the user filters. (if the user doesn't use
        //this filter, then these are null
        public int? type_id {  get; set; }

        public int? region_id {  get; set; }

        //type_id and region_id are later casted into ObjectTypesMoel and Region and are used for filtering.
        public ObjectTypesModel ObjectTypesModel { get; set; }

        public Region filter_region { get; set; }


        //these are the user longitude and latitude and are taken based on the location of the user.
        public double userLatitude { get; set; }

        public double userLongitude { get; set; }
    }
}