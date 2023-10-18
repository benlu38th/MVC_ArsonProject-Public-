namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createModelKnowledge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Knowledges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        DownloadFileName = c.String(),
                        DownloadFileUrl = c.String(),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Knowledges");
        }
    }
}
