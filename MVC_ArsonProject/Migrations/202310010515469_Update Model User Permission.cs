namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelUserPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Permission", c => c.String(maxLength: 500));
            DropColumn("dbo.Users", "NewPermission");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "NewPermission", c => c.String(maxLength: 500));
            DropColumn("dbo.Users", "Permission");
        }
    }
}
