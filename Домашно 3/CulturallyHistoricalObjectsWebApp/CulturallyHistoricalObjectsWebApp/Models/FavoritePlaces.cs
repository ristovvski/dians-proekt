using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    public class FavoritePlace
    {
        [Key]
        public int Id { get; set; }

        // Foreign key properties
        public string ApplicationUserId { get; set; }
        public int HistoricalCulturalObjectId { get; set; }

        // Navigation properties
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual HistoricalCulturalObjects HistoricalCulturalObject { get; set; }
    }
}