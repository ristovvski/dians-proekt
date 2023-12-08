using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CulturallyHistoricalObjectsWebApp.Service.Filters;

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
            Pipe<FilterDTO, List<HistoricalCulturalObjects>> pipe =
                new Pipe<FilterDTO, List<HistoricalCulturalObjects>>();
            FilterByName filterByName = new FilterByName();
            FilterByType filterByType = new FilterByType();
            pipe.addFilter(filterByName);
            pipe.addFilter(filterByType);
            return pipe.runFilters(filterDTO, objects);
        }
    }
}