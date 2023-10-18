namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createModelContactUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        GenderType = c.Int(nullable: false),
                        Telephone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactUsers");
        }
    }
}
