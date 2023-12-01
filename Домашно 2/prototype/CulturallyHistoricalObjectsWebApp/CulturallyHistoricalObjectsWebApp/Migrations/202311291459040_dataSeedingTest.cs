namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataSeedingTest : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.HistoricalCulturalObjects");
        }
    }
}
