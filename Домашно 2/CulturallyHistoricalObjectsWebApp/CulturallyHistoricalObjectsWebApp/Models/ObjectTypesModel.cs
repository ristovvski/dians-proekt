using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class ObjectTypesModel
    {
        [Key]
        public int id {  get; set; }

        public String type {  get; set; }

        public ObjectTypesModel(String type)
        {
            this.type = type;
        }
    }
}