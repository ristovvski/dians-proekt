using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CulturallyHistoricalObjectsWebApp.Models;
using CulturallyHistoricalObjectsWebApp.Service;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace CulturallyHistoricalObjectsWebApp.Controllers
{
    public class HistoricalCulturalObjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FilterService filterService = new FilterService();
        private DistanceService distanceService = new DistanceService();
        private UserService userService = new UserService();
        private HistoricalObjectsService historicalObjectService = new HistoricalObjectsService();
        private RegionsService regionService = new RegionsService();
        private TypesService typesService = new TypesService();

        // GET: HistoricalCulturalObjects
        public ActionResult Index()
        {
            return View(filterService.createFilter());
        }

        public ActionResult FindAll(FilterDTO model)
        {
            //take the Id of the user and find that user.
            var userId = User.Identity.GetUserId();

            //take all the FavoritePlaces for the current user.
            List<int> currentUserFavoriteIds = userService.getUserFavorites(userId);
            //Filter the historical cultural objects.
            List<HistoricalCulturalObjects> filteredHistoricalObjects = filterService.filterObjects(model);

            //Create an instance of the DataTransportObject and store the favorite places for the user and the other historical cultural
            //objects.
            HistoricalCOUserDTO historicalCOUserDTO = new HistoricalCOUserDTO();

            historicalCOUserDTO.HistoricalCulturalObjects = filteredHistoricalObjects;
            historicalCOUserDTO.favoritePlacesIds = currentUserFavoriteIds;

            //return a View with this Model, so later in the view when looping through the historical objects, if the object is in
            //the user's favorite list, it would be marked as already there (the button would be 'Remove', instead of 'Favorite'
            return View(historicalCOUserDTO);
        }


        public ActionResult FindClosest(FilterDTO model)
        {
            if (model.userLongitude == 0 || model.userLatitude == 0)
            {
                //Read the explanation above "UserLocation" to understand the Redirecting.
                return RedirectToAction("UserLocation");
            }
            else
            {
                DistanceRequestDTO distanceRequestDTO = new DistanceRequestDTO();

                distanceRequestDTO.FilteredObjects = filterService.filterObjects(model);
                distanceRequestDTO.UserLatitude = model.userLatitude;
                distanceRequestDTO.UserLongitude = model.userLongitude;

                ClosestFavoriteDTO closestFavoriteDTO = null;

                // Use HttpClient to send a POST request to the Distance microservice
                using (HttpClient client = new HttpClient())
                {
                    // Replace "YourDistanceMicroserviceBaseUri" with the actual base URI of your Distance microservice
                    string distanceMicroserviceUri = "https://localhost:44331/api/Distance";

                    try
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        };

                        string jsonContent = JsonConvert.SerializeObject(distanceRequestDTO, settings);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PostAsync(distanceMicroserviceUri, content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            // Read the JSON response content
                            string jsonResponse = response.Content.ReadAsStringAsync().Result;

                            // Deserialize the JSON response into an object
                            closestFavoriteDTO = JsonConvert.DeserializeObject<ClosestFavoriteDTO>(jsonResponse);
                        }
                        else
                        {
                            // Handle the case where the API request was not successful
                            // You might want to log the error or take appropriate action
                            // For simplicity, let's set closestFavoriteDTO to null
                            closestFavoriteDTO = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log the error, or take appropriate action
                        closestFavoriteDTO = null;
                    }
                }


                var userId = User.Identity.GetUserId();

                List<int> currentUserFavoriteIds = userService.getUserFavorites(userId);

                //We check if closestFavoriteDTO is not null, because for example we might filter and search for monument in
                //a region where there aren't any monuments, so because of that the distanceService.distance will return null
                if (closestFavoriteDTO != null)
                {
                    closestFavoriteDTO.favoritePlacesIds = currentUserFavoriteIds;
                }

                return View(closestFavoriteDTO);
            }
        }


        //Here we take all the Favorite places for the User and display it in /HistoricalCulturalObjects/FavoritePlaces page.
        public ActionResult FavoritePlaces()
        {
            var userId = User.Identity.GetUserId();

            List<int> currentUserFavoriteIds = userService.getUserFavorites(userId);
            List<HistoricalCulturalObjects> objects = historicalObjectService.listAll();
            return View(currentUserFavoriteIds.Select(c => objects.Where(o=> o.id==c).First()).ToList());
        }

        //UserLocation is like a "middle page" that shows up only in a situations where we don't have the user's longitude and latitude, but
        //the user wants to use the FindClosest functionality of the application.
        //In that case, the user is navigated to a page UserLocation which is specific only for taking the user's location and redirecting it
        //to FindClosest after.
        public ActionResult UserLocation()
        {
            FilterDTO model = new FilterDTO();

            return View(model);
        }

        // GET: HistoricalCulturalObjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HistoricalCulturalObjects hhistoricalCulturalObject = historicalObjectService.findObjectById(id);
            if (hhistoricalCulturalObject == null)
            {
                return HttpNotFound();
            }
            return View(hhistoricalCulturalObject);
        }

        [Authorize(Roles ="Administrator")]
        // GET: HistoricalCulturalObjects/Create
        public ActionResult Create()
        {
            EditAndCreateDTO edit = new EditAndCreateDTO();
            edit.regions = regionService.listAll();
            edit.objectTypes = typesService.listAll();
            edit.historicalCultralObject = new HistoricalCulturalObjects();
            return View(edit);
        }

        // POST: HistoricalCulturalObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditAndCreateDTO create)
        {
            if (ModelState.IsValid)
            {
                historicalObjectService.CreateHistoricalObject(create);
                return RedirectToAction("Index");
            }

            return View(create.historicalCultralObject);
        }

        // GET: HistoricalCulturalObjects/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricalCulturalObjects historicalCulturalObjects = historicalObjectService.findObjectById(id);
            if (historicalCulturalObjects == null)
            {
                return HttpNotFound();
            }
            EditAndCreateDTO edit = historicalObjectService.editHistoricalObject(id);
            return View(edit);
        }

        // POST: HistoricalCulturalObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(EditAndCreateDTO edit)
        {
            if (ModelState.IsValid)
            {
                    if(historicalObjectService.editHistoricalObject(edit) != null)
                        return RedirectToAction("Index");
            }
            return View(edit.historicalCultralObject);
        }
        
        public ActionResult Delete(int id)
        {
            historicalObjectService.DeleteObject(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
