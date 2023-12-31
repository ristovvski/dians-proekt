using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set;}

        public string name { get; set;}

        public Region() { this.name = null; }

        public Region(string name) { this.name = name; }
    }
}