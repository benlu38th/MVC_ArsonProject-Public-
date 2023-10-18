namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModelMemberJobTitle1JobTitle2etc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "JobTitle1", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Members", "JobTitle2", c => c.String(maxLength: 100));
            AddColumn("dbo.Members", "JobTitle3", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "JobTitle3");
            DropColumn("dbo.Members", "JobTitle2");
            DropColumn("dbo.Members", "JobTitle1");
        }
    }
}
