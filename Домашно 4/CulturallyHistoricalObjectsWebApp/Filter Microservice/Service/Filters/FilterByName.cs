using System.Collections.Generic;
using System.Linq;
using CulturallyHistoricalObjectsWebApp.Models;

namespace Filter_Microservice.Service.Filters
{
    public class FilterByName : IFilter<FilterDTO, List<HistoricalCulturalObjects>>
    {
        public List<HistoricalCulturalObjects> execute(FilterDTO input, List<HistoricalCulturalObjects> objects)
        {
            objects = objects.Where(o => o.name.ToLower().Contains(input.name.ToLower())).ToList();
            return objects;
        }
    }
}