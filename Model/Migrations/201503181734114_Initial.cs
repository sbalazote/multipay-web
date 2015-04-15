using System.Data.Entity.Migrations;

namespace Model.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(),
                        Password = c.String(nullable: false),
                        AuthCode = c.String(),
                        AccessToken = c.String(),
                        RefreshToken = c.String(),
                        TokenExpires = c.Int(),
                        TokenRequested = c.DateTime(),
                        UserMLA = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
