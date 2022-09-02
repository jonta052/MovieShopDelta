namespace MovieShopDelta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        BillingAddress = c.String(nullable: false, maxLength: 200),
                        BillingCity = c.String(nullable: false, maxLength: 100),
                        BillingZip = c.String(nullable: false, maxLength: 100),
                        DeliveryAddress = c.String(nullable: false, maxLength: 200),
                        DeliveryCity = c.String(nullable: false, maxLength: 100),
                        DeliveryZip = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 25),
                        EmailAddress = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.OrderRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Director = c.String(nullable: false),
                        ReleaseYear = c.Int(nullable: false),
                        Genre = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageURL = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderRows", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderRows", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderRows", new[] { "MovieId" });
            DropIndex("dbo.OrderRows", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Movies");
            DropTable("dbo.OrderRows");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
