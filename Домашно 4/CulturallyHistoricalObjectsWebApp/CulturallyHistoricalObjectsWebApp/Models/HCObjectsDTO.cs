using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{

    //HCObjectsDTO is a DataTransportObject that helps in reading data from the JSON file.

    //Basically, in the JSON we have data that consists of name, type, lon, lat, state, full_address (there are more attributes here,
    //but these are the only ones we need).
    //So, we have a class that is used to help in Deserializing the JSON, an that's how we have a List of HCObjectsDTO,
    //all with data that is later used in creating and populating the HistoricalCulturalObjects table, Regions table and ObjectTypes table
    //in the database with data.
    public class HCObjectsDTO
    {
        public String name {  get; set; }
        public String type {  get; set; }
        public double lon {  get; set; }
        public double lat {  get; set; }
        public string state {  get; set; }
        public string full_address {  get; set; }
    }
}