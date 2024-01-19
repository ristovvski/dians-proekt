using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Service
{
    public class RegionsService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Region> listAll()
        {
            return db.regions.ToList();
        }


    }
}