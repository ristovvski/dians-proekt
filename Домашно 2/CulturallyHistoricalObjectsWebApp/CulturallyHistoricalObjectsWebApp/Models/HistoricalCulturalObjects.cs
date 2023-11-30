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

        public String name { get; set; }

        public String type {  get; set; }

        public double lon { get; set; }

        public double lat {  get; set; }
    }
}