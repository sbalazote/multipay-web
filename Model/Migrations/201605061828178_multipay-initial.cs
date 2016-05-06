namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multipayinitial : DbMigration
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
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationId = c.String(),
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
                "dbo.MarketPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientId = c.String(),
                        SecretId = c.String(),
                        RedirectUri = c.String(),
                        NotificationsCallbackUri = c.String(),
                        SenderId = c.String(),
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
                        SocialNetworkId = c.String(),
                        MPCustomerId = c.String(),
                        LastName = c.String(),
                        MPSellerUserId = c.Int(),
                        AuthCode = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Device_Id = c.Int(),
                        Address_Id = c.Int(),
                        Identification_Id = c.Int(),
                        Phone_Id = c.Int(),
                        Token_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.Device_Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Identifications", t => t.Identification_Id)
                .ForeignKey("dbo.Phones", t => t.Phone_Id)
                .ForeignKey("dbo.Tokens", t => t.Token_Id)
                .Index(t => t.Device_Id)
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
            DropForeignKey("dbo.Users", "Device_Id", "dbo.Devices");
            DropIndex("dbo.Users", new[] { "Token_Id" });
            DropIndex("dbo.Users", new[] { "Phone_Id" });
            DropIndex("dbo.Users", new[] { "Identification_Id" });
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropIndex("dbo.Users", new[] { "Device_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Tokens");
            DropTable("dbo.Phones");
            DropTable("dbo.MarketPlaces");
            DropTable("dbo.Identifications");
            DropTable("dbo.Devices");
            DropTable("dbo.Addresses");
        }
    }
}
