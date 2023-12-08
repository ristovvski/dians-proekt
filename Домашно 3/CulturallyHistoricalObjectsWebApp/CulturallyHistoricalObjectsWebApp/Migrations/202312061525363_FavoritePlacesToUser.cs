namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavoritePlacesToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricalCulturalObjects", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.HistoricalCulturalObjects", "ApplicationUserId");
            AddForeignKey("dbo.HistoricalCulturalObjects", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricalCulturalObjects", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.HistoricalCulturalObjects", new[] { "ApplicationUserId" });
            DropColumn("dbo.HistoricalCulturalObjects", "ApplicationUserId");
        }
    }
}
