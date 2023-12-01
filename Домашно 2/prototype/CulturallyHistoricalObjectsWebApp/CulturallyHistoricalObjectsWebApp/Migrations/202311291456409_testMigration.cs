namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMigration : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.HistoricalCulturalObjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HistoricalCulturalObjects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        type = c.String(),
                        lon = c.Double(nullable: false),
                        lat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
