using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Service.Filters
{
    public class FilterByRegion : IFilter<FilterDTO, List<HistoricalCulturalObjects>>
    {
        public List<HistoricalCulturalObjects> execute(FilterDTO input, List<HistoricalCulturalObjects> objects)
        {
            if (input.filter_region != null)
            {
                objects = objects.Where(o => o.region != null && o.region.Equals(input.filter_region)).ToList();
            }

            return objects;
        }
    }
}