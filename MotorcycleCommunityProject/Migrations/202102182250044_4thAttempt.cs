namespace MotorcycleCommunityProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4thAttempt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TripRiders", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.TripRiders", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.TripRiders", new[] { "Trip_TripId" });
            DropIndex("dbo.TripRiders", new[] { "Rider_RiderId" });
            AddColumn("dbo.Trips", "LeadRiderId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "Rider_RiderId", c => c.Int());
            AddColumn("dbo.Trips", "LeadRider_RiderId", c => c.Int());
            AddColumn("dbo.Riders", "Trip_TripId", c => c.Int());
            CreateIndex("dbo.Trips", "Rider_RiderId");
            CreateIndex("dbo.Trips", "LeadRider_RiderId");
            CreateIndex("dbo.Riders", "Trip_TripId");
            AddForeignKey("dbo.Trips", "Rider_RiderId", "dbo.Riders", "RiderId");
            AddForeignKey("dbo.Trips", "LeadRider_RiderId", "dbo.Riders", "RiderId");
            AddForeignKey("dbo.Riders", "Trip_TripId", "dbo.Trips", "TripId");
            DropColumn("dbo.Trips", "LeadRider");
            DropTable("dbo.TripRiders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TripRiders",
                c => new
                    {
                        Trip_TripId = c.Int(nullable: false),
                        Rider_RiderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trip_TripId, t.Rider_RiderId });
            
            AddColumn("dbo.Trips", "LeadRider", c => c.Int(nullable: false));
            DropForeignKey("dbo.Riders", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.Trips", "LeadRider_RiderId", "dbo.Riders");
            DropForeignKey("dbo.Trips", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.Riders", new[] { "Trip_TripId" });
            DropIndex("dbo.Trips", new[] { "LeadRider_RiderId" });
            DropIndex("dbo.Trips", new[] { "Rider_RiderId" });
            DropColumn("dbo.Riders", "Trip_TripId");
            DropColumn("dbo.Trips", "LeadRider_RiderId");
            DropColumn("dbo.Trips", "Rider_RiderId");
            DropColumn("dbo.Trips", "LeadRiderId");
            CreateIndex("dbo.TripRiders", "Rider_RiderId");
            CreateIndex("dbo.TripRiders", "Trip_TripId");
            AddForeignKey("dbo.TripRiders", "Rider_RiderId", "dbo.Riders", "RiderId", cascadeDelete: true);
            AddForeignKey("dbo.TripRiders", "Trip_TripId", "dbo.Trips", "TripId", cascadeDelete: true);
        }
    }
}
