using CulturallyHistoricalObjectsWebApp.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public List<HistoricalCulturalObjects> GetFilteredObjectsFromMicroservice(FilterDTO model)
        {
            string filterMicroserviceUri = "https://localhost:44364/api/Filter";
            List<HistoricalCulturalObjects> filteredObjects = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    string jsonContent = JsonConvert.SerializeObject(model, settings);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(filterMicroserviceUri, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        filteredObjects = JsonConvert.DeserializeObject<List<HistoricalCulturalObjects>>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log the error, or take appropriate action
                filteredObjects = null;
            }

            return filteredObjects;
        }

        public ClosestFavoriteDTO GetClosestFavoriteFromMicroservice(DistanceRequestDTO distanceRequestDTO)
        {
            string distanceMicroserviceUri = "https://localhost:44331/api/Distance";
            ClosestFavoriteDTO closestFavoriteDTO = null;

            try
            {
                using (HttpClient client = new HttpClient())
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
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        closestFavoriteDTO = JsonConvert.DeserializeObject<ClosestFavoriteDTO>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log the error, or take appropriate action
                closestFavoriteDTO = null;
            }

            return closestFavoriteDTO;
        }
    }
}