using Culturolocator.Models;
using Culturolocator.Models.DTO;
using Culturolocator.Repository.Interface;
using System.Collections.Generic;

namespace Culturolocator.Data
{
    public class FormatData
    {
        

        public List<Region> GetRegions(List<ObjectDTO> objects)
        {
            HashSet<string> RegionsName = new HashSet<string>();

            foreach (ObjectDTO objectDTO in objects)
            {
                RegionsName.Add(objectDTO.state);
            }

            List<Region> Regions = new List<Region>();

            foreach (string s in RegionsName)
            {
                Regions.Add(new Region(s));
            }

            return Regions;
        }

        public List<Models.Type> GetTypes(List<ObjectDTO> objects)
        {
            HashSet<string> TypesName = new HashSet<string>();

            foreach (ObjectDTO objectDTO in objects)
            {
                TypesName.Add(objectDTO.type);
            }

            List<Models.Type> Types = new List<Models.Type>();

            foreach (string s in TypesName)
            {
                Types.Add(new Models.Type(s));
            }

            return Types;
        }

        public List<HistoricalCulturalObjects> GetHistoricalCulturalObjects(List<ObjectDTO> objects, List<Region> Regions, List<Models.Type> Types)
        {
            List<HistoricalCulturalObjects> hcObjects = new List<HistoricalCulturalObjects>();

            foreach(ObjectDTO objectDTO in objects)
            {
                HistoricalCulturalObjects temp = new HistoricalCulturalObjects(objectDTO.name);
                temp.FullAddress = objectDTO.full_address;
                temp.Lon = objectDTO.lon;
                temp.Lat = objectDTO.lat;
                temp.Region = Regions.FirstOrDefault(x => x.Name == objectDTO.state);
                temp.RegionId = Regions.FirstOrDefault(x => x.Name == objectDTO.state).Id;
                temp.Type = Types.FirstOrDefault(x => x.Name == objectDTO.type);
                temp.TypeId = Types.FirstOrDefault(x => x.Name == objectDTO.type).Id;


                hcObjects.Add(temp);
            }

            return hcObjects;
        }
    }
}
