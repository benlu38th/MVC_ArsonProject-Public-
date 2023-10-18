namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelNewsInitdateType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "InitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "InitDate", c => c.DateTime());
        }
    }
}
