namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createModelNewsRe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Sumary = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        CoverUrl = c.String(maxLength: 1000),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
