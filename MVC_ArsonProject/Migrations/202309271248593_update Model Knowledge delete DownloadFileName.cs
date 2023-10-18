namespace MVC_ArsonProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelKnowledgedeleteDownloadFileName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Knowledges", "DownloadFileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Knowledges", "DownloadFileName", c => c.String());
        }
    }
}
