using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CulturallyHistoricalObjectsWebApp.Models;
using CulturallyHistoricalObjectsWebApp.Service;

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
            return View(filterService.filterObjects(model));
        }

        public ActionResult FindClosest(FilterDTO model)
        {
            List<HistoricalCulturalObjects> objects = filterService.filterObjects(model);

            return View(distanceService.distance(model, objects));
        }

        // GET: HistoricalCulturalObjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects.Find(id);
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
            return View();
        }

        // POST: HistoricalCulturalObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,type,lon,lat")] HistoricalCulturalObjects historicalCulturalObjects)
        {
            if (ModelState.IsValid)
            {
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
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects.Find(id);
            if (historicalCulturalObjects == null)
            {
                return HttpNotFound();
            }
            return View(historicalCulturalObjects);
        }

        // POST: HistoricalCulturalObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "id,name,type,lon,lat")] HistoricalCulturalObjects historicalCulturalObjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historicalCulturalObjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historicalCulturalObjects);
        }

        // GET: HistoricalCulturalObjects/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects.Find(id);
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
