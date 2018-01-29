namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFirstHalfSemester : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterRecords", "IsFirstHalfSemester", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SemesterRecords", "IsFirstHalfSemester");
        }
    }
}
