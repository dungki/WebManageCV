namespace Project.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eProjectDb_lastv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vacancies", "CloseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vacancies", "CloseDate");
        }
    }
}
