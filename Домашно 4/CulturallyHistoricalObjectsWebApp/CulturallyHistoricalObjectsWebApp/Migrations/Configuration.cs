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

    internal sealed class Configuration : DbMigrationsConfiguration<CulturallyHistoricalObjectsWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CulturallyHistoricalObjectsWebApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            string relativePath = @"Content\json_data\historical_cultural_objects.json";
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\", relativePath));
            DataReading appSeeding = new DataReading();
            List<HCObjectsDTO> objectsDTO = appSeeding.parseJson(fullPath);
            List<String> typeStringified = appSeeding.getStringTypes(objectsDTO);
            List<string> regionStringified = appSeeding.getStringRegion(objectsDTO);

            // Extract unique types
            List<string> uniqueTypes = typeStringified.Distinct().ToList();
            List<string> uniqueRegions = regionStringified.Distinct().ToList();


            // Populate ObjectTypesModel table
            foreach (string type in uniqueTypes)
            {
                ObjectTypesModel newType = new ObjectTypesModel(type);
                context.objectTypes.Add(newType);
            }

            context.SaveChanges();
            // Populate Regions table
            foreach (string region in uniqueRegions)
            {
                Region regionObject = new Region(region);
                context.regions.Add(regionObject);
            }

            context.SaveChanges();

            //Populate HistoricalCulturalObjects table
            foreach (var obj in objectsDTO)
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

                if(regionType != null)
                {
                    tmp.region = regionType;
                }

                context.culturalObjects.AddOrUpdate(x => x.name, tmp);
            }
            context.SaveChanges();
        }
    }
}
