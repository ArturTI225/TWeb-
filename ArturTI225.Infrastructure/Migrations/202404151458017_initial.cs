namespace ArturTI225.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        EngineType = c.String(),
                        Transmission = c.String(),
                        HasNavigation = c.Boolean(nullable: false),
                        HasSunroof = c.Boolean(nullable: false),
                        HasLeatherSeats = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        Color = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CarDetails", "CarId", "dbo.Cars");
            DropIndex("dbo.Roles", new[] { "User_UserId" });
            DropIndex("dbo.Orders", new[] { "CarId" });
            DropIndex("dbo.CarDetails", new[] { "CarId" });
            DropTable("dbo.Users");
            DropTable("dbo.Services");
            DropTable("dbo.Roles");
            DropTable("dbo.Orders");
            DropTable("dbo.Cars");
            DropTable("dbo.CarDetails");
        }
    }
}
