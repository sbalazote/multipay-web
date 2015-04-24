namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Identifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AreaCode = c.Int(nullable: false),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessToken = c.String(),
                        Type = c.String(),
                        RefreshToken = c.String(),
                        Expiration = c.Int(nullable: false),
                        Scope = c.String(),
                        RequestedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Users", "Address_Id", c => c.Int());
            AddColumn("dbo.Users", "Identification_Id", c => c.Int());
            AddColumn("dbo.Users", "Phone_Id", c => c.Int());
            AddColumn("dbo.Users", "Token_Id", c => c.Int());
            CreateIndex("dbo.Users", "Address_Id");
            CreateIndex("dbo.Users", "Identification_Id");
            CreateIndex("dbo.Users", "Phone_Id");
            CreateIndex("dbo.Users", "Token_Id");
            AddForeignKey("dbo.Users", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Users", "Identification_Id", "dbo.Identifications", "Id");
            AddForeignKey("dbo.Users", "Phone_Id", "dbo.Phones", "Id");
            AddForeignKey("dbo.Users", "Token_Id", "dbo.Tokens", "Id");
            DropColumn("dbo.Users", "Surname");
            DropColumn("dbo.Users", "AccessToken");
            DropColumn("dbo.Users", "RefreshToken");
            DropColumn("dbo.Users", "TokenExpires");
            DropColumn("dbo.Users", "TokenRequested");
            DropColumn("dbo.Users", "UserMLA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserMLA", c => c.String());
            AddColumn("dbo.Users", "TokenRequested", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "TokenExpires", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "RefreshToken", c => c.String());
            AddColumn("dbo.Users", "AccessToken", c => c.String());
            AddColumn("dbo.Users", "Surname", c => c.String());
            DropForeignKey("dbo.Users", "Token_Id", "dbo.Tokens");
            DropForeignKey("dbo.Users", "Phone_Id", "dbo.Phones");
            DropForeignKey("dbo.Users", "Identification_Id", "dbo.Identifications");
            DropForeignKey("dbo.Users", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "Token_Id" });
            DropIndex("dbo.Users", new[] { "Phone_Id" });
            DropIndex("dbo.Users", new[] { "Identification_Id" });
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropColumn("dbo.Users", "Token_Id");
            DropColumn("dbo.Users", "Phone_Id");
            DropColumn("dbo.Users", "Identification_Id");
            DropColumn("dbo.Users", "Address_Id");
            DropColumn("dbo.Users", "Discriminator");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "Date");
            DropTable("dbo.Tokens");
            DropTable("dbo.Phones");
            DropTable("dbo.Identifications");
            DropTable("dbo.Addresses");
        }
    }
}
