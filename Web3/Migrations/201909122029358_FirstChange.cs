namespace Web3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vendors", "SubOrderId", "dbo.Orders");
            DropIndex("dbo.Vendors", new[] { "SubOrderId" });
            AddColumn("dbo.Catalogs", "VendorId", c => c.Int());
            CreateIndex("dbo.Catalogs", "VendorId");
            AddForeignKey("dbo.Catalogs", "VendorId", "dbo.Vendors", "Id");
            DropColumn("dbo.Vendors", "SubOrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendors", "SubOrderId", c => c.Int());
            DropForeignKey("dbo.Catalogs", "VendorId", "dbo.Vendors");
            DropIndex("dbo.Catalogs", new[] { "VendorId" });
            DropColumn("dbo.Catalogs", "VendorId");
            CreateIndex("dbo.Vendors", "SubOrderId");
            AddForeignKey("dbo.Vendors", "SubOrderId", "dbo.Orders", "Id");
        }
    }
}
