namespace CulturallyHistoricalObjectsWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishListUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistoricalCulturalObjects", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.HistoricalCulturalObjects", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.ApplicationUserHistoricalCulturalObjects",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        HistoricalCulturalObjects_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.HistoricalCulturalObjects_id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.HistoricalCulturalObjects", t => t.HistoricalCulturalObjects_id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.HistoricalCulturalObjects_id);
            
            AlterColumn("dbo.HistoricalCulturalObjects", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserHistoricalCulturalObjects", "HistoricalCulturalObjects_id", "dbo.HistoricalCulturalObjects");
            DropForeignKey("dbo.ApplicationUserHistoricalCulturalObjects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserHistoricalCulturalObjects", new[] { "HistoricalCulturalObjects_id" });
            DropIndex("dbo.ApplicationUserHistoricalCulturalObjects", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.HistoricalCulturalObjects", "ApplicationUserId", c => c.String(maxLength: 128));
            DropTable("dbo.ApplicationUserHistoricalCulturalObjects");
            CreateIndex("dbo.HistoricalCulturalObjects", "ApplicationUserId");
            AddForeignKey("dbo.HistoricalCulturalObjects", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
