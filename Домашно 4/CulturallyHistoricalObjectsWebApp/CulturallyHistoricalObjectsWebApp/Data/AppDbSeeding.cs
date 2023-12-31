using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using CulturallyHistoricalObjectsWebApp.Models;
using Newtonsoft.Json;
using Owin;

namespace CulturallyHistoricalObjectsWebApp.Data
{
    public class AppDbSeeding { 

        public List<HCObjectsDTO> parseJson(string filePath)
        {
            List<HCObjectsDTO> historicalObjects = new List<HCObjectsDTO>();

            try
            {
                string jsonString = File.ReadAllText(filePath);

                // Deserialize the JSON array into a list of HistoricalObject instances
                historicalObjects = JsonConvert.DeserializeObject<List<HCObjectsDTO>>(jsonString);
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception
                Console.WriteLine("File not found!");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return historicalObjects;
        }

        public List<String> getStringTypes(List<HCObjectsDTO> objects)
        {
            List<string> typesList = objects.Select(o => o.type).ToList();

            return typesList;
        }

        public List<string> getStringRegion(List<HCObjectsDTO> objects)
        {
            List<string> regionList = objects.Select(o => o.state).ToList();

            return regionList;
        }
    }

}