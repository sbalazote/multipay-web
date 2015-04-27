namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Password = c.String(),
                        Active = c.Boolean(nullable: false),
                        LastName = c.String(),
                        AuthCode = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Address_Id = c.Int(),
                        Identification_Id = c.Int(),
                        Phone_Id = c.Int(),
                        Token_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Identifications", t => t.Identification_Id)
                .ForeignKey("dbo.Phones", t => t.Phone_Id)
                .ForeignKey("dbo.Tokens", t => t.Token_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.Identification_Id)
                .Index(t => t.Phone_Id)
                .Index(t => t.Token_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Token_Id", "dbo.Tokens");
            DropForeignKey("dbo.Users", "Phone_Id", "dbo.Phones");
            DropForeignKey("dbo.Users", "Identification_Id", "dbo.Identifications");
            DropForeignKey("dbo.Users", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "Token_Id" });
            DropIndex("dbo.Users", new[] { "Phone_Id" });
            DropIndex("dbo.Users", new[] { "Identification_Id" });
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Tokens");
            DropTable("dbo.Phones");
            DropTable("dbo.Identifications");
            DropTable("dbo.Addresses");
        }
    }
}
