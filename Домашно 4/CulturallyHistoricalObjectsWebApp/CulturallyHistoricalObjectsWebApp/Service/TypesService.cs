using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturallyHistoricalObjectsWebApp.Service
{
    public class TypesService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<ObjectTypesModel> listAll()
        {
            return db.objectTypes.ToList();
        }
    }
}