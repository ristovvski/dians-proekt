using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filter_Microservice.Service.Filters
{
    public class FilterByRegion : IFilter<FilterDTO, List<HistoricalCulturalObjects>>
    {
        public List<HistoricalCulturalObjects> execute(FilterDTO input, List<HistoricalCulturalObjects> objects)
        {
            objects = objects.Where(o => o.region != null && o.region.Equals(input.filter_region)).ToList();

            return objects;
        }
    }
}