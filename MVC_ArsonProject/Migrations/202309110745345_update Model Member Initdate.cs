namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelMemberInitdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "InitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "InitDate");
        }
    }
}
