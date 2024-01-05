using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{

    //This is a separate class for the Regions that we have in the data, (Skopje region/Vardar Region, etc.)

    //We create a separate Model for this and a separate table for this because we want to work with the IDs in future for filtering,
    //instead of using Strings, because it's more complicated to use Strings.

    //For example: When adding new Region, we just add a new row in the Database for that Region and later we have it as option everywhere,
    //in filtering, editing, creating new objects etc.

    //if we used Strings, we would have to manually update other parts of the code with the new String, or if we wanted that to be more
    //automated, we would have to go through all the Historical Objects and retrieve the regions.
    public class Region
    {
        [Key]
        public int Id { get; set;}

        //name of the region
        public string name { get; set;}

        public Region() { this.name = null; }

        public Region(string name) { this.name = name; }
    }
}