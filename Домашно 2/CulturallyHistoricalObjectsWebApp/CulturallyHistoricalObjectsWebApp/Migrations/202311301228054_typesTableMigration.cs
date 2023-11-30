namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typesTableMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ObjectTypesModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.HistoricalCulturalObjects", "type_id", c => c.Int());
            CreateIndex("dbo.HistoricalCulturalObjects", "type_id");
            AddForeignKey("dbo.HistoricalCulturalObjects", "type_id", "dbo.ObjectTypesModels", "id");
            DropColumn("dbo.HistoricalCulturalObjects", "type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HistoricalCulturalObjects", "type", c => c.String());
            DropForeignKey("dbo.HistoricalCulturalObjects", "type_id", "dbo.ObjectTypesModels");
            DropIndex("dbo.HistoricalCulturalObjects", new[] { "type_id" });
            DropColumn("dbo.HistoricalCulturalObjects", "type_id");
            DropTable("dbo.ObjectTypesModels");
        }
    }
}
