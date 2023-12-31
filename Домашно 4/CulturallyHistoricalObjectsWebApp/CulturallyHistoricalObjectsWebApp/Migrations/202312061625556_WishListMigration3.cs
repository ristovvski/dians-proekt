namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishListMigration3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HistoricalCulturalObjects", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HistoricalCulturalObjects", "ApplicationUserId", c => c.String());
        }
    }
}
