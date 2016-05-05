namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04_05_2016 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SocialNetworkId", c => c.String());
            AddColumn("dbo.Users", "MPCustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "MPCustomerId");
            DropColumn("dbo.Users", "SocialNetworkId");
        }
    }
}
