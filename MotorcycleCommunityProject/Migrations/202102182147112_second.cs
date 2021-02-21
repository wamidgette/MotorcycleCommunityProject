namespace MotorcycleCommunityProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "Rider_RiderId", "dbo.Riders");
            DropForeignKey("dbo.Trips", "RiderId", "dbo.Riders");
            DropForeignKey("dbo.Riders", "Trip_TripId", "dbo.Trips");
            DropIndex("dbo.Trips", new[] { "RiderId" });
            DropIndex("dbo.Trips", new[] { "Rider_RiderId" });
            DropIndex("dbo.Riders", new[] { "Trip_TripId" });
            CreateTable(
                "dbo.RiderTrips",
                c => new
                    {
                        Rider_RiderId = c.Int(nullable: false),
                        Trip_TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rider_RiderId, t.Trip_TripId })
                .ForeignKey("dbo.Riders", t => t.Rider_RiderId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.Trip_TripId, cascadeDelete: true)
                .Index(t => t.Rider_RiderId)
                .Index(t => t.Trip_TripId);
            
            AddColumn("dbo.Trips", "LeadRider", c => c.Int(nullable: false));
            DropColumn("dbo.Trips", "RiderId");
            DropColumn("dbo.Trips", "Rider_RiderId");
            DropColumn("dbo.Riders", "Trip_TripId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riders", "Trip_TripId", c => c.Int());
            AddColumn("dbo.Trips", "Rider_RiderId", c => c.Int());
            AddColumn("dbo.Trips", "RiderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RiderTrips", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.RiderTrips", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.RiderTrips", new[] { "Trip_TripId" });
            DropIndex("dbo.RiderTrips", new[] { "Rider_RiderId" });
            DropColumn("dbo.Trips", "LeadRider");
            DropTable("dbo.RiderTrips");
            CreateIndex("dbo.Riders", "Trip_TripId");
            CreateIndex("dbo.Trips", "Rider_RiderId");
            CreateIndex("dbo.Trips", "RiderId");
            AddForeignKey("dbo.Riders", "Trip_TripId", "dbo.Trips", "TripId");
            AddForeignKey("dbo.Trips", "RiderId", "dbo.Riders", "RiderId", cascadeDelete: true);
            AddForeignKey("dbo.Trips", "Rider_RiderId", "dbo.Riders", "RiderId");
        }
    }
}
