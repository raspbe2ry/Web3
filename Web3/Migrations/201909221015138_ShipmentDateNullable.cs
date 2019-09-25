namespace Web3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShipmentDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubOrders", "ExpectedShipmentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubOrders", "ExpectedShipmentDate", c => c.DateTime(nullable: false));
        }
    }
}
