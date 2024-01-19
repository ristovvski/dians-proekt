using CulturallyHistoricalObjectsWebApp.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace CulturallyHistoricalObjectsWebApp.Service
{
    public class UserService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser getUser(string userId)
        {
            return db.Users.Find(userId);
        }

        public List<int> getUserFavorites(string userId)
        {
            var currentUser = this.getUser(userId);
            return currentUser.FavoritePlaces.Select(f => f.id).ToList();
        }

        public void AddPlaceToFavorites(string currentUser, int id)
        {
            if (currentUser == null)
            {
                throw new HttpException(400, "No user logged in!");
            }

            ApplicationUser user = db.Users.Find(currentUser);

            if (user == null)
            {
                throw new HttpException(404, "User not found!");
            }

            HistoricalCulturalObjects placeToAdd = db.culturalObjects.Find(id);

            if (placeToAdd == null)
            {
                throw new HttpException(404, "Place not found!");
            }

            if (user.FavoritePlaces.Contains(placeToAdd))
            {
                throw new HttpException(400, "Place is already in favorites!");
            }

            user.FavoritePlaces.Add(placeToAdd);
            db.SaveChanges();
        }

        public void RemoveFromFavorites(string currentUser, int id)
        {
            if (currentUser == null)
            {
                throw new HttpException(400, "No user logged in!");
            }

            ApplicationUser user = db.Users.Find(currentUser);

            if (user == null)
            {
                throw new HttpException(404, "User not found!");
            }

            HistoricalCulturalObjects placeToRemove = db.culturalObjects.Find(id);

            if (placeToRemove == null)
            {
                throw new HttpException(404, "Place not found!");
            }

            if (!user.FavoritePlaces.Contains(placeToRemove))
            {
                throw new HttpException(400, "Place is not in favorites!");
            }

            user.FavoritePlaces.Remove(placeToRemove);
            db.SaveChanges();
        }
    }
}