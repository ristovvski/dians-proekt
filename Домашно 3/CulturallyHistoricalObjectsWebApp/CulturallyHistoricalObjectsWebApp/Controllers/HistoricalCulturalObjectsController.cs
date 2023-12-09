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


        [HttpPost]
        public ActionResult Filter(FilterDTO model)
        {
            if (Request.Form["actionType"].Equals("FindAll"))
            {
                return RedirectToAction("FindAll", model);
            }
            return RedirectToAction("FindClosest", model);
        }

        public ActionResult FindAll(FilterDTO model)
        {
            // Fetch the favorite IDs for the current user outside the loop
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            List<int> currentUserFavoriteIds = currentUser.FavoritePlaces.Select(f => f.id).ToList();
            List<HistoricalCulturalObjects> hist = filterService.filterObjects(model);

            HistoricalCOUserDTO historicalCOUserDTO = new HistoricalCOUserDTO();

            historicalCOUserDTO.HistoricalCulturalObjects = hist;
            historicalCOUserDTO.favoritePlacesIds = currentUserFavoriteIds;

            return View(historicalCOUserDTO);
        }

        public ActionResult FindClosest(FilterDTO model)
        {
            List<HistoricalCulturalObjects> objects = filterService.filterObjects(model);

            if (model.userLongitude == 0 || model.userLatitude == 0)
            {
                return RedirectToAction("UserLocation");
            }
            else
            {

                ClosestFavoriteDTO closestFavoriteDTO = new ClosestFavoriteDTO();

                closestFavoriteDTO.closestDistance = distanceService.distance(model, objects);

                var userId = User.Identity.GetUserId();
                var currentUser = db.Users.Find(userId);

                List<int> currentUserFavoriteIds = currentUser.FavoritePlaces.Select(f => f.id).ToList();

                closestFavoriteDTO.favoritePlacesIds = currentUserFavoriteIds;

                return View(closestFavoriteDTO);
            }
        }

        public ActionResult FavoritePlaces()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            List<int> currentUserFavoriteIds = currentUser.FavoritePlaces.Select(f => f.id).ToList();
            List<HistoricalCulturalObjects> objects = db.culturalObjects.Include("type").Include("region").ToList();
            return View(currentUserFavoriteIds.Select(c => objects.Where(o=> o.id==c).First()).ToList());
        }

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
            EditDTO edit = new EditDTO();
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
        public ActionResult Create(EditDTO create)
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
            EditDTO edit = new EditDTO();
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
        public ActionResult Edit(EditDTO edit)
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
