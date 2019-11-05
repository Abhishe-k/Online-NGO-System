namespace Trial3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(),
                        Venue = c.String(),
                        Description = c.String(),
                        About = c.String(),
                        AllowedQuantity = c.Int(nullable: false),
                        City = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.EventUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.EventId })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        State = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Pincode = c.String(nullable: false),
                        MobileNo = c.String(nullable: false),
                        EmailId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserDonations",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ItemsId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ItemsId })
                .ForeignKey("dbo.Items", t => t.ItemsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ItemsId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemsId = c.Int(nullable: false, identity: true),
                        ItemsName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ItemsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDonations", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserDonations", "ItemsId", "dbo.Items");
            DropForeignKey("dbo.EventUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.EventUsers", "EventId", "dbo.Events");
            DropIndex("dbo.UserDonations", new[] { "ItemsId" });
            DropIndex("dbo.UserDonations", new[] { "UserId" });
            DropIndex("dbo.EventUsers", new[] { "EventId" });
            DropIndex("dbo.EventUsers", new[] { "UserId" });
            DropTable("dbo.Items");
            DropTable("dbo.UserDonations");
            DropTable("dbo.Users");
            DropTable("dbo.EventUsers");
            DropTable("dbo.Events");
        }
    }
}
