using System.Collections.Generic;
using System.Linq;
using CulturallyHistoricalObjectsWebApp.Models;

namespace CulturallyHistoricalObjectsWebApp.Service.Filters
{
    public class Pipe<T, U>
    {
        private List<IFilter<T, U>> filters = new List<IFilter<T, U>>();

        public void addFilter(IFilter<T, U> filter)
        {
            filters.Add(filter);
        }

        public U runFilters(T input, U objects)
        {
            foreach (IFilter<T, U> filter in filters) 
            {
                    objects = filter.execute(input, objects);
            }
            return objects;
        }
    }
}