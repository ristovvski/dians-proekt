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
            string relativePath = @"Content\json_data\result.json";
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\", relativePath));
            AppDbSeeding appSeeding = new AppDbSeeding();
            List<HCObjectsDTO> objectsDTO = appSeeding.parseJson(fullPath);
            List<String> typeStringified = appSeeding.getStringTypes(objectsDTO);

            // Extract unique types
            List<string> uniqueTypes = typeStringified.Distinct().ToList();

            // Populate ObjectTypesModel table
            foreach (string type in uniqueTypes)
            {
                // Check if the type exists in the database
                bool typeExists = context.objectTypes.Any(x => x.type == type);

                if (!typeExists)
                {
                    ObjectTypesModel newType = new ObjectTypesModel(type);
                    context.objectTypes.Add(newType);
                }
            }

            context.SaveChanges();

            int i = 0;
            foreach (var obj in objectsDTO)
            {
                HistoricalCulturalObjects tmp = new HistoricalCulturalObjects();
                tmp.name = obj.name;
                tmp.lat = obj.lat;
                tmp.lon = obj.lon;

                // Retrieve ObjectTypesModel instance based on the type string
                ObjectTypesModel objectType = context.objectTypes.SingleOrDefault(x => x.type == typeStringified[i]);

                if (objectType != null)
                {
                    tmp.type = objectType;

                }

                context.culturalObjects.AddOrUpdate(x => x.name, tmp);
                i++;
            }
            context.SaveChanges();
        }
    }
}
