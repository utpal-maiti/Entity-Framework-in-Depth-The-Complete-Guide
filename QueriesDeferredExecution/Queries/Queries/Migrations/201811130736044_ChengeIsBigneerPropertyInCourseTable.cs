namespace Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChengeIsBigneerPropertyInCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsBegineer", c => c.Boolean(nullable: false));
           
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "IsBegineer"); ;
        }
    }
}
