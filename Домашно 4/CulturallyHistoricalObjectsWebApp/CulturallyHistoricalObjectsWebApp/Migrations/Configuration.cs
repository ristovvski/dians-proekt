namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CulturallyHistoricalObjectsWebApp.Models;
    using CulturallyHistoricalObjectsWebApp.Data;
    using System.Web;
    using System.Web.Hosting;
    using System.IO;
    using System.Data.Entity.Core.Objects;

    internal sealed class Configuration : DbMigrationsConfiguration<CulturallyHistoricalObjectsWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CulturallyHistoricalObjectsWebApp.Models.ApplicationDbContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM HistoricalCulturalObjects");
            context.Database.ExecuteSqlCommand("DELETE FROM ObjectTypesModels");
            context.Database.ExecuteSqlCommand("DELETE FROM UserFavoritePlaces");
            context.Database.ExecuteSqlCommand("DELETE FROM Regions");

            DataReading appSeeding = new DataReading();

            List<HCObjectsDTO> list = readJSON(appSeeding);


            PopulateRegions(list, appSeeding, context);

            PopulateTypes(list, appSeeding, context);

            PopulateHistoricalCulturalObjects(list, context);
        }

        private List<HCObjectsDTO> readJSON(DataReading appSeeding)
        {
            string relativePath = @"Content\json_data\historical_cultural_objects.json";
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\", relativePath));
            return appSeeding.parseJson(fullPath);
        }

        private void PopulateHistoricalCulturalObjects(List<HCObjectsDTO> historicalObjects, ApplicationDbContext context)
        {
            //Populate HistoricalCulturalObjects table
            foreach (var obj in historicalObjects)
            {
                HistoricalCulturalObjects tmp = new HistoricalCulturalObjects();
                tmp.name = obj.name;
                tmp.lat = obj.lat;
                tmp.lon = obj.lon;
                tmp.address = obj.full_address;

                // Retrieve ObjectTypesModel instance based on the type from current object
                ObjectTypesModel objectType = context.objectTypes.SingleOrDefault(x => x.type == obj.type);

                Region regionType = context.regions.SingleOrDefault(x => x.name == obj.state);

                if (objectType != null)
                {
                    tmp.type = objectType;

                }

                if (regionType != null)
                {
                    tmp.region = regionType;
                }

                context.culturalObjects.AddOrUpdate(x => x.name, tmp);
            }
            context.SaveChanges();
        }

        // Populate Regions table
        private void PopulateRegions(List<HCObjectsDTO> historicalObjects, DataReading appSeeding, ApplicationDbContext context)
        {
            List<string> regionStringified = appSeeding.getStringRegion(historicalObjects);

            List<string> uniqueRegions = regionStringified.Distinct().ToList();


            foreach (string region in uniqueRegions)
            {
                Region regionObject = new Region(region);
                context.regions.Add(regionObject);
            }

            context.SaveChanges();
        }

        //Populate Types table
        private void PopulateTypes(List<HCObjectsDTO> historicalObjects, DataReading appSeeding, ApplicationDbContext context)
        {
            List<String> typeStringified = appSeeding.getStringTypes(historicalObjects);

            // Extract unique types
            List<string> uniqueTypes = typeStringified.Distinct().ToList();


            // Populate ObjectTypesModel table
            foreach (string type in uniqueTypes)
            {
                ObjectTypesModel newType = new ObjectTypesModel(type);
                context.objectTypes.Add(newType);
            }

            context.SaveChanges();
        }
    }
}
