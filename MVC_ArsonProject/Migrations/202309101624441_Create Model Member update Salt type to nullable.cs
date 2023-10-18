namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModelMemberupdateSalttypetonullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Salt", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Salt", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
