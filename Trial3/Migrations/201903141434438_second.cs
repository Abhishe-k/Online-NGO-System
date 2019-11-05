namespace Trial3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EventUsers");
            DropPrimaryKey("dbo.UserDonations");
            AddColumn("dbo.EventUsers", "EventUserId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.UserDonations", "UserDonationId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.EventUsers", "EventUserId");
            AddPrimaryKey("dbo.UserDonations", "UserDonationId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserDonations");
            DropPrimaryKey("dbo.EventUsers");
            DropColumn("dbo.UserDonations", "UserDonationId");
            DropColumn("dbo.EventUsers", "EventUserId");
            AddPrimaryKey("dbo.UserDonations", new[] { "UserId", "ItemsId" });
            AddPrimaryKey("dbo.EventUsers", new[] { "UserId", "EventId" });
        }
    }
}
