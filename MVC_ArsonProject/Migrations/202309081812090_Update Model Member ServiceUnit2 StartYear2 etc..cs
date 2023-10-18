namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelMemberServiceUnit2StartYear2etc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "ServiceUnit2", c => c.String(maxLength: 100));
            AlterColumn("dbo.Members", "StartYear2", c => c.String());
            AlterColumn("dbo.Members", "StartMonth2", c => c.String());
            AlterColumn("dbo.Members", "EndYear2", c => c.String());
            AlterColumn("dbo.Members", "EndMonth2", c => c.String());
            AlterColumn("dbo.Members", "ServiceUnit3", c => c.String(maxLength: 100));
            AlterColumn("dbo.Members", "StartYear3", c => c.String());
            AlterColumn("dbo.Members", "StartMonth3", c => c.String());
            AlterColumn("dbo.Members", "EndYear3", c => c.String());
            AlterColumn("dbo.Members", "EndMonth3", c => c.String());
            AlterColumn("dbo.Members", "TotalYear", c => c.Int(nullable: false));
            AlterColumn("dbo.Members", "TotalMonth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "TotalMonth", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "TotalYear", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "EndMonth3", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "EndYear3", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "StartMonth3", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "StartYear3", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "ServiceUnit3", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Members", "EndMonth2", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "EndYear2", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "StartMonth2", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "StartYear2", c => c.String(nullable: false));
            AlterColumn("dbo.Members", "ServiceUnit2", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
