using System.Collections.Generic;
using System.Linq;
using CulturallyHistoricalObjectsWebApp.Models;

namespace CulturallyHistoricalObjectsWebApp.Service.Filters
{
    public class FilterByType : IFilter<FilterDTO, List<HistoricalCulturalObjects>>
    {
        public List<HistoricalCulturalObjects> execute(FilterDTO input, List<HistoricalCulturalObjects> objects)
        {
            
            //filter the historical cultural objects that are from a specific type stored in input.ObjectTypesModel
            objects = objects.Where(o => o.type != null && o.type.Equals(input.ObjectTypesModel)).ToList();

            return objects;
        }
    }
}