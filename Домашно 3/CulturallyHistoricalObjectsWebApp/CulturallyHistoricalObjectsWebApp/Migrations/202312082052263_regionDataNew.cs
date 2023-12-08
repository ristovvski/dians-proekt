namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regionDataNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricalCulturalObjects", "address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoricalCulturalObjects", "address");
        }
    }
}
