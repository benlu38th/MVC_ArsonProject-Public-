namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelMemberContactAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ContactAddress", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "ContactAddress");
        }
    }
}
