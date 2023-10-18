namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelPostTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(maxLength: 1000));
            DropColumn("dbo.Posts", "Title");
        }
    }
}
