namespace Web3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BeginingDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Code = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Abbreviation = c.String(maxLength: 5),
                        TelephoneCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(maxLength: 50),
                        Code = c.String(maxLength: 10),
                        CategoryCode = c.String(maxLength: 10),
                        CatalogId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catalogs", t => t.CatalogId)
                .Index(t => t.CatalogId);
            
            CreateTable(
                "dbo.SubOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpectedShipmentDate = c.DateTime(nullable: false),
                        VendorId = c.Int(),
                        OrderId = c.Int(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.VendorId)
                .Index(t => t.OrderId)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpirationDate = c.DateTime(),
                        CatalogDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExtraDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExtraDiscountDescription = c.String(maxLength: 200),
                        SubOrderId = c.Int(),
                        ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.SubOrders", t => t.SubOrderId)
                .Index(t => t.SubOrderId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(maxLength: 200),
                        UserId = c.Int(),
                        SubOrderId = c.Int(),
                        OrderItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.SubOrders", t => t.SubOrderId)
                .ForeignKey("dbo.OrderItems", t => t.OrderItem_Id)
                .Index(t => t.UserId)
                .Index(t => t.SubOrderId)
                .Index(t => t.OrderItem_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                        UserId = c.Int(nullable: false),
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
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Address = c.String(maxLength: 100),
                        City = c.String(maxLength: 100),
                        Zip = c.String(maxLength: 20),
                        Telephone = c.String(maxLength: 30),
                        Email = c.String(maxLength: 100),
                        Code = c.String(maxLength: 20),
                        CountryId = c.Int(),
                        SubOrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Orders", t => t.SubOrderId)
                .Index(t => t.CountryId)
                .Index(t => t.SubOrderId);
            
            CreateTable(
                "dbo.OrderItemShipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Note = c.String(maxLength: 200),
                        OrderItemId = c.Int(),
                        ShipmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemId)
                .ForeignKey("dbo.Shipments", t => t.ShipmentId)
                .Index(t => t.OrderItemId)
                .Index(t => t.ShipmentId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderItemShipments", "ShipmentId", "dbo.Shipments");
            DropForeignKey("dbo.OrderItemShipments", "OrderItemId", "dbo.OrderItems");
            DropForeignKey("dbo.SubOrders", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.SubOrders", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "SubOrderId", "dbo.Orders");
            DropForeignKey("dbo.Vendors", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.OrderItems", "SubOrderId", "dbo.SubOrders");
            DropForeignKey("dbo.Shipments", "OrderItem_Id", "dbo.OrderItems");
            DropForeignKey("dbo.Shipments", "SubOrderId", "dbo.SubOrders");
            DropForeignKey("dbo.Shipments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CatalogId", "dbo.Catalogs");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderItemShipments", new[] { "ShipmentId" });
            DropIndex("dbo.OrderItemShipments", new[] { "OrderItemId" });
            DropIndex("dbo.Vendors", new[] { "SubOrderId" });
            DropIndex("dbo.Vendors", new[] { "CountryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Shipments", new[] { "OrderItem_Id" });
            DropIndex("dbo.Shipments", new[] { "SubOrderId" });
            DropIndex("dbo.Shipments", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "ItemId" });
            DropIndex("dbo.OrderItems", new[] { "SubOrderId" });
            DropIndex("dbo.SubOrders", new[] { "Item_Id" });
            DropIndex("dbo.SubOrders", new[] { "OrderId" });
            DropIndex("dbo.SubOrders", new[] { "VendorId" });
            DropIndex("dbo.Items", new[] { "CatalogId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderItemShipments");
            DropTable("dbo.Vendors");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Shipments");
            DropTable("dbo.OrderItems");
            DropTable("dbo.SubOrders");
            DropTable("dbo.Items");
            DropTable("dbo.Countries");
            DropTable("dbo.Catalogs");
        }
    }
}
