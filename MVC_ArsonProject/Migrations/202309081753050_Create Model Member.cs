namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModelMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 30),
                        Salt = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false, maxLength: 100),
                        GenderType = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ApplicationType = c.Int(nullable: false),
                        Telephone = c.String(nullable: false, maxLength: 30),
                        Cellphone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        InternationalMembership = c.Boolean(nullable: false),
                        MembershipFileUrl = c.String(),
                        Job = c.String(nullable: false, maxLength: 100),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        HighestEducation = c.String(nullable: false, maxLength: 100),
                        ServiceUnit1 = c.String(nullable: false, maxLength: 100),
                        StartYear1 = c.String(nullable: false),
                        StartMonth1 = c.String(nullable: false),
                        EndYear1 = c.String(nullable: false),
                        EndMonth1 = c.String(nullable: false),
                        ServiceUnit2 = c.String(nullable: false, maxLength: 100),
                        StartYear2 = c.String(nullable: false),
                        StartMonth2 = c.String(nullable: false),
                        EndYear2 = c.String(nullable: false),
                        EndMonth2 = c.String(nullable: false),
                        ServiceUnit3 = c.String(nullable: false, maxLength: 100),
                        StartYear3 = c.String(nullable: false),
                        StartMonth3 = c.String(nullable: false),
                        EndYear3 = c.String(nullable: false),
                        EndMonth3 = c.String(nullable: false),
                        TotalYear = c.String(nullable: false),
                        TotalMonth = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Members");
        }
    }
}
