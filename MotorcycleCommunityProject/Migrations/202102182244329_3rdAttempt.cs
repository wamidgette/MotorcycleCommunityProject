namespace MotorcycleCommunityProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3rdAttempt : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RiderTrips", newName: "TripRiders");
            DropPrimaryKey("dbo.TripRiders");
            AddPrimaryKey("dbo.TripRiders", new[] { "Trip_TripId", "Rider_RiderId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TripRiders");
            AddPrimaryKey("dbo.TripRiders", new[] { "Rider_RiderId", "Trip_TripId" });
            RenameTable(name: "dbo.TripRiders", newName: "RiderTrips");
        }
    }
}
