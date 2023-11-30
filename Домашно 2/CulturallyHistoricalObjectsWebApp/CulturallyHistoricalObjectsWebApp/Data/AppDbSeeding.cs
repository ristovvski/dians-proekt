using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Hosting;
using CulturallyHistoricalObjectsWebApp.Models;
using Newtonsoft.Json;
using Owin;

namespace CulturallyHistoricalObjectsWebApp.Data
{
    public class AppDbSeeding { 

        public List<HistoricalCulturalObjects> parseJson(string filePath)
        {
            List<HistoricalCulturalObjects> historicalObjects = new List<HistoricalCulturalObjects>();

            try
            {
                string jsonString = File.ReadAllText(filePath);

                // Deserialize the JSON array into a list of HistoricalObject instances
                historicalObjects = JsonConvert.DeserializeObject<List<HistoricalCulturalObjects>>(jsonString);
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
    }

}