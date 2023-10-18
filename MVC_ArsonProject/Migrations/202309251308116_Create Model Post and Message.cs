namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModelPostandMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessagerId = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                        InitDate = c.DateTime(nullable: false),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MessagerId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.MessagerId)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PosterId = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.PosterId, cascadeDelete: true)
                .Index(t => t.PosterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PosterId", "dbo.Members");
            DropForeignKey("dbo.Messages", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Messages", "MessagerId", "dbo.Members");
            DropIndex("dbo.Posts", new[] { "PosterId" });
            DropIndex("dbo.Messages", new[] { "Post_Id" });
            DropIndex("dbo.Messages", new[] { "MessagerId" });
            DropTable("dbo.Posts");
            DropTable("dbo.Messages");
        }
    }
}
