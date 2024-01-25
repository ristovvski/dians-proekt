using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class HistoricalCulturalObjects
    {
        [Key]
        public int id { get; set; }

        //Historical Cultural Object name
        public String name { get; set; }

        //Historical Cultural Object type
        //Many to One, one Historical cultural object can have one type, one type can belong to multiple historical objects.
        public ObjectTypesModel type {  get; set; }

        //Region for historical cultural object
        //many to one, one historical cultural object can belong in one region, in one region belongs multiple historical objects.
        public Region region { get; set; }

        public string address {  get; set; }

        //longitude
        public double lon { get; set; }

        //latitude
        public double lat {  get; set; }
        // Navigation property for the ApplicationUser

        [JsonIgnore]
        //List of Users that the object is favorite to.
        public virtual List<ApplicationUser> FavoriteForUsers { get; set; }
    }
}