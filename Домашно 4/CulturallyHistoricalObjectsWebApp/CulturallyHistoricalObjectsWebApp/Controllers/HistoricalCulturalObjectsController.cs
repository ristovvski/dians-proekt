using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Web;
using System.Web.Mvc;
using CulturallyHistoricalObjectsWebApp.Models;
using CulturallyHistoricalObjectsWebApp.Service;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace CulturallyHistoricalObjectsWebApp.Controllers
{
    public class HistoricalCulturalObjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FilterService filterService = new FilterService();
        private DistanceService distanceService = new DistanceService();

        // GET: HistoricalCulturalObjects
        public ActionResult Index()
        {
            FilterDTO filterDTO= new FilterDTO();
            filterDTO.types = db.objectTypes.ToList();
            filterDTO.regions = db.regions.ToList();

            return View(filterDTO);
        }

        public ActionResult FindAll(FilterDTO model)
        {
            //take the Id of the user and find that user.
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);


            //take all the FavoritePlaces for the current user.
            List<int> currentUserFavoriteIds = currentUser.FavoritePlaces.Select(f => f.id).ToList();
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
            List<HistoricalCulturalObjects> objects = filterService.filterObjects(model);

            if (model.userLongitude == 0 || model.userLatitude == 0)
            {
                //Read the explanation above "UserLocation" to understand the Redirecting.
                return RedirectToAction("UserLocation");
            }
            else
            {
                //distanceService returns a ClosestFavoriteDTO object in which there is a stored value for the attributes
                //distance and historicalCulturalObject
                ClosestFavoriteDTO closestFavoriteDTO = distanceService.distance(model, objects);

                
                var userId = User.Identity.GetUserId();
                var currentUser = db.Users.Find(userId);

                List<int> currentUserFavoriteIds = currentUser.FavoritePlaces.Select(f => f.id).ToList();


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
            var currentUser = db.Users.Find(userId);

            List<int> currentUserFavoriteIds = currentUser.FavoritePlaces.Select(f => f.id).ToList();
            List<HistoricalCulturalObjects> objects = db.culturalObjects.Include("type").Include("region").ToList();
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

            HistoricalCulturalObjects historicalCulturalObjects =
                db.culturalObjects.Include("type").Include("region").ToList().Where(c=>c.id==id).First();
            if (historicalCulturalObjects == null)
            {
                return HttpNotFound();
            }
            return View(historicalCulturalObjects);
        }

        [Authorize(Roles ="Administrator")]
        // GET: HistoricalCulturalObjects/Create
        public ActionResult Create()
        {
            EditAndCreateDTO edit = new EditAndCreateDTO();
            edit.regions = db.regions.ToList();
            edit.objectTypes = db.objectTypes.ToList();
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
            HistoricalCulturalObjects historicalCulturalObjects = create.historicalCultralObject;
            if (ModelState.IsValid)
            {
                historicalCulturalObjects.region = db.regions.Find(create.region_id);
                historicalCulturalObjects.type = db.objectTypes.Find(create.objectTypeId);
                db.culturalObjects.Add(historicalCulturalObjects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(historicalCulturalObjects);
        }

        // GET: HistoricalCulturalObjects/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditAndCreateDTO edit = new EditAndCreateDTO();
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects.Find(id);
            if (historicalCulturalObjects == null)
            {
                return HttpNotFound();
            }
            edit.historicalCultralObject = db.culturalObjects
                .Include(hco => hco.type)
                .Include(hco => hco.region)
                .FirstOrDefault(hco => hco.id == id); 
            edit.regions = db.regions.ToList();
            edit.objectTypes = db.objectTypes.ToList();
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
                HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects
                .Include(hco => hco.type)
                .Include(hco => hco.region)
                .FirstOrDefault(hco => hco.id == edit.historicalCultralObject.id);

                if (historicalCulturalObjects != null)
                {
                    historicalCulturalObjects.region = db.regions.Find(edit.region_id);                    
                    historicalCulturalObjects.type = db.objectTypes.Find(edit.objectTypeId);

                    // Update the tracked entity properties
                    db.Entry(historicalCulturalObjects).CurrentValues.SetValues(edit.historicalCultralObject);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(edit.historicalCultralObject);
        }

        // GET: HistoricalCulturalObjects/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects
                .Include(hco => hco.type)
                .Include(hco => hco.region)
                .FirstOrDefault(hco => hco.id == id);
            if (historicalCulturalObjects == null)
            {
                return HttpNotFound();
            }
            return View(historicalCulturalObjects);
        }

        // POST: HistoricalCulturalObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects.Find(id);
            db.culturalObjects.Remove(historicalCulturalObjects);
            db.SaveChanges();
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
