namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createModelCertifiedMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CertifiedMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PictureUrl = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Counrty = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        Company = c.String(nullable: false, maxLength: 50),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CertifiedMembers");
        }
    }
}
