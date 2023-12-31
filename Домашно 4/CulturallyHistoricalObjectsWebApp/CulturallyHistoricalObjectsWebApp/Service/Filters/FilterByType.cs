using System.Collections.Generic;
using System.Linq;
using CulturallyHistoricalObjectsWebApp.Models;

namespace CulturallyHistoricalObjectsWebApp.Service.Filters
{
    public class FilterByType : IFilter<FilterDTO, List<HistoricalCulturalObjects>>
    {
        public List<HistoricalCulturalObjects> execute(FilterDTO input, List<HistoricalCulturalObjects> objects)
        {
            if (input.ObjectTypesModel != null)
            {
                objects = objects.Where(o => o.type != null && o.type.Equals(input.ObjectTypesModel)).ToList();
            }

            return objects;
        }
    }
}