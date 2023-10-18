namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelContactUserInitdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactUsers", "InitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactUsers", "InitDate");
        }
    }
}
