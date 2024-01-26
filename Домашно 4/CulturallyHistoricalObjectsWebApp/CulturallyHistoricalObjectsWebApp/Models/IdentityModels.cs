using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Hosting;
using CulturallyHistoricalObjectsWebApp.Data;
using CulturallyHistoricalObjectsWebApp.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CulturallyHistoricalObjectsWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        // Navigation property for favorite places
        public virtual List<HistoricalCulturalObjects> FavoritePlaces { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //culturalObject = HistoricalCulturalObjects in the database.
        public DbSet<HistoricalCulturalObjects> culturalObjects { get; set; }
        //objectTypes = ObjectTypesModels in the database.
        public DbSet<ObjectTypesModel> objectTypes { get; set; }
        //regions = Regions table in the database.
        public DbSet<Region> regions {  get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        //creating a separate Table for the Many to Many relation between User and HistoricalCUlturalObjects
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.FavoritePlaces)
                .WithMany(h => h.FavoriteForUsers)
                .Map(m =>
                {
                    m.ToTable("UserFavoritePlaces");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("HistoricalCulturalObjectsId");
                });
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}