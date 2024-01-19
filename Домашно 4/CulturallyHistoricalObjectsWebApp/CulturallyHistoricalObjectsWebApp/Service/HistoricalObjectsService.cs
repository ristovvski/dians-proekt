using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Service
{
    public class HistoricalObjectsService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void CreateHistoricalObject(EditAndCreateDTO create)
        {
            HistoricalCulturalObjects historicalCulturalObject = create.historicalCultralObject;
            historicalCulturalObject.region = db.regions.Find(create.region_id);
            historicalCulturalObject.type = db.objectTypes.Find(create.objectTypeId);
            db.culturalObjects.Add(historicalCulturalObject);
            db.SaveChanges();
        }

        //Returns EditAndCreate Data Transport Object which is later used to display in the Edit form the current
        //data that the object has.
        public EditAndCreateDTO editHistoricalObject(int? id)
        {
            EditAndCreateDTO edit = new EditAndCreateDTO();
            
            edit.historicalCultralObject = db.culturalObjects
                .FirstOrDefault(hco => hco.id == id);
            edit.regions = db.regions.ToList();
            edit.objectTypes = db.objectTypes.ToList();

            return edit;
        }

        //if everything is okay, changes the data in the database.
        public HistoricalCulturalObjects editHistoricalObject(EditAndCreateDTO edit)
        {
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects
               .FirstOrDefault(hco => hco.id == edit.historicalCultralObject.id);

            if (historicalCulturalObjects != null)
            {
                historicalCulturalObjects.region = db.regions.Find(edit.region_id);
                historicalCulturalObjects.type = db.objectTypes.Find(edit.objectTypeId);

                // Update the tracked entity properties
                db.Entry(historicalCulturalObjects).CurrentValues.SetValues(edit.historicalCultralObject);

                db.SaveChanges();

            }

            return historicalCulturalObjects;
        }

        public void DeleteObject(int? id)
        {
            HistoricalCulturalObjects historicalCulturalObjects = db.culturalObjects.Find(id);
            db.culturalObjects.Remove(historicalCulturalObjects);
            db.SaveChanges();
        }

        public HistoricalCulturalObjects findObjectById(int? id)
        {
            HistoricalCulturalObjects historicalCulturalObjects =
                db.culturalObjects.Include("type").Include("region").ToList().Where(c => c.id == id).First();
            return historicalCulturalObjects;
        }

        public List<HistoricalCulturalObjects> listAll()
        {
            return db.culturalObjects.Include("type").Include("region").ToList();
        }
    }
}