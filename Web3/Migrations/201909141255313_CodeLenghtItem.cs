namespace Web3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeLenghtItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Code", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Code", c => c.String(maxLength: 10));
        }
    }
}
