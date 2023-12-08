namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewManyToManyTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserHistoricalCulturalObjects", newName: "UserFavoritePlaces");
            RenameColumn(table: "dbo.UserFavoritePlaces", name: "ApplicationUser_Id", newName: "UserId");
            RenameColumn(table: "dbo.UserFavoritePlaces", name: "HistoricalCulturalObjects_id", newName: "HistoricalCulturalObjectsId");
            RenameIndex(table: "dbo.UserFavoritePlaces", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserFavoritePlaces", name: "IX_HistoricalCulturalObjects_id", newName: "IX_HistoricalCulturalObjectsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserFavoritePlaces", name: "IX_HistoricalCulturalObjectsId", newName: "IX_HistoricalCulturalObjects_id");
            RenameIndex(table: "dbo.UserFavoritePlaces", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.UserFavoritePlaces", name: "HistoricalCulturalObjectsId", newName: "HistoricalCulturalObjects_id");
            RenameColumn(table: "dbo.UserFavoritePlaces", name: "UserId", newName: "ApplicationUser_Id");
            RenameTable(name: "dbo.UserFavoritePlaces", newName: "ApplicationUserHistoricalCulturalObjects");
        }
    }
}
