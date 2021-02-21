namespace MotorcycleCommunityProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventStartDate = c.DateTime(nullable: false),
                        EventEndDate = c.DateTime(nullable: false),
                        EventAddress = c.String(),
                        EventTime = c.DateTime(nullable: false),
                        EventCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        TripId = c.Int(nullable: false, identity: true),
                        RiderId = c.Int(nullable: false),
                        TripName = c.String(),
                        TripRoute = c.String(),
                        TripStartDate = c.DateTime(nullable: false),
                        TripEndDate = c.DateTime(nullable: false),
                        Rider_RiderId = c.Int(),
                    })
                .PrimaryKey(t => t.TripId)
                .ForeignKey("dbo.Riders", t => t.Rider_RiderId)
                .ForeignKey("dbo.Riders", t => t.RiderId, cascadeDelete: true)
                .Index(t => t.RiderId)
                .Index(t => t.Rider_RiderId);
            
            CreateTable(
                "dbo.Riders",
                c => new
                    {
                        RiderId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Bio = c.String(),
                        BikeSize = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Trip_TripId = c.Int(),
                    })
                .PrimaryKey(t => t.RiderId)
                .ForeignKey("dbo.Trips", t => t.Trip_TripId)
                .Index(t => t.Trip_TripId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TripEvents",
                c => new
                    {
                        Trip_TripId = c.Int(nullable: false),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trip_TripId, t.Event_EventId })
                .ForeignKey("dbo.Trips", t => t.Trip_TripId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .Index(t => t.Trip_TripId)
                .Index(t => t.Event_EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Riders", "Trip_TripId", "dbo.Trips");
            DropForeignKey("dbo.Trips", "RiderId", "dbo.Riders");
            DropForeignKey("dbo.Trips", "Rider_RiderId", "dbo.Riders");
            DropForeignKey("dbo.TripEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.TripEvents", "Trip_TripId", "dbo.Trips");
            DropIndex("dbo.TripEvents", new[] { "Event_EventId" });
            DropIndex("dbo.TripEvents", new[] { "Trip_TripId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Riders", new[] { "Trip_TripId" });
            DropIndex("dbo.Trips", new[] { "Rider_RiderId" });
            DropIndex("dbo.Trips", new[] { "RiderId" });
            DropTable("dbo.TripEvents");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Riders");
            DropTable("dbo.Trips");
            DropTable("dbo.Events");
        }
    }
}
