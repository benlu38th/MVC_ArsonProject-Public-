namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelPostIcollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Messages", new[] { "Post_Id" });
            DropColumn("dbo.Messages", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Post_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Post_Id");
            AddForeignKey("dbo.Messages", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
