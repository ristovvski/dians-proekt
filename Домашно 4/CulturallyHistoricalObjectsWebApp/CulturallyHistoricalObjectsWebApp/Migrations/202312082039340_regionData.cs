namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class regionData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.HistoricalCulturalObjects", "region_Id", c => c.Int());
            CreateIndex("dbo.HistoricalCulturalObjects", "region_Id");
            AddForeignKey("dbo.HistoricalCulturalObjects", "region_Id", "dbo.Regions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricalCulturalObjects", "region_Id", "dbo.Regions");
            DropIndex("dbo.HistoricalCulturalObjects", new[] { "region_Id" });
            DropColumn("dbo.HistoricalCulturalObjects", "region_Id");
            DropTable("dbo.Regions");
        }
    }
}
