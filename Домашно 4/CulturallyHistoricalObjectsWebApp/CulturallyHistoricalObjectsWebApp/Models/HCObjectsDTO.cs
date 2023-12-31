using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
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