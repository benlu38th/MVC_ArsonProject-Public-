namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelMessageMyPostId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MyPostId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "MyPostId");
        }
    }
}
