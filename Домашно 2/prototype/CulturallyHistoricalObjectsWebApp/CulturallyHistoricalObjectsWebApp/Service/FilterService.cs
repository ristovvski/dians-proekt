using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Service
{
    public class FilterService
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public List<HistoricalCulturalObjects> filterObjects(FilterDTO filterDTO)
        {
            List<HistoricalCulturalObjects> objects = db.culturalObjects.Include("type").ToList();
            ObjectTypesModel tmp = db.objectTypes.Find(filterDTO.type_id);
            filterDTO.ObjectTypesModel = tmp;
            if (filterDTO.name != null)
            {
                objects = objects.Where(o => o.name.ToLower().Contains(filterDTO.name.ToLower())).ToList();
            }
            if (filterDTO.ObjectTypesModel != null)
            {
                objects = objects.Where(o => o.type != null && o.type.Equals(filterDTO.ObjectTypesModel)).ToList();
            }

            return objects;
        }
    }
}