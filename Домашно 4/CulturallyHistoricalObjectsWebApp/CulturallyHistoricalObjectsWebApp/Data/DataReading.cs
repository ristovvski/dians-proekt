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
    public class DataReading { 

        public List<HCObjectsDTO> parseJson(string filePath)
        {
            //reads data from the JSON
            //HCObjects is a DataTransportObject that helps in reading the data from the JSON file.
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


        //Return a list of all the types in a string type.
        public List<String> getStringTypes(List<HCObjectsDTO> objects)
        {
            List<string> typesList = objects.Select(o => o.type).ToList();

            return typesList;
        }
        //return a list of all the regions in a string.
        public List<string> getStringRegion(List<HCObjectsDTO> objects)
        {
            List<string> regionList = objects.Select(o => o.state).ToList();

            return regionList;
        }
    }

}