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
            //take all the objects from the database
            List<HistoricalCulturalObjects> objects = db.culturalObjects.Include("type").Include("region").ToList();

            //FilterDTO is a Data Transport Object which helps us to transport the data from the Filter form we have in
            //HistoricalCulturalObjects/Index.

            //Taking the type of object that we the user used for filtering.
            ObjectTypesModel type_temp = db.objectTypes.Find(filterDTO.type_id);
            //Taking the region that the user filtered by.
            Region region_temp = db.regions.Find(filterDTO.region_id);

            Pipe<FilterDTO, List<HistoricalCulturalObjects>> pipe =
                new Pipe<FilterDTO, List<HistoricalCulturalObjects>>();


            if (type_temp != null)
            {
                filterDTO.ObjectTypesModel = type_temp;
                FilterByType filterByType = new FilterByType();
                pipe.addFilter(filterByType);
            }
            if (region_temp != null)
            {
                filterDTO.filter_region = region_temp;
                FilterByRegion filterByRegion = new FilterByRegion();
                pipe.addFilter(filterByRegion);
            }
            if (filterDTO.name != null)
            {
                FilterByName filterByName = new FilterByName();
                pipe.addFilter(filterByName);
            }
            return pipe.runFilters(filterDTO, objects);
        }
    }
}