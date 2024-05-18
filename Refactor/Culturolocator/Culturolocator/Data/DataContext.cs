using Culturolocator.Models;
using Culturolocator.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;


namespace Culturolocator.Data
{
    public class DataContext : IdentityDbContext
    {
        public virtual DbSet<HistoricalCulturalObjects> HistoricalCulturalObjects { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Models.Type> Types { get; set; }

        private readonly ReadJsonData ReadJsonData;

        public DataContext(DbContextOptions<DataContext> options, ReadJsonData ReadJsonData) : base(options) 
        {
            this.ReadJsonData = ReadJsonData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>()
                .Property(r => r.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Models.Type>()
                .Property(t => t.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<HistoricalCulturalObjects>()
                .Property(h => h.Id)
                .HasDefaultValueSql("gen_random_uuid()");
        }

        public void SeedData()
        {
            List<ObjectDTO> objects = ReadJsonData.ReadDataFromJson();

            bool regionsEmpty = !Regions.Any();
            bool typesEmpty = !Types.Any();
            bool hcoEmpty = !HistoricalCulturalObjects.Any();

            FormatData fd = new FormatData();

            List<Region> RegionsLocal = fd.GetRegions(objects);
            List<Models.Type> TypesLocal = fd.GetTypes(objects);

            if (regionsEmpty)
                Regions.AddRange(RegionsLocal);

            if (typesEmpty)
                Types.AddRange(TypesLocal);

            if (hcoEmpty)
                HistoricalCulturalObjects.AddRange(fd.GetHistoricalCulturalObjects(objects, RegionsLocal, TypesLocal));

            SaveChanges();
        }
    }
}
