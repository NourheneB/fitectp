namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Password", c => c.String(nullable: false));
        }
    }
}
