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
            List<HistoricalCulturalObjects> objects = db.culturalObjects.Include("type").Include("region").ToList();
            ObjectTypesModel tmp = db.objectTypes.Find(filterDTO.type_id);
            Region region_temp = db.regions.Find(filterDTO.region_id);
            filterDTO.ObjectTypesModel = tmp;
            filterDTO.filter_region = region_temp;
            Pipe<FilterDTO, List<HistoricalCulturalObjects>> pipe =
                new Pipe<FilterDTO, List<HistoricalCulturalObjects>>();
            FilterByName filterByName = new FilterByName();
            FilterByType filterByType = new FilterByType();
            FilterByRegion filterByRegion = new FilterByRegion();
            pipe.addFilter(filterByName);
            pipe.addFilter(filterByType);
            pipe.addFilter(filterByRegion);
            return pipe.runFilters(filterDTO, objects);
        }
    }
}