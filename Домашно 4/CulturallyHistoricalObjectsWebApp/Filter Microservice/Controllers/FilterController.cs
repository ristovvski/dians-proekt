using CulturallyHistoricalObjectsWebApp.Models;
using Filter_Microservice.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Filter_microservice.Controllers
{
    public class FilterController : ApiController
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        public FilterService filterService = new FilterService();

        // POST: api/Filter
        public IHttpActionResult Post([FromBody] FilterDTO filter)
        {
            if (filter == null)
            {
                return BadRequest("Invalid input data");
            }

            try
            {
                List<HistoricalCulturalObjects> objects = filterService.filterObjects(filter);

                if (objects != null)
                {

                    var resultList = objects.Select(obj => new
                    {
                        address = obj.address,
                        id = obj.id,
                        region = obj.region,
                        type = obj.type,
                        name = obj.name,
                        lon = obj.lon,
                        lat = obj.lat
                    }).ToList();

                    return Ok(resultList);
                }

                var responseObject = new
                {
                    Result = "Nothing is found"
                };


                // Return the JSON response
                return Ok(responseObject);
            }catch(Exception ex)
            {
                return InternalServerError(ex);

            }
        }
        
    }
}
