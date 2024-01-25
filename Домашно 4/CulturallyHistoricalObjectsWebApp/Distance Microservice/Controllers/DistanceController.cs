using CulturallyHistoricalObjectsWebApp.Models;
using Distance_Microservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Distance_Microservice.Controllers
{
    public class DistanceController : ApiController
    {

        public DistanceService distanceService = new DistanceService();

        // POST: api/Distance
        public IHttpActionResult Post([FromBody] DistanceRequestDTO toCalculate)
        {
            if (toCalculate == null || toCalculate.FilteredObjects == null)
            {
                return BadRequest("Invalid input data");
            }

            // Perform distance calculation logic here
            ClosestFavoriteDTO closestFavorite = distanceService.calculateShortest(toCalculate);

            if (closestFavorite != null)
            {
                // Construct a JSON object for the response
                var responseObjectSuccess = new
                {
                    historicalCulturalObject = closestFavorite.historicalCulturalObject,
                    distance = closestFavorite.distance,
                    favoritePlacesIds = closestFavorite.favoritePlacesIds
                    // Add any other properties you want to include in the response
                };
                return Ok(responseObjectSuccess);
            }

            var responseObject = new
            {
                Result = "Nothing is found"
            };
            

            // Return the JSON response
            return Ok(responseObject);
        }
    }
}
