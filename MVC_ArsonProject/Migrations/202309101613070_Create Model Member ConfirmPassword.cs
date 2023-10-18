namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModelMemberConfirmPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ConfirmPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "ConfirmPassword");
        }
    }
}
