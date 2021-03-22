namespace Project.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eProjectDb_lastv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vacancies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Vacancies", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.Vacancies", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Vacancies", "Salary", c => c.String(nullable: false));
            AlterColumn("dbo.Vacancies", "Experience", c => c.String(nullable: false));
            AlterColumn("dbo.Vacancies", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vacancies", "Description", c => c.String());
            AlterColumn("dbo.Vacancies", "Experience", c => c.String());
            AlterColumn("dbo.Vacancies", "Salary", c => c.String());
            AlterColumn("dbo.Vacancies", "Location", c => c.String());
            AlterColumn("dbo.Vacancies", "Language", c => c.String());
            AlterColumn("dbo.Vacancies", "Name", c => c.String());
        }
    }
}
